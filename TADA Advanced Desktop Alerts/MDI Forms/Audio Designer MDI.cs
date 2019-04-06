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
        internal TADAAlertAudio alertAudio;
        private System.Windows.Controls.MediaElement me;

        private bool blocked = false;
        private bool loading = false;

        private bool isPlaying = false;
        private bool mediaLoaded = false;
        private bool loadExisting = false;

        ~Audio_Designer_MDI()
        {
            me.Stop();
            eh_1.Dispose();
        }

        internal Audio_Designer_MDI(TADAAlertAudio alertAudio, TADAAlert parentAlert = null) : base(parentAlert)
        {
            InitializeComponent();

            me = new System.Windows.Controls.MediaElement();
            eh_1.Child = me;
            me.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
            me.MediaOpened += LoadedMediaFile;
            me.MediaFailed += FailedMediaFile;

            // Remove the Angle, Magnification, and Opacity tabs from ...
            tab_AdvancedWaypointControls.TabPages.Remove(tab_AngleAndMagnification);
            tab_AdvancedWaypointControls.TabPages.Remove(tab_Opacity);
            tab_AdvancedWaypointControls.TabPages.Remove(tab_Path);

            DisplayControls(false);
            InitializeCallbacks();

            this.alertAudio = alertAudio;
            this.waypointContainer = alertAudio.Waypoints;

            this.Shown += new EventHandler((sender, e) =>
            {
                this.Refresh();
                this.loadExisting = true;
                loading = true;
                txt_Filename.Text = alertAudio.Filename;
                loading = false;
            });

            FillImportComboBox();
        }

        private void InitializeCallbacks()
        {
            txt_Filename.TextChanged += TestAudioFile;

            btn_FilenameBrowse.Click += btn_FilenameBrowse_Click;
            btn_PlayPause.Click += btn_PlayPause_Click;

            mtxt_Delay.TextChanged += UpdateDelay;
            mtxt_PlayFor.TextChanged += UpdatePlayFor;

            mtxt_StartAt.TextChanged += UpdateTimestamps;
            trk_StartAt.ValueChanged += UpdateTimestamps;

            mtxt_LoopAt.TextChanged += UpdateTimestamps;
            trk_LoopAt.ValueChanged += UpdateTimestamps;

            trk_Volume.ValueChanged += UpdateVolume;
            trk_Balance.ValueChanged += UpdateBalance;

            mtxt_PreviewPosition.TextChanged += UpdatePreview;
            trk_Position.ValueChanged += UpdatePreview;

            dg_Waypoints.Columns["col_Presets"].Visible = false;
            dg_Waypoints.Columns["xDataGridViewTextBoxColumn"].Visible = false;
            dg_Waypoints.Columns["yDataGridViewTextBoxColumn"].Visible = false;
            dg_Waypoints.Columns["transitionTimeDataGridViewTextBoxColumn"].Visible = false;
            dg_Waypoints.Columns["behindImageDataGridViewCheckBoxColumn"].Visible = false;
            dg_Waypoints.Columns["readMessageDataGridViewCheckBoxColumn"].Visible = false;
            dg_Waypoints.Columns["persistTimeDataGridViewTextBoxColumn"].HeaderText = "Waypoint Duration";
            dg_Waypoints.Columns["persistTimeDataGridViewTextBoxColumn"].ToolTipText = "How long, in seconds, before the next volume change, balance change, or macro begins.";
        }

        private void UpdatePreview (object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                if (sender == mtxt_PreviewPosition)
                {
                    // Then send that to the trackbar and the previewer.
                    TimeSpan positionTime = General.TimeSpanFromString(mtxt_PreviewPosition.Text);
                    trk_Position.Value = Convert.ToInt32(positionTime.TotalMilliseconds);
                    me.Position = positionTime;
                }
                else if (sender == trk_Position)
                {
                    // Then send that to the text field and the previewer.
                    TimeSpan positionTime = new TimeSpan(0, 0, 0, 0, trk_Position.Value);
                    mtxt_PreviewPosition.Text = General.StringFromTimeSpan(positionTime);
                    me.Position = positionTime;
                }
                else
                {
                    // Then just update the trackbar and the text field.
                    TimeSpan positionTime = me.Position;
                    trk_Position.Value = Convert.ToInt32(positionTime.TotalMilliseconds);
                    mtxt_PreviewPosition.Text = General.StringFromTimeSpan(positionTime);
                }

                blocked = false;
            }
        }

        private void btn_FilenameBrowse_Click(object sender, EventArgs e)
        {
            General.openFileDialogBox(txt_Filename, "Audio File Formats|*.aac; *,aiff; *.amr; *.flac; *.m4a; *.mp3; *.mp3; *.ogg; *.pcm; *.raw; *.wav; *.wma|All Files|*.*", 1);
        }

        private void TestAudioFile(object sender, EventArgs e)
        {
            if (!txt_Filename.Focused)
            {
                if (!String.IsNullOrEmpty(txt_Filename.Text) && System.IO.File.Exists(txt_Filename.Text))
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

            mtxt_PlayFor.Text = General.StringFromTimeSpan(mediaDuration);

            trk_StartAt.Maximum = Convert.ToInt32(mediaDuration.TotalMilliseconds);
            trk_LoopAt.Maximum = Convert.ToInt32(mediaDuration.TotalMilliseconds);

            trk_Position.Maximum = Convert.ToInt32(Math.Round(mediaDuration.TotalMilliseconds));
            trk_Position.TickFrequency = trk_Position.Maximum / 25;

            this.alertAudio.Filename = txt_Filename.Text;

            if (this.loadExisting)
            {
                mtxt_Delay.Text = General.StringFromTimeSpan(alertAudio.Delay);
                mtxt_PlayFor.Text = General.StringFromTimeSpan(alertAudio.Duration);
                mtxt_StartAt.Text = General.StringFromTimeSpan(alertAudio.StartingPosition);
                mtxt_LoopAt.Text = General.StringFromTimeSpan(alertAudio.EndingPosition);

                trk_Balance.Value = Convert.ToInt32(alertAudio.Balance * 100.0);
                trk_Volume.Value = Convert.ToInt32(alertAudio.Volume * 10.0);

                this.loadExisting = false;
            }
            else
            {
                mtxt_Delay.Text = "0";
                mtxt_PlayFor.Text = "0";
                mtxt_StartAt.Text = "0";
                mtxt_LoopAt.Text = "0";

                trk_Balance.Value = 0;
                trk_Volume.Value = 0;
            }


            DisplayControls(true);
        }

        private void FailedMediaFile(object sender, EventArgs e)
        {
            MessageBox.Show(null, "The chosen file was not recognized as a valid audio file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            txt_Duration.Text = "";

            DisplayControls(false);
        }

        private void DisplayControls(bool showControls)
        {
            lbl_Delay.Visible = showControls;
            mtxt_Delay.Visible = showControls;

            lbl_PlayFor.Visible = showControls;
            mtxt_PlayFor.Visible = showControls;

            lbl_StartAt.Visible = showControls;
            trk_StartAt.Visible = showControls;
            mtxt_StartAt.Visible = showControls;

            lbl_LoopAt.Visible = showControls;
            trk_LoopAt.Visible = showControls;
            mtxt_LoopAt.Visible = showControls;

            grp_ImagePreview.Visible = showControls;

            trk_Volume.Visible = showControls;
            lbl_Volume.Visible = showControls;

            trk_Balance.Visible = showControls;
            lbl_Balance.Visible = showControls;

            eh_1.Visible = true;
            mediaLoaded = showControls;
        }

        private void btn_PlayPause_Click(object sender, EventArgs e)
        {
            me.Volume = trk_Volume.Value;
            me.Balance = trk_Balance.Value / 100.0;

            if (this.isPlaying)
            {
                TimeSpan position = me.Position;
                me.Stop();

                this.blocked = true;
                trk_Position.Value = Convert.ToInt32(Math.Round(position.TotalMilliseconds));
                mtxt_PreviewPosition.Text = General.StringFromTimeSpan(position);
                this.blocked = false;

                this.isPlaying = false;
            }
            else
            {
                me.Position = General.TimeSpanFromString(mtxt_PreviewPosition.Text);
                me.Play();
                this.isPlaying = true;
            }
        }

        private void UpdateBalance(object sender, EventArgs e)
        {
            lbl_Balance.Text = String.Format("Balance: {0,2}", trk_Balance.Value / 100.0);
            me.Balance = trk_Balance.Value / 100.0;

            if (!loading)
            {
                this.alertAudio.Balance = trk_Balance.Value / 100.0;
            }
        }

        private void UpdateVolume(object sender, EventArgs e)
        {
            lbl_Volume.Text = String.Format("Vol: {0,2}", trk_Volume.Value / 10.0);
            me.Volume = trk_Volume.Value / 1000.0;

            if (!loading)
            {
                this.alertAudio.Volume = trk_Volume.Value / 10.0;
            }
        }

        private void UpdateDelay(object sender, EventArgs e)
        {
            // Assign this to Waypoints[0], or create Waypoints[0] if it doesn't exist.
            if (this.waypointBindingSource.Count == 0)
            {
                this.waypointBindingSource.AddNew();
            }

            (this.waypointBindingSource[0] as Waypoint).TransitionTime = General.TimeSpanFromString(mtxt_Delay.Text).TotalSeconds;
        }

        private void UpdatePlayFor (object sender, EventArgs e)
        {
            this.alertAudio.Duration = General.TimeSpanFromString(mtxt_PlayFor.Text);
        }

        private void UpdateStartAt(object sender, EventArgs e)
        {
            this.alertAudio.StartingPosition = General.TimeSpanFromString(mtxt_StartAt.Text);
        }

        private void UpdateLoopAt(object sender, EventArgs e)
        {
            this.alertAudio.EndingPosition = General.TimeSpanFromString(mtxt_LoopAt.Text);
        }

        private void UpdateTimestamps(object sender, EventArgs e)
        {
            // Make sure that the start_at timestamp is before the loop_at timestamp, update mtxt_ and trk_ at the same time.
            if (!blocked)
            {
                blocked = true;
                bool updateScreenshot = (sender == mtxt_StartAt || sender == trk_StartAt);

                if (sender == trk_StartAt)
                {
                    mtxt_StartAt.Text = General.StringFromTimeSpan(new TimeSpan(0, 0, 0, 0, trk_StartAt.Value));
                }
                else if (sender == trk_LoopAt)
                {
                    mtxt_LoopAt.Text = General.StringFromTimeSpan(new TimeSpan(0, 0, 0, 0, trk_LoopAt.Value));
                }

                if (sender == mtxt_StartAt)
                {
                    General.MaintainBounds(trk_StartAt, mtxt_StartAt);
                }
                else if (sender == mtxt_LoopAt)
                {
                    General.MaintainBounds(trk_LoopAt, mtxt_LoopAt);
                }

                if (trk_LoopAt.Value < trk_StartAt.Value)
                {
                    if (sender == mtxt_LoopAt || sender == trk_LoopAt)
                    {
                        mtxt_StartAt.Text = mtxt_LoopAt.Text;
                        trk_StartAt.Value = trk_LoopAt.Value;
                    }
                    else if (sender == mtxt_StartAt || sender == trk_StartAt)
                    {
                        mtxt_LoopAt.Text = mtxt_StartAt.Text;
                        trk_LoopAt.Value = trk_StartAt.Value;
                    }
                }

                this.alertAudio.StartingPosition = General.TimeSpanFromString(mtxt_StartAt.Text);
                this.alertAudio.EndingPosition = General.TimeSpanFromString(mtxt_LoopAt.Text);
                this.alertAudio.Duration = General.TimeSpanFromString(mtxt_PlayFor.Text);

                blocked = false;
            }
        }
    }
}
