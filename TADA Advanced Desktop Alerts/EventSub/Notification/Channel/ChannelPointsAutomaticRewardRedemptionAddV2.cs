using System;
using System.Text.Json.Serialization;
using R_ChannelPointsAutomaticRewardRedemptionAddV2 = TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel.ChannelPointsAutomaticaRewardRedemptionAddV2Condition;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel
{
    public record ChannelPointsAutomaticRewardRedemptionAddV2 (
        [property: JsonPropertyName("payload")] ChannelPointsAutomaticRewardRedemptionAddV2Payload Payload
    );

#nullable enable
    /// <summary>
    /// This object is only for the <b>default</b> channel rewards, "Bypass Sub Mode", "Send Highlighted Message",
    /// "Random Sub Emote Unlock", "Chosen Sub Emote Unlock", and "Chosen Modified Sub Emote Unlock".  It knows
    /// nothing about custom rewards, that's <see cref="ChannelPointsCustomRewardRedemptionAdd"/>.
    /// </summary>
    /// <param name="Subscription"></param>
    /// <param name="Event"></param>
    public record ChannelPointsAutomaticRewardRedemptionAddV2Payload (
        [property: JsonPropertyName("subscription")] Subscription<R_ChannelPointsAutomaticRewardRedemptionAddV2> Subscription,
        [property: JsonPropertyName("event")] ChannelPointsAutomaticRewardRedemptionAddV2Event Event
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="BroadcasterUserId">The ID of the channel where the reward was redeemed.</param>
    /// <param name="BroadcasterUserLogin">The login of the channel where the reward was redeemed.</param>
    /// <param name="BroadcasterUserName">The display name of the channel where the reward was redeemed.</param>
    /// <param name="UserId">The ID of the redeeming user.</param>
    /// <param name="UserLogin">The login of the redeeming user.</param>
    /// <param name="UserName">The display name of the redeeming user.</param>
    /// <param name="Id">The ID of the Redemption.</param>
    /// <param name="Reward">An object that contains the reward information.</param>
    /// <param name="Message">Optional. An object that contains the user message and emote information needed to recreate the message.</param>
    /// <param name="RedeemedAt">The UTC date and time (in RFC3339 format) of when the reward was redeemed.</param>
    public record ChannelPointsAutomaticRewardRedemptionAddV2Event (
        [property: JsonPropertyName("broadcaster_user_id")] string BroadcasterUserId,
        [property: JsonPropertyName("broadcaster_user_login")] string BroadcasterUserLogin,
        [property: JsonPropertyName("broadcaster_user_name")] string BroadcasterUserName,
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("user_login")] string UserLogin,
        [property: JsonPropertyName("user_name")] string UserName,
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("reward")] ChannelPointsAutomaticRewardRedemptionAddV2RedemptionReward Reward,
        [property: JsonPropertyName("message")] ChannelPointsAutomaticRewardRedemptionAddV2Message? Message,
        [property: JsonPropertyName("redeemed_at")] DateTimeOffset RedeemedAt
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type">The type of reward. One of:
    /// <list type="bullet">
    /// <item>single_message_bypass_sub_mode</item>
    /// <item>send_highlighted_message</item>
    /// <item>random_sub_emote_unlock</item>
    /// <item>chosen_sub_emote_unlock</item>
    /// <item>chosen_modified_sub_emote_unlock</item>
    /// </list>
    /// </param>
    /// <param name="ChannelPoints">Number of channel points used.</param>
    /// <param name="Emote">Optional. Emote associated with the reward.</param>
    public record ChannelPointsAutomaticRewardRedemptionAddV2RedemptionReward (
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("channel_points")] int ChannelPoints,
        [property: JsonPropertyName("emote")] ChannelPointsAutomaticRewardRedemptionAddV2RedemptionRewardEmote? Emote
    );

    /// <summary>
    /// Optional. Emote associated with the reward.
    /// </summary>
    /// <param name="Id">The emote ID.</param>
    /// <param name="Name">The human readable emote token.</param>
    public record ChannelPointsAutomaticRewardRedemptionAddV2RedemptionRewardEmote(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("name")] string Name
    );

    /// <summary>
    /// Optional. An object that contains the user message and emote information needed to recreate the message.
    /// </summary>
    /// <param name="Text">The chat message in plain text.</param>
    /// <param name="Fragments">The ordered list of chat message fragments.</param>
    public record ChannelPointsAutomaticRewardRedemptionAddV2Message (
        [property: JsonPropertyName("text")] string Text,
        [property: JsonPropertyName("fragments")] ChannelPointsAutomaticRewardRedemptionAddV2Fragment Fragments
    );

    /// <summary>
    /// The ordered list of chat message fragments.
    /// </summary>
    /// <param name="Text">The message text in fragment.</param>
    /// <param name="Type">The type of message fragment. Possible values are:
    /// <list type="bullet">
    /// <item>text</item>
    /// <item>emote</item>
    /// </list>
    /// </param>
    /// <param name="Emote">Optional. The metadata pertaining to the emote.</param>
    public record ChannelPointsAutomaticRewardRedemptionAddV2Fragment(
        [property: JsonPropertyName("text")] string Text,
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("emote")] ChannelPointsAutomaticRewardRedemptionAddV2Fragment? Emote
    );

    /// <summary>
    /// Optional. The metadata pertaining to the emote.
    /// </summary>
    /// <param name="Id">The ID that uniquely identifies this emote.</param>
    public record ChannelPointsAutomaticRewardRedemptionAddV2FragmentEmote (
        [property: JsonPropertyName("id")] string Id
    );
#nullable disable
}
