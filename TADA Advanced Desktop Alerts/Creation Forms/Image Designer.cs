using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.Creation_Forms
{
    public partial class ImageCreationForm : TADA_Advanced_Desktop_Alerts.Creation_Forms.MainWaypointForm
    {
        private bool blocked = false;   // This prevents infinite updating loops.
        private bool justLoaded = false;
        private bool playing = false;
        private bool isVideo = false;

        private Bitmap previewBitmap;

        private System.Windows.Controls.MediaElement mediaElement = new System.Windows.Controls.MediaElement();

        private TADAAlertImage alertImage;

        public ImageCreationForm(TADAAnimationWindow animationWindow, TADAAlertImage alertImage)
        {
            InitializeComponent();

            SetAnimationWindow(animationWindow);
            this.alertImage = alertImage;

            eh_1.Child = mediaElement;
            mediaElement.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
            mediaElement.MediaOpened += LoadedMediaFile;
            mediaElement.MediaFailed += FailedMediaFile;

            btn_TestItem.Click += TestImage;
            btn_Complete.Click += btn_Complete_Click;

            InitializeCallbacks();
            DisplayVideoControls(false, false);

            if (this.alertImage != null)
            {
                LoadExisting(this.alertImage);
            }
        }

        private void InitializeCallbacks()
        {
            txt_Filename.TextChanged += TestSuppliedFile;
            btn_FilenameBrowse.Click += btn_FilenameBrowse_Click;

            btn_RestoreDimensions.Click += btn_RestoreDimensions_Click;

            numud_DesiredDimensionsX.ValueChanged += UpdateDesiredSize;
            numud_DesiredDimensionsY.ValueChanged += UpdateDesiredSize;

            trk_Volume.ValueChanged += new EventHandler((sender, e) => { mediaElement.Volume = trk_Volume.Value / 1000.0; lbl_Volume.Text = String.Format("Vol: {0,2}", trk_Volume.Value / 10.0); });
            trk_Balance.ValueChanged += new EventHandler((sender, e) => { mediaElement.Balance = trk_Balance.Value / 100.0; lbl_Balance.Text = String.Format("Balance: {0,2}", trk_Balance.Value / 100.0); });
            btn_PlayPause.Click += btn_PlayPause_Click;

            trk_Position.ValueChanged += UpdatePosition;
            numud_PositionH.ValueChanged += UpdatePositionTracker;
            numud_PositionM.ValueChanged += UpdatePositionTracker;
            numud_PositionS.ValueChanged += UpdatePositionTracker;
            numud_PositionMS.ValueChanged += UpdatePositionTracker;
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;

                TimeSpan t = General.TimeSpanFromDecimals(trk_Position.Value);
                General.FillNumudsFromTimeSpan(t, numud_PositionH, numud_PositionM, numud_PositionS, numud_PositionMS);

                if (playing)
                {
                    mediaElement.Pause();
                    mediaElement.Position = t;
                    mediaElement.Play();
                }

                blocked = false;
            }
        }

        private void UpdatePositionTracker(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;

                TimeSpan t = General.TimeSpanFromDecimals(numud_PositionMS.Value, numud_PositionS.Value, numud_PositionM.Value, numud_PositionH.Value);
                trk_Position.Value = Convert.ToInt32(Math.Floor (t.TotalMilliseconds));

                if (playing)
                {
                    mediaElement.Pause();
                    mediaElement.Position = t;
                    mediaElement.Play();
                }

                blocked = false;
            }
        }

        private void DisplayVideoControls(bool isVideo, bool showControls = true)
        {
            this.isVideo = isVideo;

            lbl_AnimatedGifLoop.Visible = !isVideo && showControls;
            numud_AnimatedGifLoop.Visible = !isVideo && showControls;
            lbl_AnimatedGifTimes.Visible = !isVideo && showControls;

            pb_ImagePreview.Visible = !isVideo && showControls;

            numud_DelayH.Visible = isVideo && showControls;
            numud_DelayM.Visible = isVideo && showControls;
            numud_DelayS.Visible = isVideo && showControls;
            numud_DelayMS.Visible = isVideo && showControls;
            lbl_Delay.Visible = isVideo && showControls;
            lbl_DelayColonHM.Visible = isVideo && showControls;
            lbl_DelayColonMS.Visible = isVideo && showControls;
            lbl_DelayPeriodSM.Visible = isVideo && showControls;

            numud_PlayForH.Visible = isVideo && showControls;
            numud_PlayForM.Visible = isVideo && showControls;
            numud_PlayForS.Visible = isVideo && showControls;
            numud_PlayForMS.Visible = isVideo && showControls;
            lbl_PlayFor.Visible = isVideo && showControls;
            lbl_PlayForColonHM.Visible = isVideo && showControls;
            lbl_PlayForColonMS.Visible = isVideo && showControls;
            lbl_PlayForPeriodSM.Visible = isVideo && showControls;

            numud_StartAtH.Visible = isVideo && showControls;
            numud_StartAtM.Visible = isVideo && showControls;
            numud_StartAtS.Visible = isVideo && showControls;
            numud_StartAtMS.Visible = isVideo && showControls;
            lbl_StartAt.Visible = isVideo && showControls;
            lbl_StartAtColonHM.Visible = isVideo && showControls;
            lbl_StartAtColonMS.Visible = isVideo && showControls;
            lbl_StartAtPeriodSM.Visible = isVideo && showControls;

            numud_LoopAtH.Visible = isVideo && showControls;
            numud_LoopAtM.Visible = isVideo && showControls;
            numud_LoopAtS.Visible = isVideo && showControls;
            numud_LoopAtMS.Visible = isVideo && showControls;
            lbl_LoopAt.Visible = isVideo && showControls;
            lbl_LoopAtColonHM.Visible = isVideo && showControls;
            lbl_LoopAtColonMS.Visible = isVideo && showControls;
            lbl_LoopAtPeriodSM.Visible = isVideo && showControls;

            numud_PositionH.Visible = isVideo && showControls;
            numud_PositionM.Visible = isVideo && showControls;
            numud_PositionS.Visible = isVideo && showControls;
            numud_PositionMS.Visible = isVideo && showControls;
            lbl_Position.Visible = isVideo && showControls;
            lbl_PositionColonHM.Visible = isVideo && showControls;
            lbl_PositionColonMS.Visible = isVideo && showControls;
            lbl_PositionPeriodSM.Visible = isVideo && showControls;

            chk_ChangeAngle.Checked = chk_ChangeAngle.Checked && !isVideo;
            chk_ChangeOpacity.Checked = chk_ChangeOpacity.Checked && !isVideo;

            chk_ChangeBalance.Checked = chk_ChangeBalance.Checked && isVideo;
            chk_ChangeVolume.Checked = chk_ChangeVolume.Checked && isVideo;

            chk_ChangeAngle.Enabled = !isVideo;
            chk_ChangeOpacity.Enabled = !isVideo;
            chk_ChangeBalance.Enabled = isVideo;
            chk_ChangeVolume.Enabled = isVideo;

            grp_ImagePreview.Visible = isVideo && showControls;
            trk_Position.Visible = isVideo && showControls;
            btn_PlayPause.Visible = isVideo && showControls;

            trk_Volume.Visible = isVideo && showControls;
            lbl_Volume.Visible = isVideo && showControls;

            trk_Balance.Visible = isVideo && showControls;
            lbl_Balance.Visible = isVideo && showControls;

            eh_1.Visible = isVideo && showControls;

            if (!isVideo && showControls && playing)
            {
                mediaElement.Stop();
                playing = false;
            }
        }

        private void LoadedMediaFile(object sender, EventArgs e)
        {
            if (mediaElement.HasVideo)
            {
                int width = mediaElement.NaturalVideoWidth;
                int height = mediaElement.NaturalVideoHeight;

                TimeSpan actualDuration = (mediaElement.NaturalDuration.HasTimeSpan ? mediaElement.NaturalDuration.TimeSpan : new TimeSpan(0));
                txt_Duration.Text = General.DurationFromTimeSpan(actualDuration);

                if (!justLoaded)
                {
                    General.FillNumudsFromTimeSpan(actualDuration, numud_PlayForH, numud_PlayForM, numud_PlayForS, numud_PlayForMS);
                }

                General.FillNumudsFromTimeSpan(new TimeSpan(0), numud_PositionH, numud_PositionM, numud_PositionS, numud_PositionMS);

                numud_ActualDimensionsX.Value = width;
                numud_ActualDimensionsY.Value = height;

                if (!justLoaded)
                {
                    numud_DesiredDimensionsX.Value = width;
                    numud_DesiredDimensionsY.Value = height;
                }

                SetItemSize(width, height);

                trk_Volume.Maximum = (mediaElement.HasAudio ? 1000 : 0);
                trk_Position.Maximum = Convert.ToInt32(Math.Floor(actualDuration.TotalMilliseconds));
                trk_Position.Value = 0;

                playing = false;
                justLoaded = false;
            }
            else
            {
                MessageBox.Show(null, "This appears to be a valid media file but it does not have video.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                blocked = true;

                txt_Duration.Text = "";
                numud_ActualDimensionsX.Value = 0;
                numud_ActualDimensionsY.Value = 0;
                numud_DesiredDimensionsX.Value = 0;
                numud_DesiredDimensionsY.Value = 0;

                SetItemSize();
                DisplayVideoControls(false, false);

                blocked = false;
            }
        }

        private void FailedMediaFile(object sender, EventArgs e)
        {
            MessageBox.Show(null, "The file failed to be recognized as a media file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            blocked = true;

            txt_Duration.Text = "";
            numud_ActualDimensionsX.Value = 0;
            numud_ActualDimensionsY.Value = 0;
            numud_DesiredDimensionsX.Value = 0;
            numud_DesiredDimensionsY.Value = 0;

            SetItemSize();
            DisplayVideoControls(false, false);

            blocked = false;
        }

        private void TestSuppliedFile(object sender, EventArgs e)
        {
            // When txt_Filename changes, check to see if it is a video or a gif, check to see if it is animated,
            // call DisplayVideoControls with the appropriate isVideo argument.
            if (blocked || txt_Filename.Focused) return;

            String filename = (sender as TextBox).Text;
            if (!String.IsNullOrEmpty(filename))
            {
                int duration = 0;
                bool isVideo = false;
                try
                {
                    if (previewBitmap != null)
                    {
                        previewBitmap.Dispose();
                    }

                    previewBitmap = new Bitmap(filename);

                    if (ImageAnimator.CanAnimate(previewBitmap))   // Then it's an animated gif.
                    {
                        General.GetImageDuration(previewBitmap, out int[] timing, out duration);
                    }
                    else    // It's not an animated gif.
                    {
                        duration = 0;   // Reinforce this.
                    }

                    numud_ActualDimensionsX.Value = previewBitmap.Width;
                    numud_ActualDimensionsY.Value = previewBitmap.Height;
                    numud_DesiredDimensionsX.Value = previewBitmap.Width;
                    numud_DesiredDimensionsY.Value = previewBitmap.Height;

                    pb_ImagePreview.Image = previewBitmap;
                    pb_ImagePreview.SizeMode = PictureBoxSizeMode.Zoom;
                    pb_ImagePreview.Update();
                }
                catch (System.IO.FileNotFoundException) // The file doesn't exist in the first place.
                {
                    blocked = true;
                    txt_Filename.Text = "";
                    DisplayVideoControls(false, false);
                    blocked = false;
                }
                catch (ArgumentException) // Apparently trying to load an invalid Bitmap causes an ArgumentException.  I am okay with this.
                {
                    // So try the file as a video.
                    isVideo = true;

                    try
                    {
                        mediaElement.Source = new Uri(filename);
                        mediaElement.Play();
                        mediaElement.Stop();
                    }
                    catch (UriFormatException)
                    {
                        // The file doesn't exist in the first place.  Which -should- be caught as the FileNotFound Exception above.
                        blocked = true;
                        txt_Filename.Text = "";
                        DisplayVideoControls(false, false);
                        blocked = false;
                    }
                }

                txt_Duration.Text = General.DurationFromTimeSpan(new TimeSpan(0, 0, 0, 0, duration));

                DisplayVideoControls(isVideo);
            }
        }

        private void btn_RestoreDimensions_Click(object sender, EventArgs e)
        {
            this.blocked = true;
            numud_DesiredDimensionsX.Value = numud_ActualDimensionsX.Value;
            numud_DesiredDimensionsY.Value = numud_DesiredDimensionsY.Value;
            this.blocked = false;
        }

        private void UpdateDesiredSize(object sender, EventArgs e)
        {
            if (chk_AspectRatio.Checked)
            {
                // Then, sender matters as the other value has to be calculated.
                Decimal ratio = numud_ActualDimensionsX.Value / numud_ActualDimensionsY.Value;

                if (sender == numud_DesiredDimensionsX)
                {
                    numud_DesiredDimensionsY.Value = numud_DesiredDimensionsX.Value / ratio;
                }
                else
                {
                    numud_DesiredDimensionsX.Value = numud_DesiredDimensionsY.Value * ratio;
                }
            }

            SetItemSize((float)numud_DesiredDimensionsX.Value, (float)numud_DesiredDimensionsY.Value);
            UpdatePreview();
        }

        private void btn_FilenameBrowse_Click(object sender, EventArgs e)
        {
            General.openFileDialogBox(txt_Filename, "Valid Bitmap Files|*.bmp; *.gif; *.exif; *.jpg; *.png; *.tiff|Movie File Formats|*.avi; *.mkv; *.mov; *.mpg; *.mp4; *.wmv|All Files|*.*", 1);
        }

        private void btn_PlayPause_Click(object sender, EventArgs e)
        {
            if (playing)
            {
                playing = false;
                mediaElement.Pause();

                blocked = true;
                TimeSpan time = mediaElement.Position;

                trk_Position.Value = Convert.ToInt32(Math.Floor(time.TotalMilliseconds));
                numud_PositionH.Value = time.Hours;
                numud_PositionM.Value = time.Minutes;
                numud_PositionS.Value = time.Seconds;
                numud_PositionMS.Value = time.Milliseconds;
                blocked = false;
            }
            else
            {
                // This filthy hack ensures that the file is playing (and not stopped) so that its position can be updated.
                mediaElement.Play();
                mediaElement.Pause();
                mediaElement.Position = General.TimeSpanFromDecimals(numud_PositionMS.Value, numud_PositionS.Value, numud_PositionM.Value, numud_PositionH.Value);
                mediaElement.Play();
                playing = true;
            }
        }

        private void GenerateAlertImage()
        {
            WaypointContainer waypointContainer = GetWaypoints();
            String filename = txt_Filename.Text;
            bool isVideo = this.isVideo;
            int animationLoops = Convert.ToInt32(this.numud_AnimatedGifLoop.Value);
            Size dimensions = new Size(Convert.ToInt32(numud_DesiredDimensionsX.Value), Convert.ToInt32(numud_DesiredDimensionsY.Value));
            TimeSpan delay = (this.isVideo ? General.TimeSpanFromDecimals(numud_DelayMS.Value, numud_DelayS.Value, numud_DelayM.Value, numud_DelayH.Value) : new TimeSpan(0));
            TimeSpan duration = (this.isVideo ? General.TimeSpanFromDecimals(numud_PlayForMS.Value, numud_PlayForS.Value, numud_PlayForM.Value, numud_PlayForH.Value) : new TimeSpan(0));
            TimeSpan startingPosition = (this.isVideo ? General.TimeSpanFromDecimals(numud_StartAtMS.Value, numud_StartAtS.Value, numud_StartAtM.Value, numud_StartAtH.Value) : new TimeSpan(0));
            TimeSpan endingPosition = (this.isVideo ? General.TimeSpanFromDecimals(numud_LoopAtMS.Value, numud_LoopAtS.Value, numud_LoopAtM.Value, numud_LoopAtH.Value) : new TimeSpan(0));
            double balance = (this.isVideo ? trk_Balance.Value / 100.0 : 0);
            double volume = (this.isVideo ? trk_Volume.Value / 10 : 0);

            if (this.alertImage == null)
            {
                this.alertImage = new TADAAlertImage(filename, isVideo, animationLoops, dimensions, delay, duration, startingPosition, endingPosition, volume, balance, waypointContainer);
            }
            else
            {
                this.alertImage.Filename = filename;
                this.alertImage.IsVideo = isVideo;
                this.alertImage.AnimationLoops = animationLoops;
                this.alertImage.Dimensions = dimensions;
                this.alertImage.Delay = delay;
                this.alertImage.Duration = duration;
                this.alertImage.StartingPosition = startingPosition;
                this.alertImage.EndingPosition = endingPosition;
                this.alertImage.Balance = balance;
                this.alertImage.Volume = volume;
                this.alertImage.Waypoints = waypointContainer;
            }
        }

        private void LoadExisting(TADAAlertImage alertImage)
        {
            txt_Filename.Text = alertImage.Filename;

            this.isVideo = alertImage.IsVideo;

            numud_AnimatedGifLoop.Value = alertImage.AnimationLoops;

            General.FillNumudsFromTimeSpan(alertImage.Delay, numud_DelayH, numud_DelayM, numud_DelayS, numud_DelayMS);
            General.FillNumudsFromTimeSpan(alertImage.Duration, numud_PlayForH, numud_PlayForM, numud_PlayForS, numud_PlayForMS);
            General.FillNumudsFromTimeSpan(alertImage.StartingPosition, numud_StartAtH, numud_StartAtM, numud_StartAtS, numud_StartAtMS);
            General.FillNumudsFromTimeSpan(alertImage.EndingPosition, numud_LoopAtH, numud_LoopAtM, numud_LoopAtS, numud_LoopAtMS);

            numud_DesiredDimensionsX.Value = alertImage.Dimensions.Width;
            numud_DesiredDimensionsY.Value = alertImage.Dimensions.Height;

            trk_Balance.Value = Convert.ToInt32(alertImage.Balance * 100.0);
            trk_Volume.Value = Convert.ToInt32(alertImage.Volume * 10.0);

            this.SetWaypoints(alertImage.Waypoints);

            justLoaded = !String.IsNullOrEmpty(txt_Filename.Text);
        }

        private void TestImage(object sender, EventArgs e)
        {
            GenerateAlertImage();

            if (this.animationWindow == null)
            {
                this.animationWindow = new TADAAnimationWindow();
            }
            this.animationWindow.Show();

            if (ValidityChecks())
            {
                PlayingAlert playingAlert = new PlayingAlert(this.animationWindow, this.animationWindow.myGraphics, this.alertImage, null, null, null);
                this.animationWindow.playingAlert = playingAlert;
                playingAlert.Play();
            }
        }

        private void btn_Complete_Click(object sender, EventArgs e)
        {
            if (ValidityChecks ())
            {
                this.GenerateAlertImage();
                this.Close();
            }
        }

        private bool ValidityChecks ()
        { 
            // IF everything looks good, then register it.
            bool valid = true;

            if (String.IsNullOrEmpty(txt_Filename.Text))
            {
                MessageBox.Show(null, "You did not specify a file to work with.", "Error - Filename is empty.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (numud_DesiredDimensionsX.Value == 0 && numud_DesiredDimensionsY.Value == 0)
            {
                MessageBox.Show(null,
                                String.Format("{0}{1}{2}",
                                              "Dimensions are set to 0, 0.  This would cause the image to be invisible.",
                                              String.IsNullOrEmpty(txt_Filename.Text) ? Environment.NewLine : "",
                                              String.IsNullOrEmpty(txt_Filename.Text), "If you just want to use the audio from this file (if any), use 'Create Audio' instead."),
                                "Error - Dimensions are 0, 0",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                valid = false;
            }

            if (this.isVideo &&
                General.TimeSpanFromDecimals(numud_StartAtMS.Value, numud_StartAtS.Value, numud_StartAtM.Value, numud_StartAtH.Value, 0).Ticks >
                General.TimeSpanFromDecimals(numud_LoopAtMS.Value, numud_LoopAtS.Value, numud_LoopAtM.Value, numud_LoopAtH.Value, 0).Ticks)
            {
                MessageBox.Show(null, "Loop At time is before Start At time.", "Error - Temporal Anomaly Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (this.isVideo &&
                General.TimeSpanFromDecimals(numud_StartAtMS.Value, numud_StartAtS.Value, numud_StartAtM.Value, numud_StartAtH.Value, 0).Ticks ==
                General.TimeSpanFromDecimals(numud_LoopAtMS.Value, numud_LoopAtS.Value, numud_LoopAtM.Value, numud_LoopAtH.Value, 0).Ticks)
            {
                MessageBox.Show(null, "Loop At time is exactly Start At time, which will make this play forever and not at all.", "Error - Temporal Anomaly Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (dg_Waypoints.Rows[0].IsNewRow)
            {
                MessageBox.Show(null, "You have defined no Waypoints.  Please define at least one Waypoint.", "Error - No Waypoints Defined", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (dg_Waypoints.Rows.Count > 0)
            {
                WaypointContainer w = GetWaypoints();
                if (General.GetTotalWaypointDuration(w) == 0 && !this.isVideo)
                {
                    MessageBox.Show(null, "Your defined Waypoints have a total duration of 0 seconds.", "Error - No Time At All", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valid = false;
                }
            }

            return valid;
        }
    }
}
