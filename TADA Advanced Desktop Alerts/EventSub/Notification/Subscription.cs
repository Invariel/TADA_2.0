using System;
using System.Text.Json.Serialization;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification
{
    /// <summary>
    /// The fields necessary to create an EventSub WebSocket Subscription to Twitch.
    /// </summary>
    /// <typeparam name="TCondition">The Condition parameters for the websocket message</typeparam>
    /// <param name="Id">Your client ID.</param>
    /// <param name="Type">The notification’s subscription type.</param>
    /// <param name="Version">The version of the subscription.</param>
    /// <param name="Status">The status of the subscription.</param>
    /// <param name="Cost">How much the subscription counts against your limit. See Subscription Limits (https://dev.twitch.tv/docs/eventsub/manage-subscriptions/#subscription-limits) for more information.</param>
    /// <param name="Condition">Subscription-specific parameters.</param>
    /// <param name="Transport">Transport information.</param>
    /// <param name="CreatedAt">The time the notification was created.</param>
    public record Subscription<TCondition> (
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("type")] string @Type,
        [property: JsonPropertyName("version")] string Version,
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("cost")] int Cost,
        [property: JsonPropertyName("condition")] TCondition Condition,
        [property: JsonPropertyName("transport")] Transport Transport,
        [property: JsonPropertyName("created_at")] DateTimeOffset CreatedAt
    );

    public record Transport (
        [property: JsonPropertyName("method")] string Method,
        [property: JsonPropertyName("session_id")] string SessionId
    );
}
