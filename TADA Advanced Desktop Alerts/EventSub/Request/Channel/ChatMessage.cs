using System.Text.Json.Serialization;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel
{
    public class ChatMessage : Subscription<ChatMessageCondition>
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "channel.chat.message";

        [JsonPropertyName("version")]
        public string Version { get; } = "1";

        [JsonPropertyName("condition")]
        public ChatMessageCondition Condition { get; set; }

        [JsonPropertyName("transport")]
        public SubscriptionTransport Transport { get; } = new SubscriptionTransport();

        public ChatMessage (string broadcasterId, string sessionId)
        {
            Condition = new ChatMessageCondition() { BroadcasterUserId = broadcasterId, UserId = broadcasterId };
            Transport.SessionId = sessionId;
        }
    }

    public class ChatMessageCondition
    {
        [JsonPropertyName ("broadcaster_user_id")]
        public string BroadcasterUserId { get; set; }

        [JsonPropertyName ("user_id")]
        public string UserId { get; set; }
    }
}
