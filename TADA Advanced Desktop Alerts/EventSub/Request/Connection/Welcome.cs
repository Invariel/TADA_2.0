using System;
using System.Text.Json.Serialization;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection
{
    public record Welcome (
        [property: JsonPropertyName ("metadata")] Metadata Metadata,
        [property: JsonPropertyName ("payload")] WelcomePayload Payload
    );

    public record WelcomePayload (
        [property: JsonPropertyName ("session")] WelcomeSession Session
    );

    public record WelcomeSession (
        [property: JsonPropertyName ("id")] string Id,
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("connected_at")] DateTimeOffset ConnectedAt,
        [property: JsonPropertyName("keepalive_timeout_seconds")] int KeepaliveTimeoutSeconds,
        [property: JsonPropertyName("reconnect_url")] string ReconnectUrl
    );
}
