using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.MDI_Forms
{
    public partial class Condition_Designer_MDI : Form
    {
        public Condition_Designer_MDI()
        {
        }

        internal Condition_Designer_MDI(TADAAlertCondition mainCondition, TADAAlert mainAlert)
        { 
            InitializeComponent();

            
        }

        public void AlertTypeOptions(General.ALERTTYPES alertType)
        {
            grp_SubscriptionTier.Visible = (alertType == General.ALERTTYPES.ANONYMOUS ||
                                            alertType == General.ALERTTYPES.ANONYMOUS_COMMUNITY ||
                                            alertType == General.ALERTTYPES.COMMUNITY ||
                                            alertType == General.ALERTTYPES.SUBGIFT ||
                                            alertType == General.ALERTTYPES.SUBSCRIPTION);

            grp_RelevantQuantity.Visible = !(alertType != General.ALERTTYPES.ANONYMOUS &&
                                             alertType != General.ALERTTYPES.AUTOHOST &&
                                             alertType == General.ALERTTYPES.COMMERCE &&
                                             alertType == General.ALERTTYPES.FIRSTMESSAGE &&
                                             alertType == General.ALERTTYPES.FOLLOW &&
                                             alertType == General.ALERTTYPES.JOIN &&
                                             alertType == General.ALERTTYPES.MESSAGE);

            if (alertType == General.ALERTTYPES.ANONYMOUS_COMMUNITY || alertType == General.ALERTTYPES.COMMUNITY)
            {
                lbl_QuantityThings.Text = "Community gift subscriptions";
            }
            else if (alertType == General.ALERTTYPES.BITS || alertType == General.ALERTTYPES.ANONYMOUS_BITS)
            {
                lbl_QuantityThings.Text = "Bits dropped";
            }
            else if (alertType == General.ALERTTYPES.HOST)
            {
                lbl_QuantityThings.Text = "Viewers hosted for";
            }
            else if (alertType == General.ALERTTYPES.RAID)
            {
                lbl_QuantityThings.Text = "Raided participants";
            }
            else if (alertType == General.ALERTTYPES.SUBGIFT)
            {
                lbl_QuantityThings.Text = "Gift subscriptions";
            }
            else if (alertType == General.ALERTTYPES.SUBSCRIPTION)
            {
                lbl_QuantityThings.Text = "Months subscribed";
            }
        }
    }
}
