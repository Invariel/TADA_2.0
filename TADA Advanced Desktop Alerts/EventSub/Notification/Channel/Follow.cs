using System;
using System.Text.Json.Serialization;
using R_Follow = TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel.FollowCondition;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel
{
    public record Follow (
        [property: JsonPropertyName ("payload")] FollowPayload Payload
    );

    public record FollowPayload (
        [property: JsonPropertyName ("subscription")] Subscription<R_Follow> Subscription,
        [property: JsonPropertyName ("event")] FollowEvent Event
    );
    /// <summary>
    /// The parameters of a Channel Follow event.
    /// </summary>
    /// <param name="UserId">The user ID for the user now following the specified channel.</param>
    /// <param name="UserLogin">The user login for the user now following the specified channel.</param>
    /// <param name="UserName">The user display name for the user now following the specified channel.</param>
    /// <param name="BroadcasterUserId">The requested broadcaster ID.</param>
    /// <param name="BroadcasterUserLogin">The requested broadcaster login.</param>
    /// <param name="BroadcasterUserName">The requested broadcaster display name.</param>
    /// <param name="FollowedAt">RFC3339 timestamp of when the follow occurred.</param>
    public record FollowEvent(
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("user_login")] string UserLogin,
        [property: JsonPropertyName("user_name")] string UserName,
        [property: JsonPropertyName("broadcaster_user_id")] string BroadcasterUserId,
        [property: JsonPropertyName("broadcaster_user_login")] string BroadcasterUserLogin,
        [property: JsonPropertyName("broadcaster_user_name")] string BroadcasterUserName,
        [property: JsonPropertyName("followed_at")] DateTimeOffset FollowedAt
    );
}
