using System.Text.Json.Serialization;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel
{
    public class ChannelPointsAutomaticRewardRedemptionAddV2 : Subscription<ChannelPointsAutomaticaRewardRedemptionAddV2Condition>
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "channel.channel_points_automatic_reward_redemption.add";

        [JsonPropertyName("version")]
        public string Version { get; } = "2";

        [JsonPropertyName("condition")]
        public ChannelPointsAutomaticaRewardRedemptionAddV2Condition Condition { get; set; }

        [JsonPropertyName("transport")]
        public SubscriptionTransport Transport { get; } = new SubscriptionTransport();

        public ChannelPointsAutomaticRewardRedemptionAddV2 (string broadcasterId, string sessionId)
        {
            Condition = new ChannelPointsAutomaticaRewardRedemptionAddV2Condition() { BroadcasterUserId = broadcasterId };
            Transport.SessionId = sessionId;
        }
    }

    public class ChannelPointsAutomaticaRewardRedemptionAddV2Condition
    {
        [JsonPropertyName("broadcaster_user_id")]
        public string BroadcasterUserId { get; set; }
    }
}
