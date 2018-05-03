using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.Creation_Forms
{
    public partial class TextCreationForm : TADA_Advanced_Desktop_Alerts.Creation_Forms.MainWaypointForm
    {
        private Bitmap previewBitmap;

        private bool blocked = false;

        private TADAAlertText alertText;

        private PlayingAlert playingAlert;

        public TextCreationForm(TADAAnimationWindow animationWindow, TADAAlertText alertText, bool isStreamer) : base()
        {
            InitializeComponent();

            SetAnimationWindow(animationWindow);
            this.alertText = alertText;

            if (!isStreamer)
            {
                txt_DisplayedText.Enabled = false;
                txt_NarratedText.Enabled = false;
            }

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
            cmb_TransparentColor.DataSource = transparentColours;

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

            tab_Effects.TabPages.Remove(tab_Balance);
            tab_Effects.TabPages.Remove(tab_Volume);

            ConfigureCallbacks();
            if (this.alertText == null)
            {
                GenerateRandomValues();
            }
            else
            {
                LoadExisting(this.alertText);
            }
        }

        private void GenerateRandomValues()
        {
            Random rng = new Random();
            numud_ForeA.Value = rng.Next(0, 256);
            numud_ForeR.Value = rng.Next(0, 256);
            numud_ForeG.Value = rng.Next(0, 256);
            numud_ForeB.Value = rng.Next(0, 256);

            numud_BackA.Value = rng.Next(0, 256);
            numud_BackR.Value = rng.Next(0, 256);
            numud_BackG.Value = rng.Next(0, 256);
            numud_BackB.Value = rng.Next(0, 256);

            numud_OutlineA.Value = rng.Next(0, 256);
            numud_OutlineR.Value = rng.Next(0, 256);
            numud_OutlineG.Value = rng.Next(0, 256);
            numud_OutlineB.Value = rng.Next(0, 256);

            numud_TransparentA.Value = rng.Next(0, 256);
            numud_TransparentR.Value = rng.Next(0, 256);
            numud_TransparentG.Value = rng.Next(0, 256);
            numud_TransparentB.Value = rng.Next(0, 256);

            numud_Size.Value = rng.Next(12, 37);
            cmb_Font.SelectedIndex = rng.Next(cmb_Font.Items.Count);
        }

        private void ConfigureCallbacks()
        {
            cmb_Font.SelectedValueChanged += UpdateTextPreview;
            numud_Size.ValueChanged += UpdateTextPreview;
            numud_Outline.ValueChanged += UpdateTextPreview;

            cmb_ForeColor.SelectedValueChanged += new EventHandler((sender, e) => { UpdateFields(cmb_ForeColor, cmb_ForeColor, numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA, txt_ForeHex); });
            numud_ForeA.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_ForeA, cmb_ForeColor, numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA, txt_ForeHex); });
            numud_ForeR.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_ForeR, cmb_ForeColor, numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA, txt_ForeHex); });
            numud_ForeG.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_ForeG, cmb_ForeColor, numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA, txt_ForeHex); });
            numud_ForeB.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_ForeB, cmb_ForeColor, numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA, txt_ForeHex); });
            txt_ForeHex.TextChanged += new EventHandler((sender, e) => { UpdateFields(txt_ForeHex, cmb_ForeColor, numud_ForeR, numud_ForeG, numud_ForeB, numud_ForeA, txt_ForeHex); });

            cmb_BackColor.SelectedValueChanged += new EventHandler((sender, e) => { UpdateFields(cmb_BackColor, cmb_BackColor, numud_BackR, numud_BackG, numud_BackB, numud_BackA, txt_BackHex); });
            numud_BackA.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_BackA, cmb_BackColor, numud_BackR, numud_BackG, numud_BackB, numud_BackA, txt_BackHex); });
            numud_BackR.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_BackR, cmb_BackColor, numud_BackR, numud_BackG, numud_BackB, numud_BackA, txt_BackHex); });
            numud_BackG.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_BackG, cmb_BackColor, numud_BackR, numud_BackG, numud_BackB, numud_BackA, txt_BackHex); });
            numud_BackB.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_BackB, cmb_BackColor, numud_BackR, numud_BackG, numud_BackB, numud_BackA, txt_BackHex); });
            txt_BackHex.TextChanged += new EventHandler((sender, e) => { UpdateFields(txt_BackHex, cmb_BackColor, numud_BackR, numud_BackG, numud_BackB, numud_BackA, txt_BackHex); });

            cmb_OutlineColor.SelectedValueChanged += new EventHandler((sender, e) => { UpdateFields(cmb_OutlineColor, cmb_OutlineColor, numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA, txt_OutlineHex); });
            numud_OutlineA.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_OutlineA, cmb_OutlineColor, numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA, txt_OutlineHex); });
            numud_OutlineR.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_OutlineR, cmb_OutlineColor, numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA, txt_OutlineHex); });
            numud_OutlineG.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_OutlineG, cmb_OutlineColor, numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA, txt_OutlineHex); });
            numud_OutlineB.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_OutlineB, cmb_OutlineColor, numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA, txt_OutlineHex); });
            txt_OutlineHex.TextChanged += new EventHandler((sender, e) => { UpdateFields(txt_OutlineHex, cmb_OutlineColor, numud_OutlineR, numud_OutlineG, numud_OutlineB, numud_OutlineA, txt_OutlineHex); });

            cmb_TransparentColor.SelectedValueChanged += new EventHandler((sender, e) => { UpdateFields(cmb_TransparentColor, cmb_TransparentColor, numud_TransparentR, numud_TransparentG, numud_TransparentB, numud_TransparentA, txt_TransparentHex); });
            numud_TransparentA.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_TransparentA, cmb_TransparentColor, numud_TransparentR, numud_TransparentG, numud_TransparentB, numud_TransparentA, txt_TransparentHex); });
            numud_TransparentR.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_TransparentR, cmb_TransparentColor, numud_TransparentR, numud_TransparentG, numud_TransparentB, numud_TransparentA, txt_TransparentHex); });
            numud_TransparentG.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_TransparentG, cmb_TransparentColor, numud_TransparentR, numud_TransparentG, numud_TransparentB, numud_TransparentA, txt_TransparentHex); });
            numud_TransparentB.ValueChanged += new EventHandler((sender, e) => { UpdateFields(numud_TransparentB, cmb_TransparentColor, numud_TransparentR, numud_TransparentG, numud_TransparentB, numud_TransparentA, txt_TransparentHex); });
            txt_TransparentHex.TextChanged += new EventHandler((sender, e) => { UpdateFields(txt_TransparentHex, cmb_TransparentColor, numud_TransparentR, numud_TransparentG, numud_TransparentB, numud_TransparentA, txt_TransparentHex); });

            numud_MaxSizeX.ValueChanged += UpdateSize;
            numud_MaxSizeY.ValueChanged += UpdateSize;

            btn_TestVoice.Click += btn_TestVoice_Click;
            trk_VoiceVolume.ValueChanged += UpdateVolume;
            numud_VoiceVolume.ValueChanged += UpdateVolume;

            btn_TextSubstitution.Click += btn_TextSubstitution_Click;

            btn_TestItem.Click += btn_TestItem_Click;

            btn_Complete.Click += btn_Complete_Click;
        }

        private void UpdateSize(object sender, EventArgs e)
        {
            if (!blocked)
            {
                SetItemSize(new SizeF((float)numud_MaxSizeX.Value, (float)numud_MaxSizeY.Value));
                UpdatePreview();
                UpdateTextPreview();
            }
        }

        private void UpdateVolume(object sender, EventArgs e)
        {
            if (!blocked)
            {
                blocked = true;

                if (sender == trk_VoiceVolume)
                {
                    numud_VoiceVolume.Value = Convert.ToDecimal(trk_VoiceVolume.Value / 10.0);
                }
                else
                {
                    trk_VoiceVolume.Value = Convert.ToInt32(numud_VoiceVolume.Value * 10);
                }

                blocked = false;
            }
        }

        private void UpdateFields(Control updated, ComboBox comboBox, NumericUpDown red, NumericUpDown green, NumericUpDown blue, NumericUpDown alpha, TextBox hex)
        {
            if (!blocked)
            {
                blocked = true;

                if (updated == comboBox)
                {
                    // Then update the four numeric fields, and the hex field.
                    Color color = Color.FromName(comboBox.SelectedValue.ToString());
                    red.Value = color.R;
                    green.Value = color.G;
                    blue.Value = color.B;
                    alpha.Value = color.A;
                    hex.Text = String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color.R, color.G, color.B, color.A);
                }
                else
                {
                    if (updated == hex)
                    {
                        if (hex.Text.Length == 9)
                        {
                            red.Value = Convert.ToInt32(hex.Text.Substring(1, 2), 16);
                            green.Value = Convert.ToInt32(hex.Text.Substring(3, 2), 16);
                            blue.Value = Convert.ToInt32(hex.Text.Substring(5, 2), 16);
                            alpha.Value = Convert.ToInt32(hex.Text.Substring(7, 2), 16);
                        }
                    }
                    else
                    {
                        hex.Text = String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", Convert.ToInt32(red.Value), Convert.ToInt32(green.Value), Convert.ToInt32(blue.Value), Convert.ToInt32(alpha.Value));
                    }

                    int a = Convert.ToInt32(alpha.Value);
                    int r = Convert.ToInt32(red.Value);
                    int g = Convert.ToInt32(green.Value);
                    int b = Convert.ToInt32(blue.Value);

                    bool matched = false;
                    Color testColor = Color.FromArgb(a, r, g, b);
                    foreach (KnownColor knownColor in Enum.GetValues(typeof(KnownColor)))
                    {
                        Color color = Color.FromKnownColor(knownColor);
                        if (color.ToArgb() == testColor.ToArgb())
                        {
                            comboBox.Text = Enum.GetName(typeof(KnownColor), knownColor);
                            matched = true;
                            break;
                        }
                    }

                    if (!matched)
                    {
                        comboBox.Text = "Custom";
                    }
                }

                blocked = false;
            }

            UpdateTextPreview();
        }

        private void UpdateTextPreview(object sender = null, EventArgs e = null)
        {
            Font textFont = new Font(cmb_Font.SelectedValue.ToString(), (float)numud_Size.Value);
            Color foreColor = ColorFromDecimals(numud_ForeA.Value, numud_ForeR.Value, numud_ForeG.Value, numud_ForeB.Value);
            Color backColor = ColorFromDecimals(numud_BackA.Value, numud_BackR.Value, numud_BackG.Value, numud_BackB.Value);
            Color outlineColor = ColorFromDecimals(numud_OutlineA.Value, numud_OutlineR.Value, numud_OutlineG.Value, numud_OutlineB.Value);
            Color transparentColor = ColorFromDecimals(numud_TransparentA.Value, numud_TransparentR.Value, numud_TransparentG.Value, numud_TransparentB.Value);
            float outlineWidth = (float)numud_Outline.Value;
            Size maximumSize = new Size(Convert.ToInt32(numud_MaxSizeX.Value), Convert.ToInt32(numud_MaxSizeY.Value));
            SizeF padding = new SizeF((float)numud_PaddingX.Value, (float)numud_PaddingY.Value);
            String text = "The quick brown fox jumps over the lazy dog.";

            // Create a bitmap.
            if (previewBitmap != null)
            {
                previewBitmap.Dispose();
            }

            previewBitmap = General.GenerateMessageBitmap(foreColor, backColor, outlineColor, transparentColor, textFont, outlineWidth, maximumSize, padding, text);

            pb_TextPreview.Image = previewBitmap;
            pb_TextPreview.Update();
        }

        private Color ColorFromDecimals(Decimal a, Decimal r, Decimal g, Decimal b)
        {
            return Color.FromArgb(Convert.ToInt32(a), Convert.ToInt32(r), Convert.ToInt32(g), Convert.ToInt32(b));
        }

        private void btn_TestVoice_Click(object sender, EventArgs e)
        {
            System.Speech.Synthesis.SpeechSynthesizer talky = new System.Speech.Synthesis.SpeechSynthesizer();
            String voice = cmb_TTSVoice.SelectedValue.ToString();
            System.Speech.Synthesis.PromptBuilder prompt = new System.Speech.Synthesis.PromptBuilder();
            prompt.AppendText(String.Format("Hello, my name is {0} and it is a pleasure to talk to you.", voice));
            try
            {
                talky.SelectVoice(voice);
                talky.Volume = trk_VoiceVolume.Value / 10;
                talky.SpeakAsync(prompt);
            }
            catch (Exception)
            {
                MessageBox.Show(null, "I know you want to use that voice, but it doesn't seem to be set up correctly.  I'm sorry.  You might try reinstalling it?", "Invalid Voice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_TextSubstitution_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TokenSubstitutions.html");
        }

        private void GenerateAlertText()
        {
            String message = txt_DisplayedText.Text;
            String readMessage = txt_NarratedText.Text;
            String ttsVoice = cmb_TTSVoice.SelectedValue?.ToString();
            int ttsVolume = Convert.ToInt32(numud_VoiceVolume.Value);
            Font font = new Font(cmb_Font.SelectedValue.ToString(), Convert.ToInt32(numud_Size.Value));
            float outlineWidth = (float)numud_Outline.Value;
            Color foreground = ColorFromDecimals(numud_ForeA.Value, numud_ForeR.Value, numud_ForeG.Value, numud_ForeB.Value);
            Color background = ColorFromDecimals(numud_BackA.Value, numud_BackR.Value, numud_BackG.Value, numud_BackB.Value);
            Color outline = ColorFromDecimals(numud_OutlineA.Value, numud_OutlineR.Value, numud_OutlineG.Value, numud_OutlineB.Value);
            Color transparent = ColorFromDecimals(numud_TransparentA.Value, numud_TransparentR.Value, numud_TransparentG.Value, numud_TransparentB.Value);
            Size dimensions = new Size(Convert.ToInt32(numud_MaxSizeX.Value), Convert.ToInt32(numud_MaxSizeY.Value));
            Size padding = new Size(Convert.ToInt32(numud_PaddingX.Value), Convert.ToInt32(numud_PaddingY.Value));
            WaypointContainer waypointContainer = GetWaypoints();

            if (this.alertText == null)
            {
                this.alertText = new TADAAlertText(message, readMessage, ttsVoice, ttsVolume, font, outlineWidth, foreground, background, outline, transparent, dimensions, padding, waypointContainer);
            }
            else
            {
                this.alertText.Message = message;
                this.alertText.ReadMessage = readMessage;
                this.alertText.TTSVoice = ttsVoice;
                this.alertText.TTSVolume = ttsVolume;
                this.alertText.ChosenFont = font;
                this.alertText.OutlineWidth = outlineWidth;
                this.alertText.Foreground = foreground;
                this.alertText.Background = background;
                this.alertText.Outline = outline;
                this.alertText.Transparent = transparent;
                this.alertText.Dimensions = dimensions;
                this.alertText.Padding = padding;
                this.alertText.Waypoints = waypointContainer;
            }
        }

        private void LoadExisting(TADAAlertText alertText)
        {
            if (alertText != null)
            {
                blocked = true;

                txt_DisplayedText.Text = alertText.Message;
                txt_NarratedText.Text = alertText.ReadMessage;
                cmb_TTSVoice.Text = alertText.TTSVoice;
                trk_VoiceVolume.Value = alertText.TTSVolume;

                if (alertText.ChosenFont != null)
                {
                    cmb_Font.Text = alertText.ChosenFont.Name;
                    numud_Size.Value = Convert.ToDecimal(alertText.ChosenFont.Size);
                }

                numud_Outline.Value = Convert.ToDecimal(alertText.OutlineWidth);

                if (alertText.Foreground != Color.Empty)
                {
                    numud_ForeA.Value = alertText.Foreground.A;
                    numud_ForeR.Value = alertText.Foreground.R;
                    numud_ForeG.Value = alertText.Foreground.G;
                    numud_ForeB.Value = alertText.Foreground.B;
                }

                if (alertText.Background != Color.Empty)
                {
                    numud_BackA.Value = alertText.Background.A;
                    numud_BackR.Value = alertText.Background.R;
                    numud_BackG.Value = alertText.Background.G;
                    numud_BackB.Value = alertText.Background.B;
                }

                if (alertText.Outline != Color.Empty)
                {
                    numud_OutlineA.Value = alertText.Outline.A;
                    numud_OutlineR.Value = alertText.Outline.R;
                    numud_OutlineG.Value = alertText.Outline.G;
                    numud_OutlineB.Value = alertText.Outline.B;
                }

                if (alertText.Transparent != Color.Empty)
                {
                    numud_TransparentA.Value = alertText.Transparent.A;
                    numud_TransparentR.Value = alertText.Transparent.R;
                    numud_TransparentG.Value = alertText.Transparent.G;
                    numud_TransparentB.Value = alertText.Transparent.B;
                }

                if (alertText.Dimensions.Width > 0)
                {
                    numud_MaxSizeX.Value = alertText.Dimensions.Width;
                }

                if (alertText.Dimensions.Height > 0)
                {
                    numud_MaxSizeY.Value = alertText.Dimensions.Height;
                }

                if (alertText.Padding.Width > 0)
                {
                    numud_PaddingX.Value = alertText.Padding.Width;
                }

                if (alertText.Padding.Height > 0)
                {
                    numud_PaddingY.Value = alertText.Padding.Height;
                }

                this.SetWaypoints(alertText.Waypoints);

                blocked = false;
                UpdateTextPreview();
            }
        }

        private void btn_TestItem_Click(object sender, EventArgs e)
        {
            GenerateAlertText();

            if (playingAlert != null)
            {
                playingAlert.Stop();
                TADAAnimationWindow.AnimationWindow.RemovePlayingAlert(playingAlert);
            }

            playingAlert = new PlayingAlert(null, null, alertText, null);
            this.animationWindow.AddPlayingAlert(playingAlert);
            playingAlert.Play();
        }

        private void btn_Complete_Click(object sender, EventArgs e)
        {
            if (this.ValidityChecks())
            {
                this.GenerateAlertText();
                this.Close();
            }
        }

        private bool ValidityChecks()
        {
            // IF everything looks good, then register it.
            bool valid = true;

            if (txt_DisplayedText.Enabled && String.IsNullOrEmpty(txt_DisplayedText.Text))
            {
                // Then I actually need to get the answer from the message box.
                DialogResult result = MessageBox.Show(null, "No message will be displayed on screen, is that okay?", "Warning - No Displayed Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    valid = false;
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            if (txt_NarratedText.Enabled && String.IsNullOrEmpty(txt_NarratedText.Text) && trk_VoiceVolume.Value > 0)
            {
                DialogResult result = MessageBox.Show(null, "You have specified a non-zero narrator volume but did not specify any text for the narrator to read, is this intentional?", "Warning - No Narrator Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    valid = false;
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            if (txt_NarratedText.Enabled && !String.IsNullOrEmpty(txt_NarratedText.Text) && trk_VoiceVolume.Value == 0)
            {
                DialogResult result = MessageBox.Show(null, "You have specified text for the narrator to read, but you have set the narrator's volume to 0, is this intentional?", "Warning - Narrator is Muted", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    valid = false;
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            if (dg_Waypoints.Rows.Count > 0)
            {
                WaypointContainer w = GetWaypoints();
                if (General.GetTotalWaypointDuration(w) == 0)
                {
                    MessageBox.Show(null, "Your defined Waypoints have a total duration of 0 seconds.", "Error - No Time At All", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valid = false;
                }
            }

            return valid;
        }
    }
}
