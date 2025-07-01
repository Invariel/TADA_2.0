using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TADA_Advanced_Desktop_Alerts.EventSub.Notification;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection;

using ChannelNotification = TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel;

namespace TADA_Advanced_Desktop_Alerts.EventSub
{
    public class TADAEventSub
    {
        private readonly ClientWebSocket _clientWebSocket;
        private readonly string _broadcasterId;
        private readonly string _oauthToken;
        private string _sessionId;
        private string _clientId;

        private readonly HttpClient _httpClient;

        public event EventHandler<ChannelNotification.BitsUse> OnBitsUseReceived;
        public event EventHandler<ChannelNotification.ChannelPointsAutomaticRewardRedemptionAddV2> OnChannelPointsAutomaticRewardRedemptionAddV2Received;
        public event EventHandler<ChannelNotification.ChannelPointsCustomRewardRedemptionAdd> OnChannelPointsCustomRewardRedemptionAddReceived;
        public event EventHandler<ChannelNotification.ChatMessage> OnChatMessageReceived;
        public event EventHandler<ChannelNotification.Follow> OnFollowReceived;
        public event EventHandler<ChannelNotification.Notification> OnChatNotificationReceived;
        public event EventHandler<ChannelNotification.Subscribe> OnSubscribeReceived;
        public event EventHandler<ChannelNotification.SubscriptionGift> OnSubscriptionGiftReceived;
        public event EventHandler<ChannelNotification.SubscriptionMessage> OnSubscriptionMessageReceived;

        public virtual void BitsUsedReceived(ChannelNotification.BitsUse bitsUse) => OnBitsUseReceived?.Invoke(this, bitsUse);
        public virtual void ChannelPointsAutomaticRewardRedemptionAddV2Received(ChannelNotification.ChannelPointsAutomaticRewardRedemptionAddV2 channelPointsAutomaticRewardRedemptionAddV2) => OnChannelPointsAutomaticRewardRedemptionAddV2Received?.Invoke(this, channelPointsAutomaticRewardRedemptionAddV2);
        public virtual void ChannelPointsCustomRewardRedemptionAddReceived(ChannelNotification.ChannelPointsCustomRewardRedemptionAdd channelPointsCustomRewardRedemptionAdd) => OnChannelPointsCustomRewardRedemptionAddReceived?.Invoke(this, channelPointsCustomRewardRedemptionAdd);
        public virtual void ChatMessageReceived(ChannelNotification.ChatMessage chatMessage) => OnChatMessageReceived?.Invoke(this, chatMessage);
        public virtual void ChatNotificationReceived(ChannelNotification.Notification notificationMessage) => OnChatNotificationReceived?.Invoke(this, notificationMessage);
        public virtual void FollowReceived(ChannelNotification.Follow follow) => OnFollowReceived?.Invoke(this, follow);
        public virtual void SubscribeReceived(ChannelNotification.Subscribe subscribe) => OnSubscribeReceived?.Invoke(this, subscribe);
        public virtual void SubscriptionGiftReceived(ChannelNotification.SubscriptionGift subscriptionGift) => OnSubscriptionGiftReceived?.Invoke(this, subscriptionGift);
        public virtual void SubscriptionMessageReceived(ChannelNotification.SubscriptionMessage subscriptionMessage) => OnSubscriptionMessageReceived?.Invoke(this, subscriptionMessage);


        public TADAEventSub (string broadcasterId, string oauthToken, string clientId)
        {
            MainTADAForm.mainTADAForm.eventSub = this;

            _clientWebSocket = new ClientWebSocket();
            _clientWebSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(10); // Use unsolicited PONG messages to keep the connection alive.

            _broadcasterId = broadcasterId;
            _oauthToken = oauthToken;
            _clientId = clientId;

            _httpClient = new HttpClient();
        }

        public async Task Connect ()
        {
            byte[] byteArray = new byte[8192];
            ArraySegment<byte> byteArraySegment = new ArraySegment<byte>(byteArray);

            await _clientWebSocket.ConnectAsync(new Uri ("wss://eventsub.wss.twitch.tv/ws"), default);

            WebSocketReceiveResult connectionResponse;
            string receivedString = string.Empty;

            do
            {
                connectionResponse = await _clientWebSocket.ReceiveAsync(byteArray, default);
                var tempString = Encoding.UTF8.GetString(byteArray);
                receivedString += tempString.Substring(0, connectionResponse.Count);
            } while (!connectionResponse.EndOfMessage);

            // Then receivedBytes should not be empty, and should contain a JSON blob of data.
            var welcomeResponse = JsonSerializer.Deserialize<Welcome>(receivedString);
            _sessionId = welcomeResponse.Payload.Session.Id;

            General.LogFile("Connected via EventSub.", "twitchClient.txt");
        }

        public void Disconnect ()
            => _ = _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing.", default);

