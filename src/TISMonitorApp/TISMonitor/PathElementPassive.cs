namespace TISMonitor
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class PathElementPassive : StateElement
    {
        protected bool m_bDetectTrains;
        protected bool m_bShowInSimpleView;
        protected int m_nAverageSpeed;
        protected int m_nLength;
        [XmlIgnore]
        public Station m_StationArea;
        protected string m_strSegment;
        protected string m_strStationArea;

        public PathElementPassive()
        {
            this.m_StationArea = null;
            this.m_strSegment = "";
            this.m_nLength = 1;
            this.m_bShowInSimpleView = true;
            this.m_bDetectTrains = false;
            this.m_strStationArea = "";
            this.m_nAverageSpeed = 60;
        }

        public PathElementPassive(string strID) : base(strID)
        {
            this.m_StationArea = null;
            this.m_strSegment = "";
            this.m_nLength = 1;
            this.m_bShowInSimpleView = true;
            this.m_bDetectTrains = false;
            this.m_strStationArea = "";
            this.m_nAverageSpeed = 60;
        }

        public abstract bool SetSegmentState(PathState st);

        public int AverageSpeed
        {
            get
            {
                return this.m_nAverageSpeed;
            }
            set
            {
                this.m_nAverageSpeed = value;
            }
        }

        public bool DetectTrains
        {
            get
            {
                return this.m_bDetectTrains;
            }
            set
            {
                this.m_bDetectTrains = value;
            }
        }

        public int Length
        {
            get
            {
                return this.m_nLength;
            }
            set
            {
                this.m_nLength = value;
            }
        }

        public string Segment
        {
            get
            {
                return this.m_strSegment;
            }
            set
            {
                this.m_strSegment = value;
            }
        }

        public bool ShowInSimpleView
        {
            get
            {
                return this.m_bShowInSimpleView;
            }
            set
            {
                this.m_bShowInSimpleView = value;
            }
        }

        public string StationArea
        {
            get
            {
                return this.m_strStationArea;
            }
            set
            {
                this.m_strStationArea = value;
            }
        }

        [Serializable]
        public enum PathState
        {
            Black = 2,
            Invalid = 3,
            Red = 1,
            Undefined = -1,
            White = 0
        }
    }
}

