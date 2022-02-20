using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shell;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Subscriptions;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;

namespace TADA_Advanced_Desktop_Alerts
{
    public partial class Played_Alerts : Form
    {
        private Image redLight = Properties.Resources.circle_red;
        private Image greenLight = Properties.Resources.circle_green;

        private TwitchAPI twitchAPI;
        private TwitchPubSub pubSub;
        private TwitchClient twitchClient;

        private string accessToken;
        private string channelID;
        private string channelName;

        private Dictionary<string, List<string>> followers;
        private bool followerFileWritten = false;
        private string missingFollowers = "";

        private Dictionary<string, string> ipaDictionary = MainTADAForm.mainTADAForm.IpaDictionary();

        private Dictionary<string, List<TADAAlert>> currentAlerts = new Dictionary<string, List<TADAAlert>>();
        private Dictionary<KeyHandler, TADAAlert> currentMacros = new Dictionary<KeyHandler, TADAAlert>();

        private Dictionary<string, User> viewers = new Dictionary<string, User>();

        // Need something for storing messages.  <alert, last procced, <user, last procced>>  Need an object for this probably.
        private Dictionary<TADAAlert, RecentlyPlayed> recentlyPlayed = new Dictionary<TADAAlert, RecentlyPlayed>();

        private Mutex addingAlert = new Mutex();

        // Need a thing for storing community gift subs that are being compiled.
        private List<CommunityGiftSub> communityGiftSubs = new List<CommunityGiftSub>();

        private List<string> cheermotes = new List<string>();

        private User AnonymousUser = new User
        {
            badges = new Badges(),
            firstMessage = true,
            lastSpoke = DateTime.Now,
            subscription = null,
            subscriptionLastUpdated = DateTime.Now,
            user = null
        };

        public Played_Alerts(string channelName, string accessToken)
        {
            InitializeComponent();

            this.accessToken = accessToken;
            this.channelName = channelName;

            twitchAPI = new TwitchAPI();
            twitchAPI.Settings.ClientId = General.CLIENT_ID;
            twitchAPI.Settings.AccessToken = accessToken;

            btn_Pause.Click += PauseAlerts;
            btn_Replay.Click += ReplayAlerts;
            btn_Stop.Click += StopAlert;

            (dg_Pending.DataSource as BindingSource).AddingNew += PendingAlertAdded;
            try
            {
                Connect();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PauseAlerts(object sender, EventArgs e)
        {
            bool paused = MainTADAForm.mainTADAForm.AnimationWindow.TogglePause();
            this.Invoke((Action)(() => { this.btn_Pause.Text = (paused ? "Unpause Alerts" : "Pause Alerts"); }));
        }

        private void ReplayAlerts(object sender, EventArgs e)
        {
            if (dg_Played.SelectedRows.Count > 0)
            {
                while (dg_Played.SelectedRows.Count > 0)
                {
                    ListedAlert alert = playedAlertBindingSource[dg_Played.SelectedRows[0].Index] as ListedAlert;
                    pendingAlertBindingSource.Add(alert);
                    playedAlertBindingSource.Remove(alert);
                }

                // And then let the system know that there are alerts waiting.
                MainTADAForm.mainTADAForm.AnimationWindow.PendingAlert();
            }
        }

        private void StopAlert(object sender, EventArgs e)
        {
            MainTADAForm.mainTADAForm.AnimationWindow.nowPlaying?.Stop();
        }

        private async void Connect()
        {
            var result = await twitchAPI.Helix.Users.GetUsersAsync(null, new List<string> { channelName }, twitchAPI.Settings.AccessToken);
            if (result.Users.Length > 0)
            {
                channelID = result.Users[0].Id;
            }
            else
            {
                throw new Exception($"No user was found with the name '{channelName}'.");
            }

            /*
             *  Open question.
             *  Do I repurpose PubSub usage to just getting Follows and let TwitchClient do everything else?
             *  I don't think the objects are all that different, and it means not having to translate from one thing to another.
             *  It might be a bit much, but it might also make things much cleaner.
             *  Though, PubSub is supposed to be near-instant (yay, websocket) instead of up to ten seconds (IRC).
             */
            
            pubSub = new TwitchPubSub();
            pubSub.OnBitsReceivedV2 += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.BITS, e);
            pubSub.OnFollow += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.FOLLOW, e);
            pubSub.OnChannelSubscription += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.SUBSCRIPTION, e);
            pubSub.OnListenResponse += (sender, e) =>
            {
                if (e.Successful)
                {
                    this.Invoke((Action)(() =>
                    {
                        this.pb_PubSub.Image = this.greenLight;
                        General.ConfigureToolTip(this.pb_PubSub, "Bits, Follows, Game Purchases, and Subscriptions are being tracked.");
                    }));
                }
                else
                {
                    General.LogFile($"Failed to connect to Twitch.{Environment.NewLine}{e.Response.Error}");
                    System.Windows.Forms.MessageBox.Show("Failed to connect to Twitch to listen to bits, follows, game purchases, or subscriptions.");
                    this.Invoke((Action)(() => { General.ConfigureToolTip(this.pb_PubSub, "Bits, Follows, Game Purchases, and Subscriptions are not being tracked.  Reconnecting."); }));

                    this.pubSub.Connect();
                }
            };

            // Handle these, usually with a reconnection attempt or seventy thousand.
            pubSub.OnPubSubServiceClosed += (sender, e) =>
            {
                this.Invoke((Action)(() =>
                {
                    General.LogFile($"PubSub service closed unexpectedly.");

                    this.pb_PubSub.Image = this.redLight;
                    General.ConfigureToolTip(this.pb_PubSub, "Bits, Follows, Game Purchases, and Subscriptions are not being tracked.  Reconnecting.");
                    this.pubSub.Connect();
                }));
            };

            pubSub.OnPubSubServiceError += (sender, e) =>
            {
                this.Invoke((Action)(() =>
                {
                    General.LogFile($"PubSub service error.");

                    this.pb_PubSub.Image = this.redLight;
                    General.ConfigureToolTip(this.pb_PubSub, "Bits, Follows, Game Purchases, and Subscriptions are not being tracked.  Reconnecting.");
                    this.pubSub.Connect();
                }));
            };

