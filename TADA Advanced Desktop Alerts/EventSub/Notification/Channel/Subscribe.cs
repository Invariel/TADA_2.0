using System.Text.Json.Serialization;
using R_Subscribe = TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel.SubscribeCondition;


namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel
{
    public record Subscribe (
        [property: JsonPropertyName("payload")] SubscribePayload Payload
    );

    public record SubscribePayload (
        [property: JsonPropertyName("subscription")] Subscription<R_Subscribe> Subscription,
        [property: JsonPropertyName("event")] SubscribeEvent Event
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId">The user ID for the user who subscribed to the specified channel.</param>
    /// <param name="UserLogin">The user login for the user who subscribed to the specified channel.</param>
    /// <param name="UserName">The user display name for the user who subscribed to the specified channel.</param>
    /// <param name="BroadcasterUserId">The requested broadcaster ID.</param>
    /// <param name="BroadcasterUserLogin">The requested broadcaster login.</param>
    /// <param name="BroadcasterUserName">The requested broadcaster display name.</param>
    /// <param name="Tier">The tier of the subscription. Valid values are 1000, 2000, and 3000.</param>
    /// <param name="IsGift">Whether the subscription is a gift.</param>
    public record SubscribeEvent (
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("user_login")] string UserLogin,
        [property: JsonPropertyName("user_name")] string UserName,
        [property: JsonPropertyName("broadcaster_user_id")] string BroadcasterUserId,
        [property: JsonPropertyName("broadcaster_user_login")] string BroadcasterUserLogin,
        [property: JsonPropertyName("broadcaster_user_name")] string BroadcasterUserName,
        [property: JsonPropertyName("tier")] string Tier,
        [property: JsonPropertyName("is_gift")] bool IsGift
    );
}
