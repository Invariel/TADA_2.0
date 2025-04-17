using System;
using System.Text.Json.Serialization;
using R_ChannelPointsCustomRewardRedemptionAdd = TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel.ChannelPointsCustomRewardRedemptionAddCondition;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel
{
    public record ChannelPointsCustomRewardRedemptionAdd(
        [property: JsonPropertyName("payload")] ChannelPointsCustomRewardRedemptionAddPayload Payload
    );

    public record ChannelPointsCustomRewardRedemptionAddPayload (
        [property: JsonPropertyName("subscription")] Subscription<R_ChannelPointsCustomRewardRedemptionAdd> Subscription,
        [property: JsonPropertyName("event")] ChannelPointsCustomRewardRedemptionAddEvent Event
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id">The redemption identifier.</param>
    /// <param name="BroadcasterUserId">The requested broadcaster ID.</param>
    /// <param name="BroadcasterUserLogin">The requested broadcaster login.</param>
    /// <param name="BroadcasterUserName">The requested broadcaster display name.</param>
    /// <param name="UserId">User ID of the user that redeemed the reward.</param>
    /// <param name="UserLogin">Login of the user that redeemed the reward.</param>
    /// <param name="UserName">Display name of the user that redeemed the reward.</param>
    /// <param name="UserInput">The user input provided. Empty string if not provided.</param>
    /// <param name="Status">Defaults to unfulfilled. Possible values are unknown, unfulfilled, fulfilled, and canceled.</param>
    /// <param name="Reward">Basic information about the reward that was redeemed, at the time it was redeemed.</param>
    /// <param name="RedeemedAt">RFC3339 timestamp of when the reward was redeemed.</param>
    public record ChannelPointsCustomRewardRedemptionAddEvent (
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("broadcaster_user_id")] string BroadcasterUserId,
        [property: JsonPropertyName("broadcaster_user_login")] string BroadcasterUserLogin,
        [property: JsonPropertyName("broadcaster_user_name")] string BroadcasterUserName,
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("user_login")] string UserLogin,
        [property: JsonPropertyName("user_name")] string UserName,
        [property: JsonPropertyName("user_input")] string UserInput,
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("reward")] ChannelPointsCustomRewardRedemptionAddReward Reward,
        [property: JsonPropertyName("redeemed_at")] DateTimeOffset RedeemedAt
    );

    /// <summary>
    /// Basic information about the reward that was redeemed, at the time it was redeemed.
    /// </summary>
    /// <param name="Id">The reward identifier.</param>
    /// <param name="Title">The reward name.</param>
    /// <param name="Cost">The reward cost.</param>
    /// <param name="Prompt">The reward description.</param>
    public record ChannelPointsCustomRewardRedemptionAddReward (
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("cost")] int Cost,
        [property: JsonPropertyName("prompt")] string Prompt
    );
}