            pubSub.OnPubSubServiceConnected += delegate
            {
                // Even if there are no events of the chosen type specified, listening to them is important in case one is added later.
                pubSub.ListenToBitsEventsV2(channelID);
                pubSub.ListenToFollows(channelID);
                pubSub.SendTopics(twitchAPI.Settings.AccessToken);

                this.Invoke((Action)(() =>
                {
                    this.pb_PubSub.Image = this.greenLight;
                    General.ConfigureToolTip(this.pb_PubSub, "Bits, Follows, Game Purchases, and Subscriptions are being tracked.");
                }));
            };

            // TwitchClient is required for hosts, raids, and reading messages because it reads IRC directly.
            twitchClient = new TwitchClient(null, TwitchLib.Client.Enums.ClientProtocol.WebSocket, null);
            twitchClient.Initialize(new TwitchLib.Client.Models.ConnectionCredentials(this.channelName, this.accessToken), this.channelName, '\0', '\0', true);

            twitchClient.OnConnected += (sender, e) =>
            {
                General.LogFile("TwitchClient claims to be connected.", "twitchClient.txt");
                this.Invoke((Action)(() =>
                {
                    this.pb_TwitchApi.Image = this.greenLight;
                    General.ConfigureToolTip(this.pb_TwitchApi, "Hosts, Raids, and Messages are being tracked.");
                }));
            };
            twitchClient.OnConnectionError += (sender, e) =>
            {
                General.LogFile($"A connection error happened, causing hosts, raids, and messages to stop being tracked.{Environment.NewLine}{e.Error.Message}", "twitchClient.txt");
                this.Invoke((Action)(() =>
                {
                    this.pb_TwitchApi.Image = this.redLight;
                    General.ConfigureToolTip(this.pb_TwitchApi, "Hosts, Raids, and Messages are not being tracked.  Reconnecting.");

                    twitchClient.Connect();
                }));
            };
            twitchClient.OnIncorrectLogin += (sender, e) =>
            {
                General.LogFile($"The credentials were incorrect.{Environment.NewLine}{e.Exception.Message}", "twitchClient.txt");
                this.Invoke((Action)(() =>
                {
                    this.pb_TwitchApi.Image = this.redLight;
                    General.ConfigureToolTip(this.pb_TwitchApi, "Hosts, Raids, and Messages are not being tracked.");
                }));
            };
            twitchClient.OnJoinedChannel += (sender, e) =>
            {
                General.LogFile("Joined channel " + e.Channel, "twitchClient.txt");
                this.Invoke((Action)(() =>
                {
                    this.pb_TwitchApi.Image = this.greenLight;
                    General.ConfigureToolTip(this.pb_TwitchApi, "Hosts, Raids, and Messages are being tracked.");
                }));
            };

