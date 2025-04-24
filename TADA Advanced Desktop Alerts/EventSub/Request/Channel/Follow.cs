using System.Text.Json.Serialization;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel
{
    public class Follow : Subscription<FollowCondition>
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "channel.follow";

        [JsonPropertyName("version")]
        public string Version { get; } = "2";

        [JsonPropertyName("condition")]
        public FollowCondition Condition { get; set; }

        [JsonPropertyName("transport")]
        public SubscriptionTransport Transport { get; } = new SubscriptionTransport();

        public Follow (string broadcasterId, string sessionId)
        {
            Condition = new FollowCondition() { BroadcasterUserId = broadcasterId, ModeratorUserId = broadcasterId };
            Transport.SessionId = sessionId;
        }
    }

    public class FollowCondition
    {
        [JsonPropertyName ("broadcaster_user_id")]
        public string BroadcasterUserId { get; set; }

        [JsonPropertyName ("moderator_user_id")]
        public string ModeratorUserId { get; set; }
    }
}
