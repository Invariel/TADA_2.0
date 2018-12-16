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
        private _TADAAlert mainAlert;
        private _TADAAlertText alertText;

        private Boolean blocked = false;

        private Bitmap backgroundImage;
        private Bitmap textPreview;

        internal Text_Designer_MDI(_TADAAlertText alertText, _TADAAlert parentAlert) : base(parentAlert)
        {
            InitializeComponent();

            this.mainAlert = parentAlert;
            this.alertText = alertText;

            this._waypointContainer = this.alertText.Waypoints;
            // Load existing details.

            InitializeCallbacks();
            GeneratePreview();
        }

        private void InitializeCallbacks()
        {
            txt_DisplayedText.TextChanged += UpdateDisplayedText;

            txt_NarratedText.TextChanged += UpdateNarratedText;

            cmb_Font.TextChanged += UpdateFont;

            numud_FontSize.ValueChanged += UpdateFont;

            numud_Outline.ValueChanged += UpdateOutline;
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

            cmb_TTSVoice.TextChanged += UpdateTTS;
            trk_VoiceVolume.ValueChanged += UpdateVolume;
            numud_VoiceVolume.ValueChanged += UpdateVolume;
        }

        private void GeneratePreview()
        {
            // Draw the preview bitmap using the specified font, colours, and if set, background image.
            // Measure the string length given the selected font and size and such.
            // Then, make a copy of this.backgroundImage at the desired size.

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
            if (!blocked)
            {
                blocked = true;

                if (sender is ComboBox)
                {
                    // Use the known colour name to fill the numuds and the textbox.
                    Color color = Color.FromName(c.Text);
                    hex.Text = String.Format(General.COLOR_FORMAT, color.R, color.G, color.B, color.A);
                }
                else if (sender is TextBox)
                {
                    General.ParseHexColor(hex.Text, out int red, out int green, out int blue, out int alpha);
                    c.Text = General.GetKnownColorString(Color.FromArgb(alpha, red, green, blue));
                }

                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateBackgroundImage (object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;

                // Unload any existing image.
                if (this.backgroundImage != null)
                {
                    this.backgroundImage.Dispose();
                }

                // Test the image.  If it's actually an image, keep it.
                try
                {
                    this.backgroundImage = new Bitmap(txt_BackgroundImage.Text);

                }
                catch (Exception ex) when (ex is System.IO.FileNotFoundException || ex is ArgumentException)
                {
                    txt_BackgroundImage.Text = "";
                }

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
                blocked = false;
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
            if (!blocked)
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
                this.alertText.OutlineWidth = (float)numud_Outline.Value;
                blocked = false;
                GeneratePreview();
            }
        }
    }
}