        public async Task RegisterForEvents ()
        {
            HttpStatusCode response;

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _oauthToken);
            _httpClient.DefaultRequestHeaders.Add("Client-Id", _clientId);
            _httpClient.BaseAddress = new Uri("https://api.twitch.tv/helix/eventsub/subscriptions");

            response = await Register(new Request.Channel.BitsUse(_broadcasterId, _sessionId));
            General.LogFile($"Registered for Bits [{response}]", "twitchChannel.txt");

            response = await Register(new Request.Channel.ChannelPointsAutomaticRewardRedemptionAddV2(_broadcasterId, _sessionId));
            General.LogFile($"Registered for Channel Points Automatic [{response}]", "twitchChannel.txt");

            response = await Register(new Request.Channel.ChannelPointsCustomRewardRedemptionAdd(_broadcasterId, _sessionId));
            General.LogFile($"Registered for Channel Points Custom [{response}]", "twitchChannel.txt");

            response = await Register(new Request.Channel.ChatMessage(_broadcasterId, _sessionId));
            General.LogFile($"Registered for Chat Messages [{response}]", "twitchChannel.txt");

            response = await Register(new Request.Channel.Follow(_broadcasterId, _sessionId));
            General.LogFile($"Registered for Follows [{response}]", "twitchChannel.txt");

            response = await Register(new Request.Channel.Subscribe(_broadcasterId, _sessionId));
            General.LogFile($"Registered for Subscriptions [{response}]", "twitchChannel.txt");

            response = await Register(new Request.Channel.SubscriptionGift(_broadcasterId, _sessionId));
            General.LogFile($"Registered for Gift Subscriptions [{response}]", "twitchChannel.txt");

            response = await Register(new Request.Channel.SubscriptionMessage(_broadcasterId, _sessionId));
            General.LogFile($"Registered for Resubscriptions [{response}]", "twitchChannel.txt");

            response = await Register(new Request.Channel.Notification(_broadcasterId, _sessionId));
            General.LogFile($"Registered for Subscriptions, resubscriptions, gift subscriptions, subscription upgrades, and raids. [{response}]", "twitchChannel.txt");
        }

        public async Task<HttpStatusCode> Register(object message)
        {
            HttpRequestMessage requestMessage = new (HttpMethod.Post, string.Empty);
            requestMessage.Content = new StringContent(JsonSerializer.Serialize(message));
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.SendAsync(requestMessage);
            return response.StatusCode;
        }

        public async Task Listen()
        {
            WebSocketReceiveResult connectionResponse;
            byte[] byteArray = new byte[8192];
            string receivedString = string.Empty;

            do
            {
                do
                {
                    connectionResponse = await _clientWebSocket.ReceiveAsync(byteArray, default);
                    var tempString = Encoding.UTF8.GetString(byteArray);
                    receivedString += tempString.Substring(0, connectionResponse.Count);
                } while (!connectionResponse.EndOfMessage);

                EventNotification eventNotification = JsonSerializer.Deserialize<EventNotification>(receivedString);

                switch (eventNotification?.Metadata?.SubscriptionType)
                {
                    case "channel.bits.use":
                        BitsUsedReceived(Deserialize<ChannelNotification.BitsUse>(receivedString));
                        break;
                    case "channel.channel_points_automatic_reward_redemption.add":
                        ChannelPointsAutomaticRewardRedemptionAddV2Received(Deserialize<ChannelNotification.ChannelPointsAutomaticRewardRedemptionAddV2>(receivedString));
                        break;
                    case "channel.channel_points_custom_reward_redemption.add":
                        ChannelPointsCustomRewardRedemptionAddReceived(Deserialize<ChannelNotification.ChannelPointsCustomRewardRedemptionAdd>(receivedString));
                        break;
                    case "channel.chat.message":
                        var chatMessage = Deserialize<ChannelNotification.ChatMessage>(receivedString);
                        ChatMessageReceived(chatMessage);
                        break;
                    case "channel.follow":
                        FollowReceived(Deserialize<ChannelNotification.Follow>(receivedString));
                        break;
                    case "channel.subscribe":
                        SubscribeReceived(Deserialize<ChannelNotification.Subscribe>(receivedString));
                        break;
                    case "channel.subscription.gift":
                        SubscriptionGiftReceived(Deserialize<ChannelNotification.SubscriptionGift>(receivedString));
                        break;
                    case "channel.subscription.message":
                        SubscriptionMessageReceived(Deserialize<ChannelNotification.SubscriptionMessage>(receivedString));
                        break;
                    case "channel.chat.notification":
                        ChatNotificationReceived(Deserialize<ChannelNotification.Notification>(receivedString));
                        break;
                }

                receivedString = string.Empty;
            } while (true);
        }

        public T Deserialize<T>(string message) => JsonSerializer.Deserialize<T>(message);
    }
}
