using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.MDI_Forms
{
    public partial class Image_Designer_MDI : TADA_Advanced_Desktop_Alerts.MDI_Forms.Waypoint_Designer_MDI
    {
        private bool blocked = false;   // This prevents infinite updating loops.
        private bool justLoaded = false;
        private bool playing = false;
        private bool isVideo = false;

        private System.Windows.Controls.MediaElement mediaElement = new System.Windows.Controls.MediaElement();

        private _TADAAlertImage alertImage;

        internal Image_Designer_MDI(_TADAAlertImage alertImage, bool isNew, _TADAAlert parentAlert = null) : base(parentAlert)
        {
            InitializeComponent();

            eh_1.Child = mediaElement;
            mediaElement.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
            mediaElement.MediaOpened += LoadedMediaFile;
            mediaElement.MediaFailed += FailedMediaFile;

            InitializeCallbacks();
            DisplayVideoControls(false, false);

            if (!isNew)
            {
                LoadExisting(this.alertImage);
            }

            this.alertImage = alertImage;
            this.waypointContainer = alertImage.Waypoints;
        }

        private void InitializeCallbacks()
        {
            txt_Filename.TextChanged += TestSuppliedFile;
            btn_FilenameBrowse.Click += btn_FilenameBrowse_Click;

            btn_RestoreDimensions.Click += btn_RestoreDimensions_Click;

            numud_DesiredDimensionsX.ValueChanged += UpdateDesiredSize;
            numud_DesiredDimensionsY.ValueChanged += UpdateDesiredSize;

            trk_Balance.ValueChanged += new EventHandler((sender, e) => { mediaElement.Balance = trk_Balance.Value / 100.0; lbl_Balance.Text = String.Format("Balance: {0,2}", trk_Balance.Value / 100.0); });
            trk_Balance.ValueChanged += UpdateInitialBalance;

            trk_Volume.ValueChanged += new EventHandler((sender, e) => { mediaElement.Volume = trk_Volume.Value / 1000.0; lbl_Volume.Text = String.Format("Volume: {0,2}", trk_Volume.Value / 10.0); });
            trk_Volume.ValueChanged += UpdateInitialVolume;

            btn_PlayPause.Click += btn_PlayPause_Click;

            trk_Position.ValueChanged += UpdatePosition;
            mtxt_PreviewPosition.LostFocus += UpdatePositionTracker;

            trk_StartAt.ValueChanged += UpdateTimestamps;
            mtxt_StartAt.LostFocus += UpdateTimestamps;
            trk_LoopAt.ValueChanged += UpdateTimestamps;
            mtxt_LoopAt.LostFocus += UpdateTimestamps;

            mtxt_PlayFor.LostFocus += UpdateTimestamps;

            dg_Waypoints.CellValueChanged += delegate { UpdatePreview(); };
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
                    trk_StartAt.Value = Convert.ToInt32(General.TimeSpanFromString(mtxt_StartAt.Text).TotalMilliseconds);
                }
                else if (sender == mtxt_LoopAt)
                {
                    trk_LoopAt.Value = Convert.ToInt32(General.TimeSpanFromString(mtxt_LoopAt.Text).TotalMilliseconds);
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
                    updateScreenshot = true;
                }

                if (updateScreenshot)
                {
                    GenerateFreezeFrame();
                }

                blocked = false;
            }
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;

                TimeSpan t = General.TimeSpanFromDecimals(trk_Position.Value);
                mtxt_PreviewPosition.Text = String.Format(General.MASKED_TEXTBOX_MASK, t.Hours, t.Minutes, t.Seconds, t.Milliseconds);

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

                TimeSpan t = TimeSpan.Parse(mtxt_PreviewPosition.Text);

                trk_Position.Value = Convert.ToInt32(Math.Floor(t.TotalMilliseconds));

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

            lbl_PlayFor.Visible = isVideo && showControls;
            mtxt_PlayFor.Visible = isVideo && showControls;

            lbl_StartAt.Visible = isVideo && showControls;
            trk_StartAt.Visible = isVideo && showControls;
            mtxt_StartAt.Visible = isVideo && showControls;

            lbl_LoopAt.Visible = isVideo && showControls;
            trk_LoopAt.Visible = isVideo && showControls;
            mtxt_LoopAt.Visible = isVideo && showControls;
            if (trk_LoopAt.Visible)
            {
                trk_LoopAt.BringToFront();
            }

            mtxt_PreviewPosition.Visible = isVideo && showControls;

            chk_Angle.Checked = chk_Angle.Checked && !isVideo;
            chk_Opacity.Checked = chk_Opacity.Checked && !isVideo;

            chk_Balance.Checked = chk_Balance.Checked && isVideo;
            chk_Volume.Checked = chk_Volume.Checked && isVideo;

            chk_Angle.Enabled = !isVideo;
            chk_Opacity.Enabled = !isVideo;
            chk_Balance.Enabled = isVideo;
            chk_Volume.Enabled = isVideo;

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
                blocked = true;
                int width = mediaElement.NaturalVideoWidth;
                int height = mediaElement.NaturalVideoHeight;

                TimeSpan actualDuration = (mediaElement.NaturalDuration.HasTimeSpan ? mediaElement.NaturalDuration.TimeSpan : new TimeSpan(0));
                txt_Duration.Text = General.DurationFromTimeSpan(actualDuration);

                if (!justLoaded)
                {
                    mtxt_PlayFor.Text = General.StringFromTimeSpan(actualDuration);
                }

                mtxt_PreviewPosition.Text = String.Format(General.MASKED_TEXTBOX_MASK, 0, 0, 0, 0);

                numud_ActualDimensionsX.Value = width;
                numud_ActualDimensionsY.Value = height;

                if (!justLoaded)
                {
                    numud_DesiredDimensionsX.Value = width;
                    numud_DesiredDimensionsY.Value = height;
                }

                SetItemSize(width, height);
                GenerateFreezeFrame();

                trk_Volume.Maximum = (mediaElement.HasAudio ? 1000 : 0);
                trk_Position.Maximum = Convert.ToInt32(Math.Floor(actualDuration.TotalMilliseconds));
                trk_Position.Value = 0;

                trk_StartAt.Maximum = Convert.ToInt32(Math.Floor(actualDuration.TotalMilliseconds));
                trk_StartAt.TickFrequency = 60;
                trk_StartAt.SmallChange = 1000;
                trk_StartAt.LargeChange = 60000;

                trk_LoopAt.Maximum = Convert.ToInt32(Math.Floor(actualDuration.TotalMilliseconds));
                trk_LoopAt.TickFrequency = 60;
                trk_LoopAt.SmallChange = 1000;
                trk_LoopAt.LargeChange = 60000;

                playing = false;
                justLoaded = false;

                blocked = false;
                UpdatePreview();
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

        private void GenerateFreezeFrame()
        {
            TimeSpan currentTime = mediaElement.Position;
            TimeSpan tempTime = TimeSpan.Parse(mtxt_StartAt.Text);

            bool wasPlaying = playing;
            if (wasPlaying)
            {
                mediaElement.Pause();
            }

            mediaElement.Position = tempTime;
            mediaElement.Play();
            System.Threading.Thread.Sleep(500);
            mediaElement.Pause();

            System.Windows.Media.Imaging.RenderTargetBitmap renderTarget = new System.Windows.Media.Imaging.RenderTargetBitmap(pb_ImagePreview.Width, pb_ImagePreview.Height, 96, 96, System.Windows.Media.PixelFormats.Pbgra32);
            renderTarget.Render(mediaElement);
            System.IO.MemoryStream bitmapStream = new System.IO.MemoryStream();
            System.Windows.Media.Imaging.BitmapEncoder bitmapEncoder = new System.Windows.Media.Imaging.BmpBitmapEncoder();
            bitmapEncoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(renderTarget));
            bitmapEncoder.Save(bitmapStream);

            mediaElement.Position = currentTime;
            mediaElement.Play();
            System.Threading.Thread.Sleep(1);
            mediaElement.Pause();

            if (wasPlaying)
            {
                mediaElement.Play();
            }

            if (previewBitmap != null)
            {
                previewBitmap.Dispose();
            }

            Bitmap tempBitmap = new Bitmap(bitmapStream, true);
            previewBitmap = new Bitmap(tempBitmap, new Size(Convert.ToInt32(numud_DesiredDimensionsX.Value), Convert.ToInt32(numud_DesiredDimensionsY.Value)));
            tempBitmap.Dispose();

            UpdatePreview();
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

                if (!String.IsNullOrEmpty(txt_Filename.Text))
                {
                    this.alertImage.Filename = txt_Filename.Text;
                }

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
            this.alertImage.Dimensions = new Size(Convert.ToInt32(numud_DesiredDimensionsX.Value), Convert.ToInt32(numud_DesiredDimensionsY.Value));
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
                mtxt_PreviewPosition.Text = String.Format(General.MASKED_TEXTBOX_MASK, time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
                blocked = false;
            }
            else
            {
                // This filthy hack ensures that the file is playing (and not stopped) so that its position can be updated.
                mediaElement.Play();
                mediaElement.Pause();
                mediaElement.Position = TimeSpan.Parse(mtxt_PreviewPosition.Text);
                mediaElement.Volume = trk_Volume.Value / 10.0;
                mediaElement.Balance = trk_Balance.Value / 100.0;
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
            TimeSpan duration = (this.isVideo ? TimeSpan.Parse(mtxt_PlayFor.Text) : new TimeSpan(0));
            TimeSpan startingPosition = (this.isVideo ? TimeSpan.Parse(mtxt_StartAt.Text) : new TimeSpan(0));
            TimeSpan endingPosition = (this.isVideo ? TimeSpan.Parse(mtxt_LoopAt.Text) : new TimeSpan(0));

            if (this.alertImage == null)
            {
                if (isVideo)
                {
                    this.alertImage = new _TADAAlertImage(filename,
                                                          dimensions,
                                                          TimeSpan.Parse(mtxt_PlayFor.Text),
                                                          TimeSpan.Parse(mtxt_StartAt.Text),
                                                          TimeSpan.Parse(mtxt_LoopAt.Text),
                                                          waypointContainer);
                }
                else
                {
                    this.alertImage = new _TADAAlertImage(filename,
                                                          Convert.ToInt32(this.numud_AnimatedGifLoop.Value),
                                                          dimensions,
                                                          waypointContainer);
                }

                //this.alertImage = thisAlertImage;
            }
            else
            {
                this.alertImage.Filename = filename;
                this.alertImage.IsVideo = isVideo;
                this.alertImage.AnimationLoops = animationLoops;
                this.alertImage.Dimensions = dimensions;
                this.alertImage.Duration = duration;
                this.alertImage.StartingPosition = startingPosition;
                this.alertImage.EndingPosition = endingPosition;
                this.alertImage.Waypoints = waypointContainer;
            }
        }

        private void LoadExisting(_TADAAlertImage alertImage)
        {
            txt_Filename.Text = alertImage.Filename;
            justLoaded = !String.IsNullOrEmpty(txt_Filename.Text);

            this.isVideo = alertImage.IsVideo;

            numud_AnimatedGifLoop.Value = alertImage.AnimationLoops;

            mtxt_PlayFor.Text = String.Format("{0,3}:{1,2}:{2,2}.{3,3}", alertImage.Duration.Hours, alertImage.Duration.Minutes, alertImage.Duration.Seconds, alertImage.Duration.Milliseconds);
            mtxt_StartAt.Text = String.Format("{0,3}:{1,2}:{2,2}.{3,3}", alertImage.StartingPosition.Hours, alertImage.StartingPosition.Minutes, alertImage.StartingPosition.Seconds, alertImage.StartingPosition.Milliseconds);
            mtxt_LoopAt.Text = String.Format("{0,3}:{1,2}:{2,2}.{3,3}", alertImage.EndingPosition.Hours, alertImage.EndingPosition.Minutes, alertImage.EndingPosition.Seconds, alertImage.EndingPosition.Milliseconds);

            numud_DesiredDimensionsX.Value = alertImage.Dimensions.Width;
            numud_DesiredDimensionsY.Value = alertImage.Dimensions.Height;

            trk_Balance.Value = Convert.ToInt32(alertImage.Waypoints.InitialBalance * 100.0);
            trk_Volume.Value = Convert.ToInt32(alertImage.Waypoints.InitialVolume * 10.0);
        }

        private void TestImage(object sender, EventArgs e)
        {
            GenerateAlertImage();

            MainTADAForm.mainTADAForm.AnimationWindow.Show();

            if (ValidityChecks())
            {
                throw new NotImplementedException("PlayingAlert needs to be converted to use _TADAAlert.  Or PlayingAlert needs a rewrite too.");
                //PlayingAlert playingAlert = new PlayingAlert(this.alertImage, null, null, null);
                //MainTADAForm.mainTADAForm.AnimationWindow.AddPlayingAlert(playingAlert, true);
            }
        }

        private void btn_Complete_Click(object sender, EventArgs e)
        {
            if (ValidityChecks())
            {
                this.GenerateAlertImage();
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

            // This case shouldn't be possible with error checking on the two textboxes instead of requiring parsing numuds every edit.
            if (this.isVideo &&
                TimeSpan.Parse(mtxt_StartAt.Text).Ticks > TimeSpan.Parse(mtxt_LoopAt.Text).Ticks)
            {
                MessageBox.Show(null, "Loop At time is before Start At time.", "Error - Temporal Anomaly Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (this.isVideo &&
                TimeSpan.Parse(mtxt_StartAt.Text).Ticks == TimeSpan.Parse(mtxt_LoopAt.Text).Ticks)
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
