using System.Collections.Generic;
using System.Text.Json.Serialization;
using R_Notification = TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel.NotificationCondition;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel
{
    public record Notification (
        [property: JsonPropertyName ("payload")] NotificationPayload Payload
    );

    public record NotificationPayload (
        [property: JsonPropertyName ("subscription")] Subscription <R_Notification> Subscription,
        [property: JsonPropertyName ("event")] NotificationEvent Event
    );
#nullable enable
    /// <summary>
    /// The channel.chat.notification subscription type sends a notification when an event that appears in chat occurs, such as someone subscribing to the channel or a subscription is gifted.
    /// </summary>
    /// <param name="BroadcasterUserId">The broadcaster user ID.</param>
    /// <param name="BroadcasterUserLogin">The broadcaster login.</param>
    /// <param name="BroadcasterUserName">The broadcaster display name.</param>
    /// <param name="ChatterUserId">The user ID of the user that sent the message.</param>
    /// <param name="ChatterUserLogin">The user login of the user that sent the message.</param>
    /// <param name="ChatterUserName">The user's display name.</param>
    /// <param name="ChatterIsAnonymous">Whether or not the chatter is anonymous.</param>
    /// <param name="Color">The color of the user’s name in the chat room.</param>
    /// <param name="Badges">The user's badges?  The API field description is wrong.</param>
    /// <param name="SystemMessage">The message Twitch shows in the chat room for this notice.</param>
    /// <param name="MessageId">A UUID that identifies the message.</param>
    /// <param name="Message">The structured chat message.</param>
    /// <param name="NoticeType">The type of notice. Possible values are:
    /// <list type="bullet">
    /// <item>sub</item>
    /// <item>resub</item>
    /// <item>sub_gift</item>
    /// <item>community_sub_gift</item>
    /// <item>gift_paid_upgrade</item>
    /// <item>prime_paid_upgrade</item>
    /// <item>raid</item>
    /// <item>unraid</item>
    /// <item>pay_it_forward</item>
    /// <item>announcement</item>
    /// <item>bits_badge_tier</item>
    /// <item>charity_donation</item>
    /// <item>shared_chat_sub</item>
    /// <item>shared_chat_resub</item>
    /// <item>shared_chat_sub_gift</item>
    /// <item>shared_chat_community_sub_gift</item>
    /// <item>shared_chat_gift_paid_upgrade</item>
    /// <item>shared_chat_prime_paid_upgrade</item>
    /// <item>shared_chat_raid</item>
    /// <item>shared_chat_pay_it_forward</item>
    /// <item>shared_chat_announcement</item>
    /// </list>
    /// </param>
    /// <param name="Sub">Information about the sub event. Null if notice_type is not sub.</param>
    /// <param name="Resub">Information about the resub event. Null if notice_type is not resub.</param>
    /// <param name="SubGift">Information about the gift sub event. Null if notice_type is not sub_gift.</param>
    /// <param name="CommunitySubGift">Information about the community gift sub event. Null if notice_type is not community_sub_gift.</param>
    /// <param name="GiftPaidUpgrade">Information about the community gift paid upgrade event. Null if notice_type is not gift_paid_upgrade.</param>
    /// <param name="PrimePaidUpgrade">Information about the Prime gift paid upgrade event. Null if notice_type is not prime_paid_upgrade</param>
    /// <param name="PayItForward">Information about the pay it forward event. Null if notice_type is not pay_it_forward</param>
    /// <param name="Raid">Information about the raid event. Null if notice_type is not raid</param>
    /// <param name="Unraid">Returns an empty payload if notice_type is not unraid, otherwise returns null.</param>
    /// <param name="Announcement">Information about the announcement event. Null if notice_type is not announcement</param>
    /// <param name="BitsBadgeTier">Information about the Bits badge tier event. Null if notice_type is not bits_badge_tier</param>
    /// <param name="CharityDonation">Information about the announcement event. Null if notice_type is not charity_donation</param>
    /// <param name="SourceBroadcasterUserId">Optional. The broadcaster user ID of the channel the message was sent from. Is null when the message notification happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    /// <param name="SourceBroadcasterUserName">Optional. The user name of the broadcaster of the channel the message was sent from. Is null when the message notification happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    /// <param name="SourceBroadcasterUserLogin">Optional. The login of the broadcaster of the channel the message was sent from. Is null when the message notification happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    /// <param name="SourceMessageId">Optional. The UUID that identifies the source message from the channel the message was sent from. Is null when the message happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    /// <param name="SourceBadges">Optional. The list of chat badges for the chatter in the channel the message was sent from. Is null when the message happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    /// <param name="SharedChatSub">Optional. Information about the shared_chat_sub event. Is null if notice_type is not shared_chat_sub. This field has the same information as the sub field but for a notice that happened for a channel in a shared chat session other than the broadcaster in the subscription condition.</param>
    /// <param name="SharedChatResub">Optional. Information about the shared_chat_resub event. Is null if notice_type is not shared_chat_resub. This field has the same information as the resub field but for a notice that happened for a channel in a shared chat session other than the broadcaster in the subscription condition.</param>
    /// <param name="SharedChatSubGift">Optional. Information about the shared_chat_sub_gift event. Is null if notice_type is not shared_chat_sub_gift. This field has the same information as the chat_sub_gift field but for a notice that happened for a channel in a shared chat session other than the broadcaster in the subscription condition.</param>
    /// <param name="SharedChatCommunitySubGift">Optional. Information about the shared_chat_community_sub_gift event. Is null if notice_type is not shared_chat_community_sub_gift. This field has the same information as the community_sub_gift field but for a notice that happened for a channel in a shared chat session other than the broadcaster in the subscription condition.</param>
    /// <param name="SharedChatGiftPaidUpgrade">Optional. Information about the shared_chat_gift_paid_upgrade event. Is null if notice_type is not shared_chat_gift_paid_upgrade. This field has the same information as the gift_paid_upgrade field but for a notice that happened for a channel in a shared chat session other than the broadcaster in the subscription condition.</param>
    /// <param name="SharedChatPrimePaidUpgrade">	Optional. Information about the shared_chat_chat_prime_paid_upgrade event. Is null if notice_type is not shared_chat_prime_paid_upgrade. This field has the same information as the prime_paid_upgrade field but for a notice that happened for a channel in a shared chat session other than the broadcaster in the subscription condition.</param>
    /// <param name="SharedChatPayItForward">Optional. Information about the shared_chat_pay_it_forward event. Is null if notice_type is not shared_chat_pay_it_forward. This field has the same information as the pay_it_forward field but for a notice that happened for a channel in a shared chat session other than the broadcaster in the subscription condition.</param>
    /// <param name="SharedChatRaid">Optional. Information about the shared_chat_raid event. Is null if notice_type is not shared_chat_raid. This field has the same information as the raid field but for a notice that happened for a channel in a shared chat session other than the broadcaster in the subscription condition.</param>
    /// <param name="SharedChatAnnouncement">Optional. Information about the shared_chat_announcement event. Is null if notice_type is not shared_chat_announcement. This field has the same information as the announcement field but for a notice that happened for a channel in a shared chat session other than the broadcaster in the subscription condition.</param>
    public record NotificationEvent (
        [property: JsonPropertyName("broadcaster_user_id")] string BroadcasterUserId,
        [property: JsonPropertyName("broadcaster_user_login")] string BroadcasterUserLogin,
        [property: JsonPropertyName("broadcaster_user_name")] string BroadcasterUserName,
        [property: JsonPropertyName("chatter_user_id")] string ChatterUserId,
        [property: JsonPropertyName("chatter_user_login")] string ChatterUserLogin,
        [property: JsonPropertyName("chatter_user_name")] string ChatterUserName,
        [property: JsonPropertyName("chatter_is_anonymous")] bool ChatterIsAnonymous,
        [property: JsonPropertyName("color")] string Color,
        [property: JsonPropertyName("badges")] List<NotificationEventBadge> Badges,
        [property: JsonPropertyName("system_message")] string SystemMessage,
        [property: JsonPropertyName("message_id")] string MessageId,
        [property: JsonPropertyName("message")] NotificationEventMessage Message,
        [property: JsonPropertyName("notice_type")] string NoticeType,
        [property: JsonPropertyName("sub")] NotificationEventSubscription? Sub,
        [property: JsonPropertyName("resub")] NotificationEventResubscription? Resub,
        [property: JsonPropertyName("sub_gift")] NotificationEventSubGift? SubGift,
        [property: JsonPropertyName("community_sub_gift")] NotificationEventCommunitySubGift? CommunitySubGift,
        [property: JsonPropertyName("gift_paid_upgrade")] NotificationEventGiftPaidUpgrade? GiftPaidUpgrade,
        [property: JsonPropertyName("prime_paid_upgrade")] NotificationEventPrimePaidUpgrade? PrimePaidUpgrade,
        [property: JsonPropertyName("pay_it_forward")] NotificationEventPayItForward? PayItForward,
        [property: JsonPropertyName("raid")] NotificationEventRaid? Raid,
        [property: JsonPropertyName("unraid")] NotificationEventUnraid? Unraid,
        [property: JsonPropertyName("announcement")] NotificationEventAnnouncement? Announcement,
        [property: JsonPropertyName("bits_badge_tier")] NotificationEventBitsBadgeTier? BitsBadgeTier,
        [property: JsonPropertyName("charity_donation")] NotificationEventCharityDonation? CharityDonation,
        [property: JsonPropertyName("source_broadcaster_user_id")] string? SourceBroadcasterUserId,
        [property: JsonPropertyName("source_broadcaster_user_name")] string? SourceBroadcasterUserName,
        [property: JsonPropertyName("source_broadcaster_user_login")] string? SourceBroadcasterUserLogin,
        [property: JsonPropertyName("source_message_id")] string? SourceMessageId,
        [property: JsonPropertyName("source_badges")] List<NotificationEventBadge?> SourceBadges,
        [property: JsonPropertyName("shared_chat_sub")] NotificationEventSubscription? SharedChatSub,
        [property: JsonPropertyName("shared_chat_resub")] NotificationEventResubscription? SharedChatResub,
        [property: JsonPropertyName("shared_chat_sub_gift")] NotificationEventSubGift? SharedChatSubGift,
        [property: JsonPropertyName("shared_chat_community_sub_gift")] NotificationEventCommunitySubGift? SharedChatCommunitySubGift,
        [property: JsonPropertyName("shared_chat_gift_paid_upgrade")] NotificationEventGiftPaidUpgrade? SharedChatGiftPaidUpgrade,
        [property: JsonPropertyName("shared_chat_prime_paid_upgrade")] NotificationEventPrimePaidUpgrade? SharedChatPrimePaidUpgrade,
        [property: JsonPropertyName("shared_chat_pay_it_forward")] NotificationEventPayItForward? SharedChatPayItForward,
        [property: JsonPropertyName("shared_chat_raid")] NotificationEventRaid? SharedChatRaid,
        [property: JsonPropertyName("shared_chat_announcement")] NotificationEventAnnouncement? SharedChatAnnouncement
    );
#nullable disable

    /// <summary>
    /// Information about the user's badges.
    /// </summary>
    /// <param name="SetId">An ID that identifies this set of chat badges. For example, Bits or Subscriber.</param>
    /// <param name="Id">An ID that identifies this version of the badge. The ID can be any value. For example, for Bits, the ID is the Bits tier level, but for World of Warcraft, it could be Alliance or Horde.</param>
    /// <param name="Info">	Contains metadata related to the chat badges in the badges tag. Currently, this tag contains metadata only for subscriber badges, to indicate the number of months the user has been a subscriber.</param>
    public record NotificationEventBadge (
        [property: JsonPropertyName("set_id")] string SetId,
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("info")] string Info
    );

    public record NotificationEventMessage (
        [property: JsonPropertyName("text")] string Text,
        [property: JsonPropertyName("fragments")] List<NotificationEventMessageFragment> Fragments
    );

    public record NotificationEventMessageFragment (
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("text")] string Text,
        [property: JsonPropertyName("cheermote")] NotificationEventMessageCheermote Cheermote,
        [property: JsonPropertyName("emote")] NotificationEventMessageEmote Emote,
        [property: JsonPropertyName("mention")] NotificationEventMessageMention Mention
    );

    public record NotificationEventMessageCheermote (
        [property: JsonPropertyName("prefix")] string Prefix,
        [property: JsonPropertyName("bits")] int Bits,
        [property: JsonPropertyName("tier")] int Tier
    );

    public record NotificationEventMessageEmote (
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("emote_set_id")] string EmoteSetId,
        [property: JsonPropertyName("owner_id")] string OwnerId,
        [property: JsonPropertyName("format")] List<string> Format
    );

    public record NotificationEventMessageMention (
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("user_name")] string UserName,
        [property: JsonPropertyName("user_login")] string UserLogin
    );

    /// <summary>
    /// Information about the sub event. Null if notice_type is not sub.
    /// </summary>
    /// <param name="SubTier">The type of subscription plan being used. Possible values are:
    /// <list type="bullet">
    /// <item>1000 - Tier One (or Prime) subscription.</item>
    /// <item>2000 - Tier Two subscription.</item>
    /// <item>3000 - Tier Three subscription.</item>
    /// </list>
    /// </param>
    /// <param name="IsPrime">Indicates if the subscription was obtained through Amazon Prime.</param>
    /// <param name="DurationMonths">The number of months the subscription is for.</param>
    public record NotificationEventSubscription (
        [property: JsonPropertyName("sub_tier")] string SubTier,
        [property: JsonPropertyName("is_prime")] bool IsPrime,
        [property: JsonPropertyName("duration_months")] int DurationMonths
    );

    /// <summary>
    /// Information about the resub event. Null if notice_type is not resub.
    /// </summary>
    /// <param name="CumulativeMonths">The total number of months the user has subscribed.</param>
    /// <param name="DurationMonths">The number of months the subscription is for.</param>
    /// <param name="StreakMonths">The total number of months the user has subscribed.</param>
    /// <param name="SubTier">The type of subscription plan being used. Possible values are:
    /// <list type="bullet">
    /// <item></item>
    /// <item>1000 - Tier One (or Prime) subscription.</item>
    /// <item>2000 - Tier Two subscription.</item>
    /// <item>3000 - Tier Three subscription.</item>
    /// </param>
    /// <param name="IsPrime">Whether or not this is an Amazon Prime sub.</param>
    /// <param name="IsGift">Whether or not the resub was a result of a gift.</param>
    /// <param name="GifterIsAnonymous">Optional. Whether or not the gift was anonymous.</param>
    /// <param name="GifterUserId">The user ID of the subscription gifter. Null if anonymous.</param>
    /// <param name="GifterUserName">The user name of the subscription gifter. Null if anonymous.</param>
    /// <param name="GifterUserLogin">Optional. The user login of the subscription gifter. Null if anonymous.</param>
    public record NotificationEventResubscription (
        [property: JsonPropertyName("cumulative_months")] int CumulativeMonths,
        [property: JsonPropertyName("duration_months")] int DurationMonths,
        [property: JsonPropertyName("streak_months")] int StreakMonths,
        [property: JsonPropertyName("sub_tier")] string SubTier,
        [property: JsonPropertyName("is_prime")] bool? IsPrime,
        [property: JsonPropertyName("is_gift")] bool IsGift,
        [property: JsonPropertyName("gifter_is_anonymous")] bool? GifterIsAnonymous,
        [property: JsonPropertyName("gifter_user_id")] string GifterUserId,
        [property: JsonPropertyName("gifter_user_name")] int GifterUserName,
        [property: JsonPropertyName("gifter_user_login")] int GifterUserLogin
    );

    /// <summary>
    /// Information about the gift sub event. Null if notice_type is not sub_gift.
    /// </summary>
    /// <param name="DurationMonths">The number of months the subscription is for.</param>
    /// <param name="CumulativeTotal">Optional. The amount of gifts the gifter has given in this channel. Null if anonymous.</param>
    /// <param name="RecipientUserId">The user ID of the subscription gift recipient.</param>
    /// <param name="RecipientUserName">The user name of the subscription gift recipient.</param>
    /// <param name="RecipientUserLogin">The user login of the subscription gift recipient.</param>
    /// <param name="SubTier">The type of subscription plan being used. Possible values are:
    /// <list type="bullet">
    /// <item>1000 - Tier One (or Prime) subscription.</item>
    /// <item>2000 - Tier Two subscription.</item>
    /// <item>3000 - Tier Three subscription.</item>
    /// </param>
    /// <param name="CommunityGiftId">Optional. The ID of the associated community gift. Null if not associated with a community gift.</param>
    public record NotificationEventSubGift (
        [property: JsonPropertyName("duration_months")] int DurationMonths,
        [property: JsonPropertyName("cumulative_total")] int? CumulativeTotal,
        [property: JsonPropertyName("recipient_user_id")] string RecipientUserId,
        [property: JsonPropertyName("recipient_user_name")] string RecipientUserName,
        [property: JsonPropertyName("recipient_user_login")] string RecipientUserLogin,
        [property: JsonPropertyName("sub_tier")] string SubTier,
        [property: JsonPropertyName("community_gift_id")] string CommunityGiftId
    );

    /// <summary>
    /// Information about the community gift sub event. Null if notice_type is not community_sub_gift.
    /// </summary>
    /// <param name="Id">The ID of the associated community gift.</param>
    /// <param name="Total">Number of subscriptions being gifted.</param>
    /// <param name="SubTier">The type of subscription plan being used. Possible values are:
    /// <list type="bullet">
    /// <item>1000 - Tier One (or Prime) subscription.</item>
    /// <item>2000 - Tier Two subscription.</item>
    /// <item>3000 - Tier Three subscription.</item>
    /// </param>
    /// <param name="CumulativeTotal">Optional. The amount of gifts the gifter has given in this channel. Null if anonymous.</param>
    public record NotificationEventCommunitySubGift (
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("total")] int Total,
        [property: JsonPropertyName("sub_tier")] string SubTier,
        [property: JsonPropertyName("cumulative_total")] string CumulativeTotal
    );

