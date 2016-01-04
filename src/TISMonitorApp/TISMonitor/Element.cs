namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class Element : IComparable
    {
        [XmlIgnore]
        public int BaseWidth;
        [XmlIgnore]
        public Rectangle Bounds;
        [XmlIgnore]
        public Point Center;
        [XmlIgnore]
        public Color ForeColor;
        [XmlIgnore]
        public bool IconMode;
        [XmlIgnore]
        public ArrayList m_Connections;
        protected static Font m_Font = null;
        [XmlIgnore]
        public Layout m_Layout;
        private string m_strID;
        [XmlIgnore]
        public object Tag;

        public Element()
        {
            this.m_strID = "";
            this.BaseWidth = 3;
            this.IconMode = false;
            this.Tag = null;
            this.m_Connections = new ArrayList();
            this.ForeColor = Colors.get_Black();
            this.Bounds = new Rectangle(0, 0, 0, 0);
            this.Center = new Point(0.0, 0.0);
            this.Initialize();
        }

        public Element(string strID)
        {
            this.m_strID = "";
            this.BaseWidth = 3;
            this.IconMode = false;
            this.Tag = null;
            this.m_Connections = new ArrayList();
            this.ForeColor = Colors.get_Black();
            this.Bounds = new Rectangle(0, 0, 0, 0);
            this.Center = new Point(0.0, 0.0);
            this.ID = strID;
            this.Initialize();
        }

        public virtual bool AllowAddToSchema()
        {
            return true;
        }

        public virtual int CanConnect(Point p, Element e, int nSource, out Point pDest)
        {
            pDest = new Point(0.0, 0.0);
            return -1;
        }

        public abstract OnElementMoveDelegate CanMove(Point p);
        public object Clone()
        {
            Element element = (Element) base.MemberwiseClone();
            element.m_Connections = this.m_Connections.Clone();
            return element;
        }

        public void Connect(Connection c)
        {
            Debug.Assert(!this.m_Connections.Contains(c));
            this.m_Connections.Add(c);
        }

        public abstract void CopyLocationInfoFrom(Element e);
        public void Disconnect()
        {
            this.m_Connections.Clear();
        }

        public void Disconnect(Connection c)
        {
            Debug.Assert(this.m_Connections.Contains(c));
            this.m_Connections.Remove(c);
        }

        public abstract void Draw(Grid g);
        public virtual void Dump()
        {
            Debug.WriteLine("ID: " + this.ID);
        }

        public override bool Equals(object obj)
        {
            Element element = (Element) obj;
            return ((this.ID == element.ID) && (this.Center == element.Center));
        }

        public virtual Point GetCenterPoint()
        {
            return this.Center;
        }

        public virtual int GetConnectionSourceID(Point p)
        {
            return -1;
        }

        public virtual string GetConnectionSourceName(int nSource)
        {
            return "";
        }

        public virtual Point GetConnectionSourcePoint(int nSource)
        {
            return new Point(0.0, 0.0);
        }

        public virtual Font GetFont()
        {
            return null;
        }

        public override int GetHashCode()
        {
            return ((this.ID.GetHashCode() ^ ((int) this.Center.get_X())) ^ ((int) this.Center.get_Y()));
        }

        public abstract string GetName();
        public static Point GetPointRotatedByX(int rY, Point point)
        {
            int num = (int) point.get_X();
            int num2 = ((int) point.get_Y()) - rY;
            int num3 = rY - num2;
            return new Point((double) num, (double) num3);
        }

        public static Point GetPointRotatedByY(int width, Point point)
        {
            int num = width - ((int) point.get_X());
            int num2 = (int) point.get_Y();
            return new Point((double) num, (double) num2);
        }

        public virtual Form GetPropertiesForm()
        {
            return null;
        }

        public virtual string GetToolTipText(bool bDetailed)
        {
            string format = "{0} {1}";
            return string.Format(format, this.GetName(), this.ID);
        }

        public virtual string GetToolTipText(bool bDetailed, Point pt)
        {
            return this.GetToolTipText(bDetailed);
        }

        public virtual void InitAfterLoad(Layout l)
        {
            this.m_Layout = l;
        }

        protected void Initialize()
        {
        }

        public bool IsConnectedTo(Element pe, out int nMySource, out int nForeignSource)
        {
            nForeignSource = nMySource = -1;
            foreach (Connection connection in this.m_Connections)
            {
                Element element;
                if (connection.GetConnectedElement(this, out nMySource, out element, out nForeignSource) && (pe == element))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPtCaptured(Point p, Point ptToCapture)
        {
            return false;
        }

        public bool IsPtInBounds(Point pt)
        {
            return false;
        }

        public abstract void Move(Size size);
        public virtual void Move(Size size, bool bWithConnections)
        {
            this.Move(size);
            if (bWithConnections)
            {
                foreach (Connection connection in this.m_Connections)
                {
                    connection.Move(size);
                }
            }
        }

        public abstract void Reflect(int width, bool horizontally);
        public abstract void SetLocation(Rectangle r);
        int IComparable.CompareTo(object obj)
        {
            if (!(obj is Element))
            {
                throw new ArgumentException("Object is not an Element");
            }
            Element element = (Element) obj;
            return (this.GetHashCode() - element.GetHashCode());
        }

        public abstract void UpdateBounds();

        public string ID
        {
            get
            {
                return this.m_strID;
            }
            set
            {
                this.m_strID = value;
            }
        }
    }
}

