namespace TISMonitor
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class DisplayBase : Element
    {
        protected const int ICON_SIZE = 0x10;
        protected ArrayList m_alTimetable;
        protected Point m_BasePoint;
        protected bool m_bEnabled;
        protected Hashtable m_htTrainID2Data;
        private int m_nEmulatorPosition;
        protected ArrayList m_PerronNumbers;
        protected ArrayList m_Perrons;
        [XmlIgnore]
        public TISMonitor.Station m_Station;
        protected string m_strAddress;
        protected string m_strCaption;
        protected string m_strPerronIDs;
        protected string m_strStation;
        protected TrackedTrainList m_TrainsDeparted;
        [XmlIgnore]
        public DeviceState State;
        public const int TIMETABLE_SIZE = 10;
        [XmlIgnore]
        public string TrackFilter;

        public DisplayBase()
        {
            this.TrackFilter = "";
            this.m_nEmulatorPosition = -1;
            this.m_PerronNumbers = new ArrayList();
            this.m_bEnabled = true;
            this.m_Perrons = new ArrayList();
            this.m_strStation = "";
            this.m_strPerronIDs = "";
            this.m_strAddress = "";
            this.m_strCaption = "";
            this.m_htTrainID2Data = new Hashtable();
            this.m_TrainsDeparted = new TrackedTrainList();
            this.m_alTimetable = new ArrayList();
            this.Init();
        }

        public DisplayBase(string strID) : base(strID)
        {
            this.TrackFilter = "";
            this.m_nEmulatorPosition = -1;
            this.m_PerronNumbers = new ArrayList();
            this.m_bEnabled = true;
            this.m_Perrons = new ArrayList();
            this.m_strStation = "";
            this.m_strPerronIDs = "";
            this.m_strAddress = "";
            this.m_strCaption = "";
            this.m_htTrainID2Data = new Hashtable();
            this.m_TrainsDeparted = new TrackedTrainList();
            this.m_alTimetable = new ArrayList();
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
            Debug.Assert(e is DisplayBase);
            if (e is DisplayBase)
            {
                this.m_BasePoint = (e as DisplayBase).BasePoint;
            }
        }

        public override void Draw(Grid g)
        {
        }

        public override int GetConnectionSourceID(Point p)
        {
            if (Element.IsPtCaptured(p, this.BasePoint))
            {
                return 0;
            }
            return -1;
        }

        public virtual string GetDestinationFieldName()
        {
            return "StationName";
        }

        public Hashtable GetDisplayTrainData()
        {
            lock (this)
            {
                return this.m_htTrainID2Data.Clone();
            }
        }

        public Hashtable GetDisplayTrainDataDeparted()
        {
            return this.m_TrainsDeparted.GetHashtable();
        }

        public virtual int GetHeight()
        {
            return 0x10;
        }

        public abstract Icon GetIcon();
        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("DISPLAY");
        }

        protected SIDISServerBase GetServer()
        {
            return base.m_Layout.GetServer();
        }

        protected DateTime GetServerTime()
        {
            DateTime dateTimeNow = this.GetServer().GetDateTimeNow();
            return new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day, dateTimeNow.Hour, dateTimeNow.Minute, 0, 0);
        }

        public override string GetToolTipText(bool bDetailed)
        {
            string str;
            if (bDetailed)
            {
                str = "{0} {1} - {2}";
                return string.Format(str, this.GetName(), base.ID, IconElement.State2Description(this.State));
            }
            str = "{0} {1} - {2}";
            return string.Format(str, this.GetName(), this.m_strAddress, IconElement.State2Description(this.State));
        }

        public virtual string GetToolTipText4Emulator(bool bExtended)
        {
            int num = 0;
            string str = string.Format("   {0}", this.GetToolTipText(false));
            Hashtable displayTrainData = this.GetDisplayTrainData();
            Hashtable displayTrainDataDeparted = this.GetDisplayTrainDataDeparted();
            ArrayList list = this.Timetable.Clone();
            foreach (DisplayTimetableData data in list)
            {
                string str2;
                DisplayData data2 = (DisplayData) displayTrainData[data.Train];
                bool flag = displayTrainDataDeparted.ContainsKey(data.Train);
                string str3 = "";
                if (data2 == null)
                {
                    str2 = flag ? "x " : "   ";
                }
                else if (data2.Type == DisplayDataType.Boarding)
                {
                    str2 = flag ? "x*" : "* ";
                }
                else
                {
                    str2 = flag ? "x\x00bb" : "\x00bb ";
                    str3 = string.Format(" in {0} Min.", data2.GetMinutesToWait());
                }
                str = str + "\n";
                if (bExtended)
                {
                    str = str + string.Format("{0}{1} [{2}]  {3}   {4}   {5}   {6}{7}", new object[] { str2, data.TimetableTime.ToString("dd.MM.yy"), data.TimetableDate.ToString("dd.MM.yy"), data.TimetableTime.ToString("HH:mm"), data.TrackNo, data.Train, data.DestinationDisplay, str3 });
                }
                else
                {
                    str = str + string.Format("{0}{1}   {2}   {3}   {4}{5}", new object[] { str2, data.TimetableTime.ToString("HH:mm"), data.TrackNo, data.Train, data.DestinationDisplay, str3 });
                }
                num++;
            }
            return str;
        }

        public virtual int GetWidth()
        {
            return 0x10;
        }

        protected bool HasDisplayDataType(DisplayDataType type)
        {
            lock (this)
            {
                foreach (DisplayData data in this.m_htTrainID2Data.Values)
                {
                    if (data.Type == type)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        protected virtual void Init()
        {
            this.State = DeviceState.UNKNOWN;
        }

        public override void InitAfterLoad(Layout l)
        {
            string[] strArray = this.m_strPerronIDs.Split(new char[] { ';' });
            this.m_Station = l.Stations.GetStationByID(this.m_strStation);
            Debug.Assert(this.m_Station != null);
            if (this.m_Station != null)
            {
                foreach (string str in strArray)
                {
                    Perron perronByID = this.m_Station.GetPerronByID(str);
                    Debug.Assert(perronByID != null);
                    if (perronByID != null)
                    {
                        this.Perrons.Add(perronByID);
                        this.m_PerronNumbers.Add(perronByID.Number);
                        perronByID.Displays.Add(this);
                    }
                }
                if (this.m_PerronNumbers.Count != this.m_Station.m_Perrons.Count)
                {
                    ArrayList list = new ArrayList();
                    foreach (int num in this.m_PerronNumbers)
                    {
                        if (!list.Contains(num))
                        {
                            if (this.TrackFilter.Length > 0)
                            {
                                this.TrackFilter = this.TrackFilter + " OR ";
                            }
                            this.TrackFilter = this.TrackFilter + string.Format("TrackNo={0}", num);
                            list.Add(num);
                        }
                    }
                }
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

        public abstract void ResetDisplayTrainData(string strTrainID, bool bTrainDeparts);
        public abstract void ResetDisplayTrainData(string strTrainID, DisplayDataType type);
        protected bool SendTimeTable()
        {
            bool flag;
            this.SendTimeTable(true, out flag);
            return flag;
        }

        protected abstract bool SendTimeTable(bool bNotify, out bool bChanged);
        public abstract bool SetDisplayCustomText(object o);
        public abstract void SetDisplayTrainData(string strTrainID, DisplayData dd);
        public override void SetLocation(Rectangle r)
        {
            this.BasePoint = new Point((double) r.X, (double) r.Y);
        }

        public virtual void StopTimers()
        {
        }

        public abstract void TrainDeparts(string strTrainID);
        public override void UpdateBounds()
        {
        }

        public string Address
        {
            get
            {
                return this.m_strAddress;
            }
            set
            {
                this.m_strAddress = value;
            }
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

        public string Caption
        {
            get
            {
                return this.m_strCaption;
            }
            set
            {
                this.m_strCaption = value;
            }
        }

        public int EmulatorPosition
        {
            get
            {
                return this.m_nEmulatorPosition;
            }
            set
            {
                this.m_nEmulatorPosition = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.m_bEnabled;
            }
            set
            {
                this.m_bEnabled = value;
            }
        }

        public string PerronIDs
        {
            get
            {
                return this.m_strPerronIDs;
            }
            set
            {
                this.m_strPerronIDs = value;
            }
        }

        [XmlIgnore]
        public ArrayList PerronNumbers
        {
            get
            {
                return this.m_PerronNumbers;
            }
        }

        [XmlIgnore]
        public ArrayList Perrons
        {
            get
            {
                return this.m_Perrons;
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

        [XmlIgnore]
        public ArrayList Timetable
        {
            get
            {
                return this.m_alTimetable;
            }
        }

        public class EmulatorPositionComparer : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                return (x as DisplayBase).EmulatorPosition.CompareTo((y as DisplayBase).EmulatorPosition);
            }
        }
    }
}

