using System.Security.Permissions;
using System.Text.Json.Serialization;
using R_SubscriptionGift = TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel.SubscriptionGiftCondition;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel
{
    public record SubscriptionGift (
        [property: JsonPropertyName ("payload")] SubscriptionGiftPayload Payload
    );

    public record SubscriptionGiftPayload (
        [property: JsonPropertyName ("subscription")] Subscription<R_SubscriptionGift> Subscription,
        [property: JsonPropertyName ("event")] SubscriptionGiftEvent Event
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId">The user ID of the user who sent the subscription gift. Set to null if it was an anonymous subscription gift.</param>
    /// <param name="UserLogin">The user login of the user who sent the gift. Set to null if it was an anonymous subscription gift.</param>
    /// <param name="UserName">The user display name of the user who sent the gift. Set to null if it was an anonymous subscription gift.</param>
    /// <param name="BroadcasterUserId">The broadcaster user ID.</param>
    /// <param name="BroadcasterUserLogin">The broadcaster login.</param>
    /// <param name="BroadcasterUserName">The broadcaster display name.</param>
    /// <param name="Total">The number of subscriptions in the subscription gift.</param>
    /// <param name="Tier">The tier of subscriptions in the subscription gift.</param>
    /// <param name="CumulativeTotal">The number of subscriptions gifted by this user in the channel. This value is null for anonymous gifts or if the gifter has opted out of sharing this information.</param>
    /// <param name="IsAnonymous">Whether the subscription gift was anonymous.</param>
    public record SubscriptionGiftEvent (
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("user_login")] string UserLogin,
        [property: JsonPropertyName("user_name")] string UserName,
        [property: JsonPropertyName("broadcaster_user_id")] string BroadcasterUserId,
        [property: JsonPropertyName("broadcaster_user_login")] string BroadcasterUserLogin,
        [property: JsonPropertyName("broadcaster_user_name")] string BroadcasterUserName,
        [property: JsonPropertyName("total")] int Total,
        [property: JsonPropertyName("tier")] string Tier,
        [property: JsonPropertyName("cumulative_total")] int CumulativeTotal,
        [property: JsonPropertyName("is_anonymous")] bool IsAnonymous
    );
}
