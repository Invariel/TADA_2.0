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
        internal TADAAlertImage alertImage;

        private bool blocked = false;   // This prevents infinite updating loops.
        private bool loading = false;

        private bool justLoaded = false;
        private bool playing = false;
        private bool trackbarScrolling = false;
        private bool isVideo = false;

        private System.Windows.Controls.MediaElement mediaElement = new System.Windows.Controls.MediaElement();

        ~Image_Designer_MDI()
        {
            mediaElement.Stop();
            eh_1.Dispose();
        }

        internal Image_Designer_MDI(TADAAlertImage alertImage, TADAAlert parentAlert = null) : base(parentAlert)
        {
            InitializeComponent();

            eh_1.Child = mediaElement;
            mediaElement.LoadedBehavior = System.Windows.Controls.MediaState.Manual;

            InitializeCallbacks();
            DisplayVideoControls(false, false);

            this.alertImage = alertImage;
            this.waypointContainer = alertImage.Waypoints;

            // Even if alertImage is new, things should be initialized inside it.
            loading = true;
            LoadExisting(this.alertImage);

            UpdateVolume(null, null);
            UpdateBalance(null, null);
            loading = false;

            FillImportComboBox();
            UpdateWarning();
        }

        private void InitializeCallbacks()
        {
            txt_Filename.TextChanged += TestSuppliedFile;
            mediaElement.MediaOpened += LoadedMediaFile;
            mediaElement.MediaFailed += FailedMediaFile;

            btn_FilenameBrowse.Click += btn_FilenameBrowse_Click;

            btn_RestoreDimensions.Click += btn_RestoreDimensions_Click;

            numud_DesiredDimensionsX.ValueChanged += UpdateDesiredSize;
            numud_DesiredDimensionsY.ValueChanged += UpdateDesiredSize;

            trk_Balance.ValueChanged += UpdateBalance;
            trk_Balance.ValueChanged += UpdateInitialBalance;

            trk_Volume.ValueChanged += UpdateVolume;
            trk_Volume.ValueChanged += UpdateInitialVolume;

            btn_PlayPause.Click += btn_PlayPause_Click;

            trk_Position.MouseDown += DisablePlayback;
            trk_Position.MouseUp += EnablePlayback;
            trk_Position.ValueChanged += UpdatePosition;
            mtxt_PreviewPosition.LostFocus += UpdatePositionTracker;

            trk_StartAt.ValueChanged += UpdateTimestamps;
            mtxt_StartAt.LostFocus += UpdateTimestamps;
            trk_LoopAt.ValueChanged += UpdateTimestamps;
            mtxt_LoopAt.LostFocus += UpdateTimestamps;

            mtxt_PlayFor.LostFocus += UpdateTimestamps;

            numud_AnimatedGifLoop.ValueChanged += UpdateLoops;

            dg_Waypoints.CellValueChanged += delegate { UpdatePreview(); };
        }

        private void UpdateLoops (object sender, EventArgs e)
        {
            if (!blocked && !loading)
            {
                this.alertImage.AnimationLoops = Convert.ToInt32(numud_AnimatedGifLoop.Value);
            }
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
                    updateScreenshot = true;
                }

                this.alertImage.StartingPosition = General.TimeSpanFromString(mtxt_StartAt.Text);
                this.alertImage.EndingPosition = General.TimeSpanFromString(mtxt_LoopAt.Text);
                this.alertImage.Duration = General.TimeSpanFromString(mtxt_PlayFor.Text);

                if (updateScreenshot)
                {
                    GenerateFreezeFrame();
                }

                blocked = false;
            }

            UpdateWarning();
        }

        private void UpdateBalance(object sender, EventArgs e)
        {
            mediaElement.Balance = trk_Balance.Value / 100.0;
            lbl_Balance.Text = String.Format("Balance: {0,2}", trk_Balance.Value / 100.0);
        }

        private void UpdateVolume(object sender, EventArgs e)
        {
            mediaElement.Volume = trk_Volume.Value / 1000.0;
            lbl_Volume.Text = String.Format("Volume: {0,2}", trk_Volume.Value / 10.0);
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

        private void DisablePlayback(object sender, EventArgs e)
        {
            if (playing)
            {
                playing = false;
                trackbarScrolling = true;
                mediaElement.Pause();
            }
        }

        private void EnablePlayback(object sender, EventArgs e)
        {
            if (trackbarScrolling)
            {
                playing = true;
                trackbarScrolling = false;
                mediaElement.Position = new TimeSpan(0, 0, 0, 0, trk_Position.Value);
                mediaElement.Play();
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

            numud_InitialAngle.Enabled = !isVideo;
            numud_InitialOpacity.Enabled = !isVideo;

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

                trk_Volume.Maximum = (mediaElement.HasAudio ? 1000 : 0);
                trk_Position.Maximum = Convert.ToInt32(Math.Floor(actualDuration.TotalMilliseconds));
                trk_Position.Value = 0;

                trk_StartAt.Maximum = Convert.ToInt32(Math.Floor(actualDuration.TotalMilliseconds));
                trk_StartAt.TickFrequency = 60;
                trk_StartAt.SmallChange = 1000;
                trk_StartAt.LargeChange = 60000;

                trk_LoopAt.Maximum = Convert.ToInt32(Math.Floor(actualDuration.TotalMilliseconds));
                trk_LoopAt.Value = trk_LoopAt.Maximum;
                trk_LoopAt.TickFrequency = 60;
                trk_LoopAt.SmallChange = 1000;
                trk_LoopAt.LargeChange = 60000;

                if (!justLoaded)
                {
                    numud_DesiredDimensionsX.Value = width;
                    numud_DesiredDimensionsY.Value = height;

                    blocked = false;
                    UpdateTimestamps(trk_LoopAt, null);
                    blocked = true;
                }
                else
                {
                    // Unblocked because I want the side-effect of setting the sliders.
                    mtxt_PlayFor.Text = General.StringFromTimeSpan(alertImage.Duration);
                    mtxt_StartAt.Text = General.StringFromTimeSpan(alertImage.StartingPosition);
                    mtxt_LoopAt.Text = General.StringFromTimeSpan(alertImage.EndingPosition);

                    blocked = false;
                    UpdateTimestamps(mtxt_PlayFor, null);
                    UpdateTimestamps(mtxt_StartAt, null);
                    UpdateTimestamps(mtxt_LoopAt, null);
                    blocked = true;
                }

                SetItemSize(numud_DesiredDimensionsX.Value, numud_DesiredDimensionsY.Value);

                playing = false;
                justLoaded = false;

                this.isVideo = true;
                this.alertImage.IsVideo = true;

                blocked = false;

                GenerateFreezeFrame();
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

                SetItemSize(0f, 0f);
                DisplayVideoControls(false, false);

                this.isVideo = false;
                this.alertImage.IsVideo = false;
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

            SetItemSize(0f, 0f);
            DisplayVideoControls(false, false);

            this.alertImage.IsVideo = false;
            blocked = false;
        }

        private void UpdateWarning()
        {
            lbl_Warning.Text = this.alertImage.Warnings;
        }

        private void TestSuppliedFile(object sender, EventArgs e)
        {
            // When txt_Filename changes, check to see if it is a video or a gif, check to see if it is animated,
            // call DisplayVideoControls with the appropriate isVideo argument.
            if (blocked || txt_Filename.Focused) return;

            String filename = (sender as TextBox).Text;
            if (!String.IsNullOrEmpty(filename))
            {
                if (System.IO.File.Exists (filename))
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

                    if (!String.IsNullOrEmpty(txt_Filename.Text) && !loading)
                    {
                        this.alertImage.Filename = txt_Filename.Text;
                    }

                    DisplayVideoControls(isVideo);
                }
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
            if (!blocked)
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
                if (!loading)
                {
                    this.alertImage.Dimensions = new Size(Convert.ToInt32(numud_DesiredDimensionsX.Value), Convert.ToInt32(numud_DesiredDimensionsY.Value));
                }
                UpdatePreview();
            }
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

        private void LoadExisting(TADAAlertImage alertImage)
        {
            txt_Filename.Text = alertImage.Filename;
            justLoaded = !String.IsNullOrEmpty(txt_Filename.Text);

            numud_AnimatedGifLoop.Value = alertImage.AnimationLoops;

            this.blocked = true;
            numud_DesiredDimensionsX.Value = Math.Max(1, alertImage.Dimensions.Width);
            numud_DesiredDimensionsY.Value = Math.Max(1, alertImage.Dimensions.Height);

            trk_Balance.Value = Convert.ToInt32(alertImage.Waypoints.InitialBalance / 100.0);
            trk_Volume.Value = Convert.ToInt32(alertImage.Waypoints.InitialVolume / 10.0);
            this.blocked = false;
        }
    }
}
