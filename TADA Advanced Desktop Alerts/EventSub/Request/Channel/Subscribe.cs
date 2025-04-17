using System.Text.Json.Serialization;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel
{
    public class Subscribe : Subscription<SubscribeCondition>
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "channel.subscribe";

        [JsonPropertyName("version")]
        public string Version { get; } = "1";

        [JsonPropertyName("condition")]
        public SubscribeCondition Condition { get; set; }

        [JsonPropertyName("transport")]
        public SubscriptionTransport Transport { get; } = new SubscriptionTransport();

        public Subscribe (string broadcasterId, string sessionId)
        {
            Condition = new SubscribeCondition() { BroadcasterUserId = broadcasterId };
            Transport.SessionId = sessionId;
        }
    }

    public class SubscribeCondition
    {
        [JsonPropertyName("broadcaster_user_id")]
        public string BroadcasterUserId { get; set; }
    }
}
