using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADA_Advanced_Desktop_Alerts
{
    public sealed class TierAndPlanConverter
    {
        // Subscription Plans only mean something to PubSub subscription notifications.
        public enum SUBSCRIPTION_PLANS
        {
            NO_TIER,
            TWITCH_PRIME,
            TIER_ONE,
            TIER_TWO,
            TIER_THREE
        };

        // Subscription Tiers are just Tier 1/2/3, with Prime mapping to Tier 1.
        public enum SUBSCRIPTION_TIERS
        {
            NO_TIER,
            TIER_ONE,
            TIER_TWO,
            TIER_THREE
        };

        public static Dictionary<string, Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>> TiersAndPlans = new Dictionary<string, Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>>()
        {
            { "", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.NO_TIER, SUBSCRIPTION_TIERS.NO_TIER) },
            { "No Tier", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.NO_TIER, SUBSCRIPTION_TIERS.NO_TIER) },
            { "Twitch Prime", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.TWITCH_PRIME, SUBSCRIPTION_TIERS.TIER_ONE) },
            { "Tier One", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.TIER_ONE, SUBSCRIPTION_TIERS.TIER_ONE) },
            { "Tier Two", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.TIER_TWO, SUBSCRIPTION_TIERS.TIER_TWO) },
            { "Tier Three", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.TIER_THREE, SUBSCRIPTION_TIERS.TIER_THREE) },
            { "Prime", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.TWITCH_PRIME, SUBSCRIPTION_TIERS.TIER_ONE) },
            { "1000", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.TIER_ONE, SUBSCRIPTION_TIERS.TIER_ONE) },
            { "2000", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.TIER_TWO, SUBSCRIPTION_TIERS.TIER_TWO) },
            { "3000", new Tuple<SUBSCRIPTION_PLANS, SUBSCRIPTION_TIERS>(SUBSCRIPTION_PLANS.TIER_THREE, SUBSCRIPTION_TIERS.TIER_THREE) }
        };

        public static Dictionary<TwitchLib.Client.Enums.SubscriptionPlan, SUBSCRIPTION_PLANS> NormalizeTwitchLibClientPlan = new Dictionary<TwitchLib.Client.Enums.SubscriptionPlan, SUBSCRIPTION_PLANS>()
        {
            { TwitchLib.Client.Enums.SubscriptionPlan.NotSet, SUBSCRIPTION_PLANS.NO_TIER },
            { TwitchLib.Client.Enums.SubscriptionPlan.Prime, SUBSCRIPTION_PLANS.TWITCH_PRIME },
            { TwitchLib.Client.Enums.SubscriptionPlan.Tier1, SUBSCRIPTION_PLANS.TIER_ONE },
            { TwitchLib.Client.Enums.SubscriptionPlan.Tier2, SUBSCRIPTION_PLANS.TIER_TWO },
            { TwitchLib.Client.Enums.SubscriptionPlan.Tier3, SUBSCRIPTION_PLANS.TIER_THREE }
        };

        public static string SubscriptionPlanToString (SUBSCRIPTION_PLANS plan) => TiersAndPlans.Where(x => x.Value.Item1 == plan)?.First().Key ?? "";
        public static string SubscriptionTierToString (SUBSCRIPTION_TIERS tier) => TiersAndPlans.Where(x => x.Value.Item2 == tier)?.First().Key ?? "";
        public static SUBSCRIPTION_PLANS StringToSubscriptionPlan (string plan) => TiersAndPlans.ContainsKey(plan) ? TiersAndPlans[plan].Item1 : TiersAndPlans[""].Item1;
        public static SUBSCRIPTION_TIERS StringToSubscriptionTier (string tier) => TiersAndPlans.ContainsKey(tier) ? TiersAndPlans[tier].Item2 : TiersAndPlans[""].Item2;
        public static SUBSCRIPTION_TIERS SubscriptionTierFromPlan(SUBSCRIPTION_PLANS plan) => TiersAndPlans.Where(x => x.Value.Item1 == plan)?.First().Value.Item2 ?? SUBSCRIPTION_TIERS.NO_TIER;
        public static SUBSCRIPTION_PLANS NormalizeTwitchLibPlan(TwitchLib.Client.Enums.SubscriptionPlan plan) => NormalizeTwitchLibClientPlan[plan];
    }
}
