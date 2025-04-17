using System;
using System.Text.Json.Serialization;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification
{
    public record EventNotification (
        [property: JsonPropertyName("metadata")] Metadata Metadata
    );

    public record Metadata (
        [property: JsonPropertyName("message_id")] string MessageId,
        [property: JsonPropertyName("message_type")] string MessageType,
        [property: JsonPropertyName("message_timestamp")] DateTimeOffset MessageTimestamp,
        [property: JsonPropertyName("subscription_type")] string SubscriptionType,
        [property: JsonPropertyName("subscription_version")] string SubscriptionVersion
    );
}
