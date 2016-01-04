namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Xml.Serialization;

    [Serializable]
    public class TrainNumberField : Element
    {
        private const int HEIGHT = 12;
        private const int ICON_SIZEX = 20;
        private const int ICON_SIZEY = 5;
        [XmlIgnore]
        private ArrayList m_arBlockingList;
        private Point m_BasePoint;
        protected static Font m_fFont = null;
        private int m_nDialTrainDirection;
        private int m_nDialTrainTime;
        private ushort m_nElementIndex;
        [XmlIgnore]
        public Station m_Station;
        private string m_strDialTrainStation;
        private string m_strTrainNumber;
        [XmlIgnore]
        public TISMonitor.PathElement PathElement;
        [XmlIgnore]
        public int TrackSource;
        [XmlIgnore]
        public DataTable TrainTimeTable;
        private const int WIDTH = 30;

        public TrainNumberField()
        {
            this.m_strDialTrainStation = "";
            this.m_nDialTrainTime = 0;
            this.m_nElementIndex = 0;
            this.m_nDialTrainDirection = 0;
            this.m_strTrainNumber = "";
            this.m_arBlockingList = new ArrayList();
            this.m_Station = null;
            this.TrackSource = -1;
            this.PathElement = null;
            this.TrainTimeTable = null;
            this.Init();
        }

        public TrainNumberField(string strID) : base(strID)
        {
            this.m_strDialTrainStation = "";
            this.m_nDialTrainTime = 0;
            this.m_nElementIndex = 0;
            this.m_nDialTrainDirection = 0;
            this.m_strTrainNumber = "";
            this.m_arBlockingList = new ArrayList();
            this.m_Station = null;
            this.TrackSource = -1;
            this.PathElement = null;
            this.TrainTimeTable = null;
            this.Init();
        }

        public void AddBlockingInfoItem(string info)
        {
            this.m_arBlockingList.Add(info);
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

        public ArrayList GetBlockingList()
        {
            return this.m_arBlockingList;
        }

        public override int GetConnectionSourceID(Point p)
        {
            if (Element.IsPtCaptured(p, this.BasePoint))
            {
                return 0;
            }
            return -1;
        }

        public override Font GetFont()
        {
            if (m_fFont == null)
            {
            }
            return m_fFont;
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("TRAINNUMBERFIELD");
        }

        public override string GetToolTipText(bool bDetailed)
        {
            return (bDetailed ? string.Format("{0} {1} ({2} min)", this.GetName(), this.ElementIndex, this.DialTrainTime) : "");
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
                this.PathElement = (TISMonitor.PathElement) element;
                this.PathElement.TrainNumberField = this;
            }
            this.m_Station = base.m_Layout.Stations.GetStationByID(this.m_strDialTrainStation);
            Debug.Assert(this.m_Station != null);
            base.InitAfterLoad(l);
        }

        public bool IsBlockingActivated()
        {
            return (this.m_arBlockingList.Count > 0);
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

        public void ResetBlocking()
        {
            this.m_arBlockingList.Clear();
        }

        public void ResetTrainInfo()
        {
            this.TrainNumber = "";
            this.TrainTimeTable = null;
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

        public int DialTrainDirection
        {
            get
            {
                return this.m_nDialTrainDirection;
            }
            set
            {
                this.m_nDialTrainDirection = value;
            }
        }

        public string DialTrainStation
        {
            get
            {
                return this.m_strDialTrainStation;
            }
            set
            {
                this.m_strDialTrainStation = value;
            }
        }

        public int DialTrainTime
        {
            get
            {
                return this.m_nDialTrainTime;
            }
            set
            {
                this.m_nDialTrainTime = value;
            }
        }

        public ushort ElementIndex
        {
            get
            {
                return this.m_nElementIndex;
            }
            set
            {
                this.m_nElementIndex = value;
            }
        }

        [XmlIgnore]
        public string TrainNumber
        {
            get
            {
                return this.m_strTrainNumber;
            }
            set
            {
                this.m_strTrainNumber = value;
            }
        }
    }
}

