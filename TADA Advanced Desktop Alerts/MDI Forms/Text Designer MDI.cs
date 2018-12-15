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
            numud_OutlineA.ValueChanged += UpdateOutlineColor;
            numud_OutlineR.ValueChanged += UpdateOutlineColor;
            numud_OutlineG.ValueChanged += UpdateOutlineColor;
            numud_OutlineB.ValueChanged += UpdateOutlineColor;
            btn_OutlinePicker.Click += PickOutlineColor;

            cmb_ForeColor.TextChanged += UpdateForeColor;
            txt_ForeHex.TextChanged += UpdateForeColor;
            numud_ForeA.ValueChanged += UpdateForeColor;
            numud_ForeR.ValueChanged += UpdateForeColor;
            numud_ForeG.ValueChanged += UpdateForeColor;
            numud_ForeB.ValueChanged += UpdateForeColor;
            btn_ForePicker.Click += PickForeColor;

            cmb_BackColor.TextChanged += UpdateBackColor;
            txt_BackHex.TextChanged += UpdateBackColor;
            numud_BackA.ValueChanged += UpdateBackColor;
            numud_BackR.ValueChanged += UpdateBackColor;
            numud_BackG.ValueChanged += UpdateBackColor;
            numud_BackB.ValueChanged += UpdateBackColor;
            btn_BackPicker.Click += PickBackColor;

            numud_MaxSizeX.ValueChanged += UpdateTextDimensions;
            numud_MaxSizeY.ValueChanged += UpdateTextDimensions;
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
        }

        private void UpdateBackColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                UpdateColor(sender, cmb_BackColor, txt_BackHex, numud_BackR, numud_BackG, numud_BackB, numud_BackA);
                this.alertText.Background = General.ColorFromNumuds(numud_BackR, numud_BackG, numud_BackB, numud_BackA);
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateForeColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                UpdateColor(sender, cmb_ForeColor, txt_ForeHex, numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA);
                this.alertText.Foreground = General.ColorFromNumuds(numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA);
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateOutlineColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                UpdateColor(sender, cmb_OutlineColor, txt_OutlineHex, numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA);
                this.alertText.Outline = General.ColorFromNumuds(numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA);
                blocked = false;
                GeneratePreview();
            }
        }

        private void PickBackColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                General._ColorPicker(cmb_BackColor, txt_BackHex, numud_BackR, numud_BackG, numud_BackB, numud_BackA);
                this.alertText.Background = General.ColorFromNumuds(numud_BackR, numud_BackG, numud_BackB, numud_BackA);
                blocked = false;
                GeneratePreview();
            }
        }

        private void PickForeColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                General._ColorPicker(cmb_ForeColor, txt_ForeHex, numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA);
                this.alertText.Foreground = General.ColorFromNumuds(numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA);
                blocked = false;
                GeneratePreview();
            }
        }

        private void PickOutlineColor(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;
                General._ColorPicker(cmb_OutlineColor, txt_OutlineHex, numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA);
                this.alertText.Outline = General.ColorFromNumuds(numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA);
                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateColor (object sender, ComboBox c, TextBox hex, NumericUpDown r, NumericUpDown g, NumericUpDown b, NumericUpDown a)
        {
            if (!blocked)
            {
                blocked = true;

                if (sender is ComboBox)
                {
                    // Use the known colour name to fill the numuds and the textbox.
                    Color color = Color.FromName(c.Text);
                    r.Value = color.R;
                    g.Value = color.G;
                    b.Value = color.B;
                    a.Value = color.A;

                    hex.Text = String.Format(General.COLOR_FORMAT, color.R, color.G, color.B, color.A);
                }
                else if (sender is TextBox)
                {
                    General.ParseHexColor(hex.Text, out int red, out int green, out int blue, out int alpha);

                    r.Value = red;
                    g.Value = green;
                    b.Value = blue;
                    a.Value = alpha;

                    c.Text = General.GetKnownColorString(Color.FromArgb(alpha, red, green, blue));
                }
                else if (sender is NumericUpDown)
                {
                    int red = Convert.ToInt32(r.Value);
                    int green = Convert.ToInt32(g.Value);
                    int blue = Convert.ToInt32(b.Value);
                    int alpha = Convert.ToInt32(a.Value);

                    Color color = Color.FromArgb(alpha, red, green, blue);
                    c.Text = General.GetKnownColorString(color);

                    hex.Text = String.Format(General.COLOR_FORMAT, red, green, blue, alpha);
                }

                blocked = false;
                GeneratePreview();
            }
        }

        private void UpdateBackgroundImage (object sender, EventArgs e)
        {
            // Test the image.  If it's actually a static image, not an animated gif, keep it.  Maybe keep it if it's an animated gif, but don't animate it.

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
                this.alertText.Dimensions = new Size(Convert.ToInt32(numud_MaxSizeX.Value), Convert.ToInt32(numud_MaxSizeY.Value));
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
