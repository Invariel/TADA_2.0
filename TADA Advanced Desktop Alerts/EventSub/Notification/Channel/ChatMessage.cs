using System.Collections.Generic;
using System.Text.Json.Serialization;
using R_ChatMessage = TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel.ChatMessageCondition;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel
{
    public record ChatMessage (
        [property: JsonPropertyName ("payload")] ChatMessagePayload Payload
    );

    public record ChatMessagePayload (
        [property: JsonPropertyName ("subscription")] Subscription<R_ChatMessage> Subscription,
        [property: JsonPropertyName ("event")] ChatMessageEvent Event
    );

#nullable enable
    /// <summary>
    /// The chat message contents.
    /// </summary>
    /// <param name="BroadcasterUserId">The broadcaster user ID.</param>
    /// <param name="BroadcasterUserLogin">The broadcaster login.</param>
    /// <param name="BroadcasterUserName">The broadcaster display name.</param>
    /// <param name="ChatterUserId">The user ID of the user that sent the message.</param>
    /// <param name="ChatterUserLogin">The user login of the user that sent the message.</param>
    /// <param name="ChatterUserName">The user name of the user that sent the message.</param>
    /// <param name="MessageId">A UUID that identifies the message.</param>
    /// <param name="Message">The structured chat message.</param>
    /// <param name="Color">The color of the user’s name in the chat room. This is a hexadecimal RGB color code in the form, #&lt;RGB&gt;. This tag may be empty if it is never set.</param>
    /// <param name="Badges">List of chat badges.</param>
    /// <param name="MessageType">The type of message. Possible values:
    /// <list type="bullet">
    /// <item>text</item>
    /// <item>channel_points_highlighted</item>
    /// <item>channel_points_sub_only</item>
    /// <item>user_intro</item>
    /// <item>power_ups_message_effect</item>
    /// <item>power_ups_gigantified_emote</item>
    /// </list>
    /// </param>
    /// <param name="Cheer">Optional. Metadata if this message is a cheer.</param>
    /// <param name="Reply">Optional. Metadata if this message is a reply.</param>
    /// <param name="ChannelPointsCustomRewardId">Optional. The ID of a channel points custom reward that was redeemed.</param>
    /// <param name="SourceBroadcasterUserId">Optional. The broadcaster user ID of the channel the message was sent from. Is null when the message happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    /// <param name="SourceBroadcasterUserLogin">Optional. The login of the broadcaster of the channel the message was sent from. Is null when the message happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    /// <param name="SourceBroadcasterUserName">Optional. The user name of the broadcaster of the channel the message was sent from. Is null when the message happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    /// <param name="SourceMessageId">Optional. The UUID that identifies the source message from the channel the message was sent from. Is null when the message happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    /// <param name="SourceBadges">Optional. The list of chat badges for the chatter in the channel the message was sent from. Is null when the message happens in the same channel as the broadcaster. Is not null when in a shared chat session, and the action happens in the channel of a participant other than the broadcaster.</param>
    public record ChatMessageEvent(
        [property: JsonPropertyName ("broadcaster_user_id")] string BroadcasterUserId,
        [property: JsonPropertyName ("broadcaster_user_login")] string BroadcasterUserLogin,
        [property: JsonPropertyName ("broadcaster_user_name")] string BroadcasterUserName,
        [property: JsonPropertyName ("chatter_user_id")] string ChatterUserId,
        [property: JsonPropertyName ("chatter_user_login")] string ChatterUserLogin,
        [property: JsonPropertyName ("chatter_user_name")] string ChatterUserName,
        [property: JsonPropertyName ("message_id")] string MessageId,
        [property: JsonPropertyName ("message")] Message Message,
        [property: JsonPropertyName ("color")] string Color,
        [property: JsonPropertyName ("badges")] List<ChatBadge> Badges,
        [property: JsonPropertyName ("message_type")] string MessageType,
        [property: JsonPropertyName ("cheer")] ChatCheer? Cheer,
        [property: JsonPropertyName ("reply")] ChatReply? Reply,
        [property: JsonPropertyName ("channel_points_custom_reward_id")] string? ChannelPointsCustomRewardId,
        [property: JsonPropertyName ("source_broadcaster_user_id")] string? SourceBroadcasterUserId,
        [property: JsonPropertyName ("source_broadcaster_user_login")] string? SourceBroadcasterUserLogin,
        [property: JsonPropertyName ("source_broadcaster_user_name")] string? SourceBroadcasterUserName,
        [property: JsonPropertyName ("source_message_id")] string? SourceMessageId,
        [property: JsonPropertyName ("source_badges")] List<ChatBadge>? SourceBadges
    );
#nullable disable

    /// <summary>
    /// The structured chat message.
    /// </summary>
    /// <param name="Text">The chat message in plain text.</param>
    /// <param name="Fragments">Ordered list of chat message fragments.</param>
    public record Message (
        [property: JsonPropertyName ("text")] string Text,
        [property: JsonPropertyName ("fragments")] List<MessageFragment> Fragments
    );

#nullable enable
    /// <summary>
    /// Ordered list of chat message fragments.
    /// </summary>
    /// <param name="Type">The type of message fragment. Possible values:
    /// <list type="bullet">
    /// <item>text</item>
    /// <item>cheermote</item>
    /// <item>emote</item>
    /// <item>mention</item>
    /// </list>
    /// </param>
    /// <param name="Text">Message text in fragment.</param>
    /// <param name="Cheermote">Optional. Metadata pertaining to the cheermote.</param>
    /// <param name="Emote">Optional. Metadata pertaining to the emote.</param>
    /// <param name="Mention">Optional. Metadata pertaining to the mention.</param>
    public record MessageFragment (
        [property: JsonPropertyName ("type")] string @Type,
        [property: JsonPropertyName ("text")] string Text,
        [property: JsonPropertyName ("cheermote")] ChatCheermote? Cheermote,
        [property: JsonPropertyName ("emote")] ChatEmote? Emote,
        [property: JsonPropertyName ("mention")] ChatMention? Mention
    );
#nullable disable

    /// <summary>
    /// Metadata pertaining to the cheermote.
    /// </summary>
    /// <param name="Prefix">The name portion of the Cheermote string that you use in chat to cheer Bits. The full Cheermote string is the concatenation of {prefix} + {number of Bits}. For example, if the prefix is “Cheer” and you want to cheer 100 Bits, the full Cheermote string is Cheer100. When the Cheermote string is entered in chat, Twitch converts it to the image associated with the Bits tier that was cheered.</param>
    /// <param name="Bits">The amount of Bits cheered.</param>
    /// <param name="Tier">The tier level of the cheermote.</param>
    public record ChatCheermote (
        [property: JsonPropertyName ("prefix")] string Prefix,
        [property: JsonPropertyName ("bits")] string Bits,
        [property: JsonPropertyName ("tier")] string Tier
    );

    /// <summary>
    /// Metadata pertaining to the emote.
    /// </summary>
    /// <param name="Id">An ID that uniquely identifies this emote.</param>
    /// <param name="EmoteSetId">An ID that identifies the emote set that the emote belongs to.</param>
    /// <param name="OwnerId">The ID of the broadcaster who owns the emote.</param>
    /// <param name="Format">The formats that the emote is available in. For example, if the emote is available only as a static PNG, the array contains only static. But if the emote is available as a static PNG and an animated GIF, the array contains static and animated. The possible formats are:
    /// <list type="bullet">
    /// <item>animated - An animated GIF is available for this emote.</item>
    /// <item>static - A static PNG file is available for this emote.</item>
    /// </list>
    /// </param>
    public record ChatEmote (
        [property: JsonPropertyName ("id")] string Id,
        [property: JsonPropertyName ("emote_set_id")] string EmoteSetId,
        [property: JsonPropertyName ("owner_id")] string OwnerId,
        [property: JsonPropertyName ("format")] List<string> Format
    );

    /// <summary>
    /// Optional. Metadata pertaining to the mention.
    /// </summary>
    /// <param name="UserId">The user ID of the mentioned user.</param>
    /// <param name="UserLogin">The user name of the mentioned user.</param>
    /// <param name="UserName">The user login of the mentioned user.</param>
    public record ChatMention (
        [property: JsonPropertyName ("user_id")] string UserId,
        [property: JsonPropertyName ("user_login")] string UserLogin,
        [property: JsonPropertyName ("user_name")] string UserName
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="SetId">An ID that identifies this set of chat badges. For example, Bits or Subscriber.</param>
    /// <param name="Id">An ID that identifies this version of the badge. The ID can be any value. For example, for Bits, the ID is the Bits tier level, but for World of Warcraft, it could be Alliance or Horde.</param>
    /// <param name="Info">Contains metadata related to the chat badges in the badges tag. Currently, this tag contains metadata only for subscriber badges, to indicate the number of months the user has been a subscriber.</param>
    public record ChatBadge (
        [property: JsonPropertyName ("set_id")] string SetId,
        [property: JsonPropertyName ("id")] string Id,
        [property: JsonPropertyName ("info")] string Info
    );

    /// <summary>
    /// Metadata if this message is a cheer.
    /// </summary>
    /// <param name="Bits">The amount of Bits the user cheered.</param>
    public record ChatCheer (
        [property: JsonPropertyName ("bits")] int Bits
    );

    /// <summary>
    /// Metadata if this message is a reply.
    /// </summary>
    /// <param name="ParentMessageId"></param>
    /// <param name="ParentMessageBody"></param>
    /// <param name="ParentUserId"></param>
    /// <param name="ParentUserName"></param>
    /// <param name="ParentUserLogin"></param>
    /// <param name="ThreadMessageId"></param>
    /// <param name="ThreadUserId"></param>
    /// <param name="ThreadUserName"></param>
    /// <param name="ThreadUserLogin"></param>
    public record ChatReply (
        [property: JsonPropertyName("parent_message_id")] string ParentMessageId,
        [property: JsonPropertyName("parent_message_body")] string ParentMessageBody,
        [property: JsonPropertyName("parent_user_id")] string ParentUserId,
        [property: JsonPropertyName("parent_user_name")] string ParentUserName,
        [property: JsonPropertyName("parent_user_login")] string ParentUserLogin,
        [property: JsonPropertyName("thread_message_id")] string ThreadMessageId,
        [property: JsonPropertyName("thread_user_id")] string ThreadUserId,
        [property: JsonPropertyName("thread_user_name")] string ThreadUserName,
        [property: JsonPropertyName("thread_user_login")] string ThreadUserLogin
    );
}
