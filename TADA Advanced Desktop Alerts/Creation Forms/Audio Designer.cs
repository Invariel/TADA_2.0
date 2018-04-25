using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.Creation_Forms
{
    public partial class AudioCreationForm : TADA_Advanced_Desktop_Alerts.Creation_Forms.MainWaypointForm
    {
        private System.Windows.Controls.MediaElement me = new System.Windows.Controls.MediaElement();
        private bool isPlaying = false;
        private bool blocked = false;
        private TADAAlertAudio alertAudio;

        public AudioCreationForm(TADAAnimationWindow animationWindow, TADAAlertAudio alertAudio)
        {
            InitializeComponent();

            this.animationWindow = animationWindow;
            this.alertAudio = alertAudio;

            grp_Coordinates.Enabled = false;
            grp_Options.Enabled = false;

            tab_Effects.TabPages.Remove(tab_Angle);
            tab_Effects.TabPages.Remove(tab_Magnification);
            tab_Effects.TabPages.Remove(tab_Opacity);

            txt_Filename.TextChanged += TestAudioFile;

            btn_FilenameBrowse.Click += btn_FilenameBrowse_Click;
            btn_PlayPause.Click += btn_PlayPause_Click;
            btn_TestItem.Click += btn_TestItem_Click;

            trk_Volume.ValueChanged += UpdateVolume;
            trk_Balance.ValueChanged += UpdateBalance;

            dg_Waypoints.Columns["Presets"].Visible = false;
            dg_Waypoints.Columns["xDataGridViewTextBoxColumn"].Visible = false;
            dg_Waypoints.Columns["yDataGridViewTextBoxColumn"].Visible = false;
            dg_Waypoints.Columns["transitionTimeDataGridViewTextBoxColumn"].Visible = false;
            dg_Waypoints.Columns["persistTimeDataGridViewTextBoxColumn"].HeaderText = "Waypoint Duration";
            dg_Waypoints.Columns["persistTimeDataGridViewTextBoxColumn"].ToolTipText = "How long, in seconds, before the next volume or balance or macro begins.";

            eh_1.Child = me;
            me.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
            me.MediaOpened += LoadedMediaFile;
            me.MediaFailed += FailedMediaFile;

            btn_Complete.Click += btn_Complete_Click;
        }

        private void TestAudioFile(object sender, EventArgs e)
        {
            if (!txt_Filename.Focused)
            {
                if (!String.IsNullOrEmpty(txt_Filename.Text))
                {
                    me.Source = new Uri(txt_Filename.Text);
                    me.Play();
                    me.Stop();
                }
            }
        }

        private void LoadedMediaFile(object sender, EventArgs e)
        {
            if (!me.HasAudio)
            {
                FailedMediaFile(sender, e);
                return;
            }

            if (me.HasVideo)
            {
                MessageBox.Show(null, "This appears to be a video.  The audio portion will play, the video will be hidden.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            TimeSpan mediaDuration = me.NaturalDuration.HasTimeSpan ? me.NaturalDuration.TimeSpan : new TimeSpan(0);
            txt_Duration.Text = General.DurationFromTimeSpan(mediaDuration);

            numud_PlayForH.Value = mediaDuration.Hours;
            numud_PlayForM.Value = mediaDuration.Minutes;
            numud_PlayForS.Value = mediaDuration.Seconds;
            numud_PlayForMS.Value = mediaDuration.Milliseconds;

            trk_Position.Maximum = Convert.ToInt32(Math.Round(mediaDuration.TotalMilliseconds));
            trk_Position.TickFrequency = trk_Position.Maximum / 25;

            DisplayControls(true);
        }

        private void FailedMediaFile(object sender, EventArgs e)
        {
            MessageBox.Show(null, "The chosen file was not recognized as a valid audio file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            txt_Duration.Text = "";

            DisplayControls(false);
        }

        private void DisplayControls (bool showControls)
        {
            lbl_Delay.Visible = showControls;
            lbl_DelayColonHM.Visible = showControls;
            lbl_DelayColonMS.Visible = showControls;
            lbl_DelayPeriodSM.Visible = showControls;

            numud_DelayH.Visible = showControls;
            numud_DelayM.Visible = showControls;
            numud_DelayS.Visible = showControls;
            numud_DelayMS.Visible = showControls;

            lbl_PlayFor.Visible = showControls;
            lbl_PlayForColonHM.Visible = showControls;
            lbl_PlayForColonMS.Visible = showControls;
            lbl_PlayForPeriodSM.Visible = showControls;

            numud_PlayForH.Visible = showControls;
            numud_PlayForM.Visible = showControls;
            numud_PlayForS.Visible = showControls;
            numud_PlayForMS.Visible = showControls;

            lbl_StartAt.Visible = showControls;
            lbl_StartAtColonHM.Visible = showControls;
            lbl_StartAtColonMS.Visible = showControls;
            lbl_StartAtPeriodSM.Visible = showControls;

            numud_StartAtH.Visible = showControls;
            numud_StartAtM.Visible = showControls;
            numud_StartAtS.Visible = showControls;
            numud_StartAtMS.Visible = showControls;

            lbl_LoopAt.Visible = showControls;
            lbl_LoopAtColonHM.Visible = showControls;
            lbl_LoopAtColonMS.Visible = showControls;
            lbl_LoopAtPeriodSM.Visible = showControls;

            numud_LoopAtH.Visible = showControls;
            numud_LoopAtM.Visible = showControls;
            numud_LoopAtS.Visible = showControls;
            numud_LoopAtMS.Visible = showControls;

            grp_ImagePreview.Visible = showControls;

            trk_Volume.Visible = showControls;
            lbl_Volume.Visible = showControls;

            trk_Balance.Visible = showControls;
            lbl_Balance.Visible = showControls;
        }

        private void UpdateBalance(object sender, EventArgs e)
        {
            lbl_Balance.Text = String.Format("Balance: {0,2}", trk_Balance.Value / 100.0);
            me.Balance = trk_Balance.Value / 100.0;
        }

        private void UpdateVolume(object sender, EventArgs e)
        {
            lbl_Volume.Text = String.Format("Vol: {0,2}", trk_Volume.Value / 10.0);
            me.Volume = trk_Volume.Value / 1000.0;
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            if (!this.blocked)
            {
                this.blocked = true;
                if (sender == trk_Position)
                {
                    TimeSpan positionTimespan = new TimeSpan(0, 0, 0, 0, trk_Position.Value);
                    numud_PositionH.Value = positionTimespan.Hours;
                    numud_PositionM.Value = positionTimespan.Minutes;
                    numud_PositionS.Value = positionTimespan.Seconds;
                    numud_PositionMS.Value = positionTimespan.Milliseconds;
                }
                else
                {
                    TimeSpan positionTimespan = General.TimeSpanFromDecimals(numud_PositionMS.Value, numud_PositionS.Value, numud_PositionM.Value, numud_PositionH.Value, 0);
                }
                this.blocked = false;
            }
        }

        private void btn_FilenameBrowse_Click(object sender, EventArgs e)
        {
            General.openFileDialogBox(txt_Filename, "Audio File Formats|*.aac; *,aiff; *.amr; *.flac; *.m4a; *.mp3; *.mp3; *.ogg; *.pcm; *.raw; *.wav; *.wma|All Files|*.*", 1);
        }

        private void btn_PlayPause_Click(object sender, EventArgs e)
        {
            if (this.isPlaying)
            {
                TimeSpan position = me.Position;
                me.Stop();

                this.blocked = true;
                trk_Position.Value = Convert.ToInt32(Math.Round(position.TotalMilliseconds));
                numud_PositionH.Value = position.Hours;
                numud_PositionM.Value = position.Minutes;
                numud_PositionS.Value = position.Seconds;
                numud_PositionMS.Value = position.Milliseconds;
                this.blocked = false;

                this.isPlaying = false;
            }
            else
            {
                me.Position = General.TimeSpanFromDecimals(numud_PositionMS.Value, numud_PositionS.Value, numud_PositionM.Value, numud_PositionH.Value, 0);
                me.Volume = trk_Volume.Value;
                me.Balance = trk_Balance.Value / 100.0;
                me.Play();
                this.isPlaying = true;
            }
        }

        private void GenerateAlertAudio()
        {
            // filename, timespans, volume, balance, waypoints
            String filename = txt_Filename.Text;
            TimeSpan delay = General.TimeSpanFromDecimals(numud_DelayMS.Value, numud_DelayS.Value, numud_DelayM.Value, numud_DelayH.Value, 0);
            TimeSpan duration = General.TimeSpanFromDecimals(numud_PlayForMS.Value, numud_PlayForS.Value, numud_PlayForM.Value, numud_PlayForH.Value, 0);
            TimeSpan startingPosition = General.TimeSpanFromDecimals(numud_StartAtMS.Value, numud_StartAtS.Value, numud_StartAtM.Value, numud_StartAtH.Value, 0);
            TimeSpan endingPosition = General.TimeSpanFromDecimals(numud_LoopAtMS.Value, numud_LoopAtS.Value, numud_LoopAtM.Value, numud_LoopAtH.Value, 0);
            double volume = trk_Volume.Value / 10.0;    // This turns it from a 0-1000 scale to a 0-100 scale.  It will be normalized to 0-1 by PlayingAlert.
            double balance = trk_Balance.Value / 100.0;
            WaypointContainer waypoints = GetWaypoints();

            if (this.alertAudio == null)
            {
                this.alertAudio = new TADAAlertAudio(filename, volume, balance, delay, duration, startingPosition, endingPosition, waypoints);
            }
            else
            {
                this.alertAudio.Filename = filename;
                this.alertAudio.Delay = delay;
                this.alertAudio.Duration = duration;
                this.alertAudio.StartingPosition = startingPosition;
                this.alertAudio.EndingPosition = endingPosition;
                this.alertAudio.Balance = balance;
                this.alertAudio.Volume = volume;
                this.alertAudio.Waypoints = waypoints;
            }

        }

        private void LoadExisting(TADAAlertAudio alertAudio)
        {
            txt_Filename.Text = alertAudio.Filename;

            General.FillNumudsFromTimeSpan(alertAudio.Delay, numud_DelayH, numud_DelayM, numud_DelayS, numud_DelayMS);
            General.FillNumudsFromTimeSpan(alertAudio.Duration, numud_PlayForH, numud_PlayForM, numud_PlayForS, numud_PlayForMS);
            General.FillNumudsFromTimeSpan(alertAudio.StartingPosition, numud_StartAtH, numud_StartAtM, numud_StartAtS, numud_StartAtMS);
            General.FillNumudsFromTimeSpan(alertAudio.EndingPosition, numud_LoopAtH, numud_LoopAtM, numud_LoopAtS, numud_LoopAtMS);

            trk_Balance.Value = Convert.ToInt32(alertAudio.Balance * 100.0);
            trk_Volume.Value = Convert.ToInt32(alertAudio.Volume * 10.0);

            this.SetWaypoints(alertAudio.Waypoints);
        }

        private void btn_TestItem_Click(object sender, EventArgs e)
        {
            GenerateAlertAudio();
            if (this.animationWindow == null)
            {
                this.animationWindow = new TADAAnimationWindow();
            }
            this.animationWindow.Show();

            PlayingAlert playingAlert = new PlayingAlert(this.animationWindow, this.animationWindow.myGraphics, null, this.alertAudio, null, null);
            this.animationWindow.playingAlert = playingAlert;
            playingAlert.Play();
        }

        private void btn_Complete_Click(object sender, EventArgs e)
        {
            if (ValidityChecks())
            {
                GenerateAlertAudio();
                this.Close();
            }
        }

        private bool ValidityChecks()
        {
            // IF everything looks good, then register it.
            bool valid = true;

            if (String.IsNullOrEmpty(txt_Filename.Text))
            {
                MessageBox.Show(null, "You did not specify a file to work with.", "Error - Filename is empty.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (General.TimeSpanFromDecimals(numud_StartAtMS.Value, numud_StartAtS.Value, numud_StartAtM.Value, numud_StartAtH.Value, 0).Ticks >
                General.TimeSpanFromDecimals(numud_LoopAtMS.Value, numud_LoopAtS.Value, numud_LoopAtM.Value, numud_LoopAtH.Value, 0).Ticks)
            {
                MessageBox.Show(null, "Loop At time is before Start At time.", "Error - Temporal Anomaly Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (General.TimeSpanFromDecimals(numud_StartAtMS.Value, numud_StartAtS.Value, numud_StartAtM.Value, numud_StartAtH.Value, 0).Ticks ==
                General.TimeSpanFromDecimals(numud_LoopAtMS.Value, numud_LoopAtS.Value, numud_LoopAtM.Value, numud_LoopAtH.Value, 0).Ticks)
            {
                MessageBox.Show(null, "Loop At time is exactly Start At time, which will make this play forever and not at all.", "Error - Temporal Anomaly Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            return valid;
        }
    }
}