            twitchClient.OnBeingHosted += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.HOST, e);
            twitchClient.OnMessageReceived += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.MESSAGE, e);
            twitchClient.OnRaidNotification += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.RAID, e);

            twitchClient.OnCommunitySubscription += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.COMMUNITY, e);
            twitchClient.OnNewSubscriber += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.SUBSCRIPTION, e);
            twitchClient.OnReSubscriber += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.SUBSCRIPTION, e);
            twitchClient.OnGiftedSubscription += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.SUBGIFT, e);

            // [7:21 PM] Defender1031: But the point is that I noticed that anytime anyone hosts, EVEN if it's a smaller host not pushed by the API, I get a PM from "jtv" saying "YarlRagan31 is now hosting you."
            // twitchClient.OnWhisperReceived -> check whispers for messages from jtv, parse the message, there's no number of viewers though.

            GetAllFollowers();

            pubSub.Connect();
            twitchClient.Connect();

            TwitchLib.Api.V5.Models.Bits.Cheermotes requestedCheermotes = await twitchAPI.V5.Bits.GetCheermotesAsync(channelID);

            foreach (TwitchLib.Api.V5.Models.Bits.Action action in requestedCheermotes.Actions)
            {
                this.cheermotes.Add(action.Prefix.ToLower());
            }

            if (MainTADAForm.mainTADAForm.webSocketServer != null)
            {
                this.pb_Websocket.Image = this.greenLight;
                General.ConfigureToolTip(this.pb_Websocket, "Websocket server configured on port 21573.");
            }

            // Need to actually iterate over (MainTADAForm.mainTADAForm.snapshotAlerts()) and add macros to their own list.
            currentAlerts = MainTADAForm.mainTADAForm.snapshotAlerts();
            /*
            foreach (_TADAAlert alert in currentAlerts[General.alertTypes[General.ALERTTYPES.MACRO]])
            {
                List<Keys> modifiers = new List<Keys>();
                if (alert.macroKey.Ctrl) { modifiers.Add(Keys.Control); }
                if (alert.macroKey.Alt) { modifiers.Add(Keys.Alt); }
                if (alert.macroKey.Shift) { modifiers.Add(Keys.Shift); }
                currentMacros.Add(new KeyHandler(alert.macroKey.Key, modifiers, this), alert);
            }
            */
            MainTADAForm.mainTADAForm.AnimationWindow.SetPlayedAlerts(this);
        }

        private async void GetAllFollowers()
        {
            this.followers = new Dictionary<string, List<string>>();

            if (System.IO.File.Exists("followers.txt"))
            {
                string text = System.IO.File.ReadAllText("followers.txt");
                AddFollowers(text.Split(' ').ToList<string>());
                followerFileWritten = true;
            }

            await GetFollowers(null);
            WriteFollowers();
            followerFileWritten = true;

            this.Invoke((Action)(() =>
            {
                this.pb_FollowerList.Image = this.greenLight;
                General.ConfigureToolTip(this.pb_FollowerList, "Your followers have been updated.");
            }));

            return;
        }

        private void WriteFollowers()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("followers.txt"))
            {
                foreach (string userList in this.followers.Keys)
                {
                    foreach (string userId in followers[userList])
                    {
                        file.Write("{0} ", userId);
                    }
                }
            }
        }

        private bool FindFollower(string userId)
        {
            string substr = "0";
            if (userId.Length > 5)
            {
                substr = userId.Substring(0, 5);
            }
            return this.followers.ContainsKey(substr) && this.followers[substr].Contains(userId);
        }

        private void AddFollowers(List<string> followerList)
        {
            foreach (string follower in followerList)
            {
                string substr = "0";

                if (follower.Length > 5)
                {
                    substr = follower.Substring(0, 5);
                }

                if (!this.followers.ContainsKey(substr))
                {
                    this.followers.Add(substr, new List<string>());
                }

                if (!string.IsNullOrWhiteSpace(follower) && !this.followers[substr].Contains(follower))
                {
                    this.followers[substr].Add(follower);
                }
            }
        }

        private async Task GetFollowers(string after)
        {
            GetUsersFollowsResponse followers = await twitchAPI.Helix.Users.GetUsersFollowsAsync(after, null, 100, null, channelID);

            if (followers.Follows.Length > 0)
            {
                List<string> followerList = new List<string>();
                foreach (Follow follower in followers.Follows)
                {
                    followerList.Add(follower.FromUserId);
                }
                AddFollowers(followerList);

                if (followers.Pagination != null && followers.Pagination.Cursor != null)
                {
                    System.Threading.Thread.Sleep(1000);
                    await GetFollowers(followers.Pagination.Cursor);
                }
            }
        }

        public async void ReceivedTwitchEvent(General.ALERTTYPES alertType, Object eventDetails)
        {
            SubstitutionObject subObject = new SubstitutionObject();
            CommunityGiftSub comGift = null;
            User user = null;
            List<General.ALERTTYPES> searchAlerts = new List<General.ALERTTYPES>();

            searchAlerts.Insert(0, alertType);

            switch (alertType)
            {
                case General.ALERTTYPES.AUTOHOST:
                case General.ALERTTYPES.HOST:
                    {
                        OnBeingHostedArgs eHost = eventDetails as OnBeingHostedArgs;

                        subObject.Amount = eHost.BeingHostedNotification.Viewers;
                        subObject.ViewerName = eHost.BeingHostedNotification.HostedByChannel;

                        user = await GetUser(await GetUserIdFromUsername(eHost.BeingHostedNotification.HostedByChannel));

                        alertType = eHost.BeingHostedNotification.IsAutoHosted ? General.ALERTTYPES.AUTOHOST : General.ALERTTYPES.HOST;
                    }
                    break;
                case General.ALERTTYPES.ANONYMOUS_BITS:         // Not a real thing that can be sent.  Just adding it here for future clarity.
                case General.ALERTTYPES.BITS:
                    {
                        OnBitsReceivedV2Args eBits = eventDetails as OnBitsReceivedV2Args;

                        subObject.Amount = eBits.BitsUsed;
                        subObject.Message = eBits.ChatMessage;
                        subObject.ViewerName = (eBits.IsAnonymous ? General.ANONYMOUS_BITS_NAME : eBits.UserName);

                        if (eBits.IsAnonymous)
                        {
                            searchAlerts.Insert(0, General.ALERTTYPES.ANONYMOUS_BITS);
                            user = AnonymousUser;
                        }
                        else
                        {
                            user = await GetUser(eBits?.UserId);
                        }
                    }
                    break;
                case General.ALERTTYPES.COMMUNITY:
                    {
                        // This also needs to check for anonymous community gift subs.  Hurrah.
                        OnCommunitySubscriptionArgs eCommunity = eventDetails as OnCommunitySubscriptionArgs;

                        bool anonymous = eCommunity.GiftedSubscription.UserId == null;
                        int giftCount = eCommunity.GiftedSubscription.MsgParamMassGiftCount;

                        if (!anonymous)
                        {
                            user = await GetUser(eCommunity.GiftedSubscription.UserId);
                        }
                        else
                        {
                            user = AnonymousUser;
                        }

                        // Fill in Tier and Tier Name from the SubGift / AnonSubGift messages that come in.
                        subObject.Amount = giftCount;
                        subObject.SetPlan(eCommunity.GiftedSubscription.MsgParamSubPlan);
                        subObject.ViewerName = anonymous ? General.ANONYMOUS_SUB_GIFT_NAME : user?.user.DisplayName ?? General.ANONYMOUS_SUB_GIFT_NAME;

                        CommunityGiftSub defaultGiftSub;

                        if (user == null || anonymous)
                        {
                            defaultGiftSub = new CommunityGiftSub(subObject);
                        }
                        else
                        {
                            defaultGiftSub = new CommunityGiftSub(user, subObject);
                        }

                        CommunityGiftSub communityGiftSub = null;

                        if (anonymous && FindSuitableAlert(General.ALERTTYPES.ANONYMOUS_COMMUNITY, user, subObject) != null)
                        {
                            communityGiftSub = new CommunityGiftSub(subObject);
                            communityGiftSub.use_anonymous_alert = true;
                        }
                        else if (FindSuitableAlert (General.ALERTTYPES.COMMUNITY, user, subObject) != null)
                        {
                            communityGiftSub = null;
                            if (user != null)
                            {
                                communityGiftSub = new CommunityGiftSub(user, subObject);
                            }
                            else
                            {
                                communityGiftSub = new CommunityGiftSub(subObject);
                            }
                        }

                        if (communityGiftSub != null)
                        {
                            communityGiftSubs.Add(communityGiftSub);
                        }
                    }

                    return;
                case General.ALERTTYPES.FIRSTMESSAGE:
                case General.ALERTTYPES.MESSAGE:
                    {
                        // Originally, this cancelled out bits messages, but because bits are handled by PubSub, that event will also fire.
                        // This could match a message trigger, or could be someone's first message of the day, so this flow follows too.
                        OnMessageReceivedArgs eMessage = eventDetails as OnMessageReceivedArgs;

                        subObject.Message = eMessage.ChatMessage.Message;
                        subObject.ViewerName = eMessage.ChatMessage.Username;

                        var f = eMessage.ChatMessage.EmoteSet;

                        user = await GetUser(eMessage.ChatMessage.UserId);

                        // If the user hasn't said their first message today, add it to the alert types to search.
                        // Also, set first message even if they don't have a first message alert, because they've spoken now.
                        if (!user.firstMessage)
                        {
                            searchAlerts.Insert(0, General.ALERTTYPES.FIRSTMESSAGE);
                            user.firstMessage = true;
                        }
                    }
                    break;
                case General.ALERTTYPES.FOLLOW:
                    {
                        OnFollowArgs eFollow = eventDetails as OnFollowArgs;

                        subObject.ViewerName = eFollow.DisplayName;
                        user = await GetUser(eFollow.UserId);

                        if (FindFollower(eFollow.UserId))
                        {
                            alertType = General.ALERTTYPES.NONE;
                        }
                        else
                        {
                            AddFollowers(new List<string> { eFollow.UserId });

                            // And then add them to the followers file.
                            this.missingFollowers += eFollow.UserId + " ";
                            if (this.followerFileWritten)
                            {
                                using (System.IO.StreamWriter writer = new System.IO.StreamWriter("followers.txt", true))
                                {
                                    writer.Write(missingFollowers);
                                }
                                this.missingFollowers = "";
                            }
                        }

                        // If we don't want to toast idle people, and the user hasn't spoken in the specified time, then don't toast.
                        if (MainTADAForm.mainTADAForm.configurationSettings.dontToastLurkingFollowers &&
                            DateTime.Now - MainTADAForm.mainTADAForm.configurationSettings.lurkingFollowerTime > user.lastSpoke)
                        {
                            alertType = General.ALERTTYPES.NONE;
                        }
                    }
                    break;
                /*
                                case General.ALERTTYPES.MACRO:
                                    // This doesn't do any setup.  The trick below is to pick the appropriate
                                    // alert based on the keypress.  Or, better, it should be handled elsewhere.
                                    return;
                */
                case General.ALERTTYPES.RAID:
                    {
                        OnRaidNotificationArgs eRaid = eventDetails as OnRaidNotificationArgs;

                        subObject.Amount = Int32.Parse(eRaid.RaidNotification.MsgParamViewerCount);
                        subObject.ViewerName = eRaid.RaidNotification.DisplayName;

                        user = await GetUser(eRaid.RaidNotification.UserId);
                    }
                    break;
                case General.ALERTTYPES.SUBGIFT:
                    {
                        OnGiftedSubscriptionArgs eGift = eventDetails as OnGiftedSubscriptionArgs;

                        subObject.RecipientName = eGift.GiftedSubscription.MsgParamRecipientDisplayName;
                        subObject.SetPlan(eGift.GiftedSubscription.MsgParamSubPlan);
                        subObject.TierName = eGift.GiftedSubscription.MsgParamSubPlanName;
                        subObject.ViewerName = eGift.GiftedSubscription.DisplayName ?? General.ANONYMOUS_SUB_GIFT_NAME;

                        bool isAnonymousGift = (subObject.ViewerName == General.ANONYMOUS_SUB_GIFT_NAME);

                        /*
                         * So, here's the further problem.
                         * There's no way to tell if a particular sub is part of a community gift sub or not without that event firing.
                         * So, maybe we just delay this by a few seconds, with the hope that IRC gets to catch up?
                         */

                        Thread.Sleep(5000);

                        comGift = communityGiftSubs.DefaultIfEmpty(null).First(gift => gift.communityGifter.user.DisplayName == subObject.ViewerName);

                        user = isAnonymousGift ? AnonymousUser : await GetUser(await GetUserIdFromUsername(eGift.GiftedSubscription.DisplayName));
                        UpdateUserSubscription(await GetUser(eGift.GiftedSubscription.MsgParamRecipientId));

                        if (comGift != null)
                        {
                            comGift.giftees.Add(subObject.RecipientName);
                            comGift.subObject.Tier = subObject.Tier;   // Overwriting from the Client version.
                            comGift.subObject.TierName = subObject.TierName;

                            if (comGift.Ready())
                            {
                                subObject = comGift.subObject;

                                searchAlerts.Clear();
                                searchAlerts.Insert(0, comGift.use_anonymous_alert ? General.ALERTTYPES.ANONYMOUS_COMMUNITY : General.ALERTTYPES.COMMUNITY);
                            }
                            else
                            {
                                // The community gift sub hasn't been fully compiled yet, so exit out.
                                return;
                            }
                        }
                        else
                        {
                            if (isAnonymousGift)
                            {
                                searchAlerts.Insert(0, General.ALERTTYPES.ANONYMOUS_GIFT);
                            }
                        }

                    }
                    break;
                case General.ALERTTYPES.SUBSCRIPTION:
                    {
                        string userId = null;

                        if (eventDetails is OnNewSubscriberArgs)
                        {
                            OnNewSubscriberArgs newSubscriberArgs = eventDetails as OnNewSubscriberArgs;

                            subObject.Amount = 1;
                            subObject.Message = newSubscriberArgs.Subscriber.ResubMessage ?? "";
                            subObject.RecipientName = newSubscriberArgs.Subscriber.DisplayName ?? "";
                            subObject.SetPlan(newSubscriberArgs.Subscriber.SubscriptionPlan);
                            subObject.TierName = newSubscriberArgs.Subscriber.SubscriptionPlanName;
                            subObject.ViewerName = newSubscriberArgs.Subscriber.DisplayName ?? "";

                            userId = newSubscriberArgs.Subscriber.UserId;
                        }
                        else if (eventDetails is OnReSubscriberArgs)
                        {
                            OnReSubscriberArgs reSubscriberArgs = eventDetails as OnReSubscriberArgs;

                            string months = reSubscriberArgs.ReSubscriber.MsgParamShouldShareStreak ? reSubscriberArgs.ReSubscriber.MsgParamStreakMonths : reSubscriberArgs.ReSubscriber.MsgParamCumulativeMonths;
                            int.TryParse(months, out int m);

                            subObject.Amount = m;
                            subObject.Message = reSubscriberArgs.ReSubscriber.ResubMessage ?? "";
                            subObject.RecipientName = reSubscriberArgs.ReSubscriber.DisplayName ?? "";
                            subObject.SetPlan(reSubscriberArgs.ReSubscriber.SubscriptionPlan);
                            subObject.TierName = reSubscriberArgs.ReSubscriber.SubscriptionPlanName;
                            subObject.ViewerName = reSubscriberArgs.ReSubscriber.DisplayName ?? "";

                            userId = reSubscriberArgs.ReSubscriber.UserId;
                        }

                        user = await GetUser(userId);
                        UpdateUserSubscription(user);
                    }
                    break;
                default:
                    return;
            }

            if (user != null && DateTime.Now.Subtract(user.subscriptionLastUpdated).Hours >= 2)
            { 
                UpdateUserSubscription(user);
            }

            // Now, because searchAlerts is a thing, loop through searchAlerts until a valid alert is found.
            // THEN make that alert type the subObject alert type.
            // THEN play the alert.
            TADAAlert alertToUse = null;

            for (int i = 0; i < searchAlerts.Count; ++ i)
            {
                alertToUse = FindSuitableAlert(searchAlerts[i], user, subObject);
                if (alertToUse != null)
                {
                    subObject.AlertType = General.alertTypes[searchAlerts[i]];

                    if (alertType == General.ALERTTYPES.ANONYMOUS_COMMUNITY || alertType == General.ALERTTYPES.COMMUNITY)
                    {
                        communityGiftSubs.Remove(comGift);
                    }

                    break;
                }
            }

            if (alertToUse != null)
            {
                if (!recentlyPlayed.ContainsKey(alertToUse))
                {
                    recentlyPlayed.Add(alertToUse, new RecentlyPlayed(alertToUse, user));
                }
                else
                {
                    recentlyPlayed[alertToUse].AddTriggerer(user);
                }

                lock (addingAlert)
                {
                    PrepareAndPlayAlert(alertToUse, user, subObject, alertType);
                    MainTADAForm.mainTADAForm.webSocketServer.TellConnectedSockets(subObject);
                }
            }
        }

        private void PrepareAndPlayAlert (TADAAlert alertToUse, User user, SubstitutionObject subObject, General.ALERTTYPES alertType)
        {
            // Now that an alert has been decided on, pick pieces, and then add it to the to-play list.
            TADAAlertImage alertImage = null;
            TADAAlertAudio alertAudio = null;
            TADAAlertText alertText = null;
            TADAAlertText alertResponse = null;

            alertImage = (alertToUse.ImageCount == 0 ? null : alertToUse.Images[General.rng.Next(alertToUse.ImageCount)]);
            alertAudio = (alertToUse.AudioCount == 0 ? null : alertToUse.Audios[General.rng.Next(alertToUse.AudioCount)]);
            alertText = (alertToUse.StreamerCount == 0 ? null : alertToUse.StreamerTexts[General.rng.Next(alertToUse.StreamerCount)]);
            alertResponse = (alertToUse.ViewerCount == 0 ? null : alertToUse.ViewerTexts[General.rng.Next(alertToUse.ViewerCount)]);

            if (alertText != null)
            {
                string alertTextMessage = alertText.GetMessage(true);
                string alertReadMessage = alertText.GetReadMessage(true);
                PromptBuilder ttsPrompt = General.PerformTTSSubstitutions(alertReadMessage, subObject, ipaDictionary);

                General.PerformSubstitutions(ref alertTextMessage, subObject);
                General.PerformSubstitutions(ref alertReadMessage, subObject);

                alertText.SetMessage(alertTextMessage);
                alertText.SetReadMessage(alertReadMessage);
                alertText.SetVoicedMessage(ttsPrompt);

                if (!string.IsNullOrEmpty(alertTextMessage) || !string.IsNullOrEmpty(alertReadMessage))
                {
                    General.LogFile(string.Format("{0}; {1}", alertTextMessage ?? "(nil)", alertReadMessage ?? "(nil)"));
                }
            }

            if (alertResponse != null)
            {
                string message = subObject.Message ?? "";

                if (!string.IsNullOrEmpty(message))
                {
                    // Remove blocked words.
                    foreach (string blockedWord in MainTADAForm.mainTADAForm.configurationSettings.blockedWords.Split(' '))
                    {
                        message = General.ReplaceCaseInsensitive(message, blockedWord, "");
                    }

                    if (MainTADAForm.mainTADAForm.configurationSettings.dontReadEmotes)
                    {
                        // Figure out what the emote words are.  Remove them.
                    }

                    if (MainTADAForm.mainTADAForm.configurationSettings.dontReadCheers)
                    {
                        // Figure out what the cheer words are.  Remove them and the amounts.
                    }
                }

                alertResponse.SetMessage(message, true);

                // Search through alertResponse to see if any waypoint has readMessage checked.
                var hasReadMessage = (alertResponse.Waypoints.Waypoints.Any(x => x.ReadMessage));

                if (hasReadMessage)
                {
                    // Then, make sure they aren't TTS'ing too much.
                    if (!MainTADAForm.mainTADAForm.configurationSettings.disallowSpammyTTS ||
                        DateTime.Now - MainTADAForm.mainTADAForm.configurationSettings.spammyTTSTime >= user.lastTTS)
                    {
                        alertResponse.SetReadMessage(message);
                        user.lastTTS = DateTime.Now;
                    }
                }
            }

            ListedAlert alert = new ListedAlert(subObject.ViewerName,
                                                alertText?.GetMessage() ?? alertText?.GetReadMessage() ?? alertResponse?.GetMessage() ?? alertResponse?.GetReadMessage() ?? "no message",
                                                General.alertTypes[alertType],
                                                alertImage,
                                                alertAudio,
                                                alertText,
                                                alertResponse,
                                                subObject);
            dg_Pending.Invoke((Action)(() => { pendingAlertBindingSource.Add(alert); }));
            MainTADAForm.mainTADAForm.AnimationWindow.Invoke((Action)(() => { MainTADAForm.mainTADAForm.AnimationWindow.PendingAlert(); }));
        }