#nullable enable
    /// <summary>
    /// Information about the community gift paid upgrade event. Null if notice_type is not gift_paid_upgrade.
    /// </summary>
    /// <param name="GifterIsAnonymous">Whether the gift was given anonymously.</param>
    /// <param name="GifterUserId">Optional. The user ID of the user who gifted the subscription. Null if anonymous.</param>
    /// <param name="GifterUserName">Optional. The user name of the user who gifted the subscription. Null if anonymous.</param>
    public record NotificationEventGiftPaidUpgrade (
        [property: JsonPropertyName("gifter_is_anonymous")] bool GifterIsAnonymous,
        [property: JsonPropertyName("gifter_user_id")] string? GifterUserId,
        [property: JsonPropertyName("gifter_user_name")] string? GifterUserName
    );
#nullable disable

    /// <summary>
    /// Information about the Prime gift paid upgrade event. Null if notice_type is not prime_paid_upgrade
    /// </summary>
    /// <param name="SubTier">The type of subscription plan being used. Possible values are:
    /// <list type="bullet">
    /// <item>1000 - Tier One (or Prime) subscription.</item>
    /// <item>2000 - Tier Two subscription.</item>
    /// <item>3000 - Tier Three subscription.</item>
    /// </param>
    public record NotificationEventPrimePaidUpgrade (
        [property: JsonPropertyName("sub_tier")] string SubTier
    );

