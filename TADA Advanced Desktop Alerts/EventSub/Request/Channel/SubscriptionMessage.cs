using System.Text.Json.Serialization;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel
{
    public class SubscriptionMessage : Subscription<SubscriptionMessageCondition>
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "channel.subscription.message";

        [JsonPropertyName("version")]
        public string Version { get; } = "1";

        [JsonPropertyName("condition")]
        public SubscriptionMessageCondition Condition { get; set; }

        [JsonPropertyName("transport")]
        public SubscriptionTransport Transport { get; } = new SubscriptionTransport();

        public SubscriptionMessage (string broadcasterId, string sessionId)
        {
            Condition = new SubscriptionMessageCondition() { BroadcasterUserId = broadcasterId };
            Transport.SessionId = sessionId;
        }
    }

    public class SubscriptionMessageCondition
    {
        [JsonPropertyName("broadcaster_user_id")]
        public string BroadcasterUserId { get; set; }
    }
}
