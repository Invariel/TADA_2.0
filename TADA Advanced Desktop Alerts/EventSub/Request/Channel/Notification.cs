using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel
{
    public class Notification : Subscription<NotificationCondition>
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "channel.chat.notification";

        [JsonPropertyName("version")]
        public string Version { get; } = "1";

        [JsonPropertyName("condition")]
        public NotificationCondition Condition { get; set; }

        [JsonPropertyName("transport")]
        public SubscriptionTransport Transport { get; } = new SubscriptionTransport();

        public Notification (string broadcasterId, string sessionId)
        {
            Condition = new NotificationCondition() { BroadcasterUserId = broadcasterId, UserId = broadcasterId };
            Transport.SessionId = sessionId;
        }
    }

    public class NotificationCondition
    {
        [JsonPropertyName("broadcaster_user_id")]
        public string BroadcasterUserId { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
