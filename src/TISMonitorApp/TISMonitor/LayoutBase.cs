namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Xml.Serialization;

    [XmlInclude(typeof(Perron)), Serializable, XmlInclude(typeof(IconElement)), XmlInclude(typeof(IconCamera)), XmlInclude(typeof(IconSpeaker)), XmlInclude(typeof(IconPhone)), XmlInclude(typeof(IconLANSwitch)), XmlInclude(typeof(TrainNumberField)), XmlInclude(typeof(Element)), XmlInclude(typeof(StateElement)), XmlInclude(typeof(PathElementPassive)), XmlInclude(typeof(TrackPassive)), XmlInclude(typeof(PathElement)), XmlInclude(typeof(Layout)), XmlInclude(typeof(Track)), XmlInclude(typeof(Light)), XmlInclude(typeof(Connection)), XmlInclude(typeof(PointSwitchBase)), XmlInclude(typeof(PointSwitch)), XmlInclude(typeof(PointSimple)), XmlInclude(typeof(Station))]
    public abstract class LayoutBase
    {
        public const int CNT_SUPPORTED_SCREEN_RESOLUTION = 0x500;
        [XmlIgnore]
        public string LastError = "";
        protected ArrayList m_alConnections = new ArrayList();
        protected ArrayList m_alElements = new ArrayList();
        protected Hashtable m_htAPIID2Element = new Hashtable();
        protected Hashtable m_htID2Element = new Hashtable();

        public virtual void ClearContent()
        {
            this.Elements.Clear();
            this.Connections.Clear();
            this.m_htID2Element.Clear();
            this.m_htAPIID2Element.Clear();
        }

        private PathElement FindLastPathElement()
        {
            foreach (object obj2 in this.m_alElements)
            {
                if (obj2 is PathElement)
                {
                    PathElement element = obj2 as PathElement;
                    ArrayList pathNeighbors = element.GetPathNeighbors();
                    if ((pathNeighbors != null) && (pathNeighbors.Count == 1))
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        private Point[] GetAllElementCoordinates(object o)
        {
            if (o is Element)
            {
                Element element = o as Element;
                return new Point[] { element.Center };
            }
            return null;
        }

        public IconElement GetAPIElement(string strID)
        {
            return (IconElement) this.m_htAPIID2Element[strID];
        }

        public Element GetElement(string strID)
        {
            if (this.m_htID2Element.ContainsKey(strID))
            {
                return (Element) this.m_htID2Element[strID];
            }
            return null;
        }

        private static bool GetExternalSource(Track pe, out int nExternalSource)
        {
            nExternalSource = -2147483648;
            if (pe != null)
            {
                int[] sources = pe.GetSources();
                if (sources != null)
                {
                    foreach (int num in sources)
                    {
                        int nForeignSource = -1;
                        if (pe.GetConnectedPath(num, out nForeignSource) != null)
                        {
                            nExternalSource = num;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public double GetHeight()
        {
            Point[] allElementCoordinates;
            double num = 0.0;
            if (this.m_alElements != null)
            {
                foreach (Element element in this.m_alElements)
                {
                    allElementCoordinates = this.GetAllElementCoordinates(element);
                    if (allElementCoordinates != null)
                    {
                        foreach (Point point in allElementCoordinates)
                        {
                            num = Math.Max(num, point.get_Y() + element.Bounds.Height);
                        }
                    }
                }
            }
            if (this.m_alConnections != null)
            {
                foreach (Connection connection in this.m_alConnections)
                {
                    allElementCoordinates = this.GetAllElementCoordinates(connection);
                    if (allElementCoordinates != null)
                    {
                        foreach (Point point in allElementCoordinates)
                        {
                            num = Math.Max(num, point.get_Y() + connection.Bounds.Height);
                        }
                    }
                }
            }
            return num;
        }

        private static int GetProportionalLenght(int pixelAreaSize, int realAreaSize, int elementRealSize)
        {
            return Convert.ToInt32(Math.Floor((double) ((elementRealSize * pixelAreaSize) / ((double) realAreaSize))));
        }

        protected virtual XmlSerializer GetSerializer()
        {
            Type type = base.GetType();
            return new XmlSerializer(type, new XmlRootAttribute(type.Name));
        }

        public double GetWidth()
        {
            Point[] allElementCoordinates;
            double num = 0.0;
            if (this.m_alElements != null)
            {
                foreach (Element element in this.m_alElements)
                {
                    allElementCoordinates = this.GetAllElementCoordinates(element);
                    if (allElementCoordinates != null)
                    {
                        foreach (Point point in allElementCoordinates)
                        {
                            num = Math.Max(num, point.get_X() + element.Bounds.Width);
                        }
                    }
                }
            }
            if (this.m_alConnections != null)
            {
                foreach (Connection connection in this.m_alConnections)
                {
                    allElementCoordinates = this.GetAllElementCoordinates(connection);
                    if (allElementCoordinates != null)
                    {
                        foreach (Point point in allElementCoordinates)
                        {
                            num = Math.Max(num, point.get_X());
                        }
                    }
                }
            }
            return num;
        }

        public virtual bool LoadFile(string strFileName, string strConnect)
        {
            return true;
        }

        public virtual bool SaveFile(string strFileName)
        {
            StreamWriter writer = null;
            XmlSerializer serializer = null;
            try
            {
                serializer = this.GetSerializer();
                writer = new StreamWriter(strFileName, false);
                serializer.Serialize((TextWriter) writer, this);
            }
            catch (Exception exception)
            {
                this.LastError = exception.Message;
                Debug.WriteLine(string.Format("{0}: {1}", this.ToString(), exception.Message));
                return false;
            }
            if (writer != null)
            {
                writer.Close();
            }
            return true;
        }

        [XmlIgnore]
        public Hashtable APIElements
        {
            get
            {
                return this.m_htAPIID2Element;
            }
        }

        public ArrayList Connections
        {
            get
            {
                return this.m_alConnections;
            }
        }

        public ArrayList Elements
        {
            get
            {
                return this.m_alElements;
            }
        }
    }
}

