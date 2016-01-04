namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    [Serializable]
    public class Track : PathElement
    {
        private ArrayList m_alTrainsPlusToMinus;
        private Point m_PointMinus;
        private Point m_PointPlus;
        private PathElementPassive.PathState m_State;
        private byte Red;
        private byte White;

        public Track()
        {
            this.White = 0;
            this.Red = 0;
            this.m_State = PathElementPassive.PathState.Undefined;
            this.m_PointMinus = new Point(0.0, 0.0);
            this.m_PointPlus = new Point(0.0, 0.0);
            this.m_alTrainsPlusToMinus = new ArrayList();
            this.Init();
        }

        public Track(string sID) : base(sID)
        {
            this.White = 0;
            this.Red = 0;
            this.m_State = PathElementPassive.PathState.Undefined;
            this.m_PointMinus = new Point(0.0, 0.0);
            this.m_PointPlus = new Point(0.0, 0.0);
            this.m_alTrainsPlusToMinus = new ArrayList();
            base.Segment = sID;
            this.Init();
        }

        public override void AddTrain(TrainBase t, int nSource)
        {
            Debug.Assert(t != null);
            Debug.Assert(!this.m_alTrainsPlusToMinus.Contains(t));
            if (!this.m_alTrainsPlusToMinus.Contains(t))
            {
                switch (((ConnectionSource) nSource))
                {
                    case ConnectionSource.Plus:
                        this.m_alTrainsPlusToMinus.Insert(0, t);
                        break;

                    case ConnectionSource.Minus:
                        this.m_alTrainsPlusToMinus.Add(t);
                        break;

                    default:
                        this.m_alTrainsPlusToMinus.Add(t);
                        break;
                }
                foreach (TrainBase base2 in this.m_alTrainsPlusToMinus)
                {
                    if ((base2 != t) && !base.m_Layout.HasTrain(base2.ID))
                    {
                    }
                }
            }
        }

        public override OnElementMoveDelegate CanMove(Point p)
        {
            return null;
        }

        public override bool CanSomeTrainToMoveToNextElement(PathElement pe)
        {
            foreach (TrainBase base2 in this.m_alTrainsPlusToMinus)
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
            int index = this.m_alTrainsPlusToMinus.IndexOf(t);
            if (index == -1)
            {
                Debug.Assert(false);
                return false;
            }
            ConnectionSource source = (ConnectionSource) nSource;
            if (source == ConnectionSource.Plus)
            {
                return (index == 0);
            }
            return (index == (this.m_alTrainsPlusToMinus.Count - 1));
        }

        public override void CopyLocationInfoFrom(Element e)
        {
            Debug.Assert(e is Track);
            if (e is Track)
            {
                this.PointPlus = (e as Track).PointPlus;
                this.PointMinus = (e as Track).PointMinus;
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
            return XMLResourceLoaderUtils.GetString("TRACK");
        }

        public override PathElement GetNextPathThrough(bool bCheckOccupied, ref int nSourceFrom, out int nForeignSource)
        {
            int num2;
            nForeignSource = -1;
            if (!(!bCheckOccupied || base.IsOccupiedB()))
            {
                return null;
            }
            int oppositeSource = this.GetOppositeSource(nSourceFrom);
            nSourceFrom = num2 = oppositeSource;
            return base.GetConnectedPath(num2, out nForeignSource);
        }

        public static bool GetOffsettedValue(PathElement peHead, double HeadOffset, int HeadElementArrivedSource, ref Point ptOut)
        {
            Point point = new Point();
            int[] sources = peHead.GetSources();
            Debug.Assert(sources.Length == 2);
            Point connectionSourcePoint = peHead.GetConnectionSourcePoint(sources[0]);
            Point point3 = peHead.GetConnectionSourcePoint(sources[1]);
            if (sources.Length != 2)
            {
                return false;
            }
            double introduced12 = Math.Pow(connectionSourcePoint.get_X() - point3.get_X(), 2.0);
            double num = Math.Sqrt(introduced12 + Math.Pow(connectionSourcePoint.get_Y() - point3.get_Y(), 2.0));
            double num2 = (num * HeadOffset) / ((peHead.Length == 0) ? ((double) 1) : ((double) peHead.Length));
            double num3 = 1.0;
            double num4 = 0.0;
            if (!(connectionSourcePoint.get_Y() == point3.get_Y()))
            {
                num3 = Math.Abs((double) (point3.get_X() - connectionSourcePoint.get_X())) / num;
                num4 = Math.Abs((double) (point3.get_Y() - connectionSourcePoint.get_Y())) / num;
            }
            double num5 = num2 * num3;
            double num6 = num2 * num4;
            if (HeadElementArrivedSource == sources[0])
            {
                if (connectionSourcePoint.get_X() <= point3.get_X())
                {
                    connectionSourcePoint.set_X(connectionSourcePoint.get_X() + ((int) num5));
                }
                else
                {
                    connectionSourcePoint.set_X(connectionSourcePoint.get_X() - ((int) num5));
                }
                if (connectionSourcePoint.get_Y() <= point3.get_Y())
                {
                    connectionSourcePoint.set_Y(connectionSourcePoint.get_Y() + ((int) num6));
                }
                else
                {
                    connectionSourcePoint.set_Y(connectionSourcePoint.get_Y() - ((int) num6));
                }
                point = connectionSourcePoint;
            }
            else
            {
                if (point3.get_X() <= connectionSourcePoint.get_X())
                {
                    point3.set_X(point3.get_X() + ((int) num5));
                }
                else
                {
                    point3.set_X(point3.get_X() - ((int) num5));
                }
                if (point3.get_Y() <= connectionSourcePoint.get_Y())
                {
                    point3.set_Y(point3.get_Y() + ((int) num6));
                }
                else
                {
                    point3.set_Y(point3.get_Y() - ((int) num6));
                }
                point = point3;
            }
            ptOut = point;
            return true;
        }

        public override int GetOppositeSource(int nSource)
        {
            return ((nSource == 1) ? 2 : 1);
        }

        public override void GetPathDirection(int nSource, out Point pFrom, out Point pTo)
        {
            pFrom = pTo = this.PointMinus;
            switch (((ConnectionSource) nSource))
            {
                case ConnectionSource.Plus:
                    pFrom = this.PointPlus;
                    pTo = this.PointMinus;
                    break;

                case ConnectionSource.Minus:
                    pFrom = this.PointMinus;
                    pTo = this.PointPlus;
                    break;
            }
        }

        public override ArrayList GetPathNeighbors()
        {
            ArrayList list = new ArrayList();
            foreach (Connection connection in base.m_Connections)
            {
                PathElement item = connection.IsConnectedPath(this, 1);
                if (item != null)
                {
                    list.Add(item);
                }
                item = connection.IsConnectedPath(this, 2);
                if (item != null)
                {
                    list.Add(item);
                }
            }
            Debug.Assert((list.Count >= 0) && (list.Count <= 2));
            return list;
        }

        public override Form GetPropertiesForm()
        {
            return null;
        }

        public override int[] GetSources()
        {
            return new int[] { 1, 2 };
        }

        public override bool HasTrain(TrainBase t)
        {
            return this.m_alTrainsPlusToMinus.Contains(t);
        }

        public override bool HasTrains()
        {
            return (this.m_alTrainsPlusToMinus.Count > 0);
        }

        private void Init()
        {
            this.PointPlus = new Point(100.0, 20.0);
            this.PointMinus = new Point(200.0, 20.0);
        }

        public override void Invalidate()
        {
            this.White = (byte) (this.Red = 0);
            this.m_State = PathElementPassive.PathState.Undefined;
            base.Invalidate();
        }

        public override ArrayList IsOccupied(bool bStrict)
        {
            ArrayList list = new ArrayList();
            if (this.IsPathOccupied(this.m_State))
            {
                list.Add(1);
                list.Add(2);
            }
            return list;
        }

        public override ArrayList IsSelected()
        {
            ArrayList list = new ArrayList();
            if (this.IsPathSelected(this.m_State))
            {
                list.Add(1);
                list.Add(2);
            }
            return list;
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

        public override void RemoveTrain(TrainBase t)
        {
            this.m_alTrainsPlusToMinus.Remove(t);
        }

        public override void RemoveTrains()
        {
            this.m_alTrainsPlusToMinus.Clear();
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

        public override bool SetState(string strData, bool bState)
        {
            string[] strArray = strData.Split(new char[] { ' ' });
            if ((strArray.Length != 3) && (strArray.Length != 4))
            {
                return false;
            }
            if ((strArray[0].ToUpper() != "GLEISABSCHNITT") && ((strArray[0].Length != 2) || (strArray[0][1] != 'W')))
            {
                return false;
            }
            Debug.Assert(strArray[1] == base.Address);
            string str = strArray[2];
            byte num = bState ? ((byte) 1) : ((byte) 0);
            if (str.IndexOf("ws") == 0)
            {
                this.White = num;
            }
            else if (str.IndexOf("rt") == 0)
            {
                this.Red = num;
            }
            else
            {
                return false;
            }
            byte num2 = (byte) (this.White + this.Red);
            if (num2 == 0)
            {
                this.m_State = PathElementPassive.PathState.Black;
                base.Valid = true;
            }
            else if (num2 == 1)
            {
                this.m_State = (this.White == 1) ? PathElementPassive.PathState.White : PathElementPassive.PathState.Red;
                base.Valid = true;
            }
            else
            {
                this.m_State = PathElementPassive.PathState.Invalid;
                base.Valid = false;
            }
            base.Defined = true;
            if (base.Valid && (this.m_State != PathElementPassive.PathState.Red))
            {
                base.Defect = false;
            }
            ArrayList list = (ArrayList) base.m_Layout.m_htSegment2PathElementPassive[base.Segment];
            Debug.Assert(list != null);
            if (list != null)
            {
                foreach (PathElementPassive passive in list)
                {
                    if (passive.ID != base.ID)
                    {
                        passive.SetSegmentState(this.m_State);
                    }
                }
            }
            return base.SetState(strData, bState);
        }

        public override ArrayList Trains()
        {
            return this.m_alTrainsPlusToMinus.Clone();
        }

        public override void UpdateBounds()
        {
            int num = (int) Math.Min(this.PointPlus.get_X(), this.PointMinus.get_X());
            int num2 = (int) Math.Min(this.PointPlus.get_Y(), this.PointMinus.get_Y());
            int num3 = (int) Math.Max(this.PointPlus.get_X(), this.PointMinus.get_X());
            int num4 = (int) Math.Max(this.PointPlus.get_Y(), this.PointMinus.get_Y());
            this.Bounds.X = num;
            this.Bounds.Y = num2;
            this.Bounds.Size = new Size((double) Math.Max(num3 - num, 1), (double) Math.Max(num4 - num2, 1));
            this.Center.set_X((double) (num + ((num3 - num) >> 1)));
            this.Center.set_Y((double) (num2 + ((num4 - num2) >> 1)));
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

