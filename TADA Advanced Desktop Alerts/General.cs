using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts
{
    public enum DrawingStep
    {
        BELOW_IMAGE,
        IMAGE,
        ABOVE_IMAGE
    }

    public sealed class General
    {
        public const string CLIENT_ID = "hw23lau0o8duwhk5h3wgpal4mbznyl";
        public const string AUTH_URI = @"https://invariel.ca/webauth/tada_auth.html";

        public const string PASSWORD_FIELD_DEFAULT_TEXT = "Paste Access Token Here";

        public const string SUBDIRECTORY = @"config/";
        public const string DEFAULT_OAUTH = @"oauth.cfg";
        public const string DEFAULT_ALERTS = @"default.tada";

        public const string MASKED_TEXTBOX_MASK = "{0,3:D3}:{1,2:D2}:{2,2:D2}.{3,3:D3}";
        public const string COLOR_FORMAT = "#{0:X2}{1:X2}{2:X2}{3:X2}";

        public const double INITIAL_ANGLE = 0;
        public const double INITIAL_MAGNIFICATION = 1;
        public const double INITIAL_OPACITY = 100;
        public const double INITIAL_BALANCE = 0;
        public const double INITIAL_VOLUME = 0;

        public const String ANONYMOUS_SUB_GIFT_NAME = "An Anonymous Gifter";
        public const String ANONYMOUS_BITS_NAME = "An Anonymous Bit Dropper";

        public static Random rng = new Random();

        [Flags]
        public enum SCORES
        {
            SCORE_ABSOLUTE_MINIMUM      = -1,
            SCORE_BADGE_MATCHED         = 0x40000,
            SCORE_VIEWER_TIER_MATCHED   = 0x10000,
            SCORE_SUB_TIER_MATCHED      = 0x8000,
            SCORE_NAME_MATCHED          = 0x4000,
            SCORE_KEYWORD_MATCHED       = 0x2000,
            SCORE_WITHIN_AMOUNT         = 0x1000,
            SCORE_NO_CONDITIONS         = 0x0100
        }

        public enum ALERTTYPES
        {
            NONE,   // Used when, for whatever reason, an alert needs to not play.
            ANONYMOUS_BITS,
            ANONYMOUS_COMMUNITY,
            ANONYMOUS_GIFT,
            AUTOHOST,
            BITS,
            COMMUNITY,
            FIRSTMESSAGE,
            FOLLOW,
            HOST,
            JOIN,
            MACRO,
            MESSAGE,
            RAID,
            SUBGIFT,
            SUBSCRIPTION
        };

        public static Dictionary<ALERTTYPES, String> alertTypes = new Dictionary<ALERTTYPES, String>()
        {
            { ALERTTYPES.NONE, "No alert." },
            { ALERTTYPES.ANONYMOUS_BITS, "Anonymous Bits" },
            { ALERTTYPES.ANONYMOUS_GIFT, "Anonymous Gift Subs" },
            { ALERTTYPES.ANONYMOUS_COMMUNITY, "Anonymous Community Gift Subs" },
            { ALERTTYPES.AUTOHOST, "Auto-Host" },
            { ALERTTYPES.BITS, "Bits" },
            { ALERTTYPES.COMMUNITY, "Community Gift Subs" },
            { ALERTTYPES.FIRSTMESSAGE, "First Message" },
            { ALERTTYPES.FOLLOW, "Follow" },
            { ALERTTYPES.HOST, "Host" },
            { ALERTTYPES.JOIN, "Join" },
            { ALERTTYPES.MACRO, "Macro" },
            { ALERTTYPES.MESSAGE, "Message" },
            { ALERTTYPES.RAID, "Raid" },
            { ALERTTYPES.SUBGIFT, "Sub Gift" },
            { ALERTTYPES.SUBSCRIPTION, "Subscription" },
        };

        public static Dictionary<String, ALERTTYPES> alertStrings = new Dictionary<string, ALERTTYPES>()
        {
            { "No alert.", ALERTTYPES.NONE },
            { "Auto-Host", ALERTTYPES.AUTOHOST },
            { "Anonymous Bits", ALERTTYPES.ANONYMOUS_BITS },
            { "Anonymous Community Gift Subs", ALERTTYPES.ANONYMOUS_COMMUNITY },
            { "Anonymous Gift Subs", ALERTTYPES.ANONYMOUS_GIFT },
            { "Bits", ALERTTYPES.BITS },
            { "Community Gift Subs", ALERTTYPES.COMMUNITY },
            { "First Message", ALERTTYPES.FIRSTMESSAGE },
            { "Follow", ALERTTYPES.FOLLOW },
            { "Host", ALERTTYPES.HOST },
            { "Join", ALERTTYPES.JOIN },
            { "Macro", ALERTTYPES.MACRO },
            { "Message", ALERTTYPES.MESSAGE },
            { "Raid", ALERTTYPES.RAID  },
            { "Sub Gift", ALERTTYPES.SUBGIFT },
            { "Subscription", ALERTTYPES.SUBSCRIPTION },
        };

        public static void LogFile(String message)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"./logfile.txt", true))
                {
                    file.WriteLine(String.Format("[{0}] : {1}", DateTime.Now, message));
                }
            }
            catch (Exception) { }
        }

        public static void LogFile(String message, String filename)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, true))
            {
                file.WriteLine(String.Format("[{0}] : {1}", DateTime.Now, message));
            }
        }

        public const string STATE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

        public static string GenerateStateString()
        {
            Random random = new Random();

            string stateString = "TADA_";

            for (int i = 0; i < 40; ++ i)
            {
                stateString += STATE_CHARACTERS[random.Next(STATE_CHARACTERS.Length)];
            }

            return stateString;
        }

        public static void ConfigureToolTip(List<Control> controls, String text)
        {
            foreach (Control c in controls)
            {
                ConfigureToolTip(c, text);
            }
        }

        public static void ConfigureToolTip(Control c, String text)
        {
            ToolTip t = new ToolTip
            {
                AutomaticDelay = 500,
                AutoPopDelay = 5000
            };
            t.SetToolTip(c, text);
        }

        public static void openFileDialogBox(TextBox destination, String filter, int index)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = filter,
                FilterIndex = index,
                RestoreDirectory = false,
                Multiselect = false
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                destination.Text = fileDialog.FileName;
            }
        }

        internal static void ParseHexColor(string hex, out int r, out int g, out int b, out int a)
        {
            if (String.IsNullOrEmpty(hex))
            {
                hex = "#00000000";
            }

            while (hex.Length < 9)
            {
                hex += "0";
            }

            r = Convert.ToInt32(hex.Substring(1, 2), 16);
            g = Convert.ToInt32(hex.Substring(3, 2), 16);
            b = Convert.ToInt32(hex.Substring(5, 2), 16);
            a = Convert.ToInt32(hex.Substring(7, 2), 16);
        }

        internal static void ParseHexColor(string hex, out Color color)
        {
            ParseHexColor(hex, out int r, out int g, out int b, out int a);
            color = Color.FromArgb(a, r, g, b);
        }

        internal static string StringFromColor(Color color)
        {
            return String.Format(General.COLOR_FORMAT, color.R, color.G, color.B, color.A);
        }

        internal static void MaintainBounds (TrackBar trackBar, MaskedTextBox maskedTextBox)
        {
            TimeSpan timespan = General.TimeSpanFromString(maskedTextBox.Text);
            if (timespan.TotalMilliseconds > trackBar.Maximum)
            {
                trackBar.Value = trackBar.Maximum;
                maskedTextBox.Text = General.StringFromTimeSpan(new TimeSpan(0, 0, 0, 0, trackBar.Value));
            }
            else
            {
                trackBar.Value = Convert.ToInt32(timespan.TotalMilliseconds);
            }
        }

        internal static void ColourPicker(NumericUpDown red = null, NumericUpDown green = null, NumericUpDown blue = null, NumericUpDown alpha = null, TextBox hexBox = null)
        {
            ColorDialog colorDialog = new ColorDialog()
            {
                Color = Color.FromArgb(
                    Convert.ToInt32(alpha?.Value ?? 0),
                    Convert.ToInt32(red?.Value ?? 0),
                    Convert.ToInt32(green?.Value ?? 0),
                    Convert.ToInt32(blue?.Value ?? 0)),
                FullOpen = true
            };

            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Color color = colorDialog.Color;

                SetIfNotNull(red, color.R);
                SetIfNotNull(green, color.G);
                SetIfNotNull(blue, color.B);
                SetIfNotNull(alpha, color.A);

                SetIfNotNull(hexBox, color);
            }
        }

        internal static void SetIfNotNull (NumericUpDown numud, Decimal d)
        {
            if (numud != null)
            {
                numud.Value = d;
            }
        }

        internal static void SetIfNotNull (TextBox textBox, Color c)
        {
            if (textBox != null)
            {
                textBox.Text = General.StringFromColor(c);
            }
        }

        internal static Color _ColorPicker(ComboBox comboBox, TextBox hexBox)
        {
            Color color = Color.Black;
            General.ParseHexColor(hexBox.Text, out int red, out int green, out int blue, out int alpha);

            ColorDialog colorDialog = new ColorDialog()
            {
                // Instead of getting argb from numuds that don't exist, parse the textbox string.
                Color = Color.FromArgb(
                    Convert.ToInt32(alpha),
                    Convert.ToInt32(red),
                    Convert.ToInt32(green),
                    Convert.ToInt32(blue)),
                FullOpen = true
            };

            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                color = colorDialog.Color;

                comboBox.Text = color.IsNamedColor ? color.Name : "Custom";
                hexBox.Text = General.StringFromColor(color);
            }

            return color;
        }

        internal static String GetKnownColorString(Color color)
        {
            String returnColor = "Custom";

            foreach (KnownColor knownColor in Enum.GetValues(typeof(KnownColor)))
            {
                Color testColor = Color.FromKnownColor(knownColor);
                if (color.ToArgb() == testColor.ToArgb())
                {
                    returnColor = Enum.GetName(typeof(KnownColor), knownColor);
                    break;
                }
            }

            return returnColor;
        }

        internal static void ResizeDropdown(ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
            {
                int scrollBarWidth = (comboBox.Items.Count > comboBox.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;
                comboBox.DropDownWidth = comboBox.Items.Cast<string>().Max(x => TextRenderer.MeasureText(x, comboBox.Font).Width + scrollBarWidth);
                if (comboBox.SelectedIndex <= 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
        }

        public static void ExtractAllFrames(Bitmap bitmap, Size newSize, out Bitmap[] frames, out int[] timing, out int totalDuration)
        {
            byte[] delayValues = bitmap.GetPropertyItem(0x5100).Value;

            int frameCount = bitmap.GetFrameCount(FrameDimension.Time);
            frames = new Bitmap[frameCount];
            timing = new int[frameCount];

            totalDuration = 0;

            for (int frame = 0; frame < frameCount; ++frame)
            {
                bitmap.SelectActiveFrame(FrameDimension.Time, frame);
                frames[frame] = new Bitmap(bitmap, newSize);
                timing[frame] = ((delayValues[frame * 4 + 0] << 00) +
                                 (delayValues[frame * 4 + 1] << 08) +
                                 (delayValues[frame * 4 + 2] << 16) +
                                 (delayValues[frame * 4 + 3] << 24)) * 10; // Frame timings are in 1/100s, we need 1/1000s.

                timing[frame] = Math.Max(10, timing[frame]);    // 0-duration timings exist, for some stupid reason.  Change them to be 1/100s instead.

                totalDuration += timing[frame];
            }
        }

        public static void GetImageDuration(Bitmap bitmap, out int[] timing, out int totalDuration)
        {
            ExtractAllFrames(bitmap, bitmap.Size, out Bitmap[] frames, out timing, out totalDuration);

            for (int i = 0; i < frames.Length; ++i)
            {
                frames[i].Dispose();
                frames[i] = null;
            }
        }

        public static String DurationFromTimeSpan(TimeSpan timespan)
        {
            String durationString;

            if (timespan.TotalHours >= 1)
            {
                durationString = String.Format("{0}:{1:D2}:{2:D2}.{3:D3}", timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
            }
            else if (timespan.TotalMinutes >= 1)
            {
                durationString = String.Format("{0}:{1:D2}.{2:D3}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
            }
            else
            {
                durationString = String.Format("{0}.{1:D3}", timespan.Seconds, timespan.Milliseconds);
            }

            return durationString;
        }

        public static TimeSpan TimeSpanFromDecimals(Decimal milliseconds, Decimal seconds = 0, Decimal minutes = 0, Decimal hours = 0, Decimal days = 0)
        {
            return new TimeSpan(Convert.ToInt32(days),
                                Convert.ToInt32(hours),
                                Convert.ToInt32(minutes),
                                Convert.ToInt32(seconds),
                                Convert.ToInt32(milliseconds));
        }

        // The difference between this and DurationFromTimeSpan is that Duration... is trimmed.  This needs every value because of the masked text box class these items fill.
        public static TimeSpan TimeSpanFromString(String text)
        {
            if (String.IsNullOrEmpty(text))
            {
                text = "0:0.0";
            }

            var explode = text.Split(':');

            int hours = 0,
                minutes = 0,
                seconds = 0,
                milliseconds = 0;

            var sec = explode[explode.Length - 1].Split('.');
            if (sec.Length == 1)
            {
                Int32.TryParse(sec[0], out seconds);
            }
            else
            {
                Int32.TryParse(sec[1], out milliseconds);
                Int32.TryParse(sec[0], out seconds);
                if (explode.Length >= 2) { Int32.TryParse(explode[explode.Length - 2], out minutes); }
                if (explode.Length >= 3) { Int32.TryParse(explode[explode.Length - 3], out hours); }
            }

            return new TimeSpan(0, hours, minutes, seconds, milliseconds);
        }

        public static String StringFromTimeSpan(TimeSpan span)
        {
            return String.Format(General.MASKED_TEXTBOX_MASK, span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
        }

        public static void AssignNumud<T>(NumericUpDown numud, T value) => numud.Value = Math.Min(Math.Max(Convert.ToDecimal(value), numud.Minimum), numud.Maximum);

        public static void FillNumudsFromTimeSpan(TimeSpan time, NumericUpDown hours, NumericUpDown minutes, NumericUpDown seconds, NumericUpDown milliseconds)
        {
            hours.Value = time.Hours;
            minutes.Value = time.Minutes;
            seconds.Value = time.Seconds;
            milliseconds.Value = time.Milliseconds;
        }

        public static Bitmap GenerateMessageBitmap(Color foreColor, Color backColor, Color outlineColor, Font textFont, float outlineWidth, Size bitmapDimensions, Size padding, String text, String backgroundImageLocation = null)
        {
            StringFormat textFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            if (String.IsNullOrWhiteSpace(backgroundImageLocation))
            {
                backgroundImageLocation = null;
            }

            Bitmap outputBitmap = new Bitmap(bitmapDimensions.Width, bitmapDimensions.Height);
            Bitmap textBitmap = new Bitmap(bitmapDimensions.Width - 2 * padding.Width, bitmapDimensions.Height - 2 * padding.Height);
            Bitmap backgroundBitmap = null;
            Bitmap tempBackground = null;

            try
            {
                tempBackground = new Bitmap(backgroundImageLocation);
                backgroundBitmap = new Bitmap(tempBackground, outputBitmap.Size);
            }
            catch
            {
                tempBackground = null;
                backgroundBitmap = null;
            }
            finally
            {
                tempBackground?.Dispose();
            }

            // Draw the textBitmap.
            //using (LinearGradientBrush foreBrush = new LinearGradientBrush(new PointF(0, 0), new PointF(textBitmap.Width, textBitmap.Height), Color.Black, foreColor))
            using (Brush foreBrush = new SolidBrush(foreColor))
            using (Graphics textGraphics = Graphics.FromImage(textBitmap))
            using (SolidBrush backBrush = new SolidBrush(backColor))
            using (Graphics outputGraphics = Graphics.FromImage(outputBitmap))
            {
                System.Drawing.Drawing2D.GraphicsPath textOutline = new System.Drawing.Drawing2D.GraphicsPath();
                textOutline.AddString(text, textFont.FontFamily, (int)textFont.Style, textFont.Size, new Rectangle(Point.Empty, textBitmap.Size), textFormat);

                if (outlineColor != Color.Empty && outlineWidth > 0)
                {
                    using (Pen pen = new Pen(outlineColor, outlineWidth))
                    {
                        textGraphics.DrawPath(pen, textOutline);
                    }
                }

                textGraphics.FillPath(foreBrush, textOutline);

                outputGraphics.FillRectangle(backBrush, new Rectangle(0, 0, outputBitmap.Width, outputBitmap.Height));

                if (backgroundBitmap != null)
                {
                    outputGraphics.DrawImage(backgroundBitmap, new Point(0, 0));
                }

                outputGraphics.DrawImage(textBitmap, new Point(padding.Width, padding.Height));
            }

            textBitmap.Dispose();
            backgroundBitmap?.Dispose();

            return outputBitmap;
        }

        public static double GetTotalWaypointDuration(WaypointContainer wc)
        {
            double duration = 0;
            foreach (Waypoint w in wc.Waypoints)
            {
                duration += w.TransitionTime + w.PersistTime;
            }
            return duration;
        }

        public static string ReplaceCaseInsensitive(string input, string search, string replacement)
        {
            string result = Regex.Replace(
                input,
                $"{search}",
                replacement,
                RegexOptions.IgnoreCase
            );
            return result;
        }

        internal static void PerformSubstitutions(ref string message, SubstitutionObject substitutionObject)
        {
            // {name} => substitutionObject.ViewerName;
            // {giftee} => substitutionObject.RecipientName;
            // {amount} => substitutionObject.Amount;
            // {tier} => substitutionObject.Tier;
            // {tierName} => substitutionObject.TierName;
            // {s} => "" if amount == 1
            // {es} => "" if amount == 1
            // [a|b] => extract a and b, if amount == 1, a, else b
            // <a|b|c|d> => extract a, b, c, d.  Use <Twitch Prime | Tier One | Tier Two | Tier Three>

            message = General.ReplaceCaseInsensitive(message, "{name}", substitutionObject.ViewerName);
            message = General.ReplaceCaseInsensitive(message, "{giftee}", substitutionObject.RecipientName);
            message = General.ReplaceCaseInsensitive(message, "{tier}", substitutionObject.TierString);
            message = General.ReplaceCaseInsensitive(message, "{tierName}", substitutionObject.TierName);

            message = General.ReplaceCaseInsensitive(message, "{amount}", substitutionObject.Amount.ToString());
            message = General.ReplaceCaseInsensitive(message, "{s}", substitutionObject.Amount == 1 ? "" : "s");
            message = General.ReplaceCaseInsensitive(message, "{es}", substitutionObject.Amount == 1 ? "" : "es");

            // Now figure out [a|b]
            string pattern = @"\[([^|]+)\|([^|]+)\]";  // Anything up to [, capture before |, capture after |, ]... is safe.
            Regex regex = new Regex(pattern);
            Match match;
            while ((match = regex.Match(message)) != Match.Empty)
            {
                message = regex.Replace(message, substitutionObject.Amount == 1 ? match.Groups[1].ToString() : match.Groups[2].ToString(), 1);
            }

            int tierNumber = 0;

            switch (substitutionObject.Plan)
            {
                case TierAndPlanConverter.SUBSCRIPTION_PLANS.NO_TIER: tierNumber = 1; break;
                case TierAndPlanConverter.SUBSCRIPTION_PLANS.TWITCH_PRIME: tierNumber = 2; break;
                case TierAndPlanConverter.SUBSCRIPTION_PLANS.TIER_ONE: tierNumber = 3; break;
                case TierAndPlanConverter.SUBSCRIPTION_PLANS.TIER_TWO: tierNumber = 4; break;
                case TierAndPlanConverter.SUBSCRIPTION_PLANS.TIER_THREE: tierNumber = 5; break;
            }

            // Now figure out <a|b|c|d|e>
            pattern = @"<([^|]*)\|([^|]*)\|([^|]*)\|([^|]*)\|([^|]*)>";
            regex = new Regex(pattern);
            while ((match = regex.Match(message)) != Match.Empty)
            {
                message = regex.Replace(message, match.Groups[tierNumber].ToString(), 1);
            }
        }

        internal static System.Speech.Synthesis.PromptBuilder PerformTTSSubstitutions (string message, SubstitutionObject substitutionObject, Dictionary<string, string> ipaDictionary)
        {
            System.Speech.Synthesis.PromptBuilder ttsPrompt = new System.Speech.Synthesis.PromptBuilder();

            // Thank you, {name}, for the raid.
            // {giftee} just gave {name} a subscription.

            // Scenario 1: has name, does not have giftee.
            // Scenario 2: has giftee, does not have name.
            // Scenario 3: has both.
            // Scenario 4: has neither.

            // Okay...



            bool hasName = message.Contains("{name}");
            bool hasGiftee = message.Contains("{giftee}");

            bool nameSubstitution = ipaDictionary.ContainsKey(substitutionObject.ViewerName.ToLower());
            bool gifteeSubstitution = ipaDictionary.ContainsKey(substitutionObject.RecipientName.ToLower());

            string name = nameSubstitution ? ipaDictionary[substitutionObject.ViewerName.ToLower()] : substitutionObject.ViewerName;
            string giftee = gifteeSubstitution ? ipaDictionary[substitutionObject.RecipientName.ToLower()] : substitutionObject.RecipientName;

            if (hasName && hasGiftee)
            {
                string[] withoutName = message.Split(new string[] { "{name}" }, StringSplitOptions.None);
                for (int i = 0; i < withoutName.Length; ++ i)
                {
                    string[] withoutGiftee = withoutName[i].Split(new string[] { "{giftee}" }, StringSplitOptions.None);

                    for (int j = 0; j < withoutGiftee.Length; ++ j)
                    {
                        PerformSubstitutions(ref withoutGiftee[j], substitutionObject);
                        ttsPrompt.AppendText(withoutGiftee[j]);

                        if (j != withoutGiftee.Length - 1)
                        {
                            if (gifteeSubstitution)
                            {
                                ttsPrompt.AppendTextWithPronunciation(substitutionObject.RecipientName, giftee);
                            }
                            else
                            {
                                ttsPrompt.AppendText(giftee);
                            }
                        }
                    }

                    if (i != withoutName.Length - 1)
                    {
                        if (nameSubstitution)
                        {
                            ttsPrompt.AppendTextWithPronunciation(substitutionObject.ViewerName, name);
                        }
                        else
                        {
                            ttsPrompt.AppendText(name);
                        }
                    }
                }
            }
            else if (hasName || hasGiftee)
            {
                string[] withoutText = message.Split(new string[] { hasName ? "{name}" : "{giftee}" }, StringSplitOptions.None);

                string replacementWord = hasName ? substitutionObject.ViewerName : substitutionObject.RecipientName;
                string replacementText = ipaDictionary.ContainsKey(replacementWord.ToLower()) ? ipaDictionary[replacementWord.ToLower()] : replacementWord;

                for (int i = 0; i < withoutText.Length; ++ i)
                {
                    PerformSubstitutions(ref withoutText[i], substitutionObject);

                    ttsPrompt.AppendText(withoutText[i]);

                    if (i != withoutText.Length - 1)
                    {
                        if (hasName && nameSubstitution || hasGiftee && gifteeSubstitution)
                        {
                            ttsPrompt.AppendTextWithPronunciation(replacementWord, replacementText);
                        }
                        else
                        {
                            ttsPrompt.AppendText(replacementText);
                        }
                    }
                }
            }
            else
            {
                PerformSubstitutions(ref message, substitutionObject);
                ttsPrompt.AppendText(message);
            }

            return ttsPrompt;
        }
    }

    // From https://stackoverflow.com/questions/15071359/how-to-set-timer-resolution-from-c-sharp-to-1-ms - 1ms resolution timers instead of 15ms to improve animations.
    internal sealed class TimePeriod : IDisposable
    {
        private const string WINMM = "winmm.dll";

        private static TIMECAPS timeCapabilities;

        private static int inTimePeriod;

        private readonly int period;

        private int disposed;

        [DllImport(WINMM, ExactSpelling = true)]
        private static extern int timeGetDevCaps(ref TIMECAPS ptc, int cbtc);

        [DllImport(WINMM, ExactSpelling = true)]
        private static extern int timeBeginPeriod(int uPeriod);

        [DllImport(WINMM, ExactSpelling = true)]
        private static extern int timeEndPeriod(int uPeriod);

        static TimePeriod()
        {
            int result = timeGetDevCaps(ref timeCapabilities, Marshal.SizeOf(typeof(TIMECAPS)));
            if (result != 0)
            {
                throw new InvalidOperationException("The request to get time capabilities was not completed because an unexpected error with code " + result + " occured.");
            }
        }

        internal TimePeriod(int period)
        {
            if (Interlocked.Increment(ref inTimePeriod) != 1)
            {
                Interlocked.Decrement(ref inTimePeriod);
                throw new NotSupportedException("The process is already within a time period. Nested time periods are not supported.");
            }

            if (period < timeCapabilities.wPeriodMin || period > timeCapabilities.wPeriodMax)
            {
                throw new ArgumentOutOfRangeException("period", "The request to begin a time period was not completed because the resolution specified is out of range.");
            }

            int result = timeBeginPeriod(period);
            if (result != 0)
            {
                throw new InvalidOperationException("The request to begin a time period was not completed because an unexpected error with code " + result + " occured.");
            }

            this.period = period;
        }

        internal static int MinimumPeriod
        {
            get
            {
                return timeCapabilities.wPeriodMin;
            }
        }

        internal static int MaximumPeriod
        {
            get
            {
                return timeCapabilities.wPeriodMax;
            }
        }

        internal int Period
        {
            get
            {
                if (this.disposed > 0)
                {
                    throw new ObjectDisposedException("The time period instance has been disposed.");
                }

                return this.period;
            }
        }

        public void Dispose()
        {
            if (Interlocked.Increment(ref this.disposed) == 1)
            {
                timeEndPeriod(this.period);
                Interlocked.Decrement(ref inTimePeriod);
            }
            else
            {
                Interlocked.Decrement(ref this.disposed);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TIMECAPS
        {
            internal int wPeriodMin;

            internal int wPeriodMax;
        }
    }

    class CurrentPosition
    {
        public PointF location;
        public float angle;
        public float magnification;
        public float opacity;
        public float balance;
        public float volume;
        public bool aroundCenter;
        public bool actuallyDraw;
    }
}
