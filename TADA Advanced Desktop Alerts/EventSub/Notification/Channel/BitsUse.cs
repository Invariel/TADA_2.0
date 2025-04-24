using System.Collections.Generic;
using System.Text.Json.Serialization;
using R_BitsUse = TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel.BitsUseCondition;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel
{
    public record BitsUse (
        [property: JsonPropertyName("payload")] BitsUsePayload Payload
    );

    public record BitsUsePayload (
        [property: JsonPropertyName ("subscription")] Subscription<R_BitsUse> Subscription,
        [property: JsonPropertyName ("event")] BitsUseEvent Event
    );

    /// <summary>
    /// The channel.bits.use subscription type sends a notification whenever Bits are used on a channel. <br />
    /// Triggers <br />
    /// This event is designed to be an all-purpose event for when Bits are used in a channel and might be updated in the future as more Twitch features use Bits.
    /// Currently, this event will be sent when a user: <br />
    /// Cheers in a channel <br />
    /// Uses a Power-up <br />
    /// Will not emit when a streamer uses a Power-up for free in their own channel.
    /// Bits transactions via Twitch Extensions are not included in this subscription type.
    /// </summary>
    /// <param name="UserId">The User ID of the redeeming user.</param>
    /// <param name="UserLogin">The login name of the redeeming user.</param>
    /// <param name="UserName">The display name of the redeeming user.</param>
    /// <param name="BroadcasterUserId">The User ID of the channel where the Bits were redeemed.</param>
    /// <param name="BroadcasterUserLogin">The login of the channel where the Bits were used.</param>
    /// <param name="BroadcasterUserName">The display name of the channel where the Bits were used.</param>
    /// <param name="Bits">The number of Bits used.</param>
    /// <param name="Type">Possible values are:<list type="bullet"><item>cheer</item><item>power_up</item></list></param>
    /// <param name="PowerUp">Optional. Data about Power-up.</param>
    /// <param name="Message">Optional. An object that contains the user message and emote information needed to recreate the message.</param>
    public record BitsUseEvent (
        [property: JsonPropertyName ("user_id")] string UserId,
        [property: JsonPropertyName ("user_login")] string UserLogin,
        [property: JsonPropertyName ("user_name")] string UserName,
        [property: JsonPropertyName ("broadcaster_user_id")] string BroadcasterUserId,
        [property: JsonPropertyName ("broadcaster_user_login")] string BroadcasterUserLogin,
        [property: JsonPropertyName ("broadcaster_user_name")] string BroadcasterUserName,
        [property: JsonPropertyName ("bits")] int Bits,
        [property: JsonPropertyName ("type")] string Type,
        [property: JsonPropertyName ("power_up")] string PowerUp,
        [property: JsonPropertyName ("message")] BitsMessage Message
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Text">The chat message in plain text.</param>
    /// <param name="Fragments">The ordered list of chat message fragments.</param>
    public record BitsMessage (
        [property: JsonPropertyName ("text")] string Text,
        [property: JsonPropertyName ("fragments")] List<BitsMessageFragment> Fragments
    );

#nullable enable
    /// <summary>
    /// The ordered list of chat message fragments.
    /// </summary>
    /// <param name="Type">The type of message fragment. Possible values are:
    /// <list type="bullet">
    /// <item>text</item>
    /// <item>cheermote</item>
    /// <item>emote</item>
    /// </list>
    /// </param>
    /// <param name="Text">	The message text in fragment.</param>
    /// <param name="Cheermote">Optional. The metadata pertaining to the cheermote.</param>
    /// <param name="Emote">Optional. Emote associated with the reward.</param>
    public record BitsMessageFragment (
        [property: JsonPropertyName ("type")] string @Type,
        [property: JsonPropertyName ("text")] string Text,
        [property: JsonPropertyName ("cheermote")] Cheermote? Cheermote,
        [property: JsonPropertyName ("emote")] Emote? Emote
    );
#nullable disable

    /// <summary>
    /// Optional. The metadata pertaining to the cheermote.
    /// </summary>
    /// <param name="Prefix">The name portion of the Cheermote string that you use in chat to cheer Bits. The full Cheermote string is the concatenation of {prefix} + {number of Bits}. For example, if the prefix is “Cheer” and you want to cheer 100 Bits, the full Cheermote string is Cheer100. When the Cheermote string is entered in chat, Twitch converts it to the image associated with the Bits tier that was cheered.</param>
    /// <param name="Bits">The amount of Bits cheered.</param>
    /// <param name="Tier">The tier level of the cheermote.</param>
    public record Cheermote (
        [property: JsonPropertyName ("prefix")] string Prefix,
        [property: JsonPropertyName ("bits")] int Bits,
        [property: JsonPropertyName ("tier")] int Tier
    );

    /// <summary>
    /// Optional. Emote associated with the reward.
    /// </summary>
    /// <param name="Id">The ID that uniquely identifies this emote.</param>
    /// <param name="EmoteSetId">The ID that identifies the emote set that the emote belongs to.</param>
    /// <param name="OwnerId">The ID of the broadcaster who owns the emote.</param>
    /// <param name="Format">The formats that the emote is available in. For example, if the emote is available only as a static PNG, the array contains only static. But if the emote is available as a static PNG and an animated GIF, the array contains static and animated. The possible formats are:
    /// <list type="bullet">
    /// <item>animated - An animated GIF is available for this emote.</item>
    /// <item>static - A static PNG file is available for this emote.</item>
    /// </list>
    /// </param>
    public record Emote (
        [property: JsonPropertyName ("id")] string Id,
        [property: JsonPropertyName ("emote_set_id")] string EmoteSetId,
        [property: JsonPropertyName ("owner_id")] string OwnerId,
        [property: JsonPropertyName ("format")] List<string> Format
    );
}
