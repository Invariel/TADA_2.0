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
        const bool TWITCHAPI = false;

        private TwitchAPI twitchAPI;

        private String accessToken;
        private String channelID;
        private String channelName;

        // I might want to create a User object that stores all of the things: user ID, subscription status, time they said their first words of the day, 
        private Dictionary<String, User> viewers = new Dictionary<String, User>();

        TwitchPubSub pubSub;
        TwitchClient twitchClient;

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
# if TWITCHAPI
            var result = await twitchAPI.Users.helix.GetUsersAsync(null, new List<string> { channelName }, authToken);
            channelID = result.Users[0].Id;
# else
            HttpClient twitchAPIRequest = new HttpClient();
            twitchAPIRequest.DefaultRequestHeaders.Add("Client-ID", General.CLIENT_ID);
            HttpResponseMessage twitchAPIResponse = twitchAPIRequest.GetAsync(new Uri("https://api.twitch.tv/helix/users?login=" + channelName)).Result;

            if (twitchAPIResponse.IsSuccessStatusCode)
            {
                HttpContent content = twitchAPIResponse.Content;
                JsonTextReader responseReader = new JsonTextReader(new System.IO.StringReader(content.ReadAsStringAsync().Result));
                do
                {
                    responseReader.Read();
                } while (responseReader.TokenType != JsonToken.PropertyName || (String)responseReader.Value != "id");
                responseReader.Read();
                channelID = (String)responseReader.Value;
            }
# endif

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
                //pubSub.ListenToCommerce(channelID);
                pubSub.ListenToFollows(channelID);
                pubSub.ListenToSubscriptions(channelID);
            };

            // TwitchClient is required for hosts, raids, and reading messages because it reads IRC directly.
            twitchClient = new TwitchClient();
            twitchClient.Initialize(new TwitchLib.Client.Models.ConnectionCredentials(this.channelName, this.accessToken), this.channelName, '\0', '\0', true);

            twitchClient.OnBeingHosted += OnHostReceivedEvent;
            twitchClient.OnRaidNotification += OnRaidReceivedEvent;
            twitchClient.OnMessageReceived += OnMessageReceivedEvent;







            /*
            String capn = "109832196";

            var aSubscription = await GetUserSubscription(capn);
            Console.WriteLine("Capn is subscribed at: " + aSubscription.SubPlanName);

            // These are the calls from the example.
            //Checks subscription for a specific user and the channel specified.
            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await twitchAPI.Channels.v5.CheckChannelSubscriptionByUserAsync(channelID, capn, twitchAPI.Settings.AccessToken);

            //Gets a list of all the subscritions of the specified channel.
            List<TwitchLib.Api.Models.v5.Subscriptions.Subscription> allSubscriptions = await twitchAPI.Channels.v5.GetAllSubscribersAsync(channelID);

            //Get channels a specified user follows.
            TwitchLib.Api.Models.Helix.Users.GetUsersFollows.GetUsersFollowsResponse userFollows = await twitchAPI.Users.helix.GetUsersFollowsAsync(channelID);

            //Get Spedicified Channel Follows
            var channelFollowers = await twitchAPI.Channels.v5.GetChannelFollowersAsync(channelID);

            //Return bool if channel is online/offline.
            bool isStreaming = await twitchAPI.Streams.v5.BroadcasterOnlineAsync(channelID);

            //Update Channel Title/Game
            //await twitchAPI.Channels.v5.UpdateChannelAsync("channel_id", "New stream title", "Stronghold Crusader");

            */
        }

        private void OnListenEvent(object sender, OnListenResponseArgs l)
        {
            // Don't do anything just yet.  This is where the Grizzly Tracker adds the event that was received to the status bar.
            txt_Status.Text = "Connected for bits, commerce, follows, and subscriptions.";
        }

        // If TwitchLib.Api doesn't want to work, might have to replace this with a regular call.
        private async Task<TwitchLib.Api.Models.v5.Subscriptions.Subscription> GetUserSubscription(String userID)
        {
            var subTask = twitchAPI.Users.v5.CheckUserSubscriptionByChannelAsync(userID, channelID, twitchAPI.Settings.AccessToken);
            subTask.Start();
            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = subTask.Result;
            return subscription;
        }

        private async void OnBitsReceivedEvent(object sender, OnBitsReceivedArgs bits)
        {
            SubstitutionObject subObject = new SubstitutionObject()
            {
                Amount = bits.BitsUsed,
                ViewerName = bits.Username,
            };

            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(bits.UserId);
        }

        private async void OnCommerceReceivedEvent(object sender, OnChannelCommerceReceivedArgs commerce)
        {
            SubstitutionObject subObject = new SubstitutionObject()
            {
            };

            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(commerce.UserId);
        }

        private void OnChannelSubscribeEvent(object sender, OnChannelSubscriptionArgs subscription)
        {

        }

        private async void OnFollowEvent(object sender, OnFollowArgs follow)
        {

            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(follow.UserId);
        }

        private async void OnHostReceivedEvent(object sender, OnBeingHostedArgs host)
        {
            //host.BeingHostedNotification.HostedByChannel;
            //host.BeingHostedNotification.IsAutoHosted;

            // Have to first look up the username of the hoster, because this is a username.


            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(host.BeingHostedNotification.HostedByChannel);
        }

        private async void OnRaidReceivedEvent(object sender, OnRaidNotificationArgs raid)
        {
            // raid.RaidNotificaiton.MsgParamDisplayName Name of the raider.
            // raid.RaidNotificaiton.MsgParamViewerCount Number of viewers in the raid.

            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(raid.RaidNotificaiton.UserId);
        }

        private async void OnMessageReceivedEvent(object sender, OnMessageReceivedArgs message)
        {
            TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription = await GetUserSubscription(message.ChatMessage.UserId);
        }
    }

    public class User
    {
        // I might want to create a User object that stores all of the things: user ID, subscription status, time they said their first words of the day, 
        public TwitchLib.Api.Models.Helix.Users.GetUsers.User user;
        public TwitchLib.Api.Models.v5.Subscriptions.Subscription subscription;
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
