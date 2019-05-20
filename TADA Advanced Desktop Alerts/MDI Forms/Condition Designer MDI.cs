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
        TADAAlert mainAlert;
        TADAAlertCondition condition;
        private bool loading = false;
        
        public Condition_Designer_MDI()
        {
        }

        internal Condition_Designer_MDI(TADAAlertCondition mainCondition, TADAAlert mainAlert)
        {
            this.mainAlert = mainAlert;
            this.condition = mainCondition;

            InitializeComponent();

            chk_ViewerTierNotSubscribed.CheckedChanged += UpdateViewer;
            chk_ViewerTierTierOne.CheckedChanged += UpdateViewer;
            chk_ViewerTierTierTwo.CheckedChanged += UpdateViewer;
            chk_ViewerTierTierThree.CheckedChanged += UpdateViewer;

            chk_SubscriptionTierTwitchPrime.CheckedChanged += UpdateSubscription;
            chk_SubscriptionTierTierOne.CheckedChanged += UpdateSubscription;
            chk_SubscriptionTierTierTwo.CheckedChanged += UpdateSubscription;
            chk_SubscriptionTierTierThree.CheckedChanged += UpdateSubscription;

            chk_BadgeAdmin.CheckedChanged += UpdateBadges;
            chk_BadgeBits.CheckedChanged += UpdateBadges;
            chk_BadgeBroadcaster.CheckedChanged += UpdateBadges;
            chk_BadgeGlobalMod.CheckedChanged += UpdateBadges;
            chk_BadgeModerator.CheckedChanged += UpdateBadges;
            chk_BadgePrime.CheckedChanged += UpdateBadges;
            chk_BadgeSubscription.CheckedChanged += UpdateBadges;
            chk_BadgeTurbo.CheckedChanged += UpdateBadges;
            chk_BadgeTwitchStaff.CheckedChanged += UpdateBadges;
            chk_BadgeVIP.CheckedChanged += UpdateBadges;

            chk_BadgeBits.CheckedChanged += EnableDisableBitBadge;
            chk_BadgeSubscription.CheckedChanged += EnableDisableSubscriptionMonths;

            txt_MessageKeywords.LostFocus += UpdateKeywords;
            txt_Users.LostFocus += UpdateUsers;
            numud_AmountMinimum.LostFocus += UpdateAmounts;
            numud_AmountMaximum.LostFocus += UpdateAmounts;

            txt_Users.LostFocus += delegate { UpdateDescription(); };
            txt_MessageKeywords.LostFocus += delegate { UpdateDescription(); };

            loading = true;
            LoadValues();
            loading = false;
        }

        private void LoadValues ()
        {
            chk_BadgeAdmin.Checked = condition.IsAdmin;
            chk_BadgeBits.Checked = condition.IsBits;
            chk_BadgeBroadcaster.Checked = condition.IsBroadcaster;
            chk_BadgeGlobalMod.Checked = condition.IsGlobalMod;
            chk_BadgeModerator.Checked = condition.IsModerator;
            chk_BadgePrime.Checked = condition.IsPrime;
            chk_BadgeSubscription.Checked = condition.IsSubscribed;
            chk_BadgeTurbo.Checked = condition.IsTurbo;
            chk_BadgeTwitchStaff.Checked = condition.IsStaff;
            chk_BadgeVIP.Checked = condition.IsVIP;

            cmb_BitBadgeMinimum.Text = condition.MinBits.ToString();
            cmb_BitBadgeMaximum.Text = condition.MaxBits.ToString();

            numud_SubscriptionMonthsMinimum.Value = condition.MinMonths;
            numud_SubscriptionMonthsMaximum.Value = condition.MaxMonths;

            numud_AmountMinimum.Value = condition.MinAmount;
            numud_AmountMaximum.Value = condition.MaxAmount;

            txt_MessageKeywords.Text = condition.Keywords;
            txt_Users.Text = condition.Usernames;

            chk_ViewerTierNotSubscribed.Checked = condition.ViewerTierNoTier;
            chk_ViewerTierTierOne.Checked = condition.ViewerTierTierOne;
            chk_ViewerTierTierTwo.Checked = condition.ViewerTierTierTwo;
            chk_ViewerTierTierThree.Checked = condition.ViewerTierTierThree;

            chk_SubscriptionTierTwitchPrime.Checked = condition.SubscriptionTierPrime;
            chk_SubscriptionTierTierOne.Checked = condition.SubscriptionTierTierOne;
            chk_SubscriptionTierTierTwo.Checked = condition.SubscriptionTierTierTwo;
            chk_SubscriptionTierTierThree.Checked = condition.SubscriptionTierTierThree;
        }

        private void UpdateViewer (object sender, EventArgs e)
        {
            if (!loading)
            {
                if (grp_ViewerTier.Visible)
                {
                    condition.ViewerTierNoTier = chk_ViewerTierNotSubscribed.Checked;
                    condition.ViewerTierTierOne = chk_ViewerTierTierOne.Checked;
                    condition.ViewerTierTierTwo = chk_ViewerTierTierTwo.Checked;
                    condition.ViewerTierTierThree = chk_ViewerTierTierThree.Checked;
                }
                else
                {
                    condition.ViewerTierNoTier = false;
                    condition.ViewerTierTierOne = false;
                    condition.ViewerTierTierTwo = false;
                    condition.ViewerTierTierThree = false;
                }

                UpdateDescription();
            }
        }

        private void UpdateSubscription(object sender, EventArgs e)
        {
            if (!loading)
            {
                if (grp_SubscriptionTier.Visible)
                {
                    condition.SubscriptionTierPrime = chk_SubscriptionTierTwitchPrime.Checked;
                    condition.SubscriptionTierTierOne = chk_SubscriptionTierTierOne.Checked;
                    condition.SubscriptionTierTierTwo = chk_SubscriptionTierTierTwo.Checked;
                    condition.SubscriptionTierTierThree = chk_SubscriptionTierTierThree.Checked;
                }
                else
                {
                    condition.SubscriptionTierPrime = false;
                    condition.SubscriptionTierTierOne = false;
                    condition.SubscriptionTierTierTwo = false;
                    condition.SubscriptionTierTierThree = false;
                }

                UpdateDescription();
            }
        }

        private void UpdateBadges (object sender, EventArgs e)
        {
            if (!loading)
            {
                if (grp_Badges.Visible)
                {
                    condition.IsAdmin = chk_BadgeAdmin.Checked;
                    condition.IsBits = chk_BadgeBits.Checked;
                    condition.IsBroadcaster = chk_BadgeBroadcaster.Checked;
                    condition.IsGlobalMod = chk_BadgeGlobalMod.Checked;
                    condition.IsModerator = chk_BadgeModerator.Checked;
                    condition.IsPrime = chk_BadgePrime.Checked;
                    condition.IsStaff = chk_BadgeTwitchStaff.Checked;
                    condition.IsSubscribed = chk_BadgeSubscription.Checked;
                    condition.IsTurbo = chk_BadgeTurbo.Checked;
                    condition.IsVIP = chk_BadgeVIP.Checked;

                    // Remember to swap the two if max > min.
                    condition.MinBits = chk_BadgeBits.Checked ? Int32.Parse(cmb_BitBadgeMinimum.Text) : 0;
                    condition.MaxBits = chk_BadgeBits.Checked ? Int32.Parse(cmb_BitBadgeMaximum.Text) : 0;

                    condition.MinMonths = chk_BadgeSubscription.Checked ? Convert.ToInt32(numud_SubscriptionMonthsMinimum.Value) : 0;
                    condition.MaxMonths = chk_BadgeSubscription.Checked ? Convert.ToInt32(numud_SubscriptionMonthsMaximum.Value) : 0;
                }
                else
                {
                    condition.IsAdmin = false;
                    condition.IsBits = false;
                    condition.IsBroadcaster = false;
                    condition.IsGlobalMod = false;
                    condition.IsModerator = false;
                    condition.IsPrime = false;
                    condition.IsStaff = false;
                    condition.IsSubscribed = false;
                    condition.IsTurbo = false;
                    condition.IsVIP = false;

                    condition.MinBits = 0;
                    condition.MaxBits = 0;

                    condition.MinMonths = 0;
                    condition.MaxMonths = 0;
                }

                UpdateDescription();
            }
        }

        private void UpdateKeywords(object sender, EventArgs e)
        {
            if (!loading)
            {
                this.condition.Keywords = txt_MessageKeywords.Text;
            }
        }

        private void UpdateUsers (object sender, EventArgs e)
        {
            if (!loading)
            {
                this.condition.Usernames = txt_Users.Text;
            }
        }

        private void UpdateAmounts (object sender, EventArgs e)
        {
            // There was code here to make sure that Max >= Min and such, but 0 needs to be valid for either.  So... fun.
            if (!loading)
            {
                this.condition.MinAmount = Convert.ToInt32(numud_AmountMinimum.Value);
                this.condition.MaxAmount = Convert.ToInt32(numud_AmountMaximum.Value);
            }
        }

        private void UpdateDescription()
        {
            if (condition.Empty)
            {
                txt_Description.Text = "There are no restrictions on triggering this alert.";
            }
            else
            {
                txt_Description.Text = "";

                if (condition.HasViewerTier)
                {
                    txt_Description.Text += condition.ViewerSubTier + " ";
                }

                if (condition.HasSubscriptionTier)
                {
                    txt_Description.Text += condition.SubscriptionTier + " ";
                }

                if (condition.HasBadges)
                {
                    txt_Description.Text += condition.BadgeString + " ";
                }

                if (condition.HasUsers)
                {
                    String[] users = condition.Usernames.Split(',');

                    if (users.Count() == 1)
                    {
                        txt_Description.Text += $"Only {users[0]} can trigger this alert. ";
                    }
                    else
                    {
                        users = users.Select(user => user.Trim()).ToArray<String>();
                        txt_Description.Text += $"Specifically {(String.Join(", ", users, 0, users.Count() - 1))}{(users.Count() == 2 ? "" : ",")} and {users[users.Count() - 1]} can trigger this alert. ";
                    }
                }

                if (condition.HasKeywords)
                {
                    String[] keywords = condition.Keywords.Split(',');

                    if (keywords.Count() == 1)
                    {
                        txt_Description.Text += $"Only messages containing {keywords[0]} can trigger this alert. ";
                    }
                    else
                    {
                        keywords = keywords.Select(keyword => $"'{keyword.Trim()}'").ToArray<String>();
                        txt_Description.Text += $"Messages containing one of the following strings: {String.Join(", ", keywords, 0, keywords.Count() - 1)}{(keywords.Count() == 2 ? "" : ",")} and {keywords[keywords.Count() - 1]} can trigger this alert.";
                    }
                }

                if (condition.HasAmounts)
                {
                    if (condition.MinAmount > 0 && condition.MaxAmount > 0)
                    {
                        txt_Description.Text += $"Between {condition.MinAmount} and {condition.MaxAmount} inclusive ... things ... must happen to trigger this alert.";
                    }
                    else if (condition.MaxAmount == 0)
                    {
                        txt_Description.Text += $"At least {condition.MinAmount} ... things ... must happen to trigger this alert.";
                    }
                    else
                    {
                        txt_Description.Text += $"At most {condition.MaxAmount} ... things ... must happen to trigger this alert.";
                    }
                }
            }
        }

        private void EnableDisableBitBadge(object sender, EventArgs e)
        {
            cmb_BitBadgeMinimum.Enabled = chk_BadgeBits.Checked;
            cmb_BitBadgeMaximum.Enabled = chk_BadgeBits.Checked;
        }

        private void EnableDisableSubscriptionMonths(object sender, EventArgs e)
        {
            numud_SubscriptionMonthsMinimum.Enabled = chk_BadgeSubscription.Checked;
            numud_SubscriptionMonthsMaximum.Enabled = chk_BadgeSubscription.Checked;
        }

        public void AlertTypeOptions(General.ALERTTYPES alertType)
        {
            if (alertType == General.ALERTTYPES.MACRO)
            {
                grp_ViewerTier.Visible = false;
                grp_SubscriptionTier.Visible = false;
                grp_Badges.Visible = false;
                grp_MessageKeywords.Visible = false;
                grp_TriggerUsers.Visible = false;
                grp_RelevantQuantity.Visible = false;
                txt_Description.Text = "Conditions are not relevant to macros.";
            }
            else
            {
                // Viewer tier and viewer badges need to be invisible if it's an anonymous thing.
                grp_ViewerTier.Visible = !(alertType == General.ALERTTYPES.ANONYMOUS_BITS ||
                                       alertType == General.ALERTTYPES.ANONYMOUS_COMMUNITY ||
                                       alertType == General.ALERTTYPES.ANONYMOUS_GIFT);

                grp_Badges.Visible = grp_ViewerTier.Visible;

                grp_SubscriptionTier.Visible = (alertType == General.ALERTTYPES.ANONYMOUS_GIFT ||
                                                alertType == General.ALERTTYPES.ANONYMOUS_COMMUNITY ||
                                                alertType == General.ALERTTYPES.COMMUNITY ||
                                                alertType == General.ALERTTYPES.SUBGIFT ||
                                                alertType == General.ALERTTYPES.SUBSCRIPTION);

                grp_RelevantQuantity.Visible = (alertType != General.ALERTTYPES.ANONYMOUS_GIFT &&
                                                alertType != General.ALERTTYPES.AUTOHOST &&
                                                alertType != General.ALERTTYPES.GAMEPURCHASE &&
                                                alertType != General.ALERTTYPES.FIRSTMESSAGE &&
                                                alertType != General.ALERTTYPES.FOLLOW &&
                                                alertType != General.ALERTTYPES.JOIN &&
                                                alertType != General.ALERTTYPES.MESSAGE);

                grp_MessageKeywords.Visible = !(alertType == General.ALERTTYPES.ANONYMOUS_COMMUNITY ||
                                                alertType == General.ALERTTYPES.ANONYMOUS_GIFT ||
                                                alertType == General.ALERTTYPES.AUTOHOST ||
                                                alertType == General.ALERTTYPES.GAMEPURCHASE ||
                                                alertType == General.ALERTTYPES.FOLLOW ||
                                                alertType == General.ALERTTYPES.HOST ||
                                                alertType == General.ALERTTYPES.JOIN ||
                                                alertType == General.ALERTTYPES.RAID);

                chk_BadgeBroadcaster.Enabled = (alertType == General.ALERTTYPES.COMMUNITY ||
                                                alertType == General.ALERTTYPES.FIRSTMESSAGE ||
                                                alertType == General.ALERTTYPES.JOIN ||
                                                alertType == General.ALERTTYPES.MESSAGE ||
                                                alertType == General.ALERTTYPES.SUBGIFT);

                if (alertType == General.ALERTTYPES.ANONYMOUS_COMMUNITY || alertType == General.ALERTTYPES.COMMUNITY)
                {
                    lbl_QuantityThings.Text = "Community gifts";
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
                    lbl_QuantityThings.Text = "Raiders";
                }
                else if (alertType == General.ALERTTYPES.SUBGIFT)
                {
                    lbl_QuantityThings.Text = "Gifts";
                }
                else if (alertType == General.ALERTTYPES.SUBSCRIPTION)
                {
                    lbl_QuantityThings.Text = "Months subscribed";
                }

                UpdateDescription();
            }
        }
    }
}
