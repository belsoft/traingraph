namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    [Serializable]
    public class TrackPassive : PathElementPassive
    {
        public const int CAPTION_OFFSETX = 0;
        public const int CAPTION_OFFSETY = 20;
        private Point m_PointMinus;
        private Point m_PointPlus;
        private PathElementPassive.PathState m_State;

        public TrackPassive()
        {
            this.m_State = PathElementPassive.PathState.Undefined;
            this.m_PointMinus = new Point(0.0, 0.0);
            this.m_PointPlus = new Point(0.0, 0.0);
            this.Init();
        }

        public TrackPassive(string sID) : base(sID)
        {
            this.m_State = PathElementPassive.PathState.Undefined;
            this.m_PointMinus = new Point(0.0, 0.0);
            this.m_PointPlus = new Point(0.0, 0.0);
            base.Segment = sID;
            this.Init();
        }

        public override int CanConnect(Point p, Element e, int nSource, out Point pDest)
        {
            int num = -1;
            pDest = new Point(0.0, 0.0);
            if (e != this)
            {
                if (Element.IsPtCaptured(p, this.PointPlus))
                {
                    pDest = this.PointPlus;
                    num = 1;
                }
                else if (Element.IsPtCaptured(p, this.PointMinus))
                {
                    pDest = this.PointMinus;
                    num = 2;
                }
                if (num == -1)
                {
                    return -1;
                }
                if (e is TrackPassive)
                {
                    foreach (Connection connection in base.m_Connections)
                    {
                        if (connection.IsConnected(this, e))
                        {
                            return -1;
                        }
                    }
                    foreach (Connection connection in base.m_Connections)
                    {
                        if (connection.IsConnectedPath(this, num) != null)
                        {
                            return -1;
                        }
                    }
                    return num;
                }
            }
            return -1;
        }

        public override OnElementMoveDelegate CanMove(Point p)
        {
            if (Element.IsPtCaptured(p, this.PointPlus))
            {
                return new OnElementMoveDelegate(this.OnMove1);
            }
            if (Element.IsPtCaptured(p, this.PointMinus))
            {
                return new OnElementMoveDelegate(this.OnMove2);
            }
            return null;
        }

        public override void CopyLocationInfoFrom(Element e)
        {
            Debug.Assert(e is TrackPassive);
            if (e is TrackPassive)
            {
                this.PointPlus = (e as TrackPassive).PointPlus;
                this.PointMinus = (e as TrackPassive).PointMinus;
            }
        }

        public override void Draw(Grid g)
        {
        }

        public override void Dump()
        {
            base.Dump();
        }

        public override int GetConnectionSourceID(Point p)
        {
            if (Element.IsPtCaptured(p, this.PointPlus))
            {
                return 1;
            }
            if (Element.IsPtCaptured(p, this.PointMinus))
            {
                return 2;
            }
            return -1;
        }

        public override string GetConnectionSourceName(int nSource)
        {
            switch (nSource)
            {
                case 1:
                    return "+";

                case 2:
                    return "-";
            }
            return "?";
        }

        public override Point GetConnectionSourcePoint(int nSource)
        {
            if (nSource != -1)
            {
                ConnectionSource source = (ConnectionSource) nSource;
                Debug.Assert((source == ConnectionSource.Plus) || (source == ConnectionSource.Minus));
                return ((source == ConnectionSource.Plus) ? this.PointPlus : this.PointMinus);
            }
            return this.PointPlus;
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("TRACKPASSIVE");
        }

        public override Form GetPropertiesForm()
        {
            return null;
        }

        private void Init()
        {
            this.PointPlus = new Point(100.0, 20.0);
            this.PointMinus = new Point(200.0, 20.0);
        }

        public override void Invalidate()
        {
            this.m_State = PathElementPassive.PathState.Undefined;
            base.Invalidate();
        }

        public override void Move(Size size)
        {
        }

        private Rectangle OnMove1(Point pt)
        {
            this.PointPlus = pt;
            return base.Bounds;
        }

        private Rectangle OnMove2(Point pt)
        {
            this.PointMinus = pt;
            return base.Bounds;
        }

        public override void Reflect(int width, bool h)
        {
            this.PointMinus = h ? Element.GetPointRotatedByY(width, this.PointMinus) : Element.GetPointRotatedByX(width, this.PointMinus);
            this.PointPlus = h ? Element.GetPointRotatedByY(width, this.PointPlus) : Element.GetPointRotatedByX(width, this.PointPlus);
        }

        public override void SetLocation(Rectangle r)
        {
            this.PointPlus = new Point((double) r.X, (double) r.Y);
            this.PointMinus = new Point((double) (r.X + r.Width), (double) r.Y);
        }

        public override bool SetSegmentState(PathElementPassive.PathState st)
        {
            this.m_State = st;
            return true;
        }

        public override void UpdateBounds()
        {
        }

        public Point PointMinus
        {
            get
            {
                return this.m_PointMinus;
            }
            set
            {
                if (this.m_PointMinus != value)
                {
                    this.m_PointMinus = value;
                    this.UpdateBounds();
                }
            }
        }

        public Point PointPlus
        {
            get
            {
                return this.m_PointPlus;
            }
            set
            {
                if (this.m_PointPlus != value)
                {
                    this.m_PointPlus = value;
                    this.UpdateBounds();
                }
            }
        }

        public PathElementPassive.PathState State
        {
            get
            {
                return this.m_State;
            }
        }

        public enum ConnectionSource
        {
            Minus = 2,
            Plus = 1,
            Unknown = -1
        }
    }
}

