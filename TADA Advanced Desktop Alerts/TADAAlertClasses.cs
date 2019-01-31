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

namespace TADA_Advanced_Desktop_Alerts
{
    public class oldTADAAlert : INotifyPropertyChanged, IXmlSerializable
    {
        private String alertName;
        private String alertType;

        private List<oldTADAAlertImage> images = new List<oldTADAAlertImage>();
        private List<oldTADAAlertAudio> audios = new List<oldTADAAlertAudio>();
        private List<oldTADAAlertText> streamerTexts = new List<oldTADAAlertText>();
        private List<oldTADAAlertText> viewerTexts = new List<oldTADAAlertText>();
        private List<oldTADAAlertCondition> conditions = new List<oldTADAAlertCondition>();

        public List<oldTADAAlertImage> Images
        {
            get { return this.images; }
            set
            {
                this.images = value;
                NotifyPropertyChanged("ImageCount");
            }
        }

        public List<oldTADAAlertAudio> Audios

        {
            get { return this.audios; }
            set
            {
                this.audios = value;
                NotifyPropertyChanged("AudioCount");
            }
        }

        public List<oldTADAAlertText> StreamerTexts
        {
            get { return this.streamerTexts; }
            set
            {
                this.streamerTexts = value;
                NotifyPropertyChanged("StreamerCount");
            }
        }

        public List<oldTADAAlertText> ViewerTexts
        {
            get { return this.viewerTexts; }
            set
            {
                this.viewerTexts = value;
                NotifyPropertyChanged("ViewerCount");
            }
        }

