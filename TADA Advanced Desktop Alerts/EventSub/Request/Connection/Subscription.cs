using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection
{
    /// <summary>
    /// Subscription for a WebSocket connection.
    /// </summary>
    public interface Subscription<TCondition>
    {
        /// <summary>
        /// The type of subscription to create. For a list of subscriptions that you can create, see
        /// https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#subscription-types.
        /// Set this field to the value in the Name column of the Subscription Types table.
        /// </summary>
        [JsonPropertyName ("type")]
        public string Type { get; }

        /// <summary>
        /// The version number that identifies the definition of the subscription type that you want the response to use.
        /// This information is also at https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#subscription-types.
        /// </summary>
        [JsonPropertyName ("version")]
        public string Version { get; }

        /// <summary>
        /// A JSON object that contains the parameter values that are specific to the specified subscription type.
        /// For the object’s required and optional fields, see the subscription type’s documentation.
        /// </summary>
        [JsonPropertyName ("condition")]
        public TCondition Condition { get; set; }

        /// <summary>
        /// The transport details that you want Twitch to use when sending you notifications.
        /// </summary>
        [JsonPropertyName("transport")]
        public SubscriptionTransport Transport { get; }
    }

    public class SubscriptionTransport
    {
        /// <summary>
        /// The transport method.  Since this is a WebSocket library, websocket is forced.
        /// </summary>
        [JsonPropertyName ("method")]
        public string Method { get; } = "websocket";

        /// <summary>
        /// The session ID that was received in the welcome message.
        /// </summary>
        [JsonPropertyName ("session_id")]
        public string SessionId { get; set; }
    }
}
