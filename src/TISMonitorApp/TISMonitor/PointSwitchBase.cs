namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;

    [Serializable]
    public abstract class PointSwitchBase : PathElement
    {
        protected Rectangle BoundsMinus;
        protected Rectangle BoundsPlus;
        public const int CAPTION_OFFSETX = 0;
        public const int CAPTION_OFFSETY = 20;
        private ArrayList m_alTrainsPeakToSink;
        protected Point m_PointCore;
        protected Point m_PointMinus;
        protected Point m_PointPeak;
        protected Point m_PointPlus;

        public PointSwitchBase()
        {
            this.m_PointPlus = new Point(0.0, 0.0);
            this.m_PointMinus = new Point(0.0, 0.0);
            this.m_PointCore = new Point(0.0, 0.0);
            this.m_PointPeak = new Point(0.0, 0.0);
            this.BoundsPlus = new Rectangle(0, 0, 0, 0);
            this.BoundsMinus = new Rectangle(0, 0, 0, 0);
            this.m_alTrainsPeakToSink = new ArrayList();
        }

        public PointSwitchBase(string strID) : base(strID)
        {
            this.m_PointPlus = new Point(0.0, 0.0);
            this.m_PointMinus = new Point(0.0, 0.0);
            this.m_PointCore = new Point(0.0, 0.0);
            this.m_PointPeak = new Point(0.0, 0.0);
            this.BoundsPlus = new Rectangle(0, 0, 0, 0);
            this.BoundsMinus = new Rectangle(0, 0, 0, 0);
            this.m_alTrainsPeakToSink = new ArrayList();
        }

        public override void AddTrain(TrainBase t, int nSource)
        {
            Debug.Assert(t != null);
            Debug.Assert(!this.m_alTrainsPeakToSink.Contains(t));
            if (!this.m_alTrainsPeakToSink.Contains(t))
            {
                switch (((ConnectionSource) nSource))
                {
                    case ConnectionSource.Peak:
                        this.m_alTrainsPeakToSink.Insert(0, t);
                        break;

                    case ConnectionSource.Plus:
                    case ConnectionSource.Minus:
                        this.m_alTrainsPeakToSink.Add(t);
                        break;

                    default:
                        Debug.Assert(this.m_alTrainsPeakToSink.Count == 0);
                        this.m_alTrainsPeakToSink.Add(t);
                        break;
                }
                foreach (TrainBase base2 in this.m_alTrainsPeakToSink)
                {
                    if ((base2 != t) && !base.m_Layout.HasTrain(base2.ID))
                    {
                        base2.DebugTrace("PointSwitchBase.AddTrain: Train does not exist in layout!!!");
                    }
                }
            }
        }

        public Point CalcCenter(Point pTo)
        {
            Point point = new Point();
            point.set_X(this.PointCore.get_X() + (((int) (pTo.get_X() - this.PointCore.get_X())) >> 1));
            point.set_Y(this.PointCore.get_Y() + (((int) (pTo.get_Y() - this.PointCore.get_Y())) >> 1));
            return point;
        }

        public override int CanConnect(Point p, Element e, int nSource, out Point pDest)
        {
            int connectionSourceID = -1;
            pDest = new Point(0.0, 0.0);
            if (e == this)
            {
                return -1;
            }
            connectionSourceID = this.GetConnectionSourceID(p);
            switch (connectionSourceID)
            {
                case 1:
                    pDest = this.PointPeak;
                    break;

                case 2:
                    pDest = this.PointPlus;
                    break;

                case 3:
                    pDest = this.PointMinus;
                    break;

                case -1:
                    return -1;
            }
            if (e is Light)
            {
                foreach (Connection connection in base.m_Connections)
                {
                    if (connection.IsConnectedTo(this, connectionSourceID, e.GetType()) != null)
                    {
                        return -1;
                    }
                }
                return connectionSourceID;
            }
            if ((e is Perron) || (e is TrainNumberField))
            {
                foreach (Connection connection in base.m_Connections)
                {
                    if (connection.HasElement(e.GetType()))
                    {
                        return -1;
                    }
                }
                return connectionSourceID;
            }
            if (!((e is Track) || (e is PointSwitchBase)))
            {
                return -1;
            }
            foreach (Connection connection in base.m_Connections)
            {
                if (connection.IsConnectedPath(this, connectionSourceID) != null)
                {
                    return -1;
                }
            }
            return connectionSourceID;
        }

        public override OnElementMoveDelegate CanMove(Point p)
        {
            if (Element.IsPtCaptured(p, this.PointPlus))
            {
                return new OnElementMoveDelegate(this.OnMovePlus);
            }
            if (Element.IsPtCaptured(p, this.PointMinus))
            {
                return new OnElementMoveDelegate(this.OnMoveMinus);
            }
            if (Element.IsPtCaptured(p, this.PointPeak))
            {
                return new OnElementMoveDelegate(this.OnMovePeak);
            }
            if (Element.IsPtCaptured(p, this.PointCore))
            {
                return new OnElementMoveDelegate(this.OnMoveCenter);
            }
            return null;
        }

        public override bool CanSomeTrainToMoveToNextElement(PathElement pe)
        {
            foreach (TrainBase base2 in this.m_alTrainsPeakToSink)
            {
                if ((base2.NextElement == pe) && base2.CanMoveToNextElement)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool CanTrainMoveThruSource(TrainBase t, int nSource)
        {
            int index = this.m_alTrainsPeakToSink.IndexOf(t);
            if (index == -1)
            {
                Debug.Assert(false);
                return false;
            }
            ConnectionSource source = (ConnectionSource) nSource;
            if (source == ConnectionSource.Peak)
            {
                return (index == 0);
            }
            return (index == (this.m_alTrainsPeakToSink.Count - 1));
        }

        public override void CopyLocationInfoFrom(Element e)
        {
            Debug.Assert(e is PointSwitchBase);
            if (e is PointSwitchBase)
            {
                this.PointCore = (e as PointSwitchBase).PointCore;
                this.PointPeak = (e as PointSwitchBase).PointPeak;
                this.PointMinus = (e as PointSwitchBase).PointMinus;
                this.PointPlus = (e as PointSwitchBase).PointPlus;
            }
        }

        public override Point GetCenterPoint()
        {
            return this.PointCore;
        }

        public override int GetConnectionSourceID(Point p)
        {
            if (Element.IsPtCaptured(p, this.PointPlus))
            {
                return 2;
            }
            if (Element.IsPtCaptured(p, this.PointMinus))
            {
                return 3;
            }
            if (Element.IsPtCaptured(p, this.PointPeak))
            {
                return 1;
            }
            return -1;
        }

        public override string GetConnectionSourceName(int nSource)
        {
            switch (nSource)
            {
                case 1:
                    return "p";

                case 2:
                    return "+";

                case 3:
                    return "-";
            }
            return "?";
        }

        public override Point GetConnectionSourcePoint(int nSource)
        {
            if (nSource != -1)
            {
                switch (((ConnectionSource) nSource))
                {
                    case ConnectionSource.Plus:
                        return this.PointPlus;

                    case ConnectionSource.Minus:
                        return this.PointMinus;
                }
            }
            return this.PointPeak;
        }

        public Point GetHorizontalPoint()
        {
            if (this.PointCore.get_Y() != this.PointPeak.get_Y())
            {
                if (this.PointCore.get_Y() == this.PointPlus.get_Y())
                {
                    return this.PointPlus;
                }
                if (this.PointCore.get_Y() == this.PointMinus.get_Y())
                {
                    return this.PointMinus;
                }
            }
            return this.PointPeak;
        }

        public override void GetPathDirection(int nSource, out Point pFrom, out Point pTo)
        {
            pFrom = pTo = this.PointCore;
            switch (((ConnectionSource) nSource))
            {
                case ConnectionSource.Peak:
                    pFrom = this.PointPeak;
                    pTo = this.PointCore;
                    break;

                case ConnectionSource.Plus:
                    pFrom = this.PointPlus;
                    pTo = this.PointCore;
                    break;

                case ConnectionSource.Minus:
                    pFrom = this.PointMinus;
                    pTo = this.PointCore;
                    break;
            }
        }

        public override ArrayList GetPathNeighbors()
        {
            ArrayList list = new ArrayList();
            foreach (Connection connection in base.m_Connections)
            {
                PathElement item = connection.IsConnectedPath(this, 2);
                if (item != null)
                {
                    list.Add(item);
                }
                item = connection.IsConnectedPath(this, 3);
                if (item != null)
                {
                    list.Add(item);
                }
                item = connection.IsConnectedPath(this, 1);
                if (item != null)
                {
                    list.Add(item);
                }
            }
            Debug.Assert((list.Count >= 0) && (list.Count <= 3));
            return list;
        }

        public override int[] GetSources()
        {
            return new int[] { 1, 2, 3 };
        }

        public override Point GetTrainPoint()
        {
            return this.PointCore;
        }

        public override bool HasTrain(TrainBase t)
        {
            return this.m_alTrainsPeakToSink.Contains(t);
        }

        public override bool HasTrains()
        {
            return (this.m_alTrainsPeakToSink.Count > 0);
        }

        public override void Move(Size size)
        {
            this.PointCore = new Point(this.PointCore.get_X() + size.get_Width(), this.PointCore.get_Y() + size.get_Height());
            this.PointPeak = new Point(this.PointPeak.get_X() + size.get_Width(), this.PointPeak.get_Y() + size.get_Height());
            this.PointMinus = new Point(this.PointMinus.get_X() + size.get_Width(), this.PointMinus.get_Y() + size.get_Height());
            this.PointPlus = new Point(this.PointPlus.get_X() + size.get_Width(), this.PointPlus.get_Y() + size.get_Height());
            this.UpdateBounds();
        }

        protected Rectangle OnMoveCenter(Point pt)
        {
            this.PointCore = pt;
            return base.Bounds;
        }

        protected Rectangle OnMoveMinus(Point pt)
        {
            this.PointMinus = pt;
            return base.Bounds;
        }

        protected Rectangle OnMovePeak(Point pt)
        {
            this.PointPeak = pt;
            return base.Bounds;
        }

        protected Rectangle OnMovePlus(Point pt)
        {
            this.PointPlus = pt;
            return base.Bounds;
        }

        public override void RemoveTrain(TrainBase t)
        {
            this.m_alTrainsPeakToSink.Remove(t);
        }

        public override void RemoveTrains()
        {
            this.m_alTrainsPeakToSink.Clear();
        }

        public override void SetLocation(Rectangle r)
        {
            this.PointCore = new Point((double) (r.X + (r.Width >> 1)), (double) r.Y);
            this.PointPeak = new Point((double) r.X, (double) r.Y);
            this.PointMinus = new Point((double) (r.X + r.Width), (double) r.Y);
            this.PointPlus = new Point((double) (r.X + r.Width), (double) (r.Y + r.Height));
        }

        public override ArrayList Trains()
        {
            return this.m_alTrainsPeakToSink.Clone();
        }

        public override void UpdateBounds()
        {
            int num = (int) Math.Min(Math.Min(this.PointCore.get_X(), this.PointPlus.get_X()), Math.Min(this.PointPeak.get_X(), this.PointMinus.get_X()));
            int num2 = (int) Math.Min(Math.Min(this.PointCore.get_Y(), this.PointPlus.get_Y()), Math.Min(this.PointPeak.get_Y(), this.PointMinus.get_Y()));
            int num3 = (int) Math.Max(Math.Max(this.PointCore.get_X(), this.PointPlus.get_X()), Math.Max(this.PointPeak.get_X(), this.PointMinus.get_X()));
            int num4 = (int) Math.Max(Math.Max(this.PointCore.get_Y(), this.PointPlus.get_Y()), Math.Max(this.PointPeak.get_Y(), this.PointMinus.get_Y()));
            this.Bounds.X = num;
            this.Bounds.Y = num2;
            this.Bounds.Size = new Size((double) (num3 - num), (double) (num4 - num2));
            this.Center.set_X((double) (num + ((num3 - num) >> 1)));
            this.Center.set_Y((double) (num2 + ((num4 - num2) >> 1)));
            num = (int) Math.Min(this.PointCore.get_X(), this.PointPlus.get_X());
            num2 = (int) Math.Min(this.PointCore.get_Y(), this.PointPlus.get_Y());
            num3 = (int) Math.Max(this.PointCore.get_X(), this.PointPlus.get_X());
            num4 = (int) Math.Max(this.PointCore.get_Y(), this.PointPlus.get_Y());
            this.BoundsPlus.X = num;
            this.BoundsPlus.Y = num2;
            this.BoundsPlus.Size = new Size((double) (num3 - num), (double) (num4 - num2));
            num = (int) Math.Min(this.PointCore.get_X(), this.PointMinus.get_X());
            num2 = (int) Math.Min(this.PointCore.get_Y(), this.PointMinus.get_Y());
            num3 = (int) Math.Max(this.PointCore.get_X(), this.PointMinus.get_X());
            num4 = (int) Math.Max(this.PointCore.get_Y(), this.PointMinus.get_Y());
            this.BoundsMinus.X = num;
            this.BoundsMinus.Y = num2;
            this.BoundsMinus.Size = new Size((double) (num3 - num), (double) (num4 - num2));
        }

        public Point PointCore
        {
            get
            {
                return this.m_PointCore;
            }
            set
            {
                if (this.m_PointCore != value)
                {
                    this.m_PointCore = value;
                    this.UpdateBounds();
                }
            }
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

        public Point PointPeak
        {
            get
            {
                return this.m_PointPeak;
            }
            set
            {
                if (this.m_PointPeak != value)
                {
                    this.m_PointPeak = value;
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

        public enum ConnectionSource
        {
            Minus = 3,
            Peak = 1,
            Plus = 2,
            Unknown = -1
        }
    }
}

