using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel;

namespace TADA_Advanced_Desktop_Alerts.AlertObjects
{
    public class Bits
    {
        private BitsUseEvent _event { get; set; }

        public Bits (TADA_Advanced_Desktop_Alerts.EventSub.Notification.Channel.BitsUsePayload payload)
        {
            _event = payload.Event;
        }

        public SubstitutionObject GenerateSubstitutionObject ()
        {
            return new SubstitutionObject()
            {
                AlertType = General.alertTypes[_event.Type == "cheer" ? General.ALERTTYPES.BITS : General.ALERTTYPES.POWER_UP],
                Amount = _event.Bits,
                VIP = false,
                Message = _event.Message.Text,
                ViewerName = _event.UserName,
                TierName = null,
                ChannelPointsRewardName = null
            };
        }
    }
}
