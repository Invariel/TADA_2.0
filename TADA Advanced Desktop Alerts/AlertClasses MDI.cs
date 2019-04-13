using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TwitchLib;

/**********************************************************************************************
 * WARNING!
 * 
 * Because of changes made to the objects here, nothing is null.
 * As a result, pay VERY CLOSE ATTENTION when setting up the alert to check for IsNullOrEmpty
 * and not just null.  This might already be done, idk, but that's what this warning is for.
 */

namespace TADA_Advanced_Desktop_Alerts
{
    public abstract class PropertyAndXML : INotifyPropertyChanged, IXmlSerializable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public abstract void ReadXml(XmlReader reader);
        public abstract void WriteXml(XmlWriter writer);
    }

    internal class TADAAlert : PropertyAndXML
    {
        private String alertName;
        private General.ALERTTYPES alertType;

        private List<TADAAlertImage> images = new List<TADAAlertImage>();
        private List<TADAAlertAudio> audios = new List<TADAAlertAudio>();
        private List<TADAAlertText> streamerTexts = new List<TADAAlertText>();
        private List<TADAAlertText> viewerTexts = new List<TADAAlertText>();
        private List<TADAAlertCondition> conditions = new List<TADAAlertCondition>();

        internal TimeSpan userCooldown = new TimeSpan(0);
        internal TimeSpan globalCooldown = new TimeSpan(0);

        internal Macro macroKey = new Macro();

        #region PropertyStuff
        public List<TADAAlertImage> Images
        {
            get { return this.images; }
            set
            {
                this.images = value;
                NotifyPropertyChanged("ImageCount");
            }
        }

        public List<TADAAlertAudio> Audios
        {
            get { return this.audios; }
            set
            {
                this.audios = value;
                NotifyPropertyChanged("AudioCount");
            }
        }

        public List<TADAAlertText> StreamerTexts
        {
            get { return this.streamerTexts; }
            set
            {
                this.streamerTexts = value;
                NotifyPropertyChanged("StreamerCount");
            }
        }

        public List<TADAAlertText> ViewerTexts
        {
            get { return this.viewerTexts; }
            set
            {
                this.viewerTexts = value;
                NotifyPropertyChanged("ViewerCount");
            }
        }

        public List<TADAAlertCondition> Conditions
        {
            get { return this.conditions; }
            set
            {
                this.conditions = value;
                NotifyPropertyChanged("ConditionCount");
            }
        }

        [DisplayName("Alert Nickname")]
        public String AlertName
        {
            get { return this.alertName; }
            set
            {
                this.alertName = value;
                NotifyPropertyChanged("Name");
            }
        }

        [DisplayName("Alert Type")]
        public String AlertType
        {
            get { return General.alertTypes[this.alertType]; }
            set
            {
                this.alertType = General.alertStrings[value];
                NotifyPropertyChanged("AlertType");
            }
        }

        [DisplayName("Images")]
        public int ImageCount
        {
            get { return this.Images.Count; }
        }

        [DisplayName("Audios")]
        public int AudioCount
        {
            get { return this.Audios.Count; }
        }

        [DisplayName("Streamer Messages")]
        public int StreamerCount
        {
            get { return this.StreamerTexts.Count; }
        }

        [DisplayName("Viewer Messages")]
        public int ViewerCount
        {
            get { return this.ViewerTexts.Count; }
        }

        [DisplayName("Conditions")]
        public int ConditionCount
        {
            get { return this.Conditions.Count; }
        }

        public int Combinations
        {
            get
            {
                return Math.Max(1, this.ImageCount) * Math.Max(1, this.AudioCount) * Math.Max(1, this.StreamerCount) * Math.Max(1, this.ViewerCount);
            }
        }
        #endregion PropertyStuff

        #region XML
        public override void ReadXml(XmlReader reader)
        {
            Boolean closingTag = false;
            Boolean error = false;

            this.alertName = reader.GetAttribute("Name");
            this.alertType = General.alertStrings[reader.GetAttribute("Type")];
            this.globalCooldown = General.TimeSpanFromString(reader.GetAttribute("GlobalCooldown"));
            this.userCooldown = General.TimeSpanFromString(reader.GetAttribute("UserCooldown"));

            while (reader.Read())
            {
                if (reader.IsEmptyElement)
                {
                    continue;
                }
                if (reader.IsStartElement("Images"))
                {
                    XmlHelper.Read(reader, "Image", "Images", this.images);
                }
                else if (reader.IsStartElement("Audios"))
                {
                    XmlHelper.Read(reader, "Audio", "Audios", this.audios);
                }
                else if (reader.IsStartElement("StreamerTexts"))
                {
                    XmlHelper.Read(reader, "Text", "StreamerTexts", this.streamerTexts);
                }
                else if (reader.IsStartElement("ViewerTexts"))
                {
                    XmlHelper.Read(reader, "Text", "ViewerTexts", this.viewerTexts);
                }
                else if (reader.IsStartElement("Conditions"))
                {
                    XmlHelper.Read(reader, "Condition", "Conditions", this.conditions);
                }
                else if (reader.IsStartElement("Macro"))
                {
                    this.macroKey.ReadXml(reader); 
                }
                else if (reader.IsStartElement())
                {
                    if (!error)
                    {
                        MessageBox.Show(null, String.Format("Unknown tag '{0}' detected in <Alert> definition.  No more errors for this <Alert> will be shown.", reader.Name), "Error in Parsing XML", MessageBoxButtons.OK);
                        error = true;
                    }
                }
                else if (!reader.IsStartElement() && reader.Name == "Alert")
                {
                    closingTag = true;
                    break;
                }
            }

            if (!closingTag)
            {
                throw new XmlException("Reader did not find a closing <Alert> tag.");
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Alert");
            writer.WriteAttributeString("Name", alertName);
            writer.WriteAttributeString("Type", General.alertTypes[alertType]);
            writer.WriteAttributeString("GlobalCooldown", General.StringFromTimeSpan(this.globalCooldown));
            writer.WriteAttributeString("UserCooldown", General.StringFromTimeSpan(this.userCooldown));

            XmlHelper.Write(writer, "Images", this.images);
            XmlHelper.Write(writer, "Audios", this.audios);
            XmlHelper.Write(writer, "StreamerTexts", this.streamerTexts);
            XmlHelper.Write(writer, "ViewerTexts", this.viewerTexts);
            XmlHelper.Write(writer, "Conditions", this.conditions);

            if (!this.macroKey.Empty)
            {
                this.macroKey.WriteXml(writer);
            }

            writer.WriteEndElement();
        }
        #endregion XML

        public override string ToString()
        {
            return String.Format("{0} [{1}]: {2} {3} {4} {5}", this.alertName, General.alertTypes[this.alertType], this.ImageCount, this.AudioCount, this.StreamerCount, this.ViewerCount);
        }
    }

    public class Macro : PropertyAndXML
    {
        public bool Ctrl { get; set; } = false;
        public bool Alt { get; set; } = false;
        public bool Shift { get; set; } = false;
        public Keys Key { get; set; } = Keys.None;

        public Boolean Empty
        {
            get
            {
                return (Key != Keys.None);
            }
        }

        public override void ReadXml(XmlReader reader)
        {
            if (reader.IsStartElement("Macro"))
            {
                this.Ctrl = XmlHelper.ReadAttribute(reader, "Ctrl", false);
                this.Alt = XmlHelper.ReadAttribute(reader, "Alt", false);
                this.Shift = XmlHelper.ReadAttribute(reader, "Shift", false);
                this.Key = (Keys)(Enum.Parse(typeof(Keys), reader.GetAttribute("Key")));
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            if (!this.Empty)
            {
                writer.WriteStartElement("Macro");
                writer.WriteAttributeString("Ctrl", Ctrl.ToString());
                writer.WriteAttributeString("Alt", Alt.ToString());
                writer.WriteAttributeString("Shift", Shift.ToString());
                writer.WriteAttributeString("Key", Enum.GetName(typeof(Keys), Key));
                writer.WriteEndElement();
            }
        }
    }

    internal class TADAAlertImage : PropertyAndXML
    {
        private String filename = "";
        private Size dimensions = new Size(0, 0);
        private Boolean isVideo = false;

        #region Properties
        [DisplayName("Filename")]
        public String Filename
        {
            get { return this.filename; }
            set
            {
                this.filename = value;
                NotifyPropertyChanged();
            }
        }

        [DisplayName("Size")]
        public Size Dimensions
        {
            get { return this.dimensions; }
            set
            {
                this.dimensions = value;
                NotifyPropertyChanged();
            }
        }

        [DisplayName("Video ?")]
        public Boolean IsVideo
        {
            get { return this.isVideo; }
            set
            {
                this.isVideo = value;
                NotifyPropertyChanged();
            }
        }

        public double TotalDuration
        {
            get
            {
                return (this.isVideo ? Math.Max(this.Duration.TotalSeconds, this.Waypoints.TotalDuration) : this.Waypoints.TotalDuration);
            }
        }
        #endregion Properties

        public WaypointContainer Waypoints { get; set; } = new WaypointContainer();

        public int AnimationLoops { get; set; } = 0;

        public TimeSpan Duration { get; set; } = new TimeSpan(0);
        public TimeSpan EndingPosition { get; set; } = new TimeSpan(0);
        public TimeSpan StartingPosition { get; set; } = new TimeSpan(0);

        // Constructor for XmlReader
        public TADAAlertImage () { }

        // Constructor for Image
        public TADAAlertImage(String filename, int animationLoops, Size dimensions, WaypointContainer waypoints)
        {
            this.isVideo = false;

            this.filename = filename;
            this.AnimationLoops = animationLoops;
            this.dimensions = dimensions;
            this.Waypoints = waypoints;
        }

        // Constructor for Video
        public TADAAlertImage(String filename, Size dimensions, TimeSpan duration, TimeSpan startingPosition, TimeSpan endingPosition, WaypointContainer waypoints)
        {
            this.isVideo = true;

            this.filename = filename;
            this.dimensions = dimensions;
            this.Duration = duration;
            this.StartingPosition = startingPosition;
            this.EndingPosition = endingPosition;
            this.Waypoints = waypoints;
        }

        #region XML
        public override void ReadXml(XmlReader reader)
        {
            Boolean closingTag = false;

            if (reader.IsStartElement("Image"))
            {
                // It had better be, since this is supposed to only be called by <Alert> when it's time to add an image or video.
                this.Filename = reader.GetAttribute("Filename");
                this.IsVideo = Boolean.Parse(reader.GetAttribute("IsVideo"));
                this.AnimationLoops = Int32.Parse(reader.GetAttribute("Loops") ?? "0.0");

                // Have to read all of the 1.0-written properties.
                double initialAngle = XmlHelper.ReadAttribute(reader, "Angle", 0.0d);
                double initialMagnification = XmlHelper.ReadAttribute(reader, "Magnification", 1.0d);
                double initialOpacity = XmlHelper.ReadAttribute(reader, "Opacity", 100.0d);
                double initialBalance = XmlHelper.ReadAttribute(reader, "Balance", 0.0d);
                double initialVolume = XmlHelper.ReadAttribute(reader, "Volume", 0.0d);

                while (reader.Read())
                {
                    if (reader.IsStartElement("Time"))
                    {
                        XmlHelper.ReadTimeSpan(reader, out TimeSpan timeSpan, out String name);

                        switch (name)
                        {
                            case "Duration":
                                this.Duration = timeSpan;
                                break;
                            case "StartAt":
                                this.StartingPosition = timeSpan;
                                break;
                            case "LoopAt":
                                this.EndingPosition = timeSpan;
                                break;
                        }
                    }
                    else if (reader.IsStartElement("Size"))
                    {
                        XmlHelper.ReadSize(reader, out Size size, out String name);
                        if (name == "Dimensions")
                        {
                            this.dimensions = size;
                        }
                    }
                    else if (reader.IsStartElement("Waypoints"))
                    {
                        this.Waypoints.ReadXml(reader);
                    }
                    else if (!reader.IsStartElement() && reader.Name == "Image")
                    {
                        closingTag = true;

                        // And to write the 1.0 properties if they're there.
                        if (this.Waypoints.InitialAngle == General.INITIAL_ANGLE) { this.Waypoints.InitialAngle = initialAngle; }
                        if (this.Waypoints.InitialMagnification == General.INITIAL_MAGNIFICATION) { this.Waypoints.InitialMagnification = initialMagnification; }
                        if (this.Waypoints.InitialOpacity == General.INITIAL_OPACITY) { this.Waypoints.InitialOpacity = initialOpacity; }
                        if (this.Waypoints.InitialBalance == General.INITIAL_BALANCE) { this.Waypoints.InitialBalance = initialBalance; }
                        if (this.Waypoints.InitialVolume == General.INITIAL_VOLUME) { this.Waypoints.InitialVolume = initialVolume; }

                        break;
                    }
                }
            }
            else
            {
                throw new XmlException("Reader wasn't sitting on an <Image> tag when ReadXml() was called.");
            }

            if (!closingTag)
            {
                throw new XmlException("Reader did not find a closing <Image> tag.");
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Image");
            writer.WriteAttributeString("Filename", this.filename);
            writer.WriteAttributeString("IsVideo", this.isVideo.ToString());

            if (this.isVideo)
            {
                XmlHelper.WriteTimeSpan(writer, this.Duration, "Duration");
                XmlHelper.WriteTimeSpan(writer, this.StartingPosition, "StartAt");
                XmlHelper.WriteTimeSpan(writer, this.EndingPosition, "LoopAt");
            }
            else
            {
                writer.WriteAttributeString("Loops", this.AnimationLoops.ToString());
            }

            XmlHelper.WriteSize(writer, this.dimensions, "Dimensions");

            this.Waypoints.WriteXml(writer);

            writer.WriteEndElement();
        }
        #endregion XML
    }

    internal class TADAAlertAudio : PropertyAndXML
    {
        private String filename = "";

        #region Properties
        [DisplayName("Filename")]
        public String Filename
        {
            get { return this.filename; }
            set
            {
                this.filename = value;
                NotifyPropertyChanged();
            }
        }

        [DisplayName("Balance")]
        public double Balance
        {
            get { return this.Waypoints.InitialBalance; }
            set
            {
                this.Waypoints.InitialBalance = value;
                NotifyPropertyChanged();
            }
        }

        [DisplayName("Volume")]
        public double Volume
        {
            get { return this.Waypoints.InitialVolume; }
            set
            {
                this.Waypoints.InitialVolume = value;
                NotifyPropertyChanged();
            }
        }

        public double TotalDuration
        {
            get
            {
                return this.Delay.TotalSeconds + this.Duration.TotalSeconds;
            }
        }
        #endregion Properties

        public TimeSpan Delay { get; set; } = new TimeSpan(0);
        public TimeSpan Duration { get; set; } = new TimeSpan(0);
        public TimeSpan StartingPosition { get; set; } = new TimeSpan(0);
        public TimeSpan EndingPosition { get; set; } = new TimeSpan(0);

        public WaypointContainer Waypoints { get; set; } = new WaypointContainer();

        // Constructor for XMLReader
        public TADAAlertAudio () { }

        // Constructor for Alert Designer.
        public TADAAlertAudio (String filename, TimeSpan delay, TimeSpan duration, TimeSpan startAt, TimeSpan endAt, WaypointContainer waypoints)
        {
            this.filename = filename;
            this.Delay = delay;
            this.Duration = duration;
            this.StartingPosition = startAt;
            this.EndingPosition = endAt;
            this.Waypoints = waypoints;
        }

        #region XML
        public override void ReadXml(XmlReader reader)
        {
            Boolean closingTag = false;

            if (reader.IsStartElement("Audio"))
            {
                this.Filename = reader.GetAttribute("Filename");

                // Have to read all of the 1.0-written properties.
                double initialAngle = XmlHelper.ReadAttribute(reader, "Angle", 0.0d);
                double initialMagnification = XmlHelper.ReadAttribute(reader, "Magnification", 1.0d);
                double initialOpacity = XmlHelper.ReadAttribute(reader, "Opacity", 100.0d);
                double initialBalance = XmlHelper.ReadAttribute(reader, "Balance", 0.0d);
                double initialVolume = XmlHelper.ReadAttribute(reader, "Volume", 0.0d);

                while (reader.Read())
                {
                    if (reader.IsStartElement("Time"))
                    {
                        XmlHelper.ReadTimeSpan(reader, out TimeSpan timeSpan, out String name);

                        switch (name)
                        {
                            case "Delay":
                                this.Delay = timeSpan;
                                break;
                            case "Duration":
                                this.Duration = timeSpan;
                                break;
                            case "StartAt":
                                this.StartingPosition = timeSpan;
                                break;
                            case "LoopAt":
                                this.EndingPosition = timeSpan;
                                break;
                        }
                    }
                    else if (reader.IsStartElement("Waypoints"))
                    {
                        this.Waypoints.ReadXml(reader);
                    }
                    else if (!reader.IsStartElement() && reader.Name == "Audio")
                    {
                        closingTag = true;

                        // And to write the 1.0 properties if they're there.
                        if (this.Waypoints.InitialAngle == General.INITIAL_ANGLE) { this.Waypoints.InitialAngle = initialAngle; }
                        if (this.Waypoints.InitialMagnification == General.INITIAL_MAGNIFICATION) { this.Waypoints.InitialMagnification = initialMagnification; }
                        if (this.Waypoints.InitialOpacity == General.INITIAL_OPACITY) { this.Waypoints.InitialOpacity = initialOpacity; }
                        if (this.Waypoints.InitialBalance == General.INITIAL_BALANCE) { this.Waypoints.InitialBalance = initialBalance; }
                        if (this.Waypoints.InitialVolume == General.INITIAL_VOLUME) { this.Waypoints.InitialVolume = initialVolume; }

                        break;
                    }
                }
            }
            else
            {
                throw new XmlException("Reader wasn't sitting on an <Audio> tag when ReadXml() was called.");
            }

            if (!closingTag)
            {
                throw new XmlException("Reader did not find a closing <Audio> tag.");
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Audio");
            writer.WriteAttributeString("Filename", this.filename);

            XmlHelper.WriteTimeSpan(writer, this.Delay, "Delay");
            XmlHelper.WriteTimeSpan(writer, this.Duration, "Duration");
            XmlHelper.WriteTimeSpan(writer, this.StartingPosition, "StartAt");
            XmlHelper.WriteTimeSpan(writer, this.EndingPosition, "LoopAt");

            this.Waypoints.WriteXml(writer);

            writer.WriteEndElement();
        }
        #endregion XML
    }

    internal class TADAAlertText : PropertyAndXML
    {
        private String message = "";
        private String readMessage = "";

        private String substitutedMessage = "";
        private String substitutedReadMessage = "";

        #region Properties
        [DisplayName("Displayed Message")]
        public String Message
        {
            get { return this.message; }
            set
            {
                this.message = value;
                NotifyPropertyChanged();
            }
        }

        [DisplayName("Narrated Message")]
        public String ReadMessage
        {
            get { return this.readMessage; }
            set
            {
                this.readMessage = value;
                NotifyPropertyChanged();
            }
        }

        public double TotalDuration
        {
            get
            {
                return this.Waypoints.TotalDuration;
            }
        }
        #endregion Properties

        public String TTSVoice { get; set; } = "";
        public String BackgroundImage { get; set; } = "";

        public Font ChosenFont { get; set; } = new Font(FontFamily.GenericSerif, 10);

        public float OutlineWidth { get; set; } = 0;

        public Color Foreground { get; set; } = Color.Black;
        public Color Background { get; set; } = Color.White;
        public Color Outline { get; set; } = Color.Black;

        public Size Dimensions { get; set; } = new Size(0, 0);
        public Size Padding { get; set; } = new Size(0, 0);

        public WaypointContainer Waypoints { get; set; } = new WaypointContainer();

        public TADAAlertText () { }

        public TADAAlertText(String message, String readMessage, String ttsVoice, Font font, float outlineWidth, Color foreground, Color background, Color outline, Size dimensions, Size padding, WaypointContainer waypoints)
        {
            this.Message = message;
            this.ReadMessage = readMessage;
            this.TTSVoice = ttsVoice;

            this.ChosenFont = font;
            this.OutlineWidth = outlineWidth;

            this.Foreground = foreground;
            this.Background = background;
            this.Outline = outline;

            this.Dimensions = dimensions;
            this.Padding = padding;

            this.Waypoints = waypoints;
        }

        public String GetMessage()
        {
            return String.IsNullOrEmpty(this.substitutedMessage) ? this.message : this.substitutedMessage;
        }

        public String GetMessage(Boolean raw)
        {
            return (raw ? this.message : GetMessage());
        }

        public void SetMessage (String message)
        {
            this.substitutedMessage = message;
        }

        public String GetReadMessage()
        {
            return String.IsNullOrEmpty(this.substitutedReadMessage) ? this.message : this.substitutedReadMessage;
        }

        public String GetReadMessage(Boolean raw)
        {
            String outString = null;
            if (raw)
            {
                outString = String.IsNullOrEmpty(this.readMessage) ? this.message : this.readMessage;
            }
            else
            {
                outString = GetReadMessage();
            }
            return outString;
        }

        public void SetReadMessage (String message)
        {
            this.substitutedReadMessage = message;
        }

        #region XML
        public override void ReadXml(XmlReader reader)
        {
            Boolean closingTag = false;
            Boolean readNext = true;

            if (reader.IsStartElement("Text"))
            {
                // Version 1.0 of the file writes Message and ReadMessage as attributes.
                this.Message = reader.GetAttribute("Message");
                this.ReadMessage = reader.GetAttribute("Read");
                this.TTSVoice = reader.GetAttribute("Voice");

                // Have to read all of the 1.0-written properties.
                double initialAngle = XmlHelper.ReadAttribute(reader, "Angle", 0.0d);
                double initialMagnification = XmlHelper.ReadAttribute(reader, "Magnification", 1.0d);
                double initialOpacity = XmlHelper.ReadAttribute(reader, "Opacity", 100.0d);
                double initialBalance = XmlHelper.ReadAttribute(reader, "Balance", 0.0d);
                double initialVolume = XmlHelper.ReadAttribute(reader, "Volume", 0.0d);

                while (!closingTag)
                {
                    if (readNext)
                    {
                        reader.Read();
                    }

                    readNext = true;

                    // Version 2.0 of the file writes Message and ReadMessage as elements with String content.
                    if (reader.IsStartElement("Message"))
                    {
                        this.Message = reader.ReadElementContentAsString();
                        readNext = false;
                    }
                    else if (reader.IsStartElement("Read"))
                    {
                        this.ReadMessage = reader.ReadElementContentAsString();
                        readNext = false;
                    }
                    else if (reader.IsStartElement("Background"))
                    {
                        this.BackgroundImage = reader.ReadElementContentAsString();
                        readNext = false;
                    }
                    else if (reader.IsStartElement("Font"))
                    {
                        String fontface = reader.GetAttribute("Name");
                        int points = Int32.Parse(reader.GetAttribute("Size"));
                        this.OutlineWidth = float.Parse(reader.GetAttribute("Outline"));
                        this.ChosenFont = new Font(fontface, points);
                    }
                    else if (reader.IsStartElement("Color") || reader.IsStartElement("Colour"))
                    {
                        XmlHelper.ReadColor(reader, out Color color, out String name);

                        switch (name)
                        {
                            case "Foreground":
                                this.Foreground = color;
                                break;
                            case "Background":
                                this.Background = color;
                                break;
                            case "Outline":
                                this.Outline = color;
                                break;
                        }
                    }
                    else if (reader.IsStartElement("Size"))
                    {
                        XmlHelper.ReadSize(reader, out Size size, out String name);

                        switch (name)
                        {
                            case "Dimensions":
                                this.Dimensions = size;
                                break;
                            case "Padding":
                                this.Padding = size;
                                break;
                        }
                    }
                    else if (reader.IsStartElement("Waypoints"))
                    {
                        this.Waypoints.ReadXml(reader);
                    }
                    else if (!reader.IsStartElement() && reader.Name == "Text")
                    {
                        closingTag = true;

                        // And to write the 1.0 properties if they're there.  Can't do it in <Waypoints> because there's no guarantee that tag exists.
                        if (this.Waypoints.InitialAngle == General.INITIAL_ANGLE) { this.Waypoints.InitialAngle = initialAngle; }
                        if (this.Waypoints.InitialMagnification == General.INITIAL_MAGNIFICATION) { this.Waypoints.InitialMagnification = initialMagnification; }
                        if (this.Waypoints.InitialOpacity == General.INITIAL_OPACITY) { this.Waypoints.InitialOpacity = initialOpacity; }
                        if (this.Waypoints.InitialBalance == General.INITIAL_BALANCE) { this.Waypoints.InitialBalance = initialBalance; }
                        if (this.Waypoints.InitialVolume == General.INITIAL_VOLUME) { this.Waypoints.InitialVolume = initialVolume; }

                        break;
                    }
                }
            }
            else
            {
                throw new XmlException("Reader wasn't sitting on a <Text> tag when ReadXml() was called.");
            }

            if (!closingTag)
            {
                throw new XmlException("Reader did not find a closing <Text> tag.");
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Text");
            writer.WriteAttributeString("Voice", this.TTSVoice);

            writer.WriteElementString("Message", this.Message);
            writer.WriteElementString("Read", this.ReadMessage);
            if (!String.IsNullOrEmpty(this.BackgroundImage))
            {
                writer.WriteElementString("Background", this.BackgroundImage);
            }

            writer.WriteStartElement("Font");
            writer.WriteAttributeString("Name", this.ChosenFont.Name);
            writer.WriteAttributeString("Size", this.ChosenFont.SizeInPoints.ToString());
            writer.WriteAttributeString("Outline", this.OutlineWidth.ToString());
            writer.WriteEndElement();

            XmlHelper.WriteColor(writer, this.Foreground, "Foreground");
            XmlHelper.WriteColor(writer, this.Background, "Background");
            XmlHelper.WriteColor(writer, this.Outline, "Outline");

            XmlHelper.WriteSize(writer, this.Dimensions, "Dimensions");
            XmlHelper.WriteSize(writer, this.Padding, "Padding");

            this.Waypoints.WriteXml(writer);

            writer.WriteEndElement();
        }
        #endregion XML
    }

    internal class TADAAlertCondition : PropertyAndXML
    {
        #region Properties
        [DisplayName ("Viewer's Subscription Tier")]
        public String ViewerSubTier
        {
            get
            {
                int count = (ViewerTierNoTier ? 1 : 0) +
                            (ViewerTierTierOne ? 1 : 0) +
                            (ViewerTierTierTwo ? 1 : 0) +
                            (ViewerTierTierThree ? 1 : 0);

                String retval;

                // This absurd construction creates a human-readable phrase that describes the tier of user that can trigger the alert.
                switch (count)
                {
                    case 0:
                    case 4:
                        retval = "Any viewer, regardless of subscription tier, ";
                        break;
                    case 1:
                        if (ViewerTierNoTier)
                        {
                            retval = "Only someone without an active subscription";
                        }
                        else
                        {
                            retval = "Only someone subscribed at tier " + (ViewerTierTierOne ? "one" : ViewerTierTierTwo ? "two" : "three");
                        }
                        break;
                    default:
                        if (ViewerTierNoTier)
                        {
                            retval = "Someone with no active subscription, or subscribed at ";
                            if (ViewerTierTierOne && ViewerTierTierTwo)
                            {
                                retval += "tier one or tier two";
                            }
                            else if (ViewerTierTierOne && ViewerTierTierThree)
                            {
                                retval += "tier one or tier three";
                            }
                            else if (ViewerTierTierTwo && ViewerTierTierThree)
                            {
                                retval += "tier two or tier three";
                            }
                            else
                            {
                                retval += "tier " + (ViewerTierTierOne ? "one" : ViewerTierTierTwo ? "two" : "three");
                            }
                        }
                        else
                        {
                            retval = "Someone subscribed at ";
                            if (ViewerTierTierOne && ViewerTierTierTwo && ViewerTierTierThree)
                            {
                                retval += "any tier ";
                            }
                            else
                            {
                                if (ViewerTierTierOne)
                                {
                                    retval += "tier one or " + (ViewerTierTierTwo ? "two" : "three");
                                }
                                else
                                {
                                    retval += "tier two or three";
                                }
                            }
                        }

                        break;
                }

                retval += " can trigger this alert.";

                return retval;
            }
        }

        [DisplayName("Subscription Tier")]
        public String SubscriptionTier
        {
            get
            {
                int count = (SubscriptionTierPrime ? 1 : 0) +
                            (SubscriptionTierTierOne ? 1 : 0) +
                            (SubscriptionTierTierTwo ? 1 : 0) +
                            (SubscriptionTierTierThree ? 1 : 0);

                String retval;

                // This absurd construction creates a human-readable phrase that describes the subscription tier(s) that can trigger this alert.
                switch (count)
                {
                    case 0:
                    case 4:
                        retval = "A subscription at any tier";
                        break;
                    case 1:
                        retval = String.Format("Only a {0} subscription", (SubscriptionTierPrime ? "Twitch Prime" : ("tier " + (SubscriptionTierTierOne ? "one" : (SubscriptionTierTierTwo ? "two" : "three")))));
                        break;
                    case 3:
                        if (!SubscriptionTierPrime)
                        {
                            retval = "A tier one, two, or three subscription";
                        }
                        else if (!SubscriptionTierTierOne)
                        {
                            retval = "A Twitch Prime, tier two, or tier three subscription";
                        }
                        else if (!SubscriptionTierTierTwo)
                        {
                            retval = "A Twitch Prime, tier one, or tier three subscription";
                        }
                        else
                        {
                            retval = "A Twitch Prime, tier one, or tier two subscription";
                        }
                        break;
                    default:
                        if (SubscriptionTierPrime)
                        {
                            retval = "A Twitch Prime or tier " + (SubscriptionTierTierOne ? "one" : (SubscriptionTierTierTwo ? "two" : "three")) + " subscription";
                        }
                        else if (SubscriptionTierTierOne)
                        {
                            retval = "A tier one or " + (SubscriptionTierTierTwo ? "two" : "three") + " subscription";
                        }
                        else
                        {
                            retval = "A tier two or three subscription";
                        }
                        break;
                }

                retval += " can trigger this alert.";
                return retval;
            }
        }

        [DisplayName("Viewer Badges")]
        public String BadgeString
        {
            get
            {
                String retval = "";
                bool concat = false;

                int count = (IsAdmin ? 1 : 0) +
                            (IsBits ? 1 : 0) +
                            (IsBroadcaster ? 1 : 0) +
                            (IsGlobalMod ? 1 : 0) +
                            (IsModerator ? 1 : 0) +
                            (IsPrime ? 1 : 0) +
                            (IsSubscribed ? 1 : 0) +
                            (IsStaff ? 1 : 0) +
                            (IsTurbo ? 1 : 0) +
                            (IsVIP ? 1 : 0);


                if (IsAdmin)
                {
                    retval += "The viewer must be a Twitch Admin";
                    concat = true;
                    --count;
                }

                if (IsBits)
                {
                    retval += (concat ? ", " + (count == 1 ? "and " : "") : "The viewer ");
                    concat = true;
                    --count;

                    if (MinBits == 0 && MaxBits == 0)
                    {
                        retval += "must have dropped at least one bit in the channel";
                    }
                    else if (MinBits == 0)
                    {
                        retval += $"must have dropped no more than {MaxBits} bits in the channel";
                    }
                    else if (MaxBits == 0)
                    {
                        retval += $"must have dropped at least {MinBits} bits in the channel";
                    }
                    else if (MinBits == MaxBits)
                    {
                        retval += $"must have the {MinBits} bit badge";
                    }
                    else
                    {
                        retval += $"must have dropped between {MinBits} and {MaxBits} bits in the channel";
                    }
                }

                if (IsBroadcaster || IsModerator || IsVIP)
                {
                    retval += (concat ? ", " : "The viewer ");

                    int internalCount = (IsBroadcaster ? 1 : 0) +
                                        (IsModerator ? 1 : 0) +
                                        (IsVIP ? 1 : 0);

                    if (internalCount == 3)
                    {
                        retval += (concat && count == 3 ? "and " : "") + "must be you, one of your moderators, or one of your VIPs";
                        count -= 3;
                    }
                    else if (internalCount == 2)
                    {
                        retval += (concat && count == 2 ? "and " : "");
                        count -= 2;

                        if (!IsBroadcaster)
                        {
                            retval += "must be one of your moderators or your VIPs";
                        }
                        else if (!IsModerator)
                        {
                            retval += "must be you or one of your VIPs";
                        }
                        else
                        {
                            retval += "must be you or one of your moderators";
                        }
                    }
                    else
                    {
                        retval += (concat && count == 1 ? "and " : "") + "must be " + (IsBroadcaster ? "you" : "one of your " + (IsModerator ? "moderators" : "VIPs"));
                        --count;
                    }

                    concat = true;
                }

                if (IsGlobalMod)
                {
                    retval += (concat ? ", " + (count == 1 ? "and " : "") : "The viewer ") + "must be a Twitch Global Moderator";
                    concat = true;
                    --count;
                }


                if (IsPrime)
                {
                    retval += (concat ? ", " + (count == 1 ? "and " : "") : "The viewer ") + "must pay for Twitch Prime";
                    concat = true;
                    --count;
                }

                if (IsSubscribed)
                {
                    retval += (concat ? ", " + (count == 1 ? "and " : "") : "The viewer ");
                    concat = true;
                    --count;

                    if (MinMonths == 0 && MaxMonths == 0)
                    {
                        retval += "must be subscribed to the channel";
                    }
                    else if (MinMonths == 0)
                    {
                        retval += $"must be subscribed to the channel for at most {MaxMonths} months";
                    }
                    else if (MaxMonths == 0)
                    {
                        retval += $"must be subscribed to the channel for more than {MinMonths} months";
                    }
                    else if (MinMonths == MaxMonths)
                    {
                        retval += $"must be subscribed to the channel for exactly {MinMonths} months";
                    }
                    else
                    {
                        retval += $"must be subscribed to the channel for between {MinMonths} and {MaxMonths} months";
                    }
                }

                if (IsStaff)
                {
                    retval += (concat ? ", " + (count == 1 ? "and " : "") : "The viewer ") + "must be a member of Twitch staff";
                    concat = true;
                    --count;
                }

                if (IsTurbo)
                {
                    retval += (concat ? ", " + (count == 1 ? "and " : "") : "The viewer ") + "must subscribe to Twitch Turbo";
                    concat = true;
                    --count;
                }

                retval += ".";

                return retval;
            }
        }
        #endregion

        public bool ViewerTierNoTier;
        public bool ViewerTierTierOne;
        public bool ViewerTierTierTwo;
        public bool ViewerTierTierThree;

        public bool SubscriptionTierPrime;
        public bool SubscriptionTierTierOne;
        public bool SubscriptionTierTierTwo;
        public bool SubscriptionTierTierThree;

        public bool IsAdmin;
        public bool IsBits;
        public bool IsBroadcaster;
        public bool IsGlobalMod;
        public bool IsModerator;
        public bool IsPrime;
        public bool IsSubscribed;
        public bool IsStaff;
        public bool IsTurbo;
        public bool IsVIP;

        public String Usernames;
        public String Keywords;

        public int MinBits;
        public int MaxBits;

        public int MinMonths;
        public int MaxMonths;

        public int MinAmount;
        public int MaxAmount;

        public bool Empty
        {
            get
            {
                return !(HasViewerTier || HasSubscriptionTier || HasBadges || HasUsers || HasKeywords || HasAmounts);
            }
        }

        public bool HasViewerTier
        {
            get
            {
                return ViewerTierNoTier || ViewerTierTierOne || ViewerTierTierTwo || ViewerTierTierThree;
            }
        }

        public bool HasSubscriptionTier
        {
            get
            {
                return SubscriptionTierPrime || SubscriptionTierTierOne || SubscriptionTierTierTwo || SubscriptionTierTierThree;
            }
        }

        public bool HasBadges
        {
            get
            {
                return IsAdmin || IsBits || IsBroadcaster || IsGlobalMod || IsModerator || IsPrime || IsSubscribed || IsStaff || IsTurbo || IsVIP;
            }
        }

        public bool HasUsers
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.Usernames);
            }
        }

        public bool HasKeywords
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.Keywords);
            }
        }

        public bool HasAmounts
        {
            get
            {
                return !(this.MinAmount == 0 && this.MaxAmount == 0);
            }
        }
               
        #region XML
        public override void ReadXml(XmlReader reader)
        {
            /*
             <Condition>
                <ViewerTier NoTier="true" TierOne="true" TierTwo="true" TierThree="true" />
                <SubscriptionTier TwitchPrime="true" TierOne="true" TierTwo="True" TierThree="true" />
                <Badges>
                    <SubscribeMonths Min="1" Max="1" />
                    <BitBadge Min="0" Max="1000000" />
                </Badges>
            </Condition>
             */
            Boolean closingTag = false;
            Boolean readNext = true;

            if (reader.IsStartElement("Condition"))
            {
                while (!closingTag)
                {
                    if (readNext)
                    {
                        reader.Read();
                    }

                    readNext = true;

                    if (reader.IsStartElement("ViewerTier"))
                    {
                        this.ViewerTierNoTier = XmlHelper.ReadAttribute(reader, "NoTier", false);
                        this.ViewerTierTierOne = XmlHelper.ReadAttribute(reader, "TierOne", false);
                        this.ViewerTierTierTwo = XmlHelper.ReadAttribute(reader, "TierTwo", false);
                        this.ViewerTierTierThree = XmlHelper.ReadAttribute(reader, "TierThree", false);

                        if (reader.NodeType != XmlNodeType.EndElement)
                        {
                            XmlHelper.ReadUntilClose(reader, "ViewerTier");
                            if (reader.EOF)
                            {
                                throw new XmlException("Did not find a self-closing or closed ViewerTier tag.");
                            }
                        }
                    }
                    else if (reader.IsStartElement("SubscriptionTier"))
                    {
                        this.SubscriptionTierPrime = XmlHelper.ReadAttribute(reader, "Prime", false);
                        this.SubscriptionTierTierOne = XmlHelper.ReadAttribute(reader, "TierOne", false);
                        this.SubscriptionTierTierTwo = XmlHelper.ReadAttribute(reader, "TierTwo", false);
                        this.SubscriptionTierTierThree = XmlHelper.ReadAttribute(reader, "TierThree", false);

                        if (reader.NodeType != XmlNodeType.EndElement)
                        {
                            XmlHelper.ReadUntilClose(reader, "SubscriptionTier");
                            if (reader.EOF)
                            {
                                throw new XmlException("Did not find a self-closing or closed SubscriptionTier tag.");
                            }
                        }
                    }
                    else if (reader.IsStartElement("Badges"))
                    {
                        this.IsAdmin = XmlHelper.ReadAttribute(reader, "Admin", false);
                        this.IsBroadcaster = XmlHelper.ReadAttribute(reader, "Broadcaster", false);
                        this.IsGlobalMod = XmlHelper.ReadAttribute(reader, "GlobalMod", false);
                        this.IsModerator = XmlHelper.ReadAttribute(reader, "Moderator", false);
                        this.IsPrime = XmlHelper.ReadAttribute(reader, "Prime", false);
                        this.IsTurbo = XmlHelper.ReadAttribute(reader, "Turbo", false);
                        this.IsStaff = XmlHelper.ReadAttribute(reader, "Staff", false);
                        this.IsVIP = XmlHelper.ReadAttribute(reader, "VIP", false);

                        if (reader.NodeType != XmlNodeType.EndElement)
                        {
                            while (reader.Read())
                            {
                                // Check for the Bits tag.
                                if (reader.IsStartElement("Bits"))
                                {
                                    this.MinBits = Convert.ToInt32(XmlHelper.ReadAttribute(reader, "Min", 0));
                                    this.MaxBits = Convert.ToInt32(XmlHelper.ReadAttribute(reader, "Max", 0));
                                    this.IsBits = true;
                                }
                                else if (reader.IsStartElement("Subscription"))
                                {
                                    this.MinMonths = Convert.ToInt32(XmlHelper.ReadAttribute(reader, "Min", 0));
                                    this.MaxMonths = Convert.ToInt32(XmlHelper.ReadAttribute(reader, "Max", 0));
                                    this.IsSubscribed = true;
                                }
                                else if (reader.Name == "Badges" && reader.NodeType == XmlNodeType.EndElement)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (reader.IsStartElement("Usernames"))
                    {
                        this.Usernames = reader.ReadElementContentAsString();

                        // This filthy hack is required for using XmlReader instead of turning it into an XMLDocument that I have no schema for.
                        readNext = false;
                    }
                    else if (reader.IsStartElement("Keywords"))
                    {
                        this.Keywords = reader.ReadElementContentAsString();

                        // This filthy hack is required for using XmlReader instead of turning it into an XMLDocument that I have no schema for.
                        readNext = false;
                    }
                    else if (reader.IsStartElement("Time"))
                    {
                        /*
                         * <Time>
                         *  <Day Mon=true Tues=false Wed=false Thur=false Fri=true Sat=false Sun=true>
                         *  <Hour Start=22:00 End=2:00> -- make it wrap from PM through AM somehow.
                         *  <Date Start=YY?/MM/DD End=YY?MM/DD>
                         * </Time>
                         * 
                         * Make this as configurable as possible -- Holiday repeating events, extra trigger conditions, birthdays, etc.
                         */
                    }
                    else if (reader.Name == "Amount")
                    {
                        this.MinAmount = Convert.ToInt32(XmlHelper.ReadAttribute(reader, "Min", 0));
                        this.MaxAmount = Convert.ToInt32(XmlHelper.ReadAttribute(reader, "Max", 0));
                    }
                    else if (reader.Name == "Condition" && reader.NodeType == XmlNodeType.EndElement)
                    {
                        closingTag = true;
                        break;
                    }
                    else if (reader.EOF)
                    {
                        throw new XmlException("Did not find a closing <Conditions> tag.");
                    }
                }
            }
            else
            {
                throw new XmlException("Reader was not pointed at a <Condition> tag when ReadXml() was called.");
            }

            if (!closingTag)
            {
                throw new XmlException("Did not find a closing <Conditions> tag.");
            }
        }

        public void WriteAttributes(XmlWriter writer, List<Tuple<Boolean, String>> tuples)
        {
            foreach (Tuple <Boolean, String> tuple in tuples)
            {
                if (tuple.Item1)
                {
                    writer.WriteAttributeString(tuple.Item2, "true");
                }
            }
        }
        
        public override void WriteXml(XmlWriter writer)
        {
            if (!this.Empty)
            {
                /*
                 <Condition>
                    <ViewerTier NoTier="true" TierOne="true" TierTwo="true" TierThree="true" />
                    <SubscriptionTier TwitchPrime="true" TierOne="true" TierTwo="True" TierThree="true" />
                    <Badges>
                        <SubscribeMonths Min="1" Max="1" />
                        <BitBadge Min="0" Max="1000000" />
                    </Badges>
                </Condition>
                 */

                writer.WriteStartElement("Condition");

                if (HasViewerTier)
                {
                    List<Tuple<Boolean, String>> viewerTiers = new List<Tuple<Boolean, String>>()
                    {
                        new Tuple<Boolean, String>(this.ViewerTierNoTier, "NoTier"),
                        new Tuple<Boolean, String>(this.ViewerTierTierOne, "TierOne"),
                        new Tuple<Boolean, String>(this.ViewerTierTierTwo, "TierTwo"),
                        new Tuple<Boolean, String>(this.ViewerTierTierThree, "TierThree")
                    };

                    writer.WriteStartElement("ViewerTier");
                    WriteAttributes(writer, viewerTiers);
                    writer.WriteEndElement();
                }

                if (HasSubscriptionTier)
                {
                    List<Tuple<Boolean, String>> subscriptionTiers = new List<Tuple<Boolean, String>>()
                    {
                        new Tuple<Boolean, String>(this.SubscriptionTierPrime, "Prime"),
                        new Tuple<Boolean, String>(this.SubscriptionTierTierOne, "TierOne"),
                        new Tuple<Boolean, String>(this.SubscriptionTierTierTwo, "TierTwo"),
                        new Tuple<Boolean, String>(this.SubscriptionTierTierThree, "TierThree")
                    };

                    writer.WriteStartElement("SubscriptionTier");
                    WriteAttributes(writer, subscriptionTiers);
                    writer.WriteEndElement();
                }

                if (HasBadges)
                {
                    List<Tuple<Boolean, String>> simpleBadges = new List<Tuple<Boolean, String>>()
                    {
                        new Tuple<Boolean, String>(this.IsAdmin, "Admin"),
                        new Tuple<Boolean, String>(this.IsBroadcaster, "Broadcaster"),
                        new Tuple<Boolean, String>(this.IsGlobalMod, "GlobalMod"),
                        new Tuple<Boolean, String>(this.IsModerator, "Moderator"),
                        new Tuple<Boolean, String>(this.IsPrime, "Prime"),
                        new Tuple<Boolean, String>(this.IsTurbo, "Turbo"),
                        new Tuple<Boolean, String>(this.IsStaff, "Staff"),
                        new Tuple<Boolean, String>(this.IsVIP, "VIP")
                    };

                    writer.WriteStartElement("Badges");
                    WriteAttributes(writer, simpleBadges);

                    if (this.IsBits)
                    {
                        writer.WriteStartElement("Bits");
                        writer.WriteAttributeString("Min", this.MinBits.ToString());
                        writer.WriteAttributeString("Max", this.MaxBits.ToString());
                        writer.WriteEndElement();
                    }

                    if (this.IsSubscribed)
                    {
                        writer.WriteStartElement("Subscription");
                        writer.WriteAttributeString("Min", this.MinMonths.ToString());
                        writer.WriteAttributeString("Max", this.MaxMonths.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                if (!String.IsNullOrEmpty(this.Usernames) && !String.IsNullOrWhiteSpace(this.Usernames))
                {
                    writer.WriteStartElement("Usernames");
                    writer.WriteString(this.Usernames);
                    writer.WriteEndElement();
                }

                if (!String.IsNullOrEmpty(this.Keywords) && !String.IsNullOrWhiteSpace(this.Keywords))
                {
                    writer.WriteStartElement("Keywords");
                    writer.WriteString(this.Keywords);
                    writer.WriteEndElement();
                }

                if (this.MinAmount > 0 || this.MaxAmount > 0)
                {
                    writer.WriteStartElement("Amount");
                    writer.WriteAttributeString("Min", this.MinAmount.ToString());
                    writer.WriteAttributeString("Max", this.MaxAmount.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }
        #endregion XML
    }

    public class SubstitutionObject
    {
        public String AlertType { get; set; } = General.alertTypes[General.ALERTTYPES.NONE];
        public int Amount { get; set; } = 0;
        public bool VIP { get; set; } = false;
        public String Message { get; set; } = "";
        public String RecipientName { get; set; } = "";
        public String ViewerName { get; set; } = "";
        public String TierName { get; set; } = "";

        public TierAndPlanConverter.SUBSCRIPTION_PLANS Plan { get; protected set; }
        public TierAndPlanConverter.SUBSCRIPTION_TIERS Tier { get; set; } = TierAndPlanConverter.SUBSCRIPTION_TIERS.NO_TIER;

        public void SetPlan(TwitchLib.Client.Enums.SubscriptionPlan plan)
        {
            Plan = TierAndPlanConverter.NormalizePlanName(plan);
            Tier = TierAndPlanConverter.SubscriptionTierFromPlan(Plan);
        }

        public void SetPlan(TierAndPlanConverter.SUBSCRIPTION_PLANS plan)
        {
            Plan = plan;
            Tier = TierAndPlanConverter.SubscriptionTierFromPlan(Plan);
        }

        public String PlanString
        {
            get { return TierAndPlanConverter.SubscriptionPlanToString(Plan); }
        }

        public String TierString
        {
            get { return TierAndPlanConverter.SubscriptionTierToString(Tier); }
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}", this.ViewerName, this.Amount, this.Message, this.TierName);
        }
    }
}