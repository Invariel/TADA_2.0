using System.Text.Json.Serialization;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel
{
    public class BitsUse : Subscription<BitsUseCondition>
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "channel.bits.use";

        [JsonPropertyName("version")]
        public string Version { get; } = "1";

        [JsonPropertyName("condition")]
        public BitsUseCondition Condition { get; set; }

        [JsonPropertyName("transport")]
        public SubscriptionTransport Transport { get; } = new SubscriptionTransport();

        public BitsUse (string broadcasterId, string sessionId)
        {
            Condition = new BitsUseCondition() { BroadcasterUserId = broadcasterId };
            Transport.SessionId = sessionId;
        }
    }

    public class BitsUseCondition
    {
        [JsonPropertyName ("broadcaster_user_id")]
        public string BroadcasterUserId { get; set; }
    }
}
