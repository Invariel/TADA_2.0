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
        const int ABSOLUTE_MINIMUM_SCORE = -10000000;

        private TwitchAPI twitchAPI;
        private TwitchPubSub pubSub;
        private TwitchClient twitchClient;

        private String accessToken;
        private String channelID;
        private String channelName;

        private Dictionary<String, List<String>> followers;
        private bool followerFileWritten = false;
        private String missingFollowers = "";

        private Dictionary<String, List<TADAAlert>> currentAlerts;

        private Dictionary<String, User> viewers = new Dictionary<String, User>();

        // Need a thing for storing community gift subs that are being compiled.
        private List<CommunityGiftSub> communityGiftSubs = new List<CommunityGiftSub>();

        private List<String> cheermotes = new List<String>();

        private Image redLight = Properties.Resources.circle_red;
        private Image greenLight = Properties.Resources.circle_green;

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
                    // Then copy them over, in the order selected
                    pendingAlertBindingSource.Add(playedAlertBindingSource[dg_Played.SelectedRows[0].Index]);

                    // and deletes them from dg_Played
                    playedAlertBindingSource.RemoveAt(dg_Played.SelectedRows[0].Index);
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
            channelID = result.Users[0].Id;

            pubSub = new TwitchPubSub();
            pubSub.OnBitsReceived += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.BITS, e);
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
                pubSub.ListenToBitsEvents(channelID);
                pubSub.ListenToCommerce(channelID);
                pubSub.ListenToFollows(channelID);
                pubSub.ListenToSubscriptions(channelID);
                pubSub.SendTopics(twitchAPI.Settings.AccessToken);

                this.Invoke((Action)(() =>
                {
                    this.pb_PubSub.Image = this.greenLight;
                    General.ConfigureToolTip(this.pb_PubSub, "Bits, Follows, Game Purchases, and Subscriptions are being tracked.");
                }));
            };


            // TwitchClient is required for hosts, raids, and reading messages because it reads IRC directly.
            twitchClient = new TwitchClient(null);
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

            currentAlerts = (MainTADAForm.mainTADAForm).snapshotAlerts();
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
            User user = null;

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
                case General.ALERTTYPES.BITS:
                    {
                        OnBitsReceivedArgs eBits = eventDetails as OnBitsReceivedArgs;

                        subObject.Amount = eBits.BitsUsed;
                        subObject.Message = eBits.ChatMessage;
                        subObject.ViewerName = eBits.Username;

                        user = await GetUser(eBits.UserId);
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
                        /*
                         * Entirely guessing, because thanks undocumented everything.
                         * But, the guess is that MassGiftCount is the number of gift subs for this event.
                         * Then, parcel up the next <count> subs --from this person-- and turn them into a mass alert.
                         * Which means grabbing gift sub events and applying them to an internal variable.
                         * When that variable has enough names, it's ready and so play it.
                         */

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
                        subObject.AlertType = General.alertTypes[anonymous ? General.ALERTTYPES.ANONYMOUS_COMMUNITY : General.ALERTTYPES.COMMUNITY];
                        subObject.Amount = giftCount;
                        subObject.Tier = (TwitchLib.PubSub.Enums.SubscriptionPlan)eCommunity.GiftedSubscription.MsgParamSubPlan;
                        subObject.ViewerName = anonymous ? General.ANONYMOUS_SUB_GIFT_NAME : user?.user.DisplayName ?? General.ANONYMOUS_SUB_GIFT_NAME;

                        CommunityGiftSub communityGiftSub = null;

                        if (anonymous && FindSuitableAlert(General.ALERTTYPES.ANONYMOUS_COMMUNITY, subObject) != null)
                        {
                            communityGiftSub = new CommunityGiftSub(subObject);
                            communityGiftSub.use_anonymous_alert = true;
                        }
                        else if (FindSuitableAlert (General.ALERTTYPES.COMMUNITY, subObject) != null)
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
                        OnMessageReceivedArgs eMessage = eventDetails as OnMessageReceivedArgs;

                        // If the message has bits in it, this should be handled by PubSub instead.
                        if (eMessage.ChatMessage.Bits > 0) return;

                        subObject.Message = eMessage.ChatMessage.Message;
                        subObject.ViewerName = eMessage.ChatMessage.Username;

                        var f = eMessage.ChatMessage.EmoteSet;

                        user = await GetUser(eMessage.ChatMessage.UserId);

                        // A FIRSTMESSAGE can't possibly come through Twitch channels.  It doesn't know.
                        if (alertType == General.ALERTTYPES.MESSAGE)
                        {
                            if (!user.firstMessage && FindSuitableAlert(General.ALERTTYPES.FIRSTMESSAGE, subObject) != null)
                            {
                                alertType = General.ALERTTYPES.FIRSTMESSAGE;
                            }
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
                case General.ALERTTYPES.SUBSCRIPTION:
                    {
                        // Thanks to anonymous sub gifting (and community gifting), this entire section needs to be reorganized and rewritten.
                        // Thanks, Twitch!
                        OnChannelSubscriptionArgs eSubscription = eventDetails as OnChannelSubscriptionArgs;

                        subObject.Amount = eSubscription.Subscription.Months;
                        subObject.Message = eSubscription.Subscription.SubMessage.Message ?? "";
                        subObject.RecipientName = eSubscription.Subscription.RecipientName ?? "";
                        subObject.Tier = eSubscription.Subscription.SubscriptionPlan;
                        subObject.TierName = eSubscription.Subscription.SubscriptionPlanName;
                        subObject.ViewerName = eSubscription.Subscription.Username ?? General.ANONYMOUS_SUB_GIFT_NAME;

                        switch (eSubscription.Subscription.Context)
                        {
                            case "sub":
                            case "resub":
                                UpdateUserSubscription(await GetUser(eSubscription.Subscription.Username));
                                user = await GetUser(eSubscription.Subscription.UserId);
                                break;
                            case "anonsubgift":
                            case "subgift":
                                bool anonymous = eSubscription.Subscription.Context.Equals("anonsubgift");
                                bool community = communityGiftSubs.Exists(gift => gift.communityGifter.user.DisplayName == subObject.ViewerName);

                                user = (anonymous ? null : await GetUser(eSubscription.Subscription.UserId));
                                UpdateUserSubscription(await GetUser(eSubscription.Subscription.RecipientName));

                                if (community)
                                {
                                    // Have to use Exists above and Find now because Find returns an empty object if it doesn't Find anything.
                                    CommunityGiftSub comGift = communityGiftSubs.Find(gift => gift.communityGifter.user.DisplayName == subObject.ViewerName);
                                    comGift.giftees.Add(eSubscription.Subscription.RecipientName);
                                    comGift.subObject.Tier = eSubscription.Subscription.SubscriptionPlan;   // Overwriting from the Client version.
                                    comGift.subObject.TierName = eSubscription.Subscription.SubscriptionPlanName;
                                    if (comGift.Ready())
                                    {
                                        // We know that there is a valid alert AND whether it's anonymous or not.
                                        alertType = comGift.use_anonymous_alert ? General.ALERTTYPES.ANONYMOUS_COMMUNITY : General.ALERTTYPES.COMMUNITY;
                                        subObject = comGift.subObject;
                                    }
                                    else
                                    {
                                        // The community gift sub hasn't been fully compiled yet, so exit out.
                                        return;
                                    }
                                }
                                else
                                {
                                    // It's potentially an anonymous sub gift.  If anonymous is set, see if there's an anonymous sub alert set up.
                                    alertType = (anonymous && FindSuitableAlert(General.ALERTTYPES.ANONYMOUS, subObject) != null) ? General.ALERTTYPES.ANONYMOUS : General.ALERTTYPES.SUBGIFT;
                                }

                                break;
                        }
                    }
                    break;
                default:
                    return;
            }

            if (user != null && DateTime.Now.Subtract(user.subscriptionLastUpdated).Hours >= 2)
            { 
                UpdateUserSubscription(user);
            }

            subObject.AlertType = General.alertTypes[alertType];

            TADAAlert alertToUse = (alertType == General.ALERTTYPES.NONE ? null : FindSuitableAlert(alertType, subObject));

            if (alertToUse != null)
            {
                if (alertType == General.ALERTTYPES.FIRSTMESSAGE)
                {
                    user.firstMessage = true;
                }
                else if (alertType == General.ALERTTYPES.MESSAGE)
                {
                    // Somehow mark that this user triggered the message at X time, and prevent it from playing again for some cooldown period.
                    // This may involve dropping <trigger message> from the message and checking again.
                }

                PrepareAndPlayAlert(alertToUse, subObject, alertType);
                MainTADAForm.mainTADAForm.webSocketServer.TellConnectedSockets(subObject);
            }
        }

        private void PrepareAndPlayAlert (TADAAlert alertToUse, SubstitutionObject subObject, General.ALERTTYPES alertType)
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
                String alertTextMessage = alertText.Message;
                String alertReadMessage = alertText.ReadMessage;

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

                    // Optionally remove cheer words.
                }

                alertResponse.SetMessage(message);
                alertResponse.SetReadMessage(message);
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

        private bool IsVIP (TwitchLib.Client.Models.UserState userState)
        {
            bool retval = false;
            foreach (KeyValuePair<String, String> kvp in userState.Badges)
            {
                if (kvp.Key.ToLower() == "vip")
                {
                    retval = true;
                    break;
                }
            }
            return retval;
        }
        #endregion

        private async Task<TwitchLib.Api.V5.Models.Subscriptions.Subscription> GetUserSubscription(String userId)
        {
            TwitchLib.Api.V5.Models.Subscriptions.Subscription subscription = null;

            try
            {
                subscription = await twitchAPI.V5.Channels.CheckChannelSubscriptionByUserAsync(channelID, userId, twitchAPI.Settings.AccessToken);
            }
            catch (/* TwitchLib.Api.Exceptions.BadResourceException */ Exception e)
            {
                // Then this 404'd or something and we don't have a subscribed user.
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
                subObject.Tier = General.stringToTwitchLibSubPlan[user.subscription.SubPlan];
                subObject.TierName = user.subscription.SubPlanName;
            }
        }

        private TADAAlert FindSuitableAlert(General.ALERTTYPES alertType, SubstitutionObject subObject)
        {
            TADAAlert returnedAlert = null;
            List<TADAAlert> potentialAlerts = new List<TADAAlert>();

            int alertScore = ABSOLUTE_MINIMUM_SCORE;

            String alertTypeString = General.alertTypes[alertType];
            String viewerName = subObject.ViewerName;
            int amount = subObject.Amount;
            String message = subObject.Message;
            String tier = General.TwitchLibSubPlanToString[subObject.Tier];

            if (currentAlerts.ContainsKey(alertTypeString))
            {
                foreach (TADAAlert alert in currentAlerts[alertTypeString])
                {
                    /*
                     * Loop through the appropriate alerts, and assign each one a score.
                     * name > tier > keyword > amount
                     */

                    int thisAlertScore = ScoreAlert(alert, viewerName, amount, message, tier);
                    if (thisAlertScore > alertScore)
                    {
                        potentialAlerts.Clear();
                        alertScore = thisAlertScore;
                    }

                    if (thisAlertScore == alertScore && alertScore != ABSOLUTE_MINIMUM_SCORE)
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

        private int ScoreAlert (TADAAlert alert, String username, int amount, String message, String tier)
        {
            int finalScore = ABSOLUTE_MINIMUM_SCORE;
            bool continueTesting = true;

            foreach (TADAAlertCondition condition in alert.Conditions)
            {
                int score = 0;
                // First, check that the tier matches.
                if (condition.NoTier || condition.TwitchPrime || condition.TierOne || condition.TierTwo || condition.TierThree)
                {
                    // If any of the "tier" checkboxes are checked, we have to match against them.
                    if ((condition.NoTier && tier == General.TwitchLibSubPlanToString[TwitchLib.PubSub.Enums.SubscriptionPlan.NotSet]) ||
                        (condition.TwitchPrime && tier == General.TwitchLibSubPlanToString[TwitchLib.PubSub.Enums.SubscriptionPlan.Prime]) ||
                        (condition.TierOne && tier == General.TwitchLibSubPlanToString[TwitchLib.PubSub.Enums.SubscriptionPlan.Tier1]) ||
                        (condition.TierTwo && tier == General.TwitchLibSubPlanToString[TwitchLib.PubSub.Enums.SubscriptionPlan.Tier2]) ||
                        (condition.TierThree && tier == General.TwitchLibSubPlanToString[TwitchLib.PubSub.Enums.SubscriptionPlan.Tier3]))
                    {
                        score += (int)General.SCORES.SCORE_TIER_MATCHED;
                    }
                    else
                    {
                        score = ABSOLUTE_MINIMUM_SCORE;
                        continueTesting = false;
                    }
                }

                if (continueTesting && !String.IsNullOrEmpty(condition.Usernames))
                {
                    username = username.ToLower();

                    List<String> names = condition.Usernames.Split(',').Select(n => n.Trim().ToLower()).ToList<String>();
                    if (names.Contains(username))
                    {
                        score += (int)General.SCORES.SCORE_NAME_MATCHED;
                    }
                    else
                    {
                        score = ABSOLUTE_MINIMUM_SCORE;
                        continueTesting = false;
                    }
                }

                if (continueTesting && (condition.Minimum > 0 || condition.Maximum > 0))
                {
                    if ((condition.Minimum == 0 || amount >= condition.Minimum) &&
                        (condition.Maximum == 0 || amount <= condition.Maximum))
                    {
                        score += (int)General.SCORES.SCORE_WITHIN_AMOUNT;
                    }
                    else
                    {
                        score = ABSOLUTE_MINIMUM_SCORE;
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
                        score = ABSOLUTE_MINIMUM_SCORE;
                        continueTesting = false;
                    }
                }

                if (continueTesting &&
                    !condition.NoTier &&
                    !condition.TwitchPrime &&
                    !condition.TierOne &&
                    !condition.TierTwo &&
                    !condition.TierThree &&
                    String.IsNullOrEmpty(condition.Usernames) &&
                    String.IsNullOrEmpty(condition.Keywords) &&
                    condition.Minimum == 0 &&
                    condition.Maximum == 0)
                {
                    score = (int)General.SCORES.SCORE_NO_CONDITIONS;
                }

                finalScore = Math.Max(finalScore, score);
            }

            return (alert.ConditionCount == 0 ? 0 : finalScore);
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
        public DateTime lastSpoke;                  // Is this necessary?  Maybe not...
        public bool firstMessage;                   // Only gets set if the user -has- a first message of the day and if it triggers.

        public String UserID()
        {
            return user?.Id;
        }

        public String SubscriptionTier()
        {
            return subscription?.SubPlan ?? "None";
        }

        public String SubscriptionTierName()
        {
            return subscription?.SubPlanName ?? "None";
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

        public TADAAlertImage alertImage = null;
        public TADAAlertAudio alertAudio = null;
        public TADAAlertText alertStreamerText = null;
        public TADAAlertText alertViewerText = null;

        public ListedAlert(String viewerName, String alertMessage, String alertType, TADAAlertImage alertImage, TADAAlertAudio alertAudio, TADAAlertText alertStreamerText, TADAAlertText alertViewerText)
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
}
