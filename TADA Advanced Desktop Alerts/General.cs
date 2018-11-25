using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
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
        public const String CLIENT_ID = "hw23lau0o8duwhk5h3wgpal4mbznyl";
        public const String AUTH_URI = @"https://invariel.ca/webauth/tada_auth.html";

        public const String PASSWORD_FIELD_DEFAULT_TEXT = "Paste Access Token Here";

        public const String SUBDIRECTORY = @"config/";
        public const String DEFAULT_OAUTH = @"oauth.cfg";
        public const String DEFAULT_ALERTS = @"default.tada";

        public const String MASKED_TEXTBOX_MASK = "{0,3}:{1,2}:{2,2}.{3,3}";

        public const double INITIAL_ANGLE = 0;
        public const double INITIAL_MAGNIFICATION = 1;
        public const double INITIAL_OPACITY = 100;
        public const double INITIAL_BALANCE = 0;
        public const double INITIAL_VOLUME = 0;

        public const String ANONYMOUS_SUB_GIFT_NAME = "An Anonymous Gifter";

        public static Random rng = new Random();

        public static Dictionary<TwitchLib.PubSub.Enums.SubscriptionPlan, String> TwitchLibSubPlanToString = new Dictionary<TwitchLib.PubSub.Enums.SubscriptionPlan, String>() {
            { TwitchLib.PubSub.Enums.SubscriptionPlan.NotSet, "None" },
            { TwitchLib.PubSub.Enums.SubscriptionPlan.Prime, "Twitch Prime" },
            { TwitchLib.PubSub.Enums.SubscriptionPlan.Tier1, "Tier One" },
            { TwitchLib.PubSub.Enums.SubscriptionPlan.Tier2, "Tier Two" },
            { TwitchLib.PubSub.Enums.SubscriptionPlan.Tier3, "Tier Three" },
        };

        public static Dictionary<String, TwitchLib.PubSub.Enums.SubscriptionPlan> stringToTwitchLibSubPlan = new Dictionary<string, TwitchLib.PubSub.Enums.SubscriptionPlan>()
        {
            { "None", TwitchLib.PubSub.Enums.SubscriptionPlan.NotSet },
            { "0", TwitchLib.PubSub.Enums.SubscriptionPlan.NotSet },
            { "Twitch Prime", TwitchLib.PubSub.Enums.SubscriptionPlan.Prime },
            { "Prime", TwitchLib.PubSub.Enums.SubscriptionPlan.Prime },
            { "Tier One", TwitchLib.PubSub.Enums.SubscriptionPlan.Tier1 },
            { "1000", TwitchLib.PubSub.Enums.SubscriptionPlan.Tier1 },
            { "Tier Two", TwitchLib.PubSub.Enums.SubscriptionPlan.Tier2 },
            { "2000", TwitchLib.PubSub.Enums.SubscriptionPlan.Tier2 },
            { "Tier Three", TwitchLib.PubSub.Enums.SubscriptionPlan.Tier3 },
            { "3000", TwitchLib.PubSub.Enums.SubscriptionPlan.Tier3 },
        };

        [Flags]
        public enum SCORES
        {
            SCORE_ABSOLUTE_MINIMUM = -1,
            SCORE_TIER_MATCHED = 0x8000,
            SCORE_NAME_MATCHED = 0x4000,
            SCORE_KEYWORD_MATCHED = 0x2000,
            SCORE_WITHIN_AMOUNT = 0x1000,
            SCORE_NO_CONDITIONS = 0x0100
        }

        public enum ALERTTYPES
        {
            NONE,   // Used when, for whatever reason, an alert needs to not play.
            ANONYMOUS,
            ANONYMOUS_COMMUNITY,
            AUTOHOST,
            BITS,
            COMMERCE,
            COMMUNITY,
            FIRSTMESSAGE,
            FOLLOW,
            HOST,
            JOIN,
//            MACRO,
            MESSAGE,
            RAID,
            SUBGIFT,
            SUBSCRIPTION
        };

        public static Dictionary<ALERTTYPES, String> alertTypes = new Dictionary<ALERTTYPES, String>()
        {
            { ALERTTYPES.NONE, "No alert." },
            { ALERTTYPES.ANONYMOUS, "Anonymous Gift Subs" },
            { ALERTTYPES.ANONYMOUS_COMMUNITY, "Anonymous Community Gift Subs" },
            { ALERTTYPES.AUTOHOST, "Auto-Host" },
            { ALERTTYPES.BITS, "Bits" },
            { ALERTTYPES.COMMERCE, "Game Purchase" },
            { ALERTTYPES.COMMUNITY, "Community Gift Subs" },
            { ALERTTYPES.FIRSTMESSAGE, "First Message" },
            { ALERTTYPES.FOLLOW, "Follow" },
            { ALERTTYPES.HOST, "Host" },
            { ALERTTYPES.JOIN, "Join" },
//            { ALERTTYPES.MACRO, "Macro" },
            { ALERTTYPES.MESSAGE, "Message" },
            { ALERTTYPES.RAID, "Raid" },
            { ALERTTYPES.SUBGIFT, "Sub Gift" },
            { ALERTTYPES.SUBSCRIPTION, "Subscription" },
        };

        internal static void ColourPicker(NumericUpDown red, NumericUpDown green, NumericUpDown blue, NumericUpDown alpha)
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

                // Only need to set RGBA because the RGBA numuds will call their OnValueChanged code that will then update the textbox.
                if (red != null)
                {
                    red.Value = color.R;
                }

                if (green != null)
                {
                    green.Value = color.G;
                }

                if (blue != null)
                {
                    blue.Value = color.B;
                }

                if (alpha != null)
                {
                    alpha.Value = color.A;
                }
            }
        }

        public static Dictionary<String, ALERTTYPES> alertStrings = new Dictionary<string, ALERTTYPES>()
        {
            { "No alert.", ALERTTYPES.NONE },
            { "Auto-Host", ALERTTYPES.AUTOHOST },
            { "Anonymous Gift Subs", ALERTTYPES.ANONYMOUS },
            { "Anonymous Community Gift Subs", ALERTTYPES.ANONYMOUS_COMMUNITY },
            { "Bits", ALERTTYPES.BITS },
            { "Game Purchase", ALERTTYPES.COMMERCE },
            { "Community Gift Subs", ALERTTYPES.COMMUNITY },
            { "First Message", ALERTTYPES.FIRSTMESSAGE },
            { "Follow", ALERTTYPES.FOLLOW },
            { "Host", ALERTTYPES.HOST },
            { "Join", ALERTTYPES.JOIN },
//            { "Macro", ALERTTYPES.MACRO },
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
            var explode = text.Split(':');

            int hours = 0,
                minutes = 0,
                seconds = 0,
                milliseconds = 0;

            var sec = explode[explode.Length - 1].Split('.');
            Int32.TryParse(sec[1], out milliseconds);
            Int32.TryParse(sec[0], out seconds);
            if (explode.Length >= 2) { Int32.TryParse(explode[explode.Length - 2], out minutes); }
            if (explode.Length >= 3) { Int32.TryParse(explode[explode.Length - 3], out hours); }

            return new TimeSpan(0, hours, minutes, seconds, milliseconds);
        }

        public static String StringFromTimeSpan(TimeSpan span)
        {
            return String.Format(General.MASKED_TEXTBOX_MASK, span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
        }

        public static void FillNumudsFromTimeSpan(TimeSpan time, NumericUpDown hours, NumericUpDown minutes, NumericUpDown seconds, NumericUpDown milliseconds)
        {
            hours.Value = time.Hours;
            minutes.Value = time.Minutes;
            seconds.Value = time.Seconds;
            milliseconds.Value = time.Milliseconds;
        }

        public static Bitmap GenerateMessageBitmap(Color foreColor, Color backColor, Color outlineColor, Font textFont, float outlineWidth, Size maximumSize, SizeF padding, string text)
        {
            SizeF textSize;

            StringFormat textFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Figure out the actual dimensions of the image.
            // TODO:  Future work: let people choose their text alignment.
            using (Bitmap b = new Bitmap(maximumSize.Width, maximumSize.Height))
            using (Graphics image = Graphics.FromImage(b))
            {
                textSize = image.MeasureString(text, textFont, Convert.ToInt32(maximumSize.Width - padding.Width * 2), textFormat);
            }

            Bitmap outputBitmap = new Bitmap(Convert.ToInt32(Math.Min(textSize.Width, maximumSize.Width)), Convert.ToInt32(Math.Min(textSize.Height, maximumSize.Height)));

            using (SolidBrush foreBrush = new SolidBrush(foreColor))
            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                Graphics image = Graphics.FromImage(outputBitmap);
                image.FillRectangle(backBrush, new Rectangle(new Point(0, 0), outputBitmap.Size));

                System.Drawing.Drawing2D.GraphicsPath textOutline = new System.Drawing.Drawing2D.GraphicsPath();
                textOutline.AddString(text, textFont.FontFamily, (int)textFont.Style, textFont.Size, new RectangleF(new PointF(padding.Width, padding.Height), textSize), textFormat);

                if (outlineColor != Color.Empty && outlineWidth > 0)
                {
                    using (Pen pen = new Pen(outlineColor, (float)outlineWidth))
                    {
                        image.DrawPath(pen, textOutline);
                    }
                }

                image.FillPath(foreBrush, textOutline);

            }

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

            message = message.Replace("{name}", substitutionObject.ViewerName);
            message = message.Replace("{giftee}", substitutionObject.RecipientName);
            message = message.Replace("{tier}", General.TwitchLibSubPlanToString[substitutionObject.Tier]);
            message = message.Replace("{tierName}", substitutionObject.TierName);

            message = message.Replace("{amount}", substitutionObject.Amount.ToString());
            message = message.Replace("{s}", substitutionObject.Amount == 1 ? "" : "s");
            message = message.Replace("{es}", substitutionObject.Amount == 1 ? "" : "es");

            // Now figure out [a|b]
            String pattern = @"\[([^|]+)\|([^|]+)\]";  // Anything up to [, capture before |, capture after |, ]... is safe.
            Regex regex = new Regex(pattern);
            Match match;
            while ((match = regex.Match(message)) != Match.Empty)
            {
                message = regex.Replace(message, substitutionObject.Amount == 1 ? match.Groups[1].ToString() : match.Groups[2].ToString(), 1);
            }

            int tierNumber = 0;

            switch (substitutionObject.Tier)
            {
                case TwitchLib.PubSub.Enums.SubscriptionPlan.NotSet: tierNumber = 1; break;
                case TwitchLib.PubSub.Enums.SubscriptionPlan.Prime: tierNumber = 2; break;
                case TwitchLib.PubSub.Enums.SubscriptionPlan.Tier1: tierNumber = 3; break;
                case TwitchLib.PubSub.Enums.SubscriptionPlan.Tier2: tierNumber = 4; break;
                case TwitchLib.PubSub.Enums.SubscriptionPlan.Tier3: tierNumber = 5; break;
            }

            // Now figure out <a|b|c|d|e>
            pattern = @"<([^|]*)\|([^|]*)\|([^|]*)\|([^|]*)\|([^|]*)>";
            regex = new Regex(pattern);
            while ((match = regex.Match(message)) != Match.Empty)
            {
                message = regex.Replace(message, match.Groups[tierNumber].ToString(), 1);
            }
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
