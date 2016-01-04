namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class IconElement : Element
    {
        public const int HEIGHT = 0x10;
        private Point m_BasePoint;
        [XmlIgnore]
        private bool m_bEnabled;
        private int m_nNumber;
        [XmlIgnore]
        public TISMonitor.Station m_Station;
        private string m_strAPIID;
        private string m_strStation;
        [XmlIgnore]
        public DeviceState State;
        public const int WIDTH = 0x10;

        public IconElement()
        {
            this.m_strStation = "";
            this.m_nNumber = 1;
            this.m_strAPIID = "?";
            this.m_bEnabled = true;
            this.Init();
        }

        public IconElement(string strID) : base(strID)
        {
            this.m_strStation = "";
            this.m_nNumber = 1;
            this.m_strAPIID = "?";
            this.m_bEnabled = true;
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
            Debug.Assert(e is IconElement);
            if (e is IconElement)
            {
                this.m_BasePoint = (e as IconElement).BasePoint;
            }
        }

        public override void Draw(Grid g)
        {
        }

        public abstract string GetAPIName();
        public override int GetConnectionSourceID(Point p)
        {
            if (Element.IsPtCaptured(p, this.BasePoint))
            {
                return 0;
            }
            return -1;
        }

        protected abstract Icon GetIcon();
        public override Form GetPropertiesForm()
        {
            return null;
        }

        public override string GetToolTipText(bool bDetailed)
        {
            string str;
            if (bDetailed)
            {
                str = "{0} {1} {2} - {3}";
                return string.Format(str, new object[] { this.GetName(), this.Number, base.ID, this.State2Description() });
            }
            str = "{0} {1} - {2}";
            return string.Format(str, this.GetName(), this.Number, this.State2Description());
        }

        private void Init()
        {
            this.State = DeviceState.UNKNOWN;
        }

        public override void InitAfterLoad(Layout l)
        {
            this.m_Station = l.Stations.GetStationByID(this.m_strStation);
            Debug.Assert(this.m_Station != null);
            Debug.Assert(this.m_strAPIID != "?");
            base.InitAfterLoad(l);
        }

        public override void Move(Size size)
        {
            this.UpdateBounds();
        }

        private Rectangle OnMove(Point pt)
        {
            this.BasePoint = pt;
            this.UpdateBounds();
            return base.Bounds;
        }

        public override void SetLocation(Rectangle r)
        {
            this.BasePoint = new Point((double) r.X, (double) r.Y);
        }

        public virtual string State2Description()
        {
            return State2Description(this.State);
        }

        public static string State2Description(DeviceState ds)
        {
            switch (ds)
            {
                case DeviceState.UNKNOWN:
                    return XMLResourceLoaderUtils.GetString("NA");

                case DeviceState.OK:
                    return XMLResourceLoaderUtils.GetString("READY");

                case DeviceState.RING:
                    return XMLResourceLoaderUtils.GetString("RINGING");

                case DeviceState.BUSY:
                    return XMLResourceLoaderUtils.GetString("BUSY");

                case DeviceState.ERROR:
                    return XMLResourceLoaderUtils.GetString("ERROR");
            }
            return ds.ToString();
        }

        public override void UpdateBounds()
        {
        }

        public string APIID
        {
            get
            {
                return this.m_strAPIID;
            }
            set
            {
                this.m_strAPIID = value;
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
    }
}

