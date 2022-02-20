using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Enums;

namespace TADA_Advanced_Desktop_Alerts
{
    public sealed class TierAndPlanConverter
    {
        public static Dictionary<SubscriptionPlan, Tuple<string, string>> TierAndPlan = new Dictionary<SubscriptionPlan, Tuple<string, string>>()
        {
            //                                                  (Plan, Tier)
            { SubscriptionPlan.NotSet, new Tuple<string, string>("", "No Tier") },
            { SubscriptionPlan.Prime,  new Tuple<string, string>("Prime", "Twitch Prime") },
            { SubscriptionPlan.Tier1,  new Tuple<string, string>("1000", "Tier One") },
            { SubscriptionPlan.Tier2,  new Tuple<string, string>("2000", "Tier Two") },
            { SubscriptionPlan.Tier3,  new Tuple<string, string>("3000", "Tier Three") },
        };

        public static string GetPlanName(SubscriptionPlan plan)
        {
            return TierAndPlan[plan].Item1;
        }

        public static string GetPlanName (string tier)
        {
            return TierAndPlan.Where(x => x.Value.Item1 == tier).First().Value.Item2 ?? "";
        }

        public static string GetTierName (SubscriptionPlan plan)
        {
            return TierAndPlan[plan].Item2;
        }

        public static SubscriptionPlan GetSubscriptionPlanFromPlan(string plan)
        {
            return TierAndPlan.Where(x => x.Value.Item1 == plan).First().Key;
        }

        public static SubscriptionPlan GetSubscriptionPlanFromTier(string tier)
        {
            return TierAndPlan.Where(x => x.Value.Item2 == tier).First().Key;
        }
    }
}