#region UserStuff
        // These "User" functions aren't part of the User class because it doesn't know anything about TwitchLib or those parts.
        private async Task<User> GetUser(string userId)
        {
            User user;

            if (userId == null)
            {
                user = AnonymousUser;
            }
            else
            {
                if (viewers.ContainsKey(userId))
                {
                    user = viewers[userId];
                }
                else
                {
                    TwitchLib.Api.Helix.Models.Users.GetUsers.User twitchUser = (await twitchAPI.Helix.Users.GetUsersAsync(new List<string> { userId }, null, accessToken)).Users[0];
                    Subscription twitchSubscription = await GetUserSubscription(userId);

                    // Not setting lastSpoke because this isn't a guarantee.
                    user = new User()
                    {
                        user = twitchUser,
                        subscription = twitchSubscription,
                        subscriptionLastUpdated = DateTime.Now,
                        firstMessage = false,
                    };

                    try
                    {
                        viewers.Add(userId, user);
                    }
                    catch (ArgumentException)
                    {
                        // And do nothing because that likely means that the key already exists and it's not a problem.
                    }
                }
            }

            if (user != null && userId != null)
            {
                GetBadgesUndocumented(user);
            }

            return user;
        }

        private async void UpdateUserSubscription(User user)
        {
            user.UpdateSubscription(await GetUserSubscription(user.user.Id));
        }

        private async Task UpdateUserSubscription(string userId, bool forceUpdate = false)
        {
            User user = await GetUser(userId);

            if (forceUpdate || DateTime.Now.Subtract(user.subscriptionLastUpdated).Hours >= 2)
            {
                UpdateUserSubscription(user);
            }
        }

        private void GetBadgesUndocumented (User user)
        {
            if (user != null)
            {
                System.Net.Http.HttpClient twitchAPIRequest = new System.Net.Http.HttpClient();
                twitchAPIRequest.DefaultRequestHeaders.Add("Client-ID", General.CLIENT_ID);
                twitchAPIRequest.DefaultRequestHeaders.Add("Authorization", "Oauth " + twitchAPI.Settings.AccessToken);
                twitchAPIRequest.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");

                System.Net.Http.HttpResponseMessage twitchAPIResponse = twitchAPIRequest.GetAsync(new Uri(string.Format(@"https://api.twitch.tv/kraken/users/{0}/chat/channels/{1}", user.UserID, this.channelID))).Result;

                if (twitchAPIResponse.IsSuccessStatusCode)
                {
                    System.Net.Http.HttpContent content = twitchAPIResponse.Content;
                    Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(content.ReadAsStringAsync().Result);

                    if (jObject["badges"] != null)
                    {
                        for (int c = 0; c < jObject["badges"].Count(); ++c)
                        {
                            switch ((string)jObject["badges"][c]["id"])
                            {
                                case "admin":
                                    user.badges.admin = true;
                                    break;
                                case "bits":
                                    user.badges.bits = true;
                                    user.badges.bit_badge = Convert.ToUInt32((string)jObject["badges"][c]["version"]);
                                    break;
                                case "broadcaster":
                                    user.badges.broadcaster = true;
                                    break;
                                case "global_mod":
                                    user.badges.global_mod = true;
                                    break;
                                case "moderator":
                                    user.badges.moderator = true;
                                    break;
                                case "premium":
                                    user.badges.premium = true;
                                    break;
                                case "subscriber":
                                    user.badges.subscriber = true;
                                    user.badges.subscribe_months = Convert.ToUInt32((string)jObject["badges"][c]["version"]);
                                    break;
                                case "staff":
                                    user.badges.staff = true;
                                    break;
                                case "turbo":
                                    user.badges.turbo = true;
                                    break;
                                case "vip":
                                    user.badges.vip = true;
                                    break;
                            }
                        }
                    }
                }
            }
        }
