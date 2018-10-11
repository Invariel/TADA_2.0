using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TADA_Advanced_Desktop_Alerts
{
    class XmlHelper
    {
        public static void Read<T>(XmlReader reader, String elementTag, String collectionTag, List<T> listT) where T : IXmlSerializable, new()
        {
            while (reader.Read())
            {
                if (reader.IsStartElement(elementTag))
                {
                    T newItem = new T();
                    newItem.ReadXml(reader);
                    listT.Add(newItem);
                }
                else if (!reader.IsStartElement() && reader.Name == collectionTag)
                {
                    break;
                }
            }
        }

        public static void ReadColor(XmlReader reader, out Color color, out String name)
        {
            if (reader.Name == "Color" || reader.Name == "Colour")
            {
                name = reader.GetAttribute("Name");

                int a = Int32.Parse(reader.GetAttribute("A"));
                int r = Int32.Parse(reader.GetAttribute("R"));
                int g = Int32.Parse(reader.GetAttribute("G"));
                int b = Int32.Parse(reader.GetAttribute("B"));

                color = Color.FromArgb(a, r, g, b);
            }
            else
            {
                throw new XmlException("Reader does not point at a Color tag.");
            }
        }

        public static void ReadPoint(XmlReader reader, out Point point, out String name)
        {
            if (reader.Name == "Point")
            {
                name = reader.GetAttribute("Name");
                int x = Int32.Parse(reader.GetAttribute("X"));
                int y = Int32.Parse(reader.GetAttribute("Y"));

                point = new Point(x, y);
            }
            else
            {
                throw new XmlException("Reader does not point at a Point tag.");
            }
        }

        public static void ReadPoint(XmlReader reader, out PointF point, out String name)
        {
            if (reader.Name == "Point")
            {
                name = reader.GetAttribute("Name");
                float x = float.Parse(reader.GetAttribute("X"));
                float y = float.Parse(reader.GetAttribute("Y"));

                point = new PointF(x, y);
            }
            else
            {
                throw new XmlException("Reader does not point at a Point tag.");
            }
        }

        public static void ReadSize(XmlReader reader, out Size size, out String name)
        {
            if (reader.Name == "Size")
            {
                name = reader.GetAttribute("Name");
                int width = Int32.Parse(reader.GetAttribute("Width"));
                int height = Int32.Parse(reader.GetAttribute("Height"));

                size = new Size(width, height);
            }
            else
            {
                throw new XmlException("Reader does not point at a Size tag.");
            }
        }

        public static void ReadTimeSpan(XmlReader reader, out TimeSpan timeSpan, out String name)
        {
            if (reader.Name == "Time")
            {
                name = reader.GetAttribute("Name");
                timeSpan = General.TimeSpanFromString(reader.GetAttribute("Time"));
            }
            else
            {
                throw new XmlException("Reader does not point at a Time tag.");
            }
        }

        // Deprecated with AlertClasses2.
        public static void ReadWaypoints(XmlReader reader, WaypointContainer waypoints)
        {
            if (reader.IsStartElement("Waypoints"))
            {
                XmlHelper.Read<Waypoint>(reader, "Waypoint", "Waypoints", waypoints.Waypoints);
            }
        }

        public static void Write<T>(XmlWriter writer, String tag, List<T> elements) where T : IXmlSerializable
        {
            if (elements.Count > 0)
            {
                writer.WriteStartElement(tag);
                foreach (T element in elements) { element.WriteXml(writer); }
                writer.WriteEndElement();
            }
        }

        public static void WriteColor(XmlWriter writer, Color color, String colorName)
        {
            writer.WriteStartElement("Color");
            writer.WriteAttributeString("Name", colorName);
            writer.WriteAttributeString("A", color.A.ToString());
            writer.WriteAttributeString("R", color.R.ToString());
            writer.WriteAttributeString("G", color.G.ToString());
            writer.WriteAttributeString("B", color.B.ToString());
            writer.WriteEndElement();
        }

        public static void WritePoint(XmlWriter writer, Point point, String pointName)
        {
            writer.WriteStartElement("Point");
            writer.WriteAttributeString("Name", pointName);
            writer.WriteAttributeString("X", point.X.ToString());
            writer.WriteAttributeString("Y", point.Y.ToString());
            writer.WriteEndElement();
        }

        public static void WritePoint(XmlWriter writer, PointF point, String pointName)
        {
            writer.WriteStartElement("Point");
            writer.WriteAttributeString("Name", pointName);
            writer.WriteAttributeString("X", point.X.ToString());
            writer.WriteAttributeString("Y", point.Y.ToString());
            writer.WriteEndElement();
        }

        public static void WriteSize(XmlWriter writer, Size size, String pointName)
        {
            writer.WriteStartElement("Size");
            writer.WriteAttributeString("Name", pointName);
            writer.WriteAttributeString("Width", size.Width.ToString());
            writer.WriteAttributeString("Height", size.Height.ToString());
            writer.WriteEndElement();
        }

        public static void WriteTimeSpan(XmlWriter writer, TimeSpan timeSpan, String timeSpanName)
        {
            writer.WriteStartElement("Time");
            writer.WriteAttributeString("Name", timeSpanName);
            writer.WriteAttributeString("Time", General.DurationFromTimeSpan(timeSpan));
            writer.WriteEndElement();
        }
    }
}
