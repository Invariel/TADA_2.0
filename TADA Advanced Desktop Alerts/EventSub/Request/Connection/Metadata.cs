using System;
using System.Text.Json.Serialization;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection
{
    public record Metadata (
        [property: JsonPropertyName ("message_id")] string MessageId,
        [property: JsonPropertyName ("message_type")] string MessageType,
        [property: JsonPropertyName ("message_timestamp")] string MessageTimestamp
    )
    {
        public DateTimeOffset dtMessageTimestamp {
            get => DateTimeOffset.TryParse(MessageTimestamp, out DateTimeOffset time) ? time : DateTimeOffset.MinValue;
        }
    }
}
