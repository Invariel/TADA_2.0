using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts
{
    public sealed class General
    {
        public static void LogFile(String message)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"./logfile.txt", true))
            {
                file.WriteLine(String.Format("[{0}] : {1}", DateTime.Now, message));
            }
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
            ToolTip t = new ToolTip();
            t.AutomaticDelay = 500;
            t.AutoPopDelay = 5000;
            t.SetToolTip(c, text);
        }

        public static void openFileDialogBox(TextBox destination, String filter, int index)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.InitialDirectory = Environment.CurrentDirectory;
            fileDialog.Filter = filter;
            fileDialog.FilterIndex = index;
            fileDialog.RestoreDirectory = false;
            fileDialog.Multiselect = false;

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
            Bitmap[] frames;
            ExtractAllFrames(bitmap, bitmap.Size, out frames, out timing, out totalDuration);

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

        public static void FillNumudsFromTimeSpan(TimeSpan time, NumericUpDown hours, NumericUpDown minutes, NumericUpDown seconds, NumericUpDown milliseconds)
        {
            hours.Value = time.Hours;
            minutes.Value = time.Minutes;
            seconds.Value = time.Seconds;
            milliseconds.Value = time.Milliseconds;
        }

        public static Bitmap GenerateMessageBitmap(Color foreColor, Color backColor, Color outlineColor, Color transparentColor, Font textFont, float outlineWidth, Size maximumSize, SizeF padding, string text)
        {
            Bitmap b = new Bitmap(maximumSize.Width, maximumSize.Height);
            Graphics image = Graphics.FromImage(b);

            using (SolidBrush foreBrush = new SolidBrush(foreColor))
            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                image.FillRectangle(backBrush, 0, 0, b.Width, b.Height);

                StringFormat textFormat = new StringFormat();
                textFormat.Alignment = StringAlignment.Center;
                textFormat.LineAlignment = StringAlignment.Center;

                SizeF textSize = image.MeasureString(text, textFont, Convert.ToInt32(maximumSize.Width - padding.Width * 2), textFormat);

                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddString(text, textFont.FontFamily, (int)textFont.Style, textFont.Size, new RectangleF(new PointF(padding.Width, padding.Height), textSize), textFormat);

                if (outlineColor != Color.Empty && outlineWidth > 0)
                {
                    using (Pen pen = new Pen(outlineColor, (float)outlineWidth))
                    {
                        image.DrawPath(pen, path);
                    }
                }

                image.FillPath(foreBrush, path);

                if (transparentColor != Color.Empty && transparentColor != Color.Transparent)
                {
                    b.MakeTransparent(transparentColor);
                }
            }

            return b;
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
            message = message.Replace("{tier}", substitutionObject.Tier);
            message = message.Replace("{tierName}", substitutionObject.TierName);

            message = message.Replace("{amount}", substitutionObject.Amount.ToString());
            message = message.Replace("{s}", substitutionObject.Amount == 1 ? "" : "s");
            message = message.Replace("{es}", substitutionObject.Amount == 1 ? "" : "es");

            // Now figure out [a|b]
            String pattern = @"\[([^|]+)|([^|]+)\]";  // Anything up to [, capture before |, capture after |, ]... is safe.
            Regex regex = new Regex(pattern);
            Match match;
            while ((match = regex.Match(message)) != Match.Empty)
            {
                message = regex.Replace(message, substitutionObject.Amount == 1 ? match.Groups[1].ToString() : match.Groups[2].ToString(), 1);
            }

            int tierNumber = 0;

            switch (substitutionObject.Tier)
            {
                case "No Tier": tierNumber = 1; break;
                case "Twitch Prime": tierNumber = 2; break;
                case "Tier One": tierNumber = 3; break;
                case "Tier Two": tierNumber = 4; break;
                case "Tier Three": tierNumber = 5; break;
            }

            // Now figure out <a|b|c|d|e>
            pattern = @"\<([^|])*|[^|])*|[^|])*|[^|])*|[^|])*\>";
            regex = new Regex(pattern);
            while ((match = regex.Match(message)) != Match.Empty)
            {
                message = regex.Replace(message, match.Groups[tierNumber].ToString(), 1);
            }
        }
    }
}
