using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private Dictionary<String, List<TADAAlert>> currentAlerts;

        private Dictionary<String, User> viewers = new Dictionary<String, User>();

        public Played_Alerts(String channelName, String accessToken)
        {
            InitializeComponent();

            this.accessToken = accessToken;
            this.channelName = channelName;

            twitchAPI = new TwitchAPI();
            twitchAPI.Settings.ClientId = General.CLIENT_ID;
            twitchAPI.Settings.AccessToken = accessToken;

            (dg_Pending.DataSource as BindingSource).AddingNew += PendingAlertAdded;
            Connect();
        }

        private async void Connect()
        {
            txt_Status.Text = "Trying to Connect()";

            var result = await twitchAPI.Users.helix.GetUsersAsync(null, new List<string> { channelName }, twitchAPI.Settings.AccessToken);
            channelID = result.Users[0].Id;

            txt_Status.Text += ", channelID = " + channelID.ToString();

            pubSub = new TwitchPubSub();
            pubSub.OnBitsReceived += (sender, e) => ReceivedTwitchEvent (General.ALERTTYPES.BITS, e);
            pubSub.OnChannelCommerceReceived += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.COMMERCE, e);
            pubSub.OnFollow += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.FOLLOW, e);
            pubSub.OnChannelSubscription += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.SUBSCRIPTION, e);
            pubSub.OnListenResponse += OnListenEvent;

            pubSub.OnPubSubServiceConnected += delegate
            {
                // Even if there are no events of the chosen type specified, listening to them is important in case one is added later.
                pubSub.ListenToBitsEvents(channelID);
                pubSub.ListenToCommerce(channelID);
                pubSub.ListenToFollows(channelID);
                pubSub.ListenToSubscriptions(channelID);
                pubSub.SendTopics(twitchAPI.Settings.AccessToken);

                if (txt_Status.InvokeRequired)
                {

                }
                else
                {
                    txt_Status.Text = "Connected to PubSub";
                }
            };

            // TwitchClient is required for hosts, raids, and reading messages because it reads IRC directly.
            twitchClient = new TwitchClient();
            twitchClient.Initialize(new TwitchLib.Client.Models.ConnectionCredentials(this.channelName, this.accessToken), this.channelName, '\0', '\0', true);

            twitchClient.OnBeingHosted += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.HOST, e);
            twitchClient.OnMessageReceived += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.MESSAGE, e);
            twitchClient.OnRaidNotification += (sender, e) => ReceivedTwitchEvent(General.ALERTTYPES.RAID, e);

            pubSub.Connect();
            twitchClient.Connect();

            currentAlerts = (MainTADAForm.mainTADAForm).snapshotAlerts();
        }

        private void OnListenEvent(object sender, OnListenResponseArgs l)
        {
            // Don't do anything just yet.  This is where the Grizzly Tracker adds the event that was received to the status bar.
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => { this.txt_Status.Text = "Connected for bits, commerce, follows, and subscriptions."; }));
            }
            else
            {
                txt_Status.Text = "Connected for bits, commerce, follows, and subscriptions.";
            }
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
                    /* This block needs a way to keep track of peoples' first messages of the day,
                     * as well as for each message when it was last triggered, so that people can't spam it.
                     * Also, because these are coming through separate sources, this could be a bits message
                     * or a subscribe message possibly?
                     */
                    OnMessageReceivedArgs eMessage = eventDetails as OnMessageReceivedArgs;

                    // If the message has bits in it, this should be handled by PubSub instead.
                    if (eMessage.ChatMessage.Bits > 0) return;

                    subObject.Message = eMessage.ChatMessage.Message;
                    subObject.ViewerName = eMessage.ChatMessage.Username;

                    user = await GetUser(eMessage.ChatMessage.UserId);
                    break;
                case General.ALERTTYPES.FOLLOW:
                    OnFollowArgs eFollow = eventDetails as OnFollowArgs;

                    subObject.ViewerName = eFollow.DisplayName;

                    user = await GetUser(eFollow.UserId);
                    break;
                case General.ALERTTYPES.MACRO:
                    // This doesn't do any setup.  The trick below is to pick the appropriate
                    // alert based on the keypress.  Or, better, it should be handled elsewhere.
                    return;
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

                    if (alertType == General.ALERTTYPES.SUBGIFT)
                    {
                        UpdateUserSubscription(await GetUser(eSubscription.Subscription.RecipientId));
                    }

                    user = await GetUser(eSubscription.Subscription.UserId);

                    break;
                default:
                    return;
            }

            if (DateTime.Now.Subtract(user.subscriptionLastUpdated).Hours >= 2)
            {
                UpdateUserSubscription(user);
            }

            if (alertType == General.ALERTTYPES.FIRSTMESSAGE)
            {
                if (user.firstMessage || FindSuitableAlert(General.ALERTTYPES.FIRSTMESSAGE, subObject) == null)
                {
                    alertType = General.ALERTTYPES.MESSAGE;
                }
            }

            DateTime rightNow = DateTime.Now;
            String filename = String.Format("{0}{1:D2}{2:D2}.{3}.txt", rightNow.Year, rightNow.Month, rightNow.Day, channelName);
            General.LogFile(General.alertTypes[alertType], filename);
            General.LogFile(user.ToString(), filename);
            General.LogFile(subObject.ToString(), filename);

            TADAAlert alertToUse = FindSuitableAlert(alertType, subObject);

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
            }

            General.LogFile((alertToUse == null ? "No alert found." : alertToUse.ToString()), filename);
            General.LogFile("", filename);

            return;

            //PrepareAndPlayAlert(alertToUse, subObject);
            /*
             * On receiving an event
             * - Figure out what type of event it actually is
             * - Fill a SubstitutionObject with relevant data
             * - Figure out which alert to play
             * - Play the alert
             */
        }

        private void PrepareAndPlayAlert (TADAAlert alertToUse, SubstitutionObject subObject)
        {
            // Now that an alert has been decided on, pick pieces, and then add it to the to-play list.
            TADAAlertImage alertImage;
            TADAAlertAudio alertAudio;
            TADAAlertText alertText;
            TADAAlertText alertResponse;

            alertImage = (alertToUse.ImageCount == 0 ? null : alertToUse.Images[General.rng.Next(alertToUse.ImageCount)]);
            alertAudio = (alertToUse.AudioCount == 0 ? null : alertToUse.Audios[General.rng.Next(alertToUse.AudioCount)]);
            alertText = (alertToUse.StreamerCount == 0 ? null : alertToUse.StreamerTexts[General.rng.Next(alertToUse.StreamerCount)]);
            alertResponse = (alertToUse.ViewerCount == 0 ? null : alertToUse.ViewerTexts[General.rng.Next(alertToUse.ViewerCount)]);

            String alertTextMessage = alertText.GetMessage();
            String alertReadMessage = alertText.GetReadMessage();

            General.PerformSubstitutions(ref alertTextMessage, subObject);
            General.PerformSubstitutions(ref alertReadMessage, subObject);

            alertText.SetMessage(alertTextMessage);
            alertText.SetReadMessage(alertReadMessage);

            PlayingAlert alertToPlay = new PlayingAlert(alertImage, alertAudio, alertText, alertResponse);
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
                    firstMessage = false
                };

                viewers.Add(userId, user);
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
            return await twitchAPI.Channels.v5.CheckChannelSubscriptionByUserAsync(channelID, userId, twitchAPI.Settings.AccessToken);
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
            // 
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

        private TADAAlert actualAlert;

        public TADAAlert GetAlert() { return this.actualAlert; }

        public ListedAlert(String viewerName, String alertMessage, String alertType, TADAAlert actualAlert)
        {
            this.ViewerName = viewerName;
            this.AlertMessage = alertMessage;
            this.AlertType = alertType;
            this.actualAlert = actualAlert;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} : {2}", this.AlertType, this.ViewerName, this.AlertMessage);
        }
    }
}
