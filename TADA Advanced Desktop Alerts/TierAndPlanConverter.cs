using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADA_Advanced_Desktop_Alerts
{
    public static class TierAndPlanConverter
    {
        private const String NO_TIER = "No Tier";
        private const String TWITCH_PRIME = "Twitch Prime";
        private const String TIER_ONE = "Tier One";
        private const String TIER_TWO = "Tier Two";
        private const String TIER_THREE = "Tier Three";

        public enum SUBSCRIPTION_PLANS
        {
            NO_TIER,
            TWITCH_PRIME,
            TIER_ONE,
            TIER_TWO,
            TIER_THREE
        };

        public enum SUBSCRIPTION_TIERS
        {
            NO_TIER,
            TIER_ONE,
            TIER_TWO,
            TIER_THREE
        };

        public static Dictionary<SUBSCRIPTION_PLANS, String> SubscriptionPlanToString = new Dictionary<SUBSCRIPTION_PLANS, string>()
        {
            { SUBSCRIPTION_PLANS.NO_TIER, NO_TIER },
            { SUBSCRIPTION_PLANS.TWITCH_PRIME, TWITCH_PRIME },
            { SUBSCRIPTION_PLANS.TIER_ONE, TIER_ONE },
            { SUBSCRIPTION_PLANS.TIER_TWO, TIER_TWO },
            { SUBSCRIPTION_PLANS.TIER_THREE, TIER_THREE },
        };

        public static Dictionary<String, SUBSCRIPTION_PLANS> StringToSubscriptionPlan = new Dictionary<string, SUBSCRIPTION_PLANS>()
        {
            { null, SUBSCRIPTION_PLANS.NO_TIER },
            { NO_TIER, SUBSCRIPTION_PLANS.NO_TIER },
            { TWITCH_PRIME, SUBSCRIPTION_PLANS.TWITCH_PRIME },
            { TIER_ONE, SUBSCRIPTION_PLANS.TIER_ONE },
            { TIER_TWO, SUBSCRIPTION_PLANS.TIER_TWO },
            { TIER_THREE, SUBSCRIPTION_PLANS.TIER_THREE },
        };

        public static Dictionary<SUBSCRIPTION_TIERS, String> SubscriptionTierToString = new Dictionary<SUBSCRIPTION_TIERS, string>()
        {
            { SUBSCRIPTION_TIERS.NO_TIER, NO_TIER },
            { SUBSCRIPTION_TIERS.TIER_ONE, TIER_ONE },
            { SUBSCRIPTION_TIERS.TIER_TWO, TIER_TWO },
            { SUBSCRIPTION_TIERS.TIER_THREE, TIER_THREE },
        };

        public static Dictionary<String, SUBSCRIPTION_TIERS> StringToSubscriptionTier = new Dictionary<string, SUBSCRIPTION_TIERS>()
        {
            { NO_TIER, SUBSCRIPTION_TIERS.NO_TIER },
            { TIER_ONE, SUBSCRIPTION_TIERS.TIER_ONE },
            { TIER_TWO, SUBSCRIPTION_TIERS.TIER_TWO },
            { TIER_THREE, SUBSCRIPTION_TIERS.TIER_THREE },
        };

        public static SUBSCRIPTION_PLANS NormalizePlanName(TwitchLib.Client.Enums.SubscriptionPlan plan)
        {
            SUBSCRIPTION_PLANS outPlan = SUBSCRIPTION_PLANS.NO_TIER;
            switch (plan)
            {
                case TwitchLib.Client.Enums.SubscriptionPlan.NotSet:
                    outPlan = SUBSCRIPTION_PLANS.NO_TIER;
                    break;
                case TwitchLib.Client.Enums.SubscriptionPlan.Prime:
                    outPlan = SUBSCRIPTION_PLANS.TWITCH_PRIME;
                    break;
                case TwitchLib.Client.Enums.SubscriptionPlan.Tier1:
                    outPlan = SUBSCRIPTION_PLANS.TIER_ONE;
                    break;
                case TwitchLib.Client.Enums.SubscriptionPlan.Tier2:
                    outPlan = SUBSCRIPTION_PLANS.TIER_TWO;
                    break;
                case TwitchLib.Client.Enums.SubscriptionPlan.Tier3:
                    outPlan = SUBSCRIPTION_PLANS.TIER_THREE;
                    break;
            }
            return outPlan;
        }

        public static SUBSCRIPTION_PLANS NormalizePlanName(TwitchLib.PubSub.Enums.SubscriptionPlan plan)
        {
            SUBSCRIPTION_PLANS outPlan = SUBSCRIPTION_PLANS.NO_TIER;
            switch (plan)
            {
                case TwitchLib.PubSub.Enums.SubscriptionPlan.NotSet:
                    outPlan = SUBSCRIPTION_PLANS.NO_TIER;
                    break;
                case TwitchLib.PubSub.Enums.SubscriptionPlan.Prime:
                    outPlan = SUBSCRIPTION_PLANS.TWITCH_PRIME;
                    break;
                case TwitchLib.PubSub.Enums.SubscriptionPlan.Tier1:
                    outPlan = SUBSCRIPTION_PLANS.TIER_ONE;
                    break;
                case TwitchLib.PubSub.Enums.SubscriptionPlan.Tier2:
                    outPlan = SUBSCRIPTION_PLANS.TIER_TWO;
                    break;
                case TwitchLib.PubSub.Enums.SubscriptionPlan.Tier3:
                    outPlan = SUBSCRIPTION_PLANS.TIER_THREE;
                    break;
            }
            return outPlan;
        }

        public static SUBSCRIPTION_TIERS SubscriptionTierFromPlan (SUBSCRIPTION_PLANS plan)
        {
            SUBSCRIPTION_TIERS tier = SUBSCRIPTION_TIERS.NO_TIER;

            switch (plan)
            {
                case SUBSCRIPTION_PLANS.NO_TIER:
                    tier = SUBSCRIPTION_TIERS.NO_TIER;
                    break;
                case SUBSCRIPTION_PLANS.TWITCH_PRIME:
                case SUBSCRIPTION_PLANS.TIER_ONE:
                    tier = SUBSCRIPTION_TIERS.TIER_ONE;
                    break;
                case SUBSCRIPTION_PLANS.TIER_TWO:
                    tier = SUBSCRIPTION_TIERS.TIER_TWO;
                    break;
                case SUBSCRIPTION_PLANS.TIER_THREE:
                    tier = SUBSCRIPTION_TIERS.TIER_THREE;
                    break;
            }

            return tier;
        }
    }
}