#endregion

        private async Task<Subscription> GetUserSubscription(string userId)
        {
            CheckUserSubscriptionResponse subscription = await twitchAPI.Helix.Subscriptions.CheckUserSubscriptionAsync(channelID, userId, twitchAPI.Settings.AccessToken);
            return subscription.Data[0];
        }

        private async Task<string> GetUserIdFromUsername(string username)
        {
            return (await twitchAPI.Helix.Users.GetUsersAsync(null, new List<string> { username }, accessToken)).Users[0].Id;
        }

        private void AddSubscriptionDetailsToSubstitutionObject(User user, SubstitutionObject subObject)
        {
            if (user.subscription?.Tier != null)
            {
                subObject.SetPlan(TierAndPlanConverter.StringToSubscriptionPlan(user.subscription?.PlanName));
                subObject.TierName = user.subscription?.Tier;
            }
        }

        private TADAAlert FindSuitableAlert(General.ALERTTYPES alertType, User user, SubstitutionObject subObject)
        {
            TADAAlert returnedAlert = null;
            List<TADAAlert> potentialAlerts = new List<TADAAlert>();

            int alertScore = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;

            string alertTypeString = General.alertTypes[alertType];
            string viewerName = subObject.ViewerName;
            int amount = subObject.Amount;
            string message = subObject.Message;
            TierAndPlanConverter.SUBSCRIPTION_PLANS plan = subObject.Plan;

            if (currentAlerts.ContainsKey(alertTypeString))
            {
                foreach (TADAAlert alert in currentAlerts[alertTypeString])
                {
                    /*
                     * Loop through the appropriate alerts, and assign each one a score.
                     * name > tier > keyword > amount
                     */

                    // If this alert was recently triggered and is on cooldown, check the next alert.
                    if (recentlyPlayed.ContainsKey(alert) && !recentlyPlayed[alert].CanTrigger(user))
                    {
                        continue;
                    }

                    int thisAlertScore = ScoreAlert(alert, user, amount, message, plan);
                    if (thisAlertScore > alertScore)
                    {
                        potentialAlerts.Clear();
                        alertScore = thisAlertScore;
                    }

                    if (thisAlertScore == alertScore && alertScore != (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM)
                    {
                        potentialAlerts.Add(alert);
                    }
                }
            }

            // Then potentialAlerts is the list of potential alerts, given the scores.  Return one of them at random.
            if (potentialAlerts.Count > 0)
            {
                returnedAlert = potentialAlerts[General.rng.Next(potentialAlerts.Count)];
            }

            return returnedAlert;
        }

        private void CheckBasicBadge(bool condition, bool userState, bool matchFalse, ref int score, ref bool continueTesting)
        {
            // If matchFalse is set, then we care about condition being false.
            if (continueTesting)
            {
                // Then we haven't short circuited.
                // If matchFalse is set, then we DO want to check when condition is false.  We always want to check when condition is true.
                // This needs to be replaced by ternary buttons.  Damn it.
                if ((matchFalse && !condition) || condition)
                {
                    score = (condition == userState) ? score + (int)General.SCORES.SCORE_BADGE_MATCHED : (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                    continueTesting = (condition == userState);
                }
            }
        }

        private int ScoreAlert (TADAAlert alert, User user, int amount, string message, TierAndPlanConverter.SUBSCRIPTION_PLANS plan)
        {
            int finalScore = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;

            if (alert.ConditionCount == 0)
            {
                finalScore = 1;
            }
            else
            {
                foreach (TADAAlertCondition condition in alert.Conditions)
                {
                    bool continueTesting = true;
                    int score = 0;

                    if (condition.Empty)
                    {
                        score = (int)General.SCORES.SCORE_NO_CONDITIONS;
                    }
                    else
                    {
                        // This has to be rewritten to consider viewer tier separately from subscription tier.
                        if (condition.HasViewerTier)
                        {
                            if ((condition.ViewerTierNoTier && user.SubscriptionTier == TierAndPlanConverter.SUBSCRIPTION_TIERS.NO_TIER) ||
                                (condition.ViewerTierTierOne && user.SubscriptionTier == TierAndPlanConverter.SUBSCRIPTION_TIERS.TIER_ONE) ||
                                (condition.ViewerTierTierTwo && user.SubscriptionTier == TierAndPlanConverter.SUBSCRIPTION_TIERS.TIER_TWO) ||
                                (condition.ViewerTierTierThree && user.SubscriptionTier == TierAndPlanConverter.SUBSCRIPTION_TIERS.TIER_THREE))
                            {
                                score += (int)General.SCORES.SCORE_VIEWER_TIER_MATCHED;
                            }
                            else
                            {
                                score = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                                continueTesting = false;
                            }
                        }

                        if (continueTesting && condition.HasSubscriptionTier)
                        {
                            if ((condition.SubscriptionTierPrime && plan == TierAndPlanConverter.SUBSCRIPTION_PLANS.TWITCH_PRIME) ||
                                (condition.SubscriptionTierTierOne && plan == TierAndPlanConverter.SUBSCRIPTION_PLANS.TIER_ONE) ||
                                (condition.SubscriptionTierTierTwo && plan == TierAndPlanConverter.SUBSCRIPTION_PLANS.TIER_TWO) ||
                                (condition.SubscriptionTierTierThree && plan == TierAndPlanConverter.SUBSCRIPTION_PLANS.TIER_THREE))
                            {
                                score += (int)General.SCORES.SCORE_SUB_TIER_MATCHED;
                            }
                            else
                            {
                                score = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                                continueTesting = false;
                            }
                        }

                        if (continueTesting && condition.HasBadges)
                        {
                            CheckBasicBadge(condition.IsAdmin, user.badges.admin, false, ref score, ref continueTesting);
                            CheckBasicBadge(condition.IsPrime, user.badges.premium, false, ref score, ref continueTesting);
                            CheckBasicBadge(condition.IsStaff, user.badges.staff, false, ref score, ref continueTesting);
                            CheckBasicBadge(condition.IsTurbo, user.badges.turbo, false, ref score, ref continueTesting);

                            if (continueTesting)
                            {
                                if (condition.IsBroadcaster && user.badges.broadcaster ||
                                    condition.IsModerator && user.badges.moderator ||
                                    condition.IsVIP && user.badges.vip)
                                {
                                    score += (int)General.SCORES.SCORE_BADGE_MATCHED;
                                }
                                else
                                {
                                    score = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                                    continueTesting = false;
                                }
                            }

                            if (continueTesting && condition.IsBits)
                            {
                                if ((condition.MinBits == 0 || amount >= condition.MinBits) &&
                                    (condition.MaxBits == 0 || amount <= condition.MaxBits))
                                {
                                    score += (int)General.SCORES.SCORE_BADGE_MATCHED;
                                }
                                else
                                {
                                    score = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                                    continueTesting = false;
                                }
                            }

                            if (continueTesting && condition.IsSubscribed)
                            {
                                if ((condition.MinMonths == 0 || user.badges.subscribe_months >= condition.MinMonths) &&
                                    (condition.MaxMonths == 0 || user.badges.subscribe_months <= condition.MaxMonths))
                                {
                                    score += (int)General.SCORES.SCORE_BADGE_MATCHED;
                                }
                                else
                                {
                                    score = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                                    continueTesting = false;
                                }
                            }
                        }

                        // Check to see if the username is not allowed to trigger this alert.
                        if (continueTesting && !string.IsNullOrEmpty(condition.BannedNames))
                        {
                            string username = user.user?.DisplayName.ToLower() ?? "anonymous";

                            List<string> bannedNames = condition.BannedNames.Split(',').Select(n => n.Trim().ToLower()).ToList();
                            
                            foreach (string bannedName in bannedNames)
                            {
                                if (username.Contains(bannedName))
                                {
                                    score = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                                    continueTesting = false;
                                    break;
                                }
                            }
                        }

                        // Then check usernames, keywords, and amount.
                        if (continueTesting && !string.IsNullOrEmpty(condition.Usernames))
                        {
                            string username = user.user?.DisplayName.ToLower() ?? "anonymous";

                            List<string> names = condition.Usernames.Split(',').Select(n => n.Trim().ToLower()).ToList<string>();
                            if (names.Contains(username))
                            {
                                score += (int)General.SCORES.SCORE_NAME_MATCHED;
                            }
                            else
                            {
                                score = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                                continueTesting = false;
                            }
                        }

                        if (continueTesting && !string.IsNullOrEmpty(condition.Keywords))
                        {
                            bool matched = false;

                            List<string> keywords = condition.Keywords.Split(',').Select(n => n.Trim().ToLower()).ToList<string>();

                            foreach (string key in keywords)
                            {
                                string keyPattern = string.Format("\\b{0}\\b", key); // Word boundary, test word/phrase, word boundary.  Will not play nicely with punctuation because regex.
                                if (System.Text.RegularExpressions.Regex.Match(message, keyPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase).Success)
                                {
                                    matched = true;
                                    score += (int)General.SCORES.SCORE_KEYWORD_MATCHED;
                                }
                            }

                            if (!matched)
                            {
                                score = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                                continueTesting = false;
                            }
                        }

                        if (continueTesting && (condition.MinAmount > 0 || condition.MaxAmount > 0))
                        {
                            if ((condition.MinAmount == 0 || amount >= condition.MinAmount) &&
                                (condition.MaxAmount == 0 || amount <= condition.MaxAmount))
                            {
                                score += (int)General.SCORES.SCORE_WITHIN_AMOUNT;
                            }
                            else
                            {
                                score = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                                continueTesting = false;
                            }
                        }
                    }

                    finalScore = Math.Max(finalScore, score);
                }
            }
            return finalScore;
        }

        private void PendingAlertAdded(object sender, AddingNewEventArgs e)
        {
            // Something has been added to the to-play list.  Let the playing alert window know that there's something there, and to play something.
            MainTADAForm.mainTADAForm.AnimationWindow.PendingAlert();
        }

        internal ListedAlert PlayFirstPendingAlert()
        {
            ListedAlert alertInTransition = (ListedAlert)pendingAlertBindingSource[0];

            dg_Pending.Invoke((Action)(() => { pendingAlertBindingSource.RemoveAt(0); }));

            // Put the most recently played alerts at the top of the screen.
            dg_Played.Invoke((Action)(() => { playedAlertBindingSource.Insert(0, alertInTransition); }));

            return alertInTransition;
        }

        public bool HasAlertsWaiting()
        {
            return pendingAlertBindingSource.Count > 0;
        }
    }

    public class Badges
    {
        public bool admin = false;
        public bool bits = false;
        public bool broadcaster = false;
        public bool global_mod = false;
        public bool moderator = false;
        public bool premium = false;
        public bool subscriber = false;
        public bool staff = false;
        public bool turbo = false;
        public bool vip = false;
        public uint bit_badge = 0;
        public uint subscribe_months = 0;
    }

    public class User
    {
        // In the future, this sort of information might be stored in a database to be stored beyond one session.
        // Also, things that could be added to this are preferred pronouns, so that messages can have pronoun/possessive tags in them that reference these values.
        // Eg. Everyone say thanks to {user} for {poss} {amount} month{s} of support
        // => Everyone say thanks to Invariel for his 3 months of support
        // => Everyone say thanks to RedDempshun for her 1 month of support
        public TwitchLib.Api.Helix.Models.Users.GetUsers.User user;
        public Subscription subscription;
        public DateTime subscriptionLastUpdated;    // Cache subscription information for a while.  One hour?  Two hours?
        public DateTime lastSpoke;
        public DateTime lastTTS;
        public bool firstMessage;                   // Only gets set if the user -has- a first message of the day and if it triggers.
        public Badges badges = new Badges();        // The badges a user has.

        public string UserID
        {
            get { return user?.Id; }
        }

        public TierAndPlanConverter.SUBSCRIPTION_TIERS SubscriptionTier
        {
            get { return TierAndPlanConverter.StringToSubscriptionTier(subscription?.Tier); }
        }

        public string SubscriptionTierName
        {
            get { return TierAndPlanConverter.SubscriptionTierToString(SubscriptionTier); }
        }

        public void UpdateLastSpoke()
        {
            this.lastSpoke = DateTime.Now;
        }

        public void UpdateSubscription(Subscription subscription)
        {
            this.subscription = subscription;
            this.subscriptionLastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} {2}", user?.DisplayName, user?.Id, subscription?.Tier);
        }
    }

    public class CommunityGiftSub
    {
        public User communityGifter;
        public int numberOfSubs;
        public List<string> giftees;
        public SubstitutionObject subObject;
        public bool is_anonymous;
        public bool use_anonymous_alert = false;

        public CommunityGiftSub(User gifter, SubstitutionObject subObject)
        {
            this.communityGifter = gifter;
            this.CombinedSetup(subObject);
            this.is_anonymous = false;
        }

        public CommunityGiftSub (SubstitutionObject subObject)
        {
            this.CombinedSetup(subObject);
            this.is_anonymous = true;
        }

        private void CombinedSetup (SubstitutionObject subObject)
        {
            this.numberOfSubs = subObject.Amount;
            this.subObject = subObject;
            this.giftees = new List<string>();
        }

        public bool Ready ()
        {
            if (giftees.Count == numberOfSubs)
            {
                subObject.RecipientName = this.ToString();
            }
            
            return giftees.Count == numberOfSubs;
        }

        public override string ToString()
        {
            if (numberOfSubs == 1)
            {
                return giftees[0];
            }
            else if (numberOfSubs == 2)
            {
                return string.Join(" and ", giftees);
            }
            else
            {
                return string.Format("{0} and {1}", string.Join(", ", giftees.GetRange(0, giftees.Count - 1)), giftees.Last());
            }
        }
    }

    public class ListedAlert
    {
        [DisplayName("Viewer Name")]
        public string ViewerName { get; set; }

        [DisplayName("Alert Message")]
        public string AlertMessage { get; set; }

        [DisplayName("Alert Type")]
        public string AlertType { get; set; }

        [DisplayName("Received Time")]
        public DateTime ReceivedTime { get; set; }

        internal TADAAlertImage alertImage = null;
        internal TADAAlertAudio alertAudio = null;
        internal TADAAlertText alertStreamerText = null;
        internal TADAAlertText alertViewerText = null;

        internal SubstitutionObject subObject = null;

        internal ListedAlert(string viewerName, string alertMessage, string alertType, TADAAlertImage alertImage, TADAAlertAudio alertAudio, TADAAlertText alertStreamerText, TADAAlertText alertViewerText, SubstitutionObject subObject)
        {
            this.ViewerName = viewerName;
            this.AlertMessage = alertMessage;
            this.AlertType = alertType;
            this.alertImage = alertImage;
            this.alertAudio = alertAudio;
            this.alertStreamerText = alertStreamerText;
            this.alertViewerText = alertViewerText;
            this.subObject = subObject;
            this.ReceivedTime = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} : {2}", this.AlertType, this.ViewerName, this.AlertMessage);
        }
    }

    // Need something for storing messages.  <alert, last procced, <user, last procced>>  Need an object for this probably.
    internal class RecentlyPlayed
    {
        private Dictionary<User, DateTime> lastTriggeredByUser = new Dictionary<User, DateTime>();
        private DateTime lastTriggered;
        private TimeSpan userCooldown;
        private TimeSpan globalCooldown;

        internal RecentlyPlayed (TADAAlert alert, User triggerer)
        {
            this.lastTriggered = DateTime.Now;
            this.userCooldown = alert.userCooldown;
            this.globalCooldown = alert.globalCooldown;
            AddTriggerer(triggerer);
        }

        public void AddTriggerer (User triggerer)
        {
            if (!lastTriggeredByUser.ContainsKey(triggerer))
            {
                lastTriggeredByUser.Add(triggerer, DateTime.Now);
            }

            lastTriggeredByUser[triggerer] = DateTime.Now;
        }

        public Boolean CanTrigger(User triggerer)
        {
            DateTime now = DateTime.Now;
            if (lastTriggeredByUser.ContainsKey(triggerer))
            {
                return lastTriggered + globalCooldown <= now && lastTriggeredByUser[triggerer] + userCooldown <= now;
            }

            return lastTriggered + globalCooldown <= now;
        }
    }
}
