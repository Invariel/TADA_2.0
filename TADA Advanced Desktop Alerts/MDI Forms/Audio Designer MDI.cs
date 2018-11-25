using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.MDI_Forms
{
    public partial class Audio_Designer_MDI : TADA_Advanced_Desktop_Alerts.MDI_Forms.Waypoint_Designer_MDI
    {
        _TADAAlertAudio alertAudio;


        internal Audio_Designer_MDI(_TADAAlertAudio alertAudio, bool isNew, _TADAAlert parentAlert = null) : base(parentAlert)
        {
            InitializeComponent();

            if (!isNew)
            {
                LoadExisting(this.alertAudio);
            }
        }

        private void LoadExisting(_TADAAlertAudio alertAudio)
        {
            txt_Filename.Text = alertAudio.Filename;

            mtxt_PlayFor.Text = String.Format("{0,3}:{1,2}:{2,2}.{3,3}", alertAudio.Duration.Hours, alertAudio.Duration.Minutes, alertAudio.Duration.Seconds, alertAudio.Duration.Milliseconds);
            mtxt_StartAt.Text = String.Format("{0,3}:{1,2}:{2,2}.{3,3}", alertAudio.StartingPosition.Hours, alertAudio.StartingPosition.Minutes, alertAudio.StartingPosition.Seconds, alertAudio.StartingPosition.Milliseconds);
            mtxt_LoopAt.Text = String.Format("{0,3}:{1,2}:{2,2}.{3,3}", alertAudio.EndingPosition.Hours, alertAudio.EndingPosition.Minutes, alertAudio.EndingPosition.Seconds, alertAudio.EndingPosition.Milliseconds);

            trk_Balance.Value = Convert.ToInt32(alertAudio.Balance * 100.0);
            trk_Volume.Value = Convert.ToInt32(alertAudio.Volume * 10.0);

            this.SetWaypoints(alertAudio.Waypoints);
        }
    }
}
