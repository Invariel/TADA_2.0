using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shell;
using TwitchLib.Api;
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

        private String accessToken;
        private String channelID;
        private String channelName;

        private Dictionary<String, List<String>> followers;
        private bool followerFileWritten = false;
        private String missingFollowers = "";

        private Dictionary<String, List<TADAAlert>> currentAlerts = new Dictionary<string, List<TADAAlert>>();
        private Dictionary<KeyHandler, TADAAlert> currentMacros = new Dictionary<KeyHandler, TADAAlert>();

        private Dictionary<String, User> viewers = new Dictionary<String, User>();

        // Need something for storing messages.  <alert, last procced, <user, last procced>>  Need an object for this probably.
        private Dictionary<TADAAlert, RecentlyPlayed> recentlyPlayed = new Dictionary<TADAAlert, RecentlyPlayed>();


        // Need a thing for storing community gift subs that are being compiled.
        private List<CommunityGiftSub> communityGiftSubs = new List<CommunityGiftSub>();

        private List<String> cheermotes = new List<String>();

        private User AnonymousUser = new User
        {
            badges = new Badges(),
            firstMessage = true,
            lastSpoke = DateTime.Now,
            subscription = null,
            subscriptionLastUpdated = DateTime.Now,
            user = null
        };

        public Played_Alerts(String channelName, String accessToken)
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
            Connect();
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
                System.Windows.Forms.MessageBox.Show("No Twitch user was found with the name '" + channelName + "'.", "Unrecognized Twitch User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
            pubSub.OnChannelCommerceReceived += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.COMMERCE, e);
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
                    General.LogFile(e.Response.Error);
                    System.Windows.Forms.MessageBox.Show("Failed to connect to Twitch to listen to bits, follows, game purchases, or subscriptions.");
                    this.Invoke((Action)(() => { General.ConfigureToolTip(this.pb_PubSub, "Bits, Follows, Game Purchases, and Subscriptions are not being tracked."); }));
                }
            };

            // Handle these, usually with a reconnection attempt or seventy thousand.
            pubSub.OnPubSubServiceClosed += (sender, e) =>
            {
                this.Invoke((Action)(() =>
                {
                    this.pb_PubSub.Image = this.redLight;
                    General.ConfigureToolTip(this.pb_PubSub, "Bits, Follows, Game Purchases, and Subscriptions are not being tracked.");
                    this.pubSub.Connect();
                }));
            };

            pubSub.OnPubSubServiceError += (sender, e) =>
            {
                this.Invoke((Action)(() =>
                {
                    this.pb_PubSub.Image = this.redLight;
                    General.ConfigureToolTip(this.pb_PubSub, "Bits, Follows, Game Purchases, and Subscriptions are not being tracked.");
                    this.pubSub.Connect();
                }));
            };

            pubSub.OnPubSubServiceConnected += delegate
            {
                // Even if there are no events of the chosen type specified, listening to them is important in case one is added later.
                pubSub.ListenToBitsEventsV2(channelID);
                pubSub.ListenToCommerce(channelID);
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
                General.LogFile(e.Error.Message, "twitchClient.txt");
                this.Invoke((Action)(() =>
                {
                    this.pb_TwitchApi.Image = this.redLight;
                    General.ConfigureToolTip(this.pb_TwitchApi, "Hosts, Raids, and Messages are not being tracked.");
                }));
            };
            twitchClient.OnIncorrectLogin += (sender, e) =>
            {
                General.LogFile(e.Exception.Message, "twitchClient.txt");
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
            this.followers = new Dictionary<String, List<string>>();

            if (System.IO.File.Exists("followers.txt"))
            {
                String text = System.IO.File.ReadAllText("followers.txt");
                AddFollowers(text.Split(' ').ToList<String>());
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
                foreach (String userList in this.followers.Keys)
                {
                    foreach (String userId in followers[userList])
                    {
                        file.Write("{0} ", userId);
                    }
                }
            }
        }

        private bool FindFollower(String userId)
        {
            String substr = "0";
            if (userId.Length > 5)
            {
                substr = userId.Substring(0, 5);
            }
            return this.followers.ContainsKey(substr) && this.followers[substr].Contains(userId);
        }

        private void AddFollowers(List<String> followerList)
        {
            foreach (String follower in followerList)
            {
                String substr = "0";

                if (follower.Length > 5)
                {
                    substr = follower.Substring(0, 5);
                }

                if (!this.followers.ContainsKey(substr))
                {
                    this.followers.Add(substr, new List<string>());
                }

                if (!String.IsNullOrWhiteSpace(follower) && !this.followers[substr].Contains(follower))
                {
                    this.followers[substr].Add(follower);
                }
            }
        }

        private async Task GetFollowers(String after)
        {
            TwitchLib.Api.Helix.Models.Users.GetUsersFollowsResponse followers = await twitchAPI.Helix.Users.GetUsersFollowsAsync(after, null, 100, channelID);
            if (followers.Follows.Length > 0)
            {
                List<String> followerList = new List<String>();
                foreach (TwitchLib.Api.Helix.Models.Users.Follow follower in followers.Follows)
                {
                    followerList.Add(follower.FromUserId);
                }
                AddFollowers(followerList);

                if (followers.Pagination != null)
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
                        subObject.ViewerName = (eBits.IsAnonymous ? General.ANONYMOUS_BITS_NAME : eBits.Username);

                        user = await GetUser(eBits?.UserId);

                        if (eBits.IsAnonymous)
                        {
                            searchAlerts.Insert(0, General.ALERTTYPES.ANONYMOUS_BITS);
                        }
                    }
                    break;
                case General.ALERTTYPES.COMMERCE:
                    {
                        OnChannelCommerceReceivedArgs eCommerce = eventDetails as OnChannelCommerceReceivedArgs;

                        subObject.Message = eCommerce.PurchaseMessage;
                        subObject.ViewerName = eCommerce.Username;

                        user = await GetUser(eCommerce.UserId);
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
                            user = null;
                        }

                        // Fill in Tier and Tier Name from the SubGift / AnonSubGift messages that come in.
                        subObject.Amount = giftCount;
                        subObject.SetPlan(eCommunity.GiftedSubscription.MsgParamSubPlan);
                        subObject.ViewerName = anonymous ? General.ANONYMOUS_SUB_GIFT_NAME : user?.user.DisplayName ?? General.ANONYMOUS_SUB_GIFT_NAME;

                        CommunityGiftSub communityGiftSub = null;

                        if (anonymous && FindSuitableAlert(General.ALERTTYPES.ANONYMOUS_COMMUNITY, user, subObject) != null)
                        {
                            communityGiftSub = new CommunityGiftSub(subObject);
                            communityGiftSub.use_anonymous_alert = true;
                        }
                        else if (FindSuitableAlert (General.ALERTTYPES.COMMUNITY, user, subObject) != null)
                        {
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
                            AddFollowers(new List<String> { eFollow.UserId });

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
                        if (MainTADAForm.configurationSettings.dontToastLurkingFollowers &&
                            DateTime.Now - MainTADAForm.configurationSettings.lurkingFollowerTime > user.lastSpoke)
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

                        subObject.Amount = Int32.Parse(eRaid.RaidNotificaiton.MsgParamViewerCount);
                        subObject.ViewerName = eRaid.RaidNotificaiton.DisplayName;

                        user = await GetUser(eRaid.RaidNotificaiton.UserId);
                    }
                    break;
                case General.ALERTTYPES.SUBGIFT:
                    {
                        OnGiftedSubscriptionArgs eGift = eventDetails as OnGiftedSubscriptionArgs;

                        subObject.RecipientName = eGift.GiftedSubscription.MsgParamRecipientDisplayName;
                        subObject.SetPlan(eGift.GiftedSubscription.MsgParamSubPlan);
                        subObject.TierName = eGift.GiftedSubscription.MsgParamSubPlanName;
                        subObject.ViewerName = eGift.GiftedSubscription.MsgParamRecipientDisplayName ?? General.ANONYMOUS_SUB_GIFT_NAME;

                        bool isAnonymousGift = (subObject.ViewerName == General.ANONYMOUS_SUB_GIFT_NAME);
                        bool communityGiftSub = communityGiftSubs.Exists(gift => gift.communityGifter.user.DisplayName == subObject.ViewerName);

                        user = isAnonymousGift ? null : await GetUser(await GetUserIdFromUsername(eGift.GiftedSubscription.DisplayName));
                        UpdateUserSubscription(await GetUser(eGift.GiftedSubscription.MsgParamRecipientId));

                        if (communityGiftSub)
                        {
                            // Have to use Exists above and Find now because Find returns an empty object if it doesn't Find anything.
                            comGift = communityGiftSubs.Find(gift => gift.communityGifter.user.DisplayName == subObject.ViewerName);
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
                        OnChannelSubscriptionArgs eSubscription = eventDetails as OnChannelSubscriptionArgs;

                        subObject.Amount = eSubscription.Subscription.Months;
                        subObject.Message = eSubscription.Subscription.SubMessage.Message ?? "";
                        subObject.RecipientName = eSubscription.Subscription.RecipientName ?? "";
                        subObject.SetPlan(eSubscription.Subscription.SubscriptionPlan);
                        subObject.TierName = eSubscription.Subscription.SubscriptionPlanName;
                        subObject.ViewerName = eSubscription.Subscription.Username ?? General.ANONYMOUS_SUB_GIFT_NAME;

                        UpdateUserSubscription(await GetUser(eSubscription.Subscription.Username));
                        user = await GetUser(eSubscription.Subscription.UserId);
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

                PrepareAndPlayAlert(alertToUse, user, subObject, alertType);
                MainTADAForm.mainTADAForm.webSocketServer.TellConnectedSockets(subObject);
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
                String alertTextMessage = alertText.GetMessage();
                String alertReadMessage = alertText.GetReadMessage();

                General.PerformSubstitutions(ref alertTextMessage, subObject);
                General.PerformSubstitutions(ref alertReadMessage, subObject);

                alertText.SetMessage(alertTextMessage);
                alertText.SetReadMessage(alertReadMessage);

                if (!String.IsNullOrEmpty(alertTextMessage) || !String.IsNullOrEmpty(alertReadMessage))
                {
                    General.LogFile(String.Format("{0}; {1}", alertTextMessage ?? "(nil)", alertReadMessage ?? "(nil)"));
                }
            }

            if (alertResponse != null)
            {
                String message = subObject.Message ?? "";

                if (!String.IsNullOrEmpty(message))
                {
                    // Remove blocked words.
                    // Optionally remove emote words.
                    // Optionally remove cheer words.
                }

                alertResponse.SetMessage(message);

                // Search through alertResponse to see if any waypoint has readMessage checked.
                var hasReadMessage = (alertResponse.Waypoints.Waypoints.Any(x => x.ReadMessage));

                if (hasReadMessage)
                {
                    // Then, make sure they aren't TTS'ing too much.
                    if (!MainTADAForm.configurationSettings.disallowSpammyTTS ||
                        DateTime.Now - MainTADAForm.configurationSettings.spammyTTSTime >= user.lastTTS)
                    {
                        alertResponse.SetReadMessage(message);
                        user.lastTTS = DateTime.Now;
                    }
                }
            }

            ListedAlert alert = new ListedAlert(subObject.ViewerName,
                                                alertText.GetMessage() ?? alertText.GetReadMessage() ?? "no message",
                                                General.alertTypes[alertType],
                                                alertImage,
                                                alertAudio,
                                                alertText,
                                                alertResponse);
            dg_Pending.Invoke((Action)(() => { pendingAlertBindingSource.Add(alert); }));
            MainTADAForm.mainTADAForm.AnimationWindow.Invoke((Action)(() => { MainTADAForm.mainTADAForm.AnimationWindow.PendingAlert(); }));
        }

        #region UserStuff
        // These "User" functions aren't part of the User class because it doesn't know anything about TwitchLib or those parts.
        private async Task<User> GetUser(String userId)
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
                    TwitchLib.Api.Helix.Models.Users.User twitchUser = (await twitchAPI.Helix.Users.GetUsersAsync(new List<string> { userId }, null, accessToken)).Users[0];
                    TwitchLib.Api.V5.Models.Subscriptions.Subscription twitchSubscription = await GetUserSubscription(userId);

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

        private async Task UpdateUserSubscription(String userId, bool forceUpdate = false)
        {
            User user = await GetUser(userId);

            if (forceUpdate || DateTime.Now.Subtract(user.subscriptionLastUpdated).Hours >= 2)
            {
                UpdateUserSubscription(user);
            }
        }

        private bool UndocumentedVIPRequest (String userId)
        {
            // Get a user's information from a channel with the following undocumented endpoint: https://api.twitch.tv/kraken/users/user_id/chat/channels/channel_id

            //twitchAPI.Undocumented.GetChatUserAsync(userId, channelId);

            bool retval = false;

            System.Net.Http.HttpClient twitchAPIRequest = new System.Net.Http.HttpClient();
            twitchAPIRequest.DefaultRequestHeaders.Add("Client-ID", General.CLIENT_ID);
            twitchAPIRequest.DefaultRequestHeaders.Add("Authorization", "Oauth " + twitchAPI.Settings.AccessToken);
            twitchAPIRequest.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");

            System.Net.Http.HttpResponseMessage twitchAPIResponse = twitchAPIRequest.GetAsync(new Uri(String.Format(@"https://api.twitch.tv/kraken/users/{0}/chat/channels/{1}", userId, this.channelID))).Result;

            if (twitchAPIResponse.IsSuccessStatusCode)
            {
                System.Net.Http.HttpContent content = twitchAPIResponse.Content;
                Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(content.ReadAsStringAsync().Result);
                
                if (jObject["badges"] != null)
                {
                    for (int c = 0; c < jObject["badges"].Count(); ++ c)
                    {
                        if ((String)jObject["badges"][c]["id"] == "vip")
                        {
                            retval = true;
                            break;
                        }
                    }
                }
            }
            
            return retval;
        }

        private void GetBadgesUndocumented (User user)
        {
            if (user != null)
            {
                System.Net.Http.HttpClient twitchAPIRequest = new System.Net.Http.HttpClient();
                twitchAPIRequest.DefaultRequestHeaders.Add("Client-ID", General.CLIENT_ID);
                twitchAPIRequest.DefaultRequestHeaders.Add("Authorization", "Oauth " + twitchAPI.Settings.AccessToken);
                twitchAPIRequest.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");

                System.Net.Http.HttpResponseMessage twitchAPIResponse = twitchAPIRequest.GetAsync(new Uri(String.Format(@"https://api.twitch.tv/kraken/users/{0}/chat/channels/{1}", user.UserID, this.channelID))).Result;

                if (twitchAPIResponse.IsSuccessStatusCode)
                {
                    System.Net.Http.HttpContent content = twitchAPIResponse.Content;
                    Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(content.ReadAsStringAsync().Result);

                    if (jObject["badges"] != null)
                    {
                        for (int c = 0; c < jObject["badges"].Count(); ++c)
                        {
                            switch ((String)jObject["badges"][c]["id"])
                            {
                                case "admin":
                                    user.badges.admin = true;
                                    break;
                                case "bits":
                                    user.badges.bits = true;
                                    user.badges.bit_badge = Convert.ToUInt32((String)jObject["badges"][c]["version"]);
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
                                    user.badges.subscribe_months = Convert.ToUInt32((String)jObject["badges"][c]["version"]);
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

        private async Task<TwitchLib.Api.V5.Models.Subscriptions.Subscription> GetUserSubscription(String userId)
        {
            TwitchLib.Api.V5.Models.Subscriptions.Subscription subscription = null;

            // Returns null if not subscribed.
            try
            {
                subscription = await twitchAPI.V5.Channels.CheckChannelSubscriptionByUserAsync(channelID, userId, twitchAPI.Settings.AccessToken);
            }
            catch (TwitchLib.Api.Core.Exceptions.BadResourceException)
            {
                // This is the current way of figuring out that there's no subscription right now.  Thanks, Twitch.
            }

            return subscription;
        }

        private async Task<string> GetUserIdFromUsername(String username)
        {
            return (await twitchAPI.Helix.Users.GetUsersAsync(null, new List<String> { username }, accessToken)).Users[0].Id;
        }

        private void AddSubscriptionDetailsToSubstitutionObject(User user, SubstitutionObject subObject)
        {
            if (user.subscription?.SubPlan != null)
            {
                subObject.SetPlan(TierAndPlanConverter.StringToSubscriptionPlan(user.subscription?.SubPlan));
                subObject.TierName = user.subscription?.SubPlanName;
            }
        }

        private TADAAlert FindSuitableAlert(General.ALERTTYPES alertType, User user, SubstitutionObject subObject)
        {
            TADAAlert returnedAlert = null;
            List<TADAAlert> potentialAlerts = new List<TADAAlert>();

            int alertScore = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;

            String alertTypeString = General.alertTypes[alertType];
            String viewerName = subObject.ViewerName;
            int amount = subObject.Amount;
            String message = subObject.Message;
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

        private void CheckBasicBadge(bool condition, bool userState, ref int score, ref bool continueTesting)
        {
            if (continueTesting)
            {
                score = (condition == userState) ? score + (int)General.SCORES.SCORE_BADGE_MATCHED : (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;
                continueTesting = (condition == userState);
            }
        }

        private int ScoreAlert (TADAAlert alert, User user, int amount, String message, TierAndPlanConverter.SUBSCRIPTION_PLANS plan)
        {
            int finalScore = (int)General.SCORES.SCORE_ABSOLUTE_MINIMUM;

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
                        CheckBasicBadge(condition.IsAdmin, user.badges.admin, ref score, ref continueTesting);
                        CheckBasicBadge(condition.IsPrime, user.badges.premium, ref score, ref continueTesting);
                        CheckBasicBadge(condition.IsStaff, user.badges.staff, ref score, ref continueTesting);
                        CheckBasicBadge(condition.IsTurbo, user.badges.turbo, ref score, ref continueTesting);

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

                    // Then check usernames, keywords, and amount.
                    if (continueTesting && !String.IsNullOrEmpty(condition.Usernames))
                    {
                        String username = user.user?.DisplayName.ToLower() ?? "anonymous";

                        List<String> names = condition.Usernames.Split(',').Select(n => n.Trim().ToLower()).ToList<String>();
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

                    if (continueTesting && !String.IsNullOrEmpty(condition.Keywords))
                    {
                        bool matched = false;

                        List<String> keywords = condition.Keywords.Split(',').Select(n => n.Trim().ToLower()).ToList<String>();

                        foreach (String key in keywords)
                        {
                            String keyPattern = String.Format("\\b{0}\\b", key); // Word boundary, test word/phrase, word boundary.  Will not play nicely with punctuation because regex.
                            if (System.Text.RegularExpressions.Regex.Match(message, keyPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase) != null)
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
            dg_Played.Invoke((Action)(() => { playedAlertBindingSource.Add(alertInTransition); }));

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
        public TwitchLib.Api.Helix.Models.Users.User user;
        public TwitchLib.Api.V5.Models.Subscriptions.Subscription subscription;
        public DateTime subscriptionLastUpdated;    // Cache subscription information for a while.  One hour?  Two hours?
        public DateTime lastSpoke;
        public DateTime lastTTS;
        public bool firstMessage;                   // Only gets set if the user -has- a first message of the day and if it triggers.
        public Badges badges = new Badges();        // The badges a user has.

        public String UserID
        {
            get { return user?.Id; }
        }

        public TierAndPlanConverter.SUBSCRIPTION_TIERS SubscriptionTier
        {
            get { return TierAndPlanConverter.SubscriptionTierFromPlan(TierAndPlanConverter.StringToSubscriptionPlan(subscription?.SubPlan)); }
        }

        public String SubscriptionTierName
        {
            get { return subscription?.SubPlanName ?? "None"; }
        }

        public void UpdateLastSpoke()
        {
            this.lastSpoke = DateTime.Now;
        }

        public void UpdateSubscription(TwitchLib.Api.V5.Models.Subscriptions.Subscription subscription)
        {
            this.subscription = subscription;
            this.subscriptionLastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} {2}", user?.DisplayName, user?.Id, subscription?.SubPlan);
        }
    }

    public class CommunityGiftSub
    {
        public User communityGifter;
        public int numberOfSubs;
        public List<String> giftees;
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
            this.giftees = new List<String>();
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
                return String.Join(" and ", giftees);
            }
            else
            {
                return String.Format("{0} and {1}", String.Join(", ", giftees.GetRange(0, giftees.Count - 1)), giftees.Last());
            }
        }
    }

    public class ListedAlert
    {
        [DisplayName("Viewer Name")]
        public String ViewerName { get; set; }

        [DisplayName("Alert Message")]
        public String AlertMessage { get; set; }

        [DisplayName("Alert Type")]
        public String AlertType { get; set; }

        internal TADAAlertImage alertImage = null;
        internal TADAAlertAudio alertAudio = null;
        internal TADAAlertText alertStreamerText = null;
        internal TADAAlertText alertViewerText = null;

        internal ListedAlert(String viewerName, String alertMessage, String alertType, TADAAlertImage alertImage, TADAAlertAudio alertAudio, TADAAlertText alertStreamerText, TADAAlertText alertViewerText)
        {
            this.ViewerName = viewerName;
            this.AlertMessage = alertMessage;
            this.AlertType = alertType;
            this.alertImage = alertImage;
            this.alertAudio = alertAudio;
            this.alertStreamerText = alertStreamerText;
            this.alertViewerText = alertViewerText;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} : {2}", this.AlertType, this.ViewerName, this.AlertMessage);
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
