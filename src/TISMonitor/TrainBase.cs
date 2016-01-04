namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    [Serializable]
    public class TrainBase
    {
        public DateTime DateTimetable = DateTime.MinValue;
        public TimeSpan Delay = new TimeSpan(0L);
        public int Length = 1;
        protected List<TrainPoint> m_alTrainPoints = new List<TrainPoint>();
        protected bool m_bCanMoveToNextElement = true;
        protected bool m_bLocomotive = false;
        protected PathElement m_HeadElement = null;
        protected Layout m_Layout = null;
        protected TISMonitor.MoveState m_MoveState = TISMonitor.MoveState.Unknown;
        protected int m_nDirection = 0;
        public int m_nHeadElementArrivedSource = -1;
        protected int m_nHeadOffset = 0;
        protected PathElement m_nNextElement = null;
        public int m_nNextElementSource = 0;
        protected ArrayList m_RoutePassed = new ArrayList();
        protected Station m_StationArea = null;
        public Station m_StationDest = null;
        public Station m_StationPrev = null;
        public Station m_StationStart = null;
        protected string m_strDescription = "";
        protected string m_strID = "";
        protected string m_strIDToDisplay = "";
        protected string m_strLineID = "";
        protected PathElement m_targetElement = null;
        protected object m_timerMove = new object();
        protected ArrayList m_TrainCourse = new ArrayList();
        public static int OnTimeThreshold = 120;
        public bool ShowAtDisplay = false;
        public DataTable StationTimeTable = null;
        public static int TooSlowThreshold = 300;
        public const int TRAIN_TYPE_PASSENGER = 1;
        public const int TRAIN_TYPE_UNKNOWN = -1;
        public int TrainType = -1;
        public static int TrainWaitsAtPerron = 30;

        protected virtual bool BuildCourse(bool bUpdateCourseData)
        {
            return false;
        }

        public bool CanBeInvalidElementState()
        {
            return ((this.Length == 1) && (this.HeadElement.Trains().Count == 1));
        }

        public bool CanSplit()
        {
            return (((this.Length == 1) && (this.HeadElement is Track)) && (this.HeadElement.Trains().Count == 1));
        }

        public object Clone()
        {
            TrainBase base2 = (TrainBase) base.MemberwiseClone();
            if (this.m_RoutePassed != null)
            {
                base2.m_RoutePassed = this.m_RoutePassed.Clone();
            }
            if (this.m_TrainCourse != null)
            {
                base2.m_TrainCourse = this.m_TrainCourse.Clone();
            }
            return base2;
        }

        public virtual void DebugTrace(string strData)
        {
            Debug.WriteLine(string.Format("{0} Train {1} (Head {2}, Length {3}): {4}", new object[] { DateTime.Now.ToString("HH:mm:ss.ff"), this.ID, this.HeadElement.ID, this.Length, strData }));
        }

        public static void GetDelayColor(TimeSpan tsDelay, out Color clrHi, out Color clrLo)
        {
            clrHi = Color.FromArgb(0xff, 0, 200, 0);
            clrLo = Color.FromArgb(0xff, 0, 0x40, 0);
            if (tsDelay.Duration().TotalSeconds <= OnTimeThreshold)
            {
                clrHi = Color.FromArgb(0xff, 0, 200, 0);
                clrLo = Color.FromArgb(0xff, 0, 0x40, 0);
            }
            else if (tsDelay.TotalSeconds > 0.0)
            {
                if (tsDelay.Duration().TotalSeconds <= TooSlowThreshold)
                {
                    clrHi = Color.FromArgb(0xff, 0xff, 0x80, 0);
                    clrLo = Color.FromArgb(0xff, 0x80, 0x40, 0);
                }
                else
                {
                    clrHi = Color.FromArgb(0xff, 0xff, 0, 0);
                    clrLo = Color.FromArgb(0xff, 0x40, 0, 0);
                }
            }
            else if (tsDelay.Duration().TotalSeconds <= TooSlowThreshold)
            {
                clrHi = Color.FromArgb(0xff, 0, 0, 0xff);
                clrLo = Color.FromArgb(0xff, 0, 0, 0x80);
            }
            else
            {
                clrHi = Color.FromArgb(0xff, 0, 200, 0xff);
                clrLo = Color.FromArgb(0xff, 0, 0, 0xff);
            }
        }

        public virtual void GetDelayColors(out Color clrHi, out Color clrLo)
        {
            clrHi = Color.FromArgb(0, 0xc0, 0xc0, 0xc0);
            clrLo = Color.FromArgb(0, 0x80, 0x80, 0x80);
        }

        public virtual Size GetDrawSize(bool bSmall)
        {
            return new Size((double) (0x20 + (Math.Max(0, this.IDToDisplay.Length - 4) * 8)), bSmall ? ((double) 12) : ((double) 0x18));
        }

        protected virtual string GetID()
        {
            return this.m_strID;
        }

        public virtual string GetToolTipText()
        {
            return "";
        }

        public bool IsLocatedAtTargetElement()
        {
            return (((this.HeadElement.Perron != null) && (this.HeadElement.Perron.m_Station == this.m_StationDest)) || ((this.TargetElement != null) && (this.TargetElement == this.HeadElement)));
        }

        public bool IsPtCaptured(Point p, Point ptToCapture)
        {
            return false;
        }

        protected virtual bool IsValidCourseMovement(PathElement pe)
        {
            if (this.m_TrainCourse.Count > 0)
            {
                int num = 0;
                bool flag = false;
                foreach (TrainCourse course in this.m_TrainCourse)
                {
                    if (course.Element == pe)
                    {
                        flag = true;
                        break;
                    }
                    if (num++ > 3)
                    {
                        break;
                    }
                }
                if (!flag)
                {
                    return false;
                }
            }
            return true;
        }

        public virtual void Remove()
        {
            this.ResetTimeTable();
            Debug.Assert(!this.TimerExists());
            this.m_RoutePassed.Clear();
            this.m_StationPrev = null;
            this.StationTimeTable = null;
        }

        public virtual void ResetDisplays()
        {
        }

        public virtual void ResetTimeTable()
        {
            lock (this)
            {
                this.MoveState = TISMonitor.MoveState.Unknown;
                this.ResetDisplays();
                this.m_TrainCourse.Clear();
                this.m_strLineID = "";
                this.m_nDirection = 0;
                this.DateTimetable = DateTime.MinValue;
                this.m_StationDest = this.m_StationStart = (Station) (this.m_StationPrev = null);
                this.m_targetElement = null;
                this.Delay = new TimeSpan(0L);
                this.ShowAtDisplay = false;
                this.TrainType = -1;
            }
            this.StopTimerMove();
        }

        protected virtual void SetID(string strID)
        {
            this.m_strID = strID;
        }

        public virtual bool SetTimeTable(string strLineID, int nDirection, DataTable table)
        {
            Debug.Assert(table != null);
            this.ResetTimeTable();
            this.m_strLineID = strLineID;
            this.m_nDirection = nDirection;
            Debug.Assert(this.m_StationDest != null);
            Debug.Assert(this.m_StationStart != null);
            return this.BuildCourse(false);
        }

        public virtual void StopTimerMove()
        {
        }

        protected virtual bool TimerExists()
        {
            return false;
        }

        public virtual bool TimeTableAssigned()
        {
            return false;
        }

        public bool CanMoveToNextElement
        {
            get
            {
                return this.m_bCanMoveToNextElement;
            }
        }

        public string Description
        {
            get
            {
                return this.m_strDescription;
            }
            set
            {
                this.m_strDescription = value;
            }
        }

        public int Direction
        {
            get
            {
                return this.m_nDirection;
            }
            set
            {
                this.m_nDirection = value;
            }
        }

        public PathElement HeadElement
        {
            get
            {
                return this.m_HeadElement;
            }
            set
            {
                this.m_HeadElement = value;
            }
        }

        public int HeadElementArrivedSource
        {
            get
            {
                return this.m_nHeadElementArrivedSource;
            }
            set
            {
                this.m_nHeadElementArrivedSource = value;
            }
        }

        public int HeadOffset
        {
            get
            {
                return this.m_nHeadOffset;
            }
            set
            {
                this.m_nHeadOffset = value;
            }
        }

        public string ID
        {
            get
            {
                return this.GetID();
            }
            set
            {
                this.SetID(value);
            }
        }

        public string IDToDisplay
        {
            get
            {
                return this.m_strIDToDisplay;
            }
            set
            {
                this.m_strIDToDisplay = value;
            }
        }

        public string LineID
        {
            get
            {
                return this.m_strLineID;
            }
        }

        public bool Locomotive
        {
            get
            {
                return this.m_bLocomotive;
            }
            set
            {
                this.m_bLocomotive = value;
                if (this.m_bLocomotive)
                {
                    this.ResetTimeTable();
                }
            }
        }

        public TISMonitor.MoveState MoveState
        {
            get
            {
                return this.m_MoveState;
            }
            set
            {
                this.m_MoveState = value;
            }
        }

        public PathElement NextElement
        {
            get
            {
                return this.m_nNextElement;
            }
            set
            {
                this.m_nNextElement = value;
            }
        }

        public ArrayList RoutePassed
        {
            get
            {
                return this.m_RoutePassed;
            }
        }

        public PathElement TargetElement
        {
            get
            {
                return this.m_targetElement;
            }
            set
            {
                this.m_targetElement = value;
            }
        }
    }
}

