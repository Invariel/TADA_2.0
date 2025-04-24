using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TADA_Advanced_Desktop_Alerts.EventSub.Request.Connection;

namespace TADA_Advanced_Desktop_Alerts.EventSub.Request.Channel
{
    public class ChannelPointsCustomRewardRedemptionAdd : Subscription<ChannelPointsCustomRewardRedemptionAddCondition>
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "channel.channel_points_custom_reward_redemption.add";

        [JsonPropertyName("version")]
        public string Version { get; } = "1";

        [JsonPropertyName("condition")]
        public ChannelPointsCustomRewardRedemptionAddCondition Condition { get; set; }

        [JsonPropertyName("transport")]
        public SubscriptionTransport Transport { get; } = new SubscriptionTransport();

        public ChannelPointsCustomRewardRedemptionAdd(string broadcasterId, string sessionId)
        {
            Condition = new ChannelPointsCustomRewardRedemptionAddCondition() { BroadcasterUserId = broadcasterId };
            Transport.SessionId = sessionId;
        }
    }

    public class ChannelPointsCustomRewardRedemptionAddCondition
    {
        [JsonPropertyName("broadcaster_user_id")]
        public string BroadcasterUserId { get; set; }
    }
}
