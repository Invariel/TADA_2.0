using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TADA_Advanced_Desktop_Alerts
{
    public class ActiveEffect : Effect
    {
        public int framesRemaining;
        public double delta;

        public override void Copy (Effect e)
        {
            base.Copy(e);
            this.framesRemaining = e.frames;
        }

        public void NextEffectFrame (ref double variable, System.Timers.Timer timer)
        {
            variable += this.delta;
            --this.framesRemaining;

            if ((this.delta < 0 && variable < this.target) || (this.delta > 0 && variable > this.target))
            {
                variable = this.target;
            }

            if (this.framesRemaining <= 0)
            {
                variable = this.target;
            }

            if (variable == this.target)
            {
                timer.Stop();
            }
        }
    }

    public class Effect : IXmlSerializable
    {
        public bool change = false;
        public int frames = 1;
        public double target = 0;
        public double duration = 0;
        public bool aroundCenter = false;

        public virtual void Copy(Effect e)
        {
            this.change = e.change;
            this.frames = e.frames;
            this.target = e.target;
            this.duration = e.duration;
            this.aroundCenter = e.aroundCenter;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.Name == "Effect")
            {
                this.change = Convert.ToBoolean(reader.GetAttribute("Change"));
                this.frames = Convert.ToInt32(reader.GetAttribute("Frames"));
                this.target = Convert.ToDouble(reader.GetAttribute("Target"));
                this.duration = Convert.ToDouble(reader.GetAttribute("Duration"));
                this.aroundCenter = Convert.ToBoolean(reader.GetAttribute("AroundCenter"));
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            this.WriteXml(writer, "");
        }

        public void WriteXml(XmlWriter writer, String name)
        {
            writer.WriteStartElement("Effect");

            if (!String.IsNullOrEmpty(name))
            {
                writer.WriteAttributeString("Name", name);
            }

            writer.WriteAttributeString("Change", this.change.ToString());
            writer.WriteAttributeString("Frames", this.frames.ToString());
            writer.WriteAttributeString("Target", this.target.ToString());
            writer.WriteAttributeString("Duration", this.duration.ToString());
            writer.WriteAttributeString("AroundCenter", this.aroundCenter.ToString());
            writer.WriteEndElement();
        }
    }

    public class Waypoint : INotifyPropertyChanged, IXmlSerializable
    {
        private PointF p1;
        private PointF c0;
        private PointF c1;

        private double transitionTime = 0,
                       persistTime = 0;

        private Effect angle = new Effect();            // No limit.
        private Effect magnification = new Effect();    // 0 - 10.0.
        private Effect opacity = new Effect();          // 0 - 100.0.
        private Effect volume = new Effect();           // 0 - 100.0.
        private Effect balance = new Effect();          // -1 - 1.  MediaElement.Balance.

        private bool leaveScreen = false,
                     behindImage = false,
                     readMessage = false,
                     isCurve = false;

        private String windowTarget = "",
                       keypresses = "";

        public event PropertyChangedEventHandler PropertyChanged;

        [DisplayName("X")]
        public float X
        {
            get { return this.p1.X; }
            set
            {
                if (this.p1.X != value)
                {
                    this.p1.X = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DisplayName("Y")]
        public float Y
        {
            get { return this.p1.Y; }
            set
            {
                if (this.p1.Y != value)
                {
                    this.p1.Y = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DisplayName("Transition Time (s)")]
        public double TransitionTime
        {
            get { return this.transitionTime; }
            set
            {
                if (this.transitionTime != value)
                {
                    this.transitionTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DisplayName("Stick Around (s)")]
        public double PersistTime
        {
            get { return this.persistTime; }
            set
            {
                if (this.persistTime != value)
                {
                    this.persistTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PointF P1
        {
            get { return this.p1; }
            set { this.p1 = value; }
        }

        public PointF C0
        {
            get { return this.c0; }
            set { this.c0 = value; }
        }

        public PointF C1
        {
            get { return this.c1; }
            set { this.c1 = value; }
        }

        public Effect Angle
        {
            get { return this.angle; }
        }

        public Effect Magnification
        {
            get { return this.magnification; }
        }

        public Effect Opacity
        {
            get { return this.opacity; }
        }

        public Effect Volume
        {
            get { return this.volume; }
        }

        public Effect Balance
        {
            get { return this.balance; }
        }

        public bool LeaveScreen
        {
            get { return this.leaveScreen; }
            set { this.leaveScreen = value; }
        }

        public bool BehindImage
        {
            get { return this.behindImage; }
            set { this.behindImage = value; }
        }

        public bool ReadMessage
        {
            get { return this.readMessage; }
            set { this.readMessage = value; }
        }

        public bool IsCurve
        {
            get { return this.isCurve; }
            set { this.isCurve = value; }
        }

        public String WindowTarget
        {
            get { return this.windowTarget; }
            set { this.windowTarget = value; }
        }

        public String Keypresses
        {
            get { return this.keypresses; }
            set { this.keypresses = value; }
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
            // Read point.  Assign to point based on Name
            // Read Transition Time, Persist Time (Stay) as attributes.

            if (reader.IsStartElement("Waypoint"))
            {
                this.transitionTime = Double.Parse(reader.GetAttribute("TransitionTime"));
                this.persistTime = Double.Parse(reader.GetAttribute("StickAround"));
                this.leaveScreen = Boolean.Parse(reader.GetAttribute("LeaveScreen"));
                this.behindImage = Boolean.Parse(reader.GetAttribute("BehindImage"));
                this.readMessage = Boolean.Parse(reader.GetAttribute("ReadMessage"));
                this.isCurve = Boolean.Parse(reader.GetAttribute("IsCurve"));
            }
            else
            {
                throw new XmlException("Reader is not seated on a <Waypoint> element.");
            }

            while (reader.Read())
            {
                if (reader.IsStartElement("Point"))
                {
                    XmlHelper.ReadPoint(reader, out PointF point, out string name);
                    if (name == "P1")
                    {
                        this.P1 = point;
                    }
                    else if (name == "C0")
                    {
                        this.C0 = point;
                    }
                    else if (name == "C1")
                    {
                        this.C1 = point;
                    }
                }
                else if (reader.IsStartElement("Effect"))
                {
                    Effect e;
                    switch (reader.GetAttribute("Name"))
                    {
                        case "Angle":
                            e = Angle;
                            break;
                        case "Magnification":
                            e = Magnification;
                            break;
                        case "Opacity":
                            e = Opacity;
                            break;
                        case "Volume":
                            e = Volume;
                            break;
                        case "Balance":
                            e = Balance;
                            break;
                        default:
                            e = new Effect();
                            System.Windows.Forms.MessageBox.Show("Found an Effect with no Name attribute.", "Error in Parsing XML", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            break;
                    }
                    e.ReadXml(reader);
                }
                else if (reader.IsStartElement("Macro"))
                {
                    this.WindowTarget = reader.GetAttribute("Window");
                    this.Keypresses = reader.GetAttribute("Keys");
                }
                else if (!reader.IsStartElement() && reader.Name == "Waypoint")
                {
                    break;
                }
            }

            // If, at this point, C0 and C1 are null, let the system know that this isn't a curve.
            if (c0 == null && c1 == null)
            {
                this.isCurve = false;
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Waypoint");
            writer.WriteAttributeString("TransitionTime", this.transitionTime.ToString());
            writer.WriteAttributeString("StickAround", this.persistTime.ToString());
            writer.WriteAttributeString("LeaveScreen", this.leaveScreen.ToString());
            writer.WriteAttributeString("BehindImage", this.behindImage.ToString());
            writer.WriteAttributeString("ReadMessage", this.readMessage.ToString());
            writer.WriteAttributeString("IsCurve", this.isCurve.ToString());

            XmlHelper.WritePoint(writer, p1, "P1");
            XmlHelper.WritePoint(writer, c0, "C0");
            XmlHelper.WritePoint(writer, c1, "C1");

            if (this.angle.change)          this.angle.WriteXml(writer, "Angle");
            if (this.magnification.change)  this.magnification.WriteXml(writer, "Magnification");
            if (this.opacity.change)        this.opacity.WriteXml(writer, "Opacity");
            if (this.volume.change)         this.volume.WriteXml(writer, "Volume");
            if (this.balance.change)        this.balance.WriteXml(writer, "Balance");

            if (!String.IsNullOrEmpty(this.windowTarget) && !String.IsNullOrEmpty(this.keypresses))
            {
                writer.WriteStartElement("Macro");
                writer.WriteAttributeString("Window", this.windowTarget);
                writer.WriteAttributeString("Keys", this.keypresses);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public Waypoint()
        {
            this.P1 = new PointF(0, 0);
            this.C0 = new PointF(0, 0);
            this.C1 = new PointF(0, 0);

            this.TransitionTime = 0;
            this.PersistTime = 0;

            this.LeaveScreen = false;
            this.BehindImage = false;
            this.ReadMessage = false;

            this.WindowTarget = "";
            this.Keypresses = "";
        }

        public Waypoint (PointF p1, double transition, double persist, bool leave = false, bool behind = false, bool read = false)
        {
            this.P1 = p1;
            this.TransitionTime = transition;
            this.PersistTime = persist;
            this.LeaveScreen = leave;
            this.BehindImage = behind;
            this.ReadMessage = read;
        }
    }

    public class WaypointContainer : InitialConditions
    {
        private int currentWaypoint = 0;

        protected List<Waypoint> waypoints;

        public double TotalDuration
        {
            get { return this.Waypoints.Sum(waypoint => waypoint.TransitionTime + waypoint.PersistTime); }
        }

        public List<Waypoint> Waypoints
        {
            get { return this.waypoints; }
            set { this.waypoints = value; }
        }

        public Waypoint CurrentWaypoint
        {
            get { return (this.currentWaypoint < this.waypoints.Count ? this.waypoints[this.currentWaypoint] : null); }
        }

        public void FirstWaypoint()
        {
            currentWaypoint = 0;
        }

        public bool IsFirstWaypoint()
        {
            return currentWaypoint == 0;
        }

        public void NextWaypoint()
        {
            ++currentWaypoint;
        }

        public int CurrentWaypointIndex ()
        {
            return this.currentWaypoint;
        }

        public void AddWaypoint(Waypoint w)
        {
            if (waypoints == null)
            {
                waypoints = new List<Waypoint>();
            }

            waypoints.Add(w);
        }

        public WaypointContainer()
        {
            this.waypoints = new List<Waypoint>();
        }

        public WaypointContainer(List<Waypoint> waypointList)
        {
            this.waypoints = waypointList;
        }

        public override void ReadXml(XmlReader reader)
        {
            // Read that it's a Waypoints tag, then...
            if (reader.Name == "Waypoints")
            {
                base.ReadXml(reader);

                XmlHelper.Read(reader, "Waypoint", "Waypoints", this.waypoints);
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Waypoints");
            base.WriteXml(writer);
            XmlHelper.Write(writer, "Waypoint", this.waypoints, false);
        }
    }
}
