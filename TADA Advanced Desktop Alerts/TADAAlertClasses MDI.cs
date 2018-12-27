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

        internal Macro macroKey;

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
        #endregion PropertyStuff

        #region XML
        public override void ReadXml(XmlReader reader)
        {
            Boolean closingTag = false;
            var error = false;

            this.alertName = reader.GetAttribute("Name");
            this.alertType = General.alertStrings[reader.GetAttribute("Type")];

            while (reader.Read())
            {
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
                throw new XmlException("Reader did not find a closing <Text> tag.");
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Alert");
            writer.WriteAttributeString("Name", alertName);
            writer.WriteAttributeString("Type", General.alertTypes[alertType]);

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
                this.Ctrl = XmlHelper.ReadBooleanAttribute(reader, "Ctrl");
                this.Alt = XmlHelper.ReadBooleanAttribute(reader, "Alt");
                this.Shift = XmlHelper.ReadBooleanAttribute(reader, "Shift");
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
                        XmlHelper.Read<Waypoint>(reader, "Waypoint", "Waypoints", this.Waypoints.Waypoints);
                    }
                    else if (!reader.IsStartElement() && reader.Name == "Image")
                    {
                        closingTag = true;
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

            if (this.Waypoints.Waypoints.Count > 0)
            {
                this.Waypoints.WriteXml(writer);
            }

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

            if (this.Waypoints.Waypoints.Count > 0)
            {
                this.Waypoints.WriteXml(writer);
            }

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
            return this.substitutedMessage ?? this.message;
        }

        public void SetMessage (String message)
        {
            this.substitutedMessage = message;
        }

        public String GetReadMessage()
        {
            return this.substitutedReadMessage ?? this.readMessage;
        }

        public void SetReadMessage (String message)
        {
            this.substitutedReadMessage = message;
        }

        #region XML
        public override void ReadXml(XmlReader reader)
        {
            Boolean closingTag = false;

            if (reader.IsStartElement("Text"))
            {
                // Version 1.0 of the file writes Message and ReadMessage as attributes.
                this.Message = reader.GetAttribute("Message");
                this.ReadMessage = reader.GetAttribute("Read");
                this.TTSVoice = reader.GetAttribute("Voice");
                
                while (reader.Read())
                {
                    // Version 2.0 of the file writes Message and ReadMessage as elements with String content.
                    if (reader.IsStartElement("Message"))
                    {
                        this.Message = reader.ReadContentAsString();
                    }
                    else if (reader.IsStartElement("Read"))
                    {
                        this.ReadMessage = reader.ReadContentAsString();
                    }
                    else if (reader.IsStartElement("Background"))
                    {
                        this.BackgroundImage = reader.ReadContentAsString();
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
        [DisplayName("No Tier")]
        public bool NoTier { get; set; }

        [DisplayName("Twitch Prime")]
        public bool TwitchPrime { get; set; }

        [DisplayName("Tier One")]
        public bool TierOne { get; set; }

        [DisplayName("Tier Two")]
        public bool TierTwo { get; set; }

        [DisplayName("Tier Three")]
        public bool TierThree { get; set; }

        [DisplayName("VIP")]
        public bool VIP { get; set; }

        [DisplayName("Usernames")]
        public String Usernames { get; set; }

        [DisplayName("Keywords")]
        public String Keywords { get; set; }

        [DisplayName("Minimum")]
        public int Minimum { get; set; }

        [DisplayName("Maximum")]
        public int Maximum { get; set; }

        [DisplayName("Keypress")]
        public String Keypress { get; set; }
        #endregion Properties

        #region XML
        public override void ReadXml(XmlReader reader)
        {
            if (reader.Name == "Condition")
            {
                NoTier = Boolean.Parse(reader.GetAttribute("NoTier"));
                TwitchPrime = Boolean.Parse(reader.GetAttribute("TwitchPrime"));
                TierOne = Boolean.Parse(reader.GetAttribute("TierOne"));
                TierTwo = Boolean.Parse(reader.GetAttribute("TierTwo"));
                TierThree = Boolean.Parse(reader.GetAttribute("TierThree"));

                VIP = Boolean.Parse(reader.GetAttribute("VIP"));

                Usernames = (String.IsNullOrEmpty(reader.GetAttribute("Usernames")) ? "" : reader.GetAttribute("Usernames"));
                Keywords = (String.IsNullOrEmpty(reader.GetAttribute("Keywords")) ? "" : reader.GetAttribute("Keywords"));
                Keypress = (String.IsNullOrEmpty(reader.GetAttribute("Keypress")) ? "" : reader.GetAttribute("Keypress"));

                Minimum = (Int32.TryParse(reader.GetAttribute("Minimum"), out int val) ? val : 0);
                Maximum = (Int32.TryParse(reader.GetAttribute("Maximum"), out val) ? val : 0);
            }
            else
            {
                throw new XmlException("Reader was not pointed at a <Condition> tag when ReadXml() was called.");
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            List<Tuple<Boolean, String>> pairs = new List<Tuple<bool, string>>()
            {
                new Tuple<Boolean, String>(this.NoTier, "NoTier"),
                new Tuple<Boolean, String>(this.TwitchPrime, "TwitchPrime"),
                new Tuple<Boolean, String>(this.TierOne, "TierOne"),
                new Tuple<Boolean, String>(this.TierTwo, "TierTwo"),
                new Tuple<Boolean, String>(this.TierThree, "TierThree"),
                new Tuple<Boolean, String>(this.VIP, "VIP")
            };

            writer.WriteStartElement("Condition");
            foreach (Tuple<Boolean, String> pair in pairs)
            {
                if (pair.Item1)
                {
                    writer.WriteAttributeString(pair.Item2, "true");
                }
            }

            if (!String.IsNullOrEmpty(Usernames))
            {
                writer.WriteAttributeString("Usernames", this.Usernames);
            }

            if (!String.IsNullOrEmpty(Keywords))
            {
                writer.WriteAttributeString("Keywords", this.Keywords);
            }

            if (this.Minimum > 0)
            {
                writer.WriteAttributeString("Minimum", this.Minimum.ToString());
            }

            if (this.Maximum > 0)
            {
                writer.WriteAttributeString("Maximum", this.Maximum.ToString());
            }

            if (String.IsNullOrEmpty(this.Keypress))
            {
                writer.WriteAttributeString("Keypress", this.Keypress);
            }

            writer.WriteEndElement();
        }
        #endregion XML
    }
}