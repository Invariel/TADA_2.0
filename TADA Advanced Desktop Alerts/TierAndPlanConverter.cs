using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADA_Advanced_Desktop_Alerts
{
    public sealed class TierAndPlanConverter
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

        public static String SubscriptionPlanToString (SUBSCRIPTION_PLANS plan)
        {
            String planString = TierAndPlanConverter.NO_TIER;

            switch (plan)
            {
                case SUBSCRIPTION_PLANS.TWITCH_PRIME: planString = TierAndPlanConverter.TWITCH_PRIME; break;
                case SUBSCRIPTION_PLANS.TIER_ONE: planString = TierAndPlanConverter.TIER_ONE; break;
                case SUBSCRIPTION_PLANS.TIER_TWO: planString = TierAndPlanConverter.TIER_TWO; break;
                case SUBSCRIPTION_PLANS.TIER_THREE: planString = TierAndPlanConverter.TIER_THREE; break;
            }

            return planString;
        }

        public static SUBSCRIPTION_PLANS StringToSubscriptionPlan (String planString)
        {
            SUBSCRIPTION_PLANS plan = SUBSCRIPTION_PLANS.NO_TIER;

            switch (planString)
            {
                case null:
                case NO_TIER:
                    plan = SUBSCRIPTION_PLANS.NO_TIER;
                    break;
                case TWITCH_PRIME: plan = SUBSCRIPTION_PLANS.TWITCH_PRIME; break;
                case TIER_ONE: plan = SUBSCRIPTION_PLANS.TIER_ONE; break;
                case TIER_TWO: plan = SUBSCRIPTION_PLANS.TIER_TWO; break;
                case TIER_THREE: plan = SUBSCRIPTION_PLANS.TIER_THREE; break;
            }

            return plan;
        }

        public static String SubscriptionTierToString (SUBSCRIPTION_TIERS tier)
        {
            String tierString = NO_TIER;

            switch (tier)
            {
                case SUBSCRIPTION_TIERS.TIER_ONE: tierString = TIER_ONE; break;
                case SUBSCRIPTION_TIERS.TIER_TWO: tierString = TIER_TWO; break;
                case SUBSCRIPTION_TIERS.TIER_THREE: tierString = TIER_THREE; break;
            }

            return tierString;
        }

        public static SUBSCRIPTION_TIERS StringToSubscriptionTier (String tierString)
        {
            SUBSCRIPTION_TIERS tier = SUBSCRIPTION_TIERS.NO_TIER;

            switch (tierString)
            {
                case null:
                case NO_TIER:
                    tier = SUBSCRIPTION_TIERS.NO_TIER;
                    break;
                case TIER_ONE: tier = SUBSCRIPTION_TIERS.TIER_ONE; break;
                case TIER_TWO: tier = SUBSCRIPTION_TIERS.TIER_TWO; break;
                case TIER_THREE: tier = SUBSCRIPTION_TIERS.TIER_THREE; break;
            }

            return tier;
        }

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
