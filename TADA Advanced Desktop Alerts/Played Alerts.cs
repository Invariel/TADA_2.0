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
        const bool TWITCHAPI = true;
        const int ABSOLUTE_MINIMUM_SCORE = -10000000;

        private TwitchAPI twitchAPI;

        private String accessToken;
        private String channelID;
        private String channelName;

        TwitchPubSub pubSub;
        TwitchClient twitchClient;

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

            Connect();
        }

        private async void Connect()
        {
            var result = await twitchAPI.Users.helix.GetUsersAsync(null, new List<string> { channelName }, accessToken);
            channelID = result.Users[0].Id;

            pubSub = new TwitchPubSub();
            pubSub.OnBitsReceived += OnBitsReceivedEvent;
            pubSub.OnChannelCommerceReceived += OnCommerceReceivedEvent;
            pubSub.OnChannelSubscription += OnChannelSubscribeEvent;
            pubSub.OnFollow += OnFollowEvent;
            pubSub.OnListenResponse += OnListenEvent;

            pubSub.OnPubSubServiceConnected += delegate
            {
                // Even if there are no events of the chosen type specified, listening to them is important in case one is added later.
                pubSub.ListenToBitsEvents(channelID);
                pubSub.ListenToCommerce(channelID);
                pubSub.ListenToFollows(channelID);
                pubSub.ListenToSubscriptions(channelID);
            };

            // TwitchClient is required for hosts, raids, and reading messages because it reads IRC directly.
            twitchClient = new TwitchClient();
            twitchClient.Initialize(new TwitchLib.Client.Models.ConnectionCredentials(this.channelName, this.accessToken), this.channelName, '\0', '\0', true);

            twitchClient.OnBeingHosted += OnHostReceivedEvent;
            twitchClient.OnRaidNotification += OnRaidReceivedEvent;
            twitchClient.OnMessageReceived += OnMessageReceivedEvent;

            currentAlerts = (MainTADAForm.mainTADAForm).snapshotAlerts();
        }

        private void OnListenEvent(object sender, OnListenResponseArgs l)
        {
            // Don't do anything just yet.  This is where the Grizzly Tracker adds the event that was received to the status bar.
            txt_Status.Text = "Connected for bits, commerce, follows, and subscriptions.";
        }

        private async Task<TwitchLib.Api.Models.v5.Subscriptions.Subscription> GetUserSubscription(String userID)
        {
            return await twitchAPI.Users.v5.CheckUserSubscriptionByChannelAsync(userID, channelID, twitchAPI.Settings.AccessToken);
        }

        private void MergeSubscriptionWithSubstitutionObject(TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription, SubstitutionObject subObject)
        {
            if (subscription.SubPlan != null)
            {
                subObject.Tier = General.stringToTwitchLibSubPlan[subscription.SubPlan];
                subObject.TierName = subscription.SubPlanName;
            }
        }

        private async void OnBitsReceivedEvent(object sender, OnBitsReceivedArgs bits)
        {
            SubstitutionObject subObject = new SubstitutionObject()
            {
                Amount = bits.BitsUsed,
                ViewerName = bits.Username,
            };

            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(bits.UserId);
            MergeSubscriptionWithSubstitutionObject(subscription, subObject);

            // Now find a suitable bits alert.
        }

        private async void OnCommerceReceivedEvent(object sender, OnChannelCommerceReceivedArgs commerce)
        {
            SubstitutionObject subObject = new SubstitutionObject()
            {
                ViewerName = commerce.Username
            };

            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(commerce.UserId);
            MergeSubscriptionWithSubstitutionObject(subscription, subObject);
        }

        private void OnChannelSubscribeEvent(object sender, OnChannelSubscriptionArgs subscription)
        {
            SubstitutionObject subObject = new SubstitutionObject()
            {
                Amount = subscription.Subscription.Months,
                RecipientName = subscription.Subscription.RecipientDisplayName,
                Tier = subscription.Subscription.SubscriptionPlan,
                TierName = subscription.Subscription.SubscriptionPlanName,
                ViewerName = subscription.Subscription.Username
            };
        }

        private async void OnFollowEvent(object sender, OnFollowArgs follow)
        {
            SubstitutionObject subObject = new SubstitutionObject()
            {
                ViewerName = follow.Username
            };

            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(follow.UserId);
            MergeSubscriptionWithSubstitutionObject(subscription, subObject);
        }

        private async void OnHostReceivedEvent(object sender, OnBeingHostedArgs host)
        {
            SubstitutionObject subObject = new SubstitutionObject()
            {
                Amount = host.BeingHostedNotification.Viewers,
                ViewerName = host.BeingHostedNotification.HostedByChannel
            };

            var result = await twitchAPI.Users.helix.GetUsersAsync(null, new List<string> { host.BeingHostedNotification.HostedByChannel }, accessToken);

            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(result.Users[0].Id);
            MergeSubscriptionWithSubstitutionObject(subscription, subObject);

            //host.BeingHostedNotification.IsAutoHosted;

        }

        private async void OnRaidReceivedEvent(object sender, OnRaidNotificationArgs raid)
        {
            // raid.RaidNotificaiton.MsgParamDisplayName Name of the raider.
            // raid.RaidNotificaiton.MsgParamViewerCount Number of viewers in the raid.
            SubstitutionObject subObject = new SubstitutionObject()
            {
                Amount = Int32.Parse(raid.RaidNotificaiton.MsgParamViewerCount),
                ViewerName = raid.RaidNotificaiton.MsgParamDisplayName
            };

            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(raid.RaidNotificaiton.UserId);
            MergeSubscriptionWithSubstitutionObject(subscription, subObject);
        }

        private async void OnMessageReceivedEvent(object sender, OnMessageReceivedArgs message)
        {
            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(message.ChatMessage.UserId);
        }

        private TADAAlert FindSuitableAlert(String alertType, String username, int amount, String message, String tier)
        {
            TADAAlert returnedAlert = null;
            List<TADAAlert> potentialAlerts = new List<TADAAlert>();

            int alertScore = ABSOLUTE_MINIMUM_SCORE;

            foreach (TADAAlert alert in currentAlerts[alertType])
            {
                /*
                 * Loop through the appropriate alerts, and assign each one a score.
                 * name > tier > keyword > amount
                 */

                if (alert.AlertType == alertType)
                {
                    int thisAlertScore = ScoreAlert(alert, username, amount, message, tier);
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
    }

    public class User
    {
        // I might want to create a User object that stores all of the things: user ID, subscription status, time they said their first words of the day, 
        public TwitchLib.Api.Models.Helix.Users.GetUsers.User user;
        public TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription;
        public DateTime lastUpdated;
        public DateTime lastSpoke;

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
    }
}