        public List<oldTADAAlertCondition> Conditions
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
                if (this.alertName != value)
                {
                    this.alertName = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        [DisplayName("Alert Type")]
        public String AlertType
        {
            get { return this.alertType; }
            set
            {
                if (this.alertType != value)
                {
                    this.alertType = value;
                    NotifyPropertyChanged("AlertType");
                }
            }
        }

        [DisplayName("Images")]
        public int ImageCount
        {
            get { return Images.Count; }
        }

        [DisplayName("Audio")]
        public int AudioCount
        {
            get { return Audios.Count; }
        }

        [DisplayName("Your Text")]
        public int StreamerCount
        {
            get { return StreamerTexts.Count; }
        }

        [DisplayName("Viewer Text")]
        public int ViewerCount
        {
            get { return ViewerTexts.Count; }
        }

        [DisplayName("Conditions")]
        public int ConditionCount
        {
            get { return Conditions.Count; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void Read<T>(XmlReader reader, String start, String end, List<T> listT) where T : IXmlSerializable, new()
        {
            while (reader.Read())
            {
                if (reader.IsStartElement(start))
                {
                    T newItem = new T();
                    newItem.ReadXml(reader);
                    listT.Add(newItem);
                }
                else if (!reader.IsStartElement() && reader.Name == end)
                {
                    break;
                }
            }
        }

        public void ReadXml(XmlReader reader)
        {
            var error = false;

            this.alertName = reader.GetAttribute("Name");
            this.alertType = reader.GetAttribute("Type");

            while (reader.Read())
            {
                if (reader.IsStartElement("Images"))
                {
                    Read<oldTADAAlertImage>(reader, "Image", "Images", this.images);
                }
                else if (reader.IsStartElement("Audios"))
                {
                    Read<oldTADAAlertAudio>(reader, "Audio", "Audios", this.audios);
                }
                else if (reader.IsStartElement("StreamerTexts"))
                {
                    Read<oldTADAAlertText>(reader, "Text", "StreamerTexts", this.streamerTexts);
                }
                else if (reader.IsStartElement("ViewerTexts"))
                {
                    Read<oldTADAAlertText>(reader, "Text", "ViewerTexts", this.viewerTexts);
                }
                else if (reader.IsStartElement("Conditions"))
                {
                    Read<oldTADAAlertCondition>(reader, "Condition", "Conditions", this.conditions);
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
                    break;
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Alert");
            writer.WriteAttributeString("Name", alertName);
            writer.WriteAttributeString("Type", alertType);

            if (images.Count > 0)
            {
                writer.WriteStartElement("Images");
                foreach (oldTADAAlertImage image in images) { image.WriteXml(writer); }
                writer.WriteEndElement();
            }

            if (audios.Count > 0)
            {
                writer.WriteStartElement("Audios");
                foreach (oldTADAAlertAudio audio in audios) { audio.WriteXml(writer); }
                writer.WriteEndElement();
            }

            if (streamerTexts.Count > 0)
            {
                writer.WriteStartElement("StreamerTexts");
                foreach (oldTADAAlertText text in streamerTexts) { text.WriteXml(writer); }
                writer.WriteEndElement();
            }

            if (viewerTexts.Count > 0)
            {
                writer.WriteStartElement("ViewerTexts");
                foreach (oldTADAAlertText text in viewerTexts) { text.WriteXml(writer); }
                writer.WriteEndElement();
            }

            if (conditions.Count > 0)
            {
                writer.WriteStartElement("Conditions");
                foreach (oldTADAAlertCondition condition in conditions) { condition.WriteXml(writer); }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public override string ToString()
        {
            return String.Format("{0} [{1}]: {1} {2} {3} {4}", this.alertName, this.alertType, this.ImageCount, this.AudioCount, this.StreamerCount, this.ViewerCount);
        }
    }

    public class oldTADAAlertImage : INotifyPropertyChanged, IXmlSerializable
    {
        private String filename;
        private Size dimensions;
        private Boolean isVideo;

        public double InitialAngle { get; set; } = 0;
        public double InitialMagnification { get; set; } = 0;
        public double InitialOpacity { get; set; } = 0;

        [DisplayName("Filename")]
        public String Filename
        {
            get { return this.filename; }
            set
            {
                if (this.filename != value)
                {
                    this.filename = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DisplayName("Size")]
        public Size Dimensions
        {
            get { return this.dimensions; }
            set
            {
                if (this.dimensions != value)
                {
                    this.dimensions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DisplayName("Video ?")]
        public bool IsVideo
        {
            get { return this.isVideo; }
            set
            {
                if (this.isVideo != value)
                {
                    this.isVideo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public WaypointContainer Waypoints { get; set; }

        public int AnimationLoops { get; set; }

        public double Volume { get; set; }
        public double Balance { get; set; } = 0;

        public TimeSpan Delay { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan EndingPosition { get; set; }
        public TimeSpan StartingPosition { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public oldTADAAlertImage(string filename, bool isVideo, int animationLoops, Size dimensions, TimeSpan delay, TimeSpan duration, TimeSpan startingPosition, TimeSpan endingPosition, double volume, double balance, WaypointContainer waypointContainer)
        {
            this.Filename = filename;
            this.IsVideo = isVideo;
            this.AnimationLoops = animationLoops;
            this.Dimensions = dimensions;
            this.Delay = delay;
            this.Duration = duration;
            this.StartingPosition = startingPosition;
            this.EndingPosition = endingPosition;
            this.Volume = volume;
            this.Balance = balance;
            this.Waypoints = waypointContainer;
        }

        public oldTADAAlertImage()
        {
            this.Waypoints = new WaypointContainer();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsStartElement("Image"))
            {
                // It had better be, since that's why this was called in the first place.
                this.Filename = reader.GetAttribute("Filename");
                this.IsVideo = Boolean.Parse(reader.GetAttribute("IsVideo"));

                this.InitialAngle = Double.Parse(reader.GetAttribute("Angle") ?? "0.0");
                this.InitialMagnification = Double.Parse(reader.GetAttribute("Magnification") ?? "1.0");
                this.InitialOpacity = Double.Parse(reader.GetAttribute("Opacity") ?? "100.0");

                if (this.IsVideo)
                {
                    this.Volume = Double.Parse(reader.GetAttribute("Volume"));
                    this.Balance = Double.Parse(reader.GetAttribute("Balance"));

                    this.AnimationLoops = 0;
                }
                else
                {
                    this.AnimationLoops = Int32.Parse(reader.GetAttribute("Loops"));

                    this.Volume = 0;
                    this.Balance = 0;
                    this.Delay = new TimeSpan(0);
                    this.Duration = new TimeSpan(0);
                    this.StartingPosition = new TimeSpan(0);
                    this.EndingPosition = new TimeSpan(0);
                }
            }
            else
            {
                throw new XmlException("Reader wasn't sitting on an <Image> tag when oldTADAAlertImage.ReadXml() was called.");
            }

            while (reader.Read())
            {
                if (reader.IsStartElement("Time"))
                {
                    XmlHelper.ReadTimeSpan(reader, out TimeSpan timeSpan, out string name);

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
                else if (reader.IsStartElement("Size"))
                {
                    XmlHelper.ReadSize(reader, out Size size, out string name);
                    if (name == "Dimensions")
                    {
                        this.dimensions = size;
                    }
                }
                else if (reader.IsStartElement("Waypoints"))
                {
                    XmlHelper.ReadWaypoints(reader, this.Waypoints);
                }
                else if (!reader.IsStartElement() && reader.Name == "Image")
                {
                    break;
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Image");
            writer.WriteAttributeString("Filename", filename);
            writer.WriteAttributeString("IsVideo", isVideo.ToString());

            writer.WriteAttributeString("Angle", InitialAngle.ToString());
            writer.WriteAttributeString("Magnification", InitialMagnification.ToString());
            writer.WriteAttributeString("Opacity", InitialOpacity.ToString());

            if (isVideo)
            {
                writer.WriteAttributeString("Volume", Volume.ToString());
                writer.WriteAttributeString("Balance", Balance.ToString());

                XmlHelper.WriteTimeSpan(writer, Delay, "Delay");
                XmlHelper.WriteTimeSpan(writer, Duration, "Duration");
                XmlHelper.WriteTimeSpan(writer, StartingPosition, "StartAt");
                XmlHelper.WriteTimeSpan(writer, EndingPosition, "LoopAt");
            }
            else
            {
                writer.WriteAttributeString("Loops", AnimationLoops.ToString());
            }

            XmlHelper.WriteSize(writer, dimensions, "Dimensions");

            if (Waypoints.Waypoints.Count > 0)
            {
                writer.WriteStartElement("Waypoints");
                foreach (Waypoint w in Waypoints.Waypoints)
                {
                    w.WriteXml(writer);
                }
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }
    }

    public class oldTADAAlertAudio : INotifyPropertyChanged, IXmlSerializable
    {
        private String filename;
        private double volume;
        private double balance;

        public String Filename
        {
            get { return this.filename; }
            set
            {
                if (this.filename != value)
                {
                    this.filename = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double Volume
        {
            get { return this.volume; }
            set
            {
                if (this.volume != value)
                {
                    this.volume = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double Balance
        {
            get { return this.balance; }
            set
            {
                if (this.balance != value)
                {
                    this.balance = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TimeSpan Delay { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan StartingPosition { get; set; }
        public TimeSpan EndingPosition { get; set; }

        public WaypointContainer Waypoints { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public oldTADAAlertAudio(String filename, double volume, double balance, TimeSpan delay, TimeSpan duration, TimeSpan startingPosition, TimeSpan endingPosition, WaypointContainer waypoints)
        {
            this.Filename = filename;
            this.Volume = volume;
            this.Balance = 0;
            this.Delay = delay;
            this.Duration = duration;
            this.StartingPosition = startingPosition;
            this.EndingPosition = endingPosition;
            this.Waypoints = waypoints;
        }

        public oldTADAAlertAudio()
        {
            this.Waypoints = new WaypointContainer();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsStartElement("Audio"))
            {
                this.Filename = reader.GetAttribute("Filename");
                this.Volume = Double.Parse(reader.GetAttribute("Volume"));
                this.Balance = Double.Parse(reader.GetAttribute("Balance"));
            }
            else
            {
                throw new XmlException("Reader wasn't sitting on an <Audio> tag when oldTADAAlertAudio.ReadXml() was called.");
            }

            while (reader.Read())
            {
                if (reader.IsStartElement("Time"))
                {
                    XmlHelper.ReadTimeSpan(reader, out TimeSpan timeSpan, out string name);

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
                    XmlHelper.ReadWaypoints(reader, this.Waypoints);
                }
                else if (!reader.IsStartElement() && reader.Name == "Audio")
                {
                    break;
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Audio");
            writer.WriteAttributeString("Filename", filename);
            writer.WriteAttributeString("Volume", volume.ToString());
            writer.WriteAttributeString("Balance", balance.ToString());

            XmlHelper.WriteTimeSpan(writer, Delay, "Delay");
            XmlHelper.WriteTimeSpan(writer, Duration, "Duration");
            XmlHelper.WriteTimeSpan(writer, StartingPosition, "StartAt");
            XmlHelper.WriteTimeSpan(writer, EndingPosition, "LoopAt");

            if (Waypoints.Waypoints.Count > 0)
            {
                writer.WriteStartElement("Waypoints");
                foreach (Waypoint w in Waypoints.Waypoints)
                {
                    w.WriteXml(writer);
                }
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }
    }

    public class oldTADAAlertText : INotifyPropertyChanged, IXmlSerializable
    {
        private String message;
        private String readMessage;

        private String substitutedMessage;
        private String substitutedReadMessage;

        public double InitialAngle { get; set; } = 0;
        public double InitialMagnification { get; set; } = 0;
        public double InitialOpacity { get; set; } = 0;

        [DisplayName("Displayed Message")]
        public String Message
        {
            get { return this.message; }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DisplayName("Narrated Message")]
        public String ReadMessage
        {
            get { return this.readMessage; }
            set
            {
                if (this.readMessage != value)
                {
                    this.readMessage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public String TTSVoice { get; set; }

        public int TTSVolume { get; set; }   // 0 - 100

        public Font ChosenFont { get; set; }

        public float OutlineWidth { get; set; }

        public Color Foreground { get; set; }
        public Color Background { get; set; }
        public Color Outline { get; set; }

        public Size Dimensions { get; set; }
        public Size Padding { get; set; }

        public WaypointContainer Waypoints { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public oldTADAAlertText(String message, String readMessage, String ttsVoice, int ttsVolume, Font font, float outlineWidth, Color foreground, Color background, Color outline, Size dimensions, Size padding, WaypointContainer waypoints)
        {
            this.Message = message;
            this.ReadMessage = readMessage;
            this.TTSVoice = ttsVoice;
            this.TTSVolume = ttsVolume;

            this.ChosenFont = font;
            this.OutlineWidth = outlineWidth;

            this.Foreground = foreground;
            this.Background = background;
            this.Outline = outline;

            this.Dimensions = dimensions;
            this.Padding = padding;

            this.Waypoints = waypoints;
        }

        public oldTADAAlertText()
        {
            this.Waypoints = new WaypointContainer();
        }

        // These are functions and not properties so that they don't affect the binding sources.
        public String GetMessage()
        {
            return this.substitutedMessage ?? this.Message;
        }

        public void SetMessage(String message)
        {
            this.substitutedMessage = message;
        }

        public String GetReadMessage()
        {
            return this.substitutedReadMessage ?? this.ReadMessage;
        }

        public void SetReadMessage(String message)
        {
            this.substitutedReadMessage = message;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsStartElement("Text"))
            {
                this.Message = reader.GetAttribute("Message");
                this.ReadMessage = reader.GetAttribute("Read");
                this.TTSVoice = reader.GetAttribute("Voice");
                this.TTSVolume = Int32.Parse(reader.GetAttribute("Volume"));

                this.InitialAngle = Double.Parse(reader.GetAttribute("Angle") ?? "0.0");
                this.InitialMagnification = Double.Parse(reader.GetAttribute("Magnification") ?? "1.0");
                this.InitialOpacity = Double.Parse(reader.GetAttribute("Opacity") ?? "100.0");
            }
            else
            {
                throw new XmlException("Reader wasn't sitting on an <Text> tag when oldTADAAlertText.ReadXml() was called.");
            }

            while (reader.Read())
            {
                if (reader.IsStartElement("Font"))
                {
                    String fontface = reader.GetAttribute("Name");
                    int points = Int32.Parse(reader.GetAttribute("Size"));
                    this.OutlineWidth = Int32.Parse(reader.GetAttribute("Outline"));
                    this.ChosenFont = new Font(fontface, points);
                }
                else if (reader.IsStartElement("Color"))
                {
                    XmlHelper.ReadColor(reader, out Color color, out string name);

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
                    XmlHelper.ReadSize(reader, out Size size, out string name);

                    if (name == "Dimensions")
                    {
                        this.Dimensions = size;
                    }
                    else if (name == "Padding")
                    {
                        this.Padding = size;
                    }
                }
                else if (reader.IsStartElement("Waypoints"))
                {
                    XmlHelper.ReadWaypoints(reader, this.Waypoints);
                }
                else if (!reader.IsStartElement() && reader.Name == "Text")
                {
                    break;
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Text");
            writer.WriteAttributeString("Message", message);
            writer.WriteAttributeString("Read", readMessage);
            writer.WriteAttributeString("Voice", TTSVoice);
            writer.WriteAttributeString("Volume", TTSVolume.ToString());

            writer.WriteAttributeString("Angle", InitialAngle.ToString());
            writer.WriteAttributeString("Magnification", InitialMagnification.ToString());
            writer.WriteAttributeString("Opacity", InitialOpacity.ToString());

            writer.WriteStartElement("Font");
            writer.WriteAttributeString("Name", ChosenFont.Name);
            writer.WriteAttributeString("Size", ChosenFont.SizeInPoints.ToString());
            writer.WriteAttributeString("Outline", OutlineWidth.ToString());
            writer.WriteEndElement();

            XmlHelper.WriteColor(writer, Foreground, "Foreground");
            XmlHelper.WriteColor(writer, Background, "Background");
            XmlHelper.WriteColor(writer, Outline, "Outline");

            XmlHelper.WriteSize(writer, Dimensions, "Dimensions");
            XmlHelper.WriteSize(writer, Padding, "Padding");

            if (Waypoints.Waypoints.Count > 0)
            {
                writer.WriteStartElement("Waypoints");
                foreach (Waypoint w in Waypoints.Waypoints)
                {
                    w.WriteXml(writer);
                }
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }
    }

    public class oldTADAAlertCondition : IXmlSerializable
    {
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.Name == "Condition")
            {
                NoTier = (reader.GetAttribute("NoTier") == "true");
                TwitchPrime = (reader.GetAttribute("TwitchPrime") == "true");
                TierOne = (reader.GetAttribute("TierOne") == "true");
                TierTwo = (reader.GetAttribute("TierTwo") == "true");
                TierThree = (reader.GetAttribute("TierThree") == "true");
                Usernames = (String.IsNullOrEmpty(reader.GetAttribute("Usernames")) ? "" : reader.GetAttribute("Usernames"));
                Keywords = (String.IsNullOrEmpty(reader.GetAttribute("Keywords")) ? "" : reader.GetAttribute("Keywords"));
                Keypress = (String.IsNullOrEmpty(reader.GetAttribute("Keypress")) ? "" : reader.GetAttribute("Keypress"));

                int val;
                Minimum = (Int32.TryParse(reader.GetAttribute("Minimum"), out val) ? val : 0);
                Maximum = (Int32.TryParse(reader.GetAttribute("Maximum"), out val) ? val : 0);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Condition");
            if (NoTier) { writer.WriteAttributeString("NoTier", "true"); }
            if (TwitchPrime) { writer.WriteAttributeString("TwitchPrime", "true"); }
            if (TierOne) { writer.WriteAttributeString("TierOne", "true"); }
            if (TierTwo) { writer.WriteAttributeString("TierTwo", "true"); }
            if (TierThree) { writer.WriteAttributeString("TierThree", "true"); }
            if (!String.IsNullOrEmpty(Usernames)) { writer.WriteAttributeString("Usernames", Usernames); }
            if (!String.IsNullOrEmpty(Keywords)) { writer.WriteAttributeString("Keywords", Keywords); }
            if (Minimum > 0) { writer.WriteAttributeString("Minimum", Minimum.ToString()); }
            if (Maximum > 0) { writer.WriteAttributeString("Maximum", Maximum.ToString()); }
            if (!String.IsNullOrEmpty(Keypress)) { writer.WriteAttributeString("Keypress", Keypress); }
            writer.WriteEndElement();
        }
    }

    public class SubstitutionObject
    {
        public String AlertType { get; set; } = General.alertTypes[General.ALERTTYPES.NONE];
        public int Amount { get; set; } = 0;
        public bool VIP { get; set; } = false;
        public String Message { get; set; } = "";
        public String RecipientName { get; set; } = "";
        public TwitchLib.PubSub.Enums.SubscriptionPlan Tier { get; set; } = TwitchLib.PubSub.Enums.SubscriptionPlan.NotSet;
        public String TierName { get; set; } = "";
        public String ViewerName { get; set; } = "";

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}", this.ViewerName, this.Amount, this.Message, this.TierName);
        }
    }
}