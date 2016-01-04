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
    public class Perron : Element
    {
        [XmlIgnore]
        public string Caption;
        [XmlIgnore]
        public Color CaptionColor;
        [XmlIgnore]
        public ArrayList Displays;
        [XmlIgnore]
        public bool Enabled;
        private const int HEIGHT = 6;
        private const int ICON_SIZEX = 20;
        private const int ICON_SIZEY = 5;
        private Point m_BasePoint;
        private int m_nDistance;
        private int m_nEscapement;
        private int m_nNumber;
        private decimal m_nTrainHeadStopPositionLatitude;
        private decimal m_nTrainHeadStopPositionLongitude;
        [XmlIgnore]
        public TISMonitor.Station m_Station;
        private string m_strStation;
        [XmlIgnore]
        public PathElement Track;
        [XmlIgnore]
        public int TrackSource;
        private const int WIDTH = 20;

        public Perron()
        {
            this.m_strStation = "";
            this.m_nTrainHeadStopPositionLatitude = 0M;
            this.m_nTrainHeadStopPositionLongitude = 0M;
            this.m_nDistance = 0;
            this.m_nNumber = 1;
            this.m_nEscapement = 0;
            this.Enabled = true;
            this.TrackSource = -1;
            this.Track = null;
            this.m_Station = null;
            this.Displays = new ArrayList();
            this.Caption = "";
            this.CaptionColor = Colors.get_Black();
            this.Init();
        }

        public Perron(string strID) : base(strID)
        {
            this.m_strStation = "";
            this.m_nTrainHeadStopPositionLatitude = 0M;
            this.m_nTrainHeadStopPositionLongitude = 0M;
            this.m_nDistance = 0;
            this.m_nNumber = 1;
            this.m_nEscapement = 0;
            this.Enabled = true;
            this.TrackSource = -1;
            this.Track = null;
            this.m_Station = null;
            this.Displays = new ArrayList();
            this.Caption = "";
            this.CaptionColor = Colors.get_Black();
            this.Init();
        }

        public override int CanConnect(Point p, Element e, int nSource, out Point pDest)
        {
            pDest = this.m_BasePoint;
            return -1;
        }

        public override OnElementMoveDelegate CanMove(Point p)
        {
            if (Element.IsPtCaptured(p, this.BasePoint))
            {
                return new OnElementMoveDelegate(this.OnMove);
            }
            return null;
        }

        public override void CopyLocationInfoFrom(Element e)
        {
            Debug.Assert(e is Perron);
            if (e is Perron)
            {
                this.BasePoint = (e as Perron).BasePoint;
            }
        }

        public override void Draw(Grid g)
        {
        }

        public Perron GetAlternativePerron()
        {
            if ((this.m_Station != null) && (this.m_Station.m_Perrons.Count == 2))
            {
                foreach (Perron perron in this.m_Station.m_Perrons.Values)
                {
                    if (perron.Number != this.Number)
                    {
                        if (!perron.Enabled)
                        {
                            return null;
                        }
                        return perron;
                    }
                }
            }
            return null;
        }

        public override int GetConnectionSourceID(Point p)
        {
            if (Element.IsPtCaptured(p, this.BasePoint))
            {
                return 0;
            }
            return -1;
        }

        public int GetDistanceToSource(int nTrackSource)
        {
            Debug.Assert(this.TrackSource != -1);
            Debug.Assert(this.Track != null);
            if (nTrackSource == -1)
            {
                return (this.Track.Length >> 1);
            }
            return ((nTrackSource == this.TrackSource) ? this.m_nDistance : Math.Abs((int) (this.Track.Length - this.m_nDistance)));
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("PERRON");
        }

        public override Form GetPropertiesForm()
        {
            return null;
        }

        public override string GetToolTipText(bool bDetailed)
        {
            return string.Format("{0} {1}{2}", this.GetName(), this.Number, this.Enabled ? "" : " (gesperrt)");
        }

        private void Init()
        {
        }

        public override void InitAfterLoad(Layout l)
        {
            Debug.Assert(base.m_Connections.Count <= 1);
            if (base.m_Connections.Count == 1)
            {
                Element element;
                (base.m_Connections[0] as Connection).GetConnectedElement(this, out element, out this.TrackSource);
                Debug.Assert(this.TrackSource != -1);
                this.Track = (PathElement) element;
                this.Track.Perron = this;
            }
            base.InitAfterLoad(l);
        }

        public override void Move(Size size)
        {
        }

        private Rectangle OnMove(Point pt)
        {
            this.BasePoint = pt;
            this.UpdateBounds();
            return base.Bounds;
        }

        public override void Reflect(int width, bool h)
        {
            this.BasePoint = h ? Element.GetPointRotatedByY(width, this.BasePoint) : Element.GetPointRotatedByX(width, this.BasePoint);
        }

        public override void SetLocation(Rectangle r)
        {
            this.BasePoint = new Point((double) r.X, (double) r.Y);
        }

        public override void UpdateBounds()
        {
        }

        public Point BasePoint
        {
            get
            {
                return this.m_BasePoint;
            }
            set
            {
                if (this.m_BasePoint != value)
                {
                    this.m_BasePoint = value;
                    this.UpdateBounds();
                }
            }
        }

        public int Distance
        {
            get
            {
                return this.m_nDistance;
            }
            set
            {
                this.m_nDistance = value;
            }
        }

        public int Escapement
        {
            get
            {
                return this.m_nEscapement;
            }
            set
            {
                this.m_nEscapement = value;
            }
        }

        public int Number
        {
            get
            {
                return this.m_nNumber;
            }
            set
            {
                this.m_nNumber = value;
            }
        }

        public string Station
        {
            get
            {
                return this.m_strStation;
            }
            set
            {
                this.m_strStation = value;
            }
        }

        public decimal TrainHeadStopPositionLatitude
        {
            get
            {
                return this.m_nTrainHeadStopPositionLatitude;
            }
            set
            {
                this.m_nTrainHeadStopPositionLatitude = value;
            }
        }

        public decimal TrainHeadStopPositionLongitude
        {
            get
            {
                return this.m_nTrainHeadStopPositionLongitude;
            }
            set
            {
                this.m_nTrainHeadStopPositionLongitude = value;
            }
        }
    }
}

