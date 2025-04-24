using System.Collections.Generic;
using System.Text.Json.Serialization;
using TwitchLib.EventSub.Core.Models.Subscriptions;
using R_SubscriptionMessage = TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel.SubscriptionMessageCondition;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel
{
    public record SubscriptionMessage(
        [property: JsonPropertyName("payload")] SubscriptionMessagePayload Payload
    );

    public record SubscriptionMessagePayload (
        [property: JsonPropertyName("subscription")] Subscription<R_SubscriptionMessage> Subscription,
        [property: JsonPropertyName("event")] SubscriptionMessageEvent Event
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId">The user ID of the user who sent a resubscription chat message.</param>
    /// <param name="UserLogin">The user login of the user who sent a resubscription chat message.</param>
    /// <param name="UserName">The user display name of the user who a resubscription chat message.</param>
    /// <param name="BroadcasterUserId">The broadcaster user ID.</param>
    /// <param name="BroadcasterUserLogin">The broadcaster login.</param>
    /// <param name="BroadcasterUserName">The broadcaster display name.</param>
    /// <param name="Tier">The tier of the user’s subscription.</param>
    /// <param name="Message">An object that contains the resubscription message and emote information needed to recreate the message.</param>
    /// <param name="CumulativeMonths">The total number of months the user has been subscribed to the channel.</param>
    /// <param name="StreakMonths">The number of consecutive months the user’s current subscription has been active. This value is null if the user has opted out of sharing this information.</param>
    /// <param name="DurationMonths">The month duration of the subscription.</param>
    public record SubscriptionMessageEvent (
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("user_login")] string UserLogin,
        [property: JsonPropertyName("user_name")] string UserName,
        [property: JsonPropertyName("broadcaster_user_id")] string BroadcasterUserId,
        [property: JsonPropertyName("broadcaster_user_login")] string BroadcasterUserLogin,
        [property: JsonPropertyName("broadcaster_user_name")] string BroadcasterUserName,
        [property: JsonPropertyName("tier")] string Tier,
        [property: JsonPropertyName("message")] SubscriptionMessageMessage Message,
        [property: JsonPropertyName("cumulative_months")] int CumulativeMonths,
        [property: JsonPropertyName("streak_months")] int StreakMonths,
        [property: JsonPropertyName("duration_months")] int DurationMonths
    );

    /// <summary>
    /// An object that contains the resubscription message and emote information needed to recreate the message.
    /// </summary>
    /// <param name="Text">The text of the resubscription chat message.</param>
    /// <param name="Emotes">An array that includes the emote ID and start and end positions for where the emote appears in the text.</param>
    public record SubscriptionMessageMessage (
        [property: JsonPropertyName("text")] string Text,
        [property: JsonPropertyName("emotes")] List<SubscriptionMessageEmote> Emotes
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Begin">The index of where the Emote starts in the text.</param>
    /// <param name="End">The index of where the Emote ends in the text.</param>
    /// <param name="Id">The emote ID.</param>
    public record SubscriptionMessageEmote (
        [property: JsonPropertyName("begin")] int Begin,
        [property: JsonPropertyName("end")] int End,
        [property: JsonPropertyName("id")] string Id
    );
}