#nullable enable
    /// <summary>
    /// Information about the pay it forward event. Null if notice_type is not pay_it_forward
    /// </summary>
    /// <param name="GifterIsAnonymous">Whether the gift was given anonymously.</param>
    /// <param name="GifterUserId">The user ID of the user who gifted the subscription. Null if anonymous.</param>
    /// <param name="GifterUserName">Optional. The user name of the user who gifted the subscription. Null if anonymous.</param>
    /// <param name="GifterUserLogin">The user login of the user who gifted the subscription. Null if anonymous.</param>
    public record NotificationEventPayItForward (
        [property: JsonPropertyName("gifter_is_anonymous")] bool GifterIsAnonymous,
        [property: JsonPropertyName("gifter_user_id")] string? GifterUserId,
        [property: JsonPropertyName("gifter_user_name")] string? GifterUserName,
        [property: JsonPropertyName("gifter_user_login")] string? GifterUserLogin
    );
#nullable disable

    /// <summary>
    /// Information about the raid event. Null if notice_type is not raid
    /// </summary>
    /// <param name="UserId">The user ID of the broadcaster raiding this channel.</param>
    /// <param name="UserName">The user name of the broadcaster raiding this channel.</param>
    /// <param name="UserLogin">The login name of the broadcaster raiding this channel.</param>
    /// <param name="ViewerCount">The number of viewers raiding this channel from the broadcaster’s channel.</param>
    /// <param name="ProfileImageUrl">Profile image URL of the broadcaster raiding this channel.</param>
    public record NotificationEventRaid (
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("user_name")] string UserName,
        [property: JsonPropertyName("user_login")] string UserLogin,
        [property: JsonPropertyName("viewer_count")] int ViewerCount,
        [property: JsonPropertyName("profile_image_url")] string ProfileImageUrl
    );

    /// <summary>
    /// Returns an empty payload if notice_type is not unraid, otherwise returns null.
    /// </summary>
    public record NotificationEventUnraid (
    );

    /// <summary>
    /// Information about the announcement event. Null if notice_type is not announcement
    /// </summary>
    /// <param name="Color">Color of the announcement.</param>
    public record NotificationEventAnnouncement (
        [property: JsonPropertyName("color")] string Color
    );

    /// <summary>
    /// Information about the Bits badge tier event. Null if notice_type is not bits_badge_tier
    /// </summary>
    /// <param name="Tier">The tier of the Bits badge the user just earned. For example, 100, 1000, or 10000.</param>
    public record NotificationEventBitsBadgeTier (
        [property: JsonPropertyName("tier")] string Tier
    );

    /// <summary>
    /// Information about the announcement event. Null if notice_type is not charity_donation
    /// </summary>
    /// <param name="CharityName">Name of the charity.</param>
    /// <param name="Amount">An object that contains the amount of money that the user paid.</param>
    public record NotificationEventCharityDonation (
        [property: JsonPropertyName("charity_name")] string CharityName,
        [property: JsonPropertyName("amount")] NotificationEventCharityDonationAmount Amount
    );

    /// <summary>
    /// An object that contains the amount of money that the user paid.
    /// </summary>
    /// <param name="Value">The monetary amount. The amount is specified in the currency’s minor unit. For example, the minor units for USD is cents, so if the amount is $5.50 USD, value is set to 550.</param>
    /// <param name="DecimalPlace">The number of decimal places used by the currency. For example, USD uses two decimal places.</param>
    /// <param name="Currency">The ISO-4217 three-letter currency code that identifies the type of currency in value.</param>
    public record NotificationEventCharityDonationAmount (
        [property: JsonPropertyName("value")] int Value,
        [property: JsonPropertyName("decimal_place")] int DecimalPlace,
        [property: JsonPropertyName("currency")] string Currency
    );
}
