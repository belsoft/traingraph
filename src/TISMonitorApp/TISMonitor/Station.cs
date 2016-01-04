namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Xml.Serialization;

    [Serializable]
    public class Station : Element
    {
        [XmlIgnore]
        public string DBID;
        [XmlIgnore]
        public int Departures;
        public const int HEIGHT = 30;
        [XmlIgnore]
        public string LineId;
        protected static Font m_fFont;
        private int m_nEscapement;
        [XmlIgnore]
        public Hashtable m_Perrons;
        private Point m_PointBase;
        private string m_ShortName;
        public const int WIDTH = 150;

        public Station()
        {
            this.m_nEscapement = 0;
            this.m_Perrons = new Hashtable();
            this.DBID = "";
            this.m_ShortName = "";
            this.LineId = "";
            this.Departures = 0;
            this.Init();
        }

        public Station(string strID) : base(strID)
        {
            this.m_nEscapement = 0;
            this.m_Perrons = new Hashtable();
            this.DBID = "";
            this.m_ShortName = "";
            this.LineId = "";
            this.Departures = 0;
            this.Init();
        }

        public override int CanConnect(Point p, Element e, int nSource, out Point pDest)
        {
            pDest = this.m_PointBase;
            if (e is Station)
            {
                foreach (Connection connection in base.m_Connections)
                {
                    if (connection.IsConnected(this, e))
                    {
                        return -1;
                    }
                }
            }
            return this.GetConnectionSourceID(p);
        }

        public override OnElementMoveDelegate CanMove(Point p)
        {
            if (Element.IsPtCaptured(p, this.PointBase))
            {
                return new OnElementMoveDelegate(this.OnMove);
            }
            return null;
        }

        public override void CopyLocationInfoFrom(Element e)
        {
            Debug.Assert(e is Station);
            if (e is Station)
            {
                this.PointBase = (e as Station).PointBase;
            }
        }

        public override void Draw(Grid g)
        {
        }

        public override int GetConnectionSourceID(Point p)
        {
            if (Element.IsPtCaptured(p, this.PointBase))
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
            return XMLResourceLoaderUtils.GetString("STATION");
        }

        public Perron GetPerronByID(string str)
        {
            if (this.m_Perrons.ContainsKey(str))
            {
                return (Perron) this.m_Perrons[str];
            }
            return null;
        }

        public Perron GetPerronRude()
        {
            using (Dictionary<object, object>.ValueCollection.Enumerator enumerator = this.m_Perrons.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    return (Perron) enumerator.Current;
                }
            }
            return null;
        }

        public override Form GetPropertiesForm()
        {
            return null;
        }

        private void Init()
        {
        }

        public override void InitAfterLoad(Layout l)
        {
            base.m_Layout = l;
            foreach (Perron perron in l.Perrons.Values)
            {
                if (perron.Station == base.ID)
                {
                    this.m_Perrons[perron.ID] = perron;
                    perron.m_Station = this;
                }
            }
            base.InitAfterLoad(l);
        }

        public bool IsFinal()
        {
            return (base.m_Connections.Count == 1);
        }

        public override void Move(Size size)
        {
        }

        private Rectangle OnMove(Point p)
        {
            this.PointBase = p;
            return new Rectangle(0, 0, 0, 0);
        }

        public override void Reflect(int width, bool h)
        {
            this.PointBase = h ? Element.GetPointRotatedByY(width, this.PointBase) : Element.GetPointRotatedByX(width, this.PointBase);
        }

        public override void SetLocation(Rectangle r)
        {
            this.m_PointBase = new Point((double) r.X, (double) r.Y);
        }

        public override void UpdateBounds()
        {
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

        public Point PointBase
        {
            get
            {
                return this.m_PointBase;
            }
            set
            {
                if (this.m_PointBase != value)
                {
                    this.m_PointBase = value;
                    this.UpdateBounds();
                }
            }
        }

        public string ShortName
        {
            get
            {
                return this.m_ShortName;
            }
            set
            {
                this.m_ShortName = value;
            }
        }
    }
}

