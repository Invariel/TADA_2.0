using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
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

        private String statusBar = "";

        private Dictionary<String, List<TADAAlert>> currentAlerts;

        private Dictionary<String, User> viewers = new Dictionary<String, User>();

        private List<String> cheermotes = new List<String>();

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
            MainTADAForm.mainTADAForm.AnimationWindow.nowPlaying.Stop();
        }

        private async void Connect()
        {
            var result = await twitchAPI.Users.helix.GetUsersAsync(null, new List<string> { channelName }, twitchAPI.Settings.AccessToken);
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
                    AppendToStatusBar("Connected for Bits, Game Purchases, Follows, Subscriptions.");
                }
            };
            // Handle these, usually with a reconnection attempt or seventy thousand.
            //pubSub.OnPubSubServiceClosed;
            //pubSub.OnPubSubServiceError;

            pubSub.OnPubSubServiceConnected += delegate
            {
                // Even if there are no events of the chosen type specified, listening to them is important in case one is added later.
                pubSub.ListenToBitsEvents(channelID);
                pubSub.ListenToCommerce(channelID);
                pubSub.ListenToFollows(channelID);
                pubSub.ListenToSubscriptions(channelID);
                pubSub.SendTopics(twitchAPI.Settings.AccessToken);

                AppendToStatusBar("Connected to PubSub.");
            };


            // TwitchClient is required for hosts, raids, and reading messages because it reads IRC directly.
            twitchClient = new TwitchClient(null);
            twitchClient.Initialize(new TwitchLib.Client.Models.ConnectionCredentials(this.channelName, this.accessToken), this.channelName, '\0', '\0', true);

            twitchClient.OnConnected += (sender, e) => { General.LogFile("TwitchClient claims to be connected.", "twitchClient.txt"); };
            twitchClient.OnConnectionError += (sender, e) => { General.LogFile(e.Error.Message, "twitchClient.txt"); };
            twitchClient.OnIncorrectLogin += (sender, e) => { General.LogFile(e.Exception.Message, "twitchClient.txt"); };
            twitchClient.OnJoinedChannel += (sender, e) => { General.LogFile("Joined channel " + e.Channel, "twitchClient.txt"); };

            twitchClient.OnBeingHosted += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.HOST, e);
            twitchClient.OnMessageReceived += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.MESSAGE, e);
            twitchClient.OnRaidNotification += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.RAID, e);

            GetAllFollowers();

            pubSub.Connect();
            twitchClient.Connect();

            TwitchLib.Api.Models.v5.Bits.Cheermotes requestedCheermotes = await twitchAPI.Bits.v5.GetCheermotesAsync(channelID);
            foreach (TwitchLib.Api.Models.v5.Bits.Action action in requestedCheermotes.Actions)
            {
                this.cheermotes.Add(action.Prefix.ToLower());
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
            TwitchLib.Api.Models.Helix.Users.GetUsersFollows.GetUsersFollowsResponse followers = await twitchAPI.Users.helix.GetUsersFollowsAsync(after, null, 100, null, channelID);
            if (followers.Follows.Length > 0)
            {
                List<String> followerList = new List<String>();
                foreach (TwitchLib.Api.Models.Helix.Users.GetUsersFollows.Follow follower in followers.Follows)
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

        private void AppendToStatusBar(String s)
        {
            this.statusBar += " " + s;

            // Don't do anything just yet.  This is where the Grizzly Tracker adds the event that was received to the status bar.
            // Have each component turn on (switch from red to green/blue/something not-red) its associated light.
            /*
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => { this.txt_Status.Text = "Connected for bits, commerce, follows, and subscriptions."; }));
            }
            else
            {
                txt_Status.Text = "Connected for bits, commerce, follows, and subscriptions.";
            }
            */
        }

        public async void ReceivedTwitchEvent(General.ALERTTYPES alertType, Object eventDetails)
        {
            SubstitutionObject subObject = new SubstitutionObject();
            User user;

            switch (alertType)
            {
                case General.ALERTTYPES.AUTOHOST:
                case General.ALERTTYPES.HOST:
                    OnBeingHostedArgs eHost = eventDetails as OnBeingHostedArgs;

                    subObject.Amount = eHost.BeingHostedNotification.Viewers;
                    subObject.ViewerName = eHost.BeingHostedNotification.HostedByChannel;

                    user = await GetUser(await GetUserIdFromUsername(eHost.BeingHostedNotification.HostedByChannel));

                    alertType = eHost.BeingHostedNotification.IsAutoHosted ? General.ALERTTYPES.AUTOHOST : General.ALERTTYPES.HOST;
                    break;
                case General.ALERTTYPES.BITS:
                    OnBitsReceivedArgs eBits = eventDetails as OnBitsReceivedArgs;

                    subObject.Amount = eBits.BitsUsed;
                    subObject.Message = eBits.ChatMessage;
                    subObject.ViewerName = eBits.Username;

                    user = await GetUser(eBits.UserId);
                    break;
                case General.ALERTTYPES.COMMERCE:
                    OnChannelCommerceReceivedArgs eCommerce = eventDetails as OnChannelCommerceReceivedArgs;

                    subObject.Message = eCommerce.PurchaseMessage;
                    subObject.ViewerName = eCommerce.Username;

                    user = await GetUser(eCommerce.UserId);
                    break;
                case General.ALERTTYPES.FIRSTMESSAGE:
                case General.ALERTTYPES.MESSAGE:
                    OnMessageReceivedArgs eMessage = eventDetails as OnMessageReceivedArgs;

                    // If the message has bits in it, this should be handled by PubSub instead.
                    if (eMessage.ChatMessage.Bits > 0) return;

                    subObject.Message = eMessage.ChatMessage.Message;
                    subObject.ViewerName = eMessage.ChatMessage.Username;

                    var f = eMessage.ChatMessage.EmoteSet;

                    user = await GetUser(eMessage.ChatMessage.UserId);
                    break;
                case General.ALERTTYPES.FOLLOW:
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

                    break;
                /*
                                case General.ALERTTYPES.MACRO:
                                    // This doesn't do any setup.  The trick below is to pick the appropriate
                                    // alert based on the keypress.  Or, better, it should be handled elsewhere.
                                    return;
                */
                case General.ALERTTYPES.RAID:
                    OnRaidNotificationArgs eRaid = eventDetails as OnRaidNotificationArgs;

                    subObject.Amount = Int32.Parse(eRaid.RaidNotificaiton.MsgParamViewerCount);
                    subObject.ViewerName = eRaid.RaidNotificaiton.DisplayName;

                    user = await GetUser(eRaid.RaidNotificaiton.UserId);
                    break;
                case General.ALERTTYPES.SUBGIFT:
                case General.ALERTTYPES.SUBSCRIPTION:
                    OnChannelSubscriptionArgs eSubscription = eventDetails as OnChannelSubscriptionArgs;

                    subObject.Amount = eSubscription.Subscription.Months;
                    subObject.Message = eSubscription.Subscription.SubMessage.Message;
                    subObject.RecipientName = eSubscription.Subscription.RecipientName;
                    subObject.ViewerName = eSubscription.Subscription.Username;

                    if (!String.IsNullOrEmpty(eSubscription.Subscription.RecipientName))
                    {
                        alertType = General.ALERTTYPES.SUBGIFT;
                    }

                    UpdateUserSubscription(await GetUser(alertType == General.ALERTTYPES.SUBGIFT ? eSubscription.Subscription.RecipientId : eSubscription.Subscription.UserId));

                    user = await GetUser(eSubscription.Subscription.UserId);

                    break;
                default:
                    return;
            }

            if (DateTime.Now.Subtract(user.subscriptionLastUpdated).Hours >= 2)
            {
                UpdateUserSubscription(user);
            }

            subObject.Tier = General.stringToTwitchLibSubPlan[user.SubscriptionTier()];
            subObject.TierName = user.SubscriptionTierName();

            // A FIRSTMESSAGE can't possibly come through Twitch channels.  It doesn't know.
            if (alertType == General.ALERTTYPES.MESSAGE)
            {
                if (!user.firstMessage && FindSuitableAlert(General.ALERTTYPES.FIRSTMESSAGE, subObject) != null)
                {
                    alertType = General.ALERTTYPES.FIRSTMESSAGE;
                }
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
            /*
             * On receiving an event
             * - Figure out what type of event it actually is
             * - Fill a SubstitutionObject with relevant data
             * - Figure out which alert to play
             * - Play the alert
             */
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
                TwitchLib.Api.Models.Helix.Users.GetUsers.User twitchUser = (await twitchAPI.Users.helix.GetUsersAsync(new List<String> { userId }, null, accessToken)).Users[0];
                TwitchLib.Api.Models.v5.Subscriptions.Subscription twitchSubscription = await GetUserSubscription(userId);

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
        #endregion

        private async Task<TwitchLib.Api.Models.v5.Subscriptions.Subscription> GetUserSubscription(String userId)
        {
            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = null;

            try
            {
                subscription = await twitchAPI.Channels.v5.CheckChannelSubscriptionByUserAsync(channelID, userId, twitchAPI.Settings.AccessToken);
            }
            catch (TwitchLib.Api.Exceptions.BadResourceException)
            {
                // Then this 404'd or something and we don't have a subscribed user.
            }

            return subscription;
        }

        private async Task<string> GetUserIdFromUsername(String username)
        {
            return (await twitchAPI.Users.helix.GetUsersAsync(null, new List<string> { username }, accessToken)).Users[0].Id;
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

                    if (alert.AlertType == alertTypeString)
                    {
                        int thisAlertScore = ScoreAlert(alert, viewerName, amount, message, tier);
                        if (thisAlertScore > alertScore)
                        {
                            potentialAlerts.Clear();
                            alertScore = thisAlertScore;
                        }

                        if (thisAlertScore == alertScore)
                        {
                            potentialAlerts.Add(alert);
                        }
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
            int score = 0;
            foreach (TADAAlertCondition condition in alert.Conditions)
            {
                if (!condition.NoTier)
                {
                    if (condition.TwitchPrime && tier == "Twitch Prime" ||
                        condition.TierOne && tier == "Tier One" ||
                        condition.TierTwo && tier == "Tier Two" ||
                        condition.TierThree && tier == "Tier Three")
                    {
                        score += (int)General.SCORES.SCORE_TIER_MATCHED;
                    }
                    else
                    {
                        // The tier was set and didn't match, so move onto the next condition.
                        score = 0;
                        continue;
                    }
                }

                if (!String.IsNullOrEmpty(condition.Usernames))
                {
                    username = username.ToLower();

                    List<String> names = condition.Usernames.Split(',').Select(n => n.Trim().ToLower()).ToList<String>();
                    if (names.Contains(username))
                    {
                        score += (int)General.SCORES.SCORE_NAME_MATCHED;
                    }
                    else
                    {
                        score = 0;
                        continue;
                    }
                }

                if (condition.Minimum > 0 || condition.Maximum > 0)
                {
                    if ((condition.Minimum == 0 || amount >= condition.Minimum) &&
                        (condition.Maximum == 0 || amount <= condition.Maximum))
                    {
                        score += (int)General.SCORES.SCORE_WITHIN_AMOUNT;
                    }
                    else
                    {
                        score = 0;
                        continue;
                    }
                }

                if (!String.IsNullOrEmpty(condition.Keywords))
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
                        score = 0;
                        continue;
                    }
                }

                if (condition.NoTier && String.IsNullOrEmpty(condition.Usernames) && String.IsNullOrEmpty(condition.Keywords) && condition.Minimum == 0 && condition.Maximum == 0)
                {
                    score = (int)General.SCORES.SCORE_NO_CONDITIONS;
                }
            }

            return score;
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
        public TwitchLib.Api.Models.Helix.Users.GetUsers.User user;
        public TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription;
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

        public void UpdateSubscription(TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription)
        {
            this.subscription = subscription;
            this.subscriptionLastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} {2}", user?.DisplayName, user?.Id, subscription?.SubPlan);
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
