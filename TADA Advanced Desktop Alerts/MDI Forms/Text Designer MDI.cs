using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.MDI_Forms
{
    public partial class Text_Designer_MDI : TADA_Advanced_Desktop_Alerts.MDI_Forms.Waypoint_Designer_MDI
    {
        private TADAAlert mainAlert;
        private TADAAlertText alertText;

        private Boolean blocked = false;

        private Bitmap textPreview;

        internal Text_Designer_MDI(TADAAlertText alertText, TADAAlert parentAlert) : base(parentAlert)
        {
            InitializeComponent();

            this.mainAlert = parentAlert;
            this.alertText = alertText;

            this._waypointContainer = this.alertText.Waypoints;

            // Fonts
            List<String> installedFonts = new List<String>();
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                installedFonts.Add(font.Name);
            }

            cmb_Font.DataSource = installedFonts;

            // Colours
            List<String> namedColours = new List<String>();
            foreach (KnownColor colour in Enum.GetValues(typeof(KnownColor)))
            {
                if (!Color.FromKnownColor(colour).IsSystemColor)
                {
                    namedColours.Add(Enum.GetName(typeof(KnownColor), colour));
                }
            }

            String[] foreColours = new String[namedColours.Count + 1];
            String[] backColours = new String[namedColours.Count + 1];
            String[] outlineColours = new String[namedColours.Count + 1];
            String[] transparentColours = new String[namedColours.Count + 2];

            namedColours.CopyTo(foreColours, 1);
            namedColours.CopyTo(backColours, 1);
            namedColours.CopyTo(outlineColours, 1);
            namedColours.CopyTo(transparentColours, 2);

            foreColours[0] = "Custom";
            backColours[0] = "Custom";
            outlineColours[0] = "Custom";
            transparentColours[0] = "None";
            transparentColours[1] = "Custom";

            cmb_ForeColor.DataSource = foreColours;
            cmb_BackColor.DataSource = backColours;
            cmb_OutlineColor.DataSource = outlineColours;

            // Voices
            List<String> installedVoices = new List<string>();
            using (System.Speech.Synthesis.SpeechSynthesizer talky = new System.Speech.Synthesis.SpeechSynthesizer())
            {
                foreach (System.Speech.Synthesis.InstalledVoice voice in talky.GetInstalledVoices())
                {
                    if (voice.Enabled)
                    {
                        installedVoices.Add(voice.VoiceInfo.Name);
                    }
                }
            }

            cmb_TTSVoice.DataSource = installedVoices;

            tab_AdvancedWaypointControls.TabPages.Remove(tab_Sound);

            // Load existing details.
            LoadExisting();

            numud_PaddingX.Maximum = Math.Floor(numud_TextWidth.Value / 2) - 1;
            numud_PaddingY.Maximum = Math.Floor(numud_TextHeight.Value / 2) - 1;

            InitializeCallbacks();
            GeneratePreview();
        }

        private void InitializeCallbacks()
        {
            txt_DisplayedText.TextChanged += UpdateDisplayedText;

            txt_NarratedText.TextChanged += UpdateNarratedText;

            cmb_Font.TextChanged += UpdateFont;

            numud_FontSize.ValueChanged += UpdateFont;

            numud_OutlineWidth.ValueChanged += UpdateOutline;
            cmb_OutlineColor.TextChanged += UpdateOutlineColor;
            txt_OutlineHex.TextChanged += UpdateOutlineColor;
            btn_OutlinePicker.Click += PickOutlineColor;

            cmb_ForeColor.TextChanged += UpdateForeColor;
            txt_ForeHex.TextChanged += UpdateForeColor;
            btn_ForePicker.Click += PickForeColor;

            cmb_BackColor.TextChanged += UpdateBackColor;
            txt_BackHex.TextChanged += UpdateBackColor;
            btn_BackPicker.Click += PickBackColor;

            numud_TextWidth.ValueChanged += UpdateTextDimensions;
            numud_TextHeight.ValueChanged += UpdateTextDimensions;
            numud_PaddingX.ValueChanged += UpdateTextPadding;
            numud_PaddingY.ValueChanged += UpdateTextPadding;

            txt_BackgroundImage.TextChanged += UpdateBackgroundImage;
            btn_BackgroundImage.Click += btn_BackgroundImage_Click;

            cmb_TTSVoice.TextChanged += UpdateTTS;
            trk_VoiceVolume.ValueChanged += UpdateVolume;
            numud_VoiceVolume.ValueChanged += UpdateVolume;
        }

        private void LoadExisting()
        {
            txt_DisplayedText.Text = this.alertText.Message;
            txt_NarratedText.Text = this.alertText.ReadMessage;

            cmb_Font.Text = this.alertText.ChosenFont.Name;
            numud_FontSize.Value = Convert.ToDecimal(this.alertText.ChosenFont.Size);
            numud_OutlineWidth.Value = Convert.ToDecimal(this.alertText.OutlineWidth);

            if (!String.IsNullOrEmpty(this.alertText.BackgroundImage))
            {
                this.txt_BackgroundImage.Text = this.alertText.BackgroundImage;
            }

            General.AssignNumud(this.numud_TextWidth, this.alertText.Dimensions.Width);
            General.AssignNumud(this.numud_PaddingX, this.alertText.Padding.Width);

            this.cmb_TTSVoice.Text = this.alertText.TTSVoice;

            // Each of these has side effects, like writing these values back to the object, or filling other fields, which is why these are done again.
            General.AssignNumud(this.numud_TextHeight, this.alertText.Dimensions.Height);
            General.AssignNumud(this.numud_PaddingY, this.alertText.Padding.Height);
            txt_ForeHex.Text = General.StringFromColor(this.alertText.Foreground);
            txt_BackHex.Text = General.StringFromColor(this.alertText.Background);
            txt_OutlineHex.Text = General.StringFromColor(this.alertText.Outline);
            this.numud_VoiceVolume.Value = Convert.ToDecimal(this.alertText.Waypoints.InitialVolume);

            Size tempSize = new Size(Convert.ToInt32(this.numud_TextWidth.Value), Convert.ToInt32(this.numud_TextHeight.Value));
            if (tempSize != this.alertText.Dimensions)
            {
                this.alertText.Dimensions = tempSize;
            }

            tempSize = new Size(Convert.ToInt32(this.numud_PaddingX.Value), Convert.ToInt32(this.numud_PaddingY.Value));
            if (tempSize != this.alertText.Padding)
            {
                this.alertText.Padding = tempSize;
            }
        }

        private void GeneratePreview()
        {
            // Draw the preview bitmap using the specified font, colours, and if set, background image.
            // Measure the string length given the selected font and size and such.
            // Then, make a copy of this.backgroundImage at the desired size.
            if (previewBitmap != null)
            {
                previewBitmap.Dispose();
            }

            previewBitmap = General.GenerateMessageBitmap(this.alertText.Foreground,
                                                          this.alertText.Background,
                                                          this.alertText.Outline,
                                                          this.alertText.ChosenFont,
                                                          this.alertText.OutlineWidth,
                                                          this.alertText.Dimensions,
                                                          this.alertText.Padding,
                                                          this.alertText.Message,
                                                          this.alertText.BackgroundImage);

            pb_TextPreview.Image = previewBitmap;
            pb_TextPreview.Update();
            UpdatePreview();
        }

        private void UpdateBackColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                UpdateColor(sender, cmb_BackColor, txt_BackHex);
                General.ParseHexColor(txt_BackHex.Text, out int r, out int g, out int b, out int a);
                this.alertText.Background = Color.FromArgb(a, r, g, b);
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateForeColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                UpdateColor(sender, cmb_ForeColor, txt_ForeHex);
                General.ParseHexColor(txt_ForeHex.Text, out int r, out int g, out int b, out int a);
                this.alertText.Foreground = Color.FromArgb(a, r, g, b);
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateOutlineColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                UpdateColor(sender, cmb_OutlineColor, txt_OutlineHex);
                General.ParseHexColor(txt_OutlineHex.Text, out int r, out int g, out int b, out int a);
                this.alertText.Outline = Color.FromArgb(a, r, g, b);
                blocked = false;
                GeneratePreview();
            }
        }

        private void PickBackColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                this.alertText.Background = General._ColorPicker(cmb_BackColor, txt_BackHex);
                blocked = false;
                GeneratePreview();
            }
        }

        private void PickForeColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                this.alertText.Foreground = General._ColorPicker(cmb_ForeColor, txt_ForeHex);
                blocked = false;
                GeneratePreview();
            }
        }

        private void PickOutlineColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                this.alertText.Outline = General._ColorPicker(cmb_OutlineColor, txt_OutlineHex);
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateColor (object sender, ComboBox c, TextBox hex)
        {
            if (sender is ComboBox)
            {
                // Use the known colour name to fill the numuds and the textbox.
                Color color = Color.FromName(c.Text);
                hex.Text = General.StringFromColor(color);
            }
            else if (sender is TextBox)
            {
                General.ParseHexColor(hex.Text, out int red, out int green, out int blue, out int alpha);
                c.Text = General.GetKnownColorString(Color.FromArgb(alpha, red, green, blue));
            }
        }

        private void UpdateBackgroundImage (object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;

                Bitmap backgroundImage = null;

                // Test the image.  If it's actually an image, keep it.
                try
                {
                    backgroundImage = new Bitmap(txt_BackgroundImage.Text);
                }
                catch (Exception ex) when (ex is System.IO.FileNotFoundException || ex is ArgumentException)
                {
                    txt_BackgroundImage.Text = "";
                }
                finally
                {
                    backgroundImage?.Dispose();
                }

                this.alertText.BackgroundImage = txt_BackgroundImage.Text;
                blocked = false;

                GeneratePreview();
            }
        }

        private void UpdateDisplayedText (object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                this.alertText.Message = txt_DisplayedText.Text;
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateNarratedText (object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                this.alertText.ReadMessage = txt_NarratedText.Text;
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateTextDimensions (object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                this.alertText.Dimensions = new Size(Convert.ToInt32(numud_TextWidth.Value), Convert.ToInt32(numud_TextHeight.Value));
                this.SetItemSize(this.alertText.Dimensions);
                blocked = false;

                numud_PaddingX.Maximum = Math.Floor(numud_TextWidth.Value / 2) - 1;
                numud_PaddingY.Maximum = Math.Floor(numud_TextHeight.Value / 2) - 1;

                GeneratePreview();
            }
        }

        private void UpdateTextPadding (object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                this.alertText.Padding = new Size(Convert.ToInt32(numud_PaddingX.Value), Convert.ToInt32(numud_PaddingY.Value));
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateTTS (object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                this.alertText.TTSVoice = cmb_TTSVoice.Text;
                blocked = false;
            }
        }

        private void UpdateVolume (object sender, EventArgs e)
        {
            if (!blocked)
            {
                if (sender is TrackBar)
                {
                    numud_VoiceVolume.Value = trk_VoiceVolume.Value / 10;
                }
                else
                {
                    trk_VoiceVolume.Value = Convert.ToInt32(numud_VoiceVolume.Value * 10);
                }

                this.alertText.Waypoints.InitialVolume = Convert.ToDouble(numud_VoiceVolume.Value);
            }
        }

        private void UpdateFont (object sender, EventArgs e)
        {
            if (!blocked && cmb_Font.Items.IndexOf(cmb_Font.Text) > -1)
            {
                blocked = true;
                this.alertText.ChosenFont = new Font(cmb_Font.SelectedValue.ToString(), (float)numud_FontSize.Value);
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateOutline (object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                this.alertText.OutlineWidth = (float)numud_OutlineWidth.Value;
                blocked = false;
                GeneratePreview();
            }
        }

        private void btn_BackgroundImage_Click(object sender, EventArgs e)
        {
            General.openFileDialogBox(txt_BackgroundImage, "Image Files (*.gif, *.jpg, *.jpeg, *.png)|*.gif; *.jpg; *.jpeg; *.png", 0);
        }
    }
}
