namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class PathElement : PathElementPassive
    {
        protected bool m_bDefect;
        [XmlIgnore]
        public TISMonitor.Perron Perron;
        public const int TRAIN_HEIGHT = 12;
        [XmlIgnore]
        public TISMonitor.TrainNumberField TrainNumberField;

        public PathElement()
        {
            this.m_bDefect = false;
            this.Perron = null;
            this.TrainNumberField = null;
            base.m_bDetectTrains = true;
        }

        public PathElement(string strID) : base(strID)
        {
            this.m_bDefect = false;
            this.Perron = null;
            this.TrainNumberField = null;
            base.m_bDetectTrains = true;
        }

        public abstract void AddTrain(TrainBase t, int nSource);
        public abstract bool CanSomeTrainToMoveToNextElement(PathElement pe);
        public abstract bool CanTrainMoveThruSource(TrainBase t, int nSource);
        public bool FindPath(PathElement peTo, out ArrayList alTrainPath)
        {
            alTrainPath = new ArrayList();
            int[] sources = this.GetSources();
            foreach (int num2 in sources)
            {
                int num;
                PathElement connectedPath = this.GetConnectedPath(num2, out num);
                alTrainPath.Clear();
                if ((connectedPath != null) && this.FindPath(peTo, this, connectedPath, num, alTrainPath))
                {
                    return true;
                }
            }
            return false;
        }

        protected bool FindPath(PathElement peTo, PathElement pePrev, PathElement pe, int nNextElementSource, ArrayList alTrainPath)
        {
            Debug.Assert(pe != null);
            TrainCourse item = new TrainCourse {
                Element = pe,
                ElementSource = nNextElementSource
            };
            alTrainPath.Add(item);
            if (peTo == pe)
            {
                return true;
            }
            pePrev = pe;
            PathInfo[] nextPathsThrough = pePrev.GetNextPathsThrough(nNextElementSource);
            if (nextPathsThrough != null)
            {
                foreach (PathInfo info in nextPathsThrough)
                {
                    if (info.ToElement != null)
                    {
                        int count = alTrainPath.Count;
                        if (this.FindPath(peTo, pePrev, info.ToElement, info.ToSource, alTrainPath))
                        {
                            return true;
                        }
                        while (alTrainPath.Count > count)
                        {
                            alTrainPath.RemoveAt(alTrainPath.Count - 1);
                        }
                    }
                }
            }
            return false;
        }

        public PathElement GetConnectedPath(int nMySource, out int nForeignSource)
        {
            nForeignSource = -1;
            foreach (Connection connection in base.m_Connections)
            {
                PathElement element = connection.IsConnectedPath(this, nMySource);
                if (element != null)
                {
                    nForeignSource = (connection.Element1 == this) ? connection.Source2 : connection.Source1;
                    return element;
                }
            }
            return null;
        }

        public virtual PathInfo[] GetNextPathsThrough(int nSourceFrom)
        {
            PathInfo info;
            info = new PathInfo {
                FromElement = this,
                FromSource = nSourceFrom,
                ToElement = this.GetNextPathThrough(false, ref info.FromSource, out info.ToSource)
            };
            return ((info.ToElement == null) ? null : new PathInfo[1]);
        }

        public abstract PathElement GetNextPathThrough(bool bCheckOccupied, ref int nSourceFrom, out int nForeignSource);
        public abstract int GetOppositeSource(int nSource);
        public virtual int[] GetOppositeSources(int nSource)
        {
            return new int[] { this.GetOppositeSource(nSource) };
        }

        public abstract void GetPathDirection(int nSource, out Point pFrom, out Point pTo);
        public abstract ArrayList GetPathNeighbors();
        public abstract int[] GetSources();
        public override string GetToolTipText(bool bDetailed, Point pt)
        {
            if (bDetailed)
            {
                int connectionSourceID = this.GetConnectionSourceID(pt);
                string str = (connectionSourceID > 0) ? string.Format(" ({0})", this.GetConnectionSourceName(connectionSourceID)) : string.Empty;
                string format = "{0} {1}{2}\n{3}/{4}, {5} m\n{6} sec on {7} KmH{8}";
                return string.Format(format, new object[] { this.GetName(), base.ID, str, base.Address, base.Segment, base.m_nLength, TimeSpan.FromHours((((double) base.m_nLength) / 1000.0) / ((double) base.AverageSpeed)).TotalSeconds, base.AverageSpeed, (base.StationArea.Length > 0) ? ("\n" + base.StationArea) : "" });
            }
            return base.GetToolTipText(bDetailed, pt);
        }

        public virtual Point GetTrainPoint()
        {
            return base.Center;
        }

        public virtual Point GetTrainPoint(ArrayList alTrains, TrainBase t)
        {
            int index = -1;
            if (alTrains.Count > 1)
            {
                index = alTrains.IndexOf(t);
            }
            Point trainPoint = this.GetTrainPoint();
            PathElement element = this;
            Point point2 = new Point();
            if (t.HeadElementArrivedSource != -1)
            {
                int[] sources = element.GetSources();
                Debug.Assert(sources.Length == 2);
                Point connectionSourcePoint = element.GetConnectionSourcePoint(sources[0]);
                Point point4 = element.GetConnectionSourcePoint(sources[1]);
                if (sources.Length != 2)
                {
                    return trainPoint;
                }
                double introduced15 = Math.Pow(connectionSourcePoint.get_X() - point4.get_X(), 2.0);
                double num2 = Math.Sqrt(introduced15 + Math.Pow(connectionSourcePoint.get_Y() - point4.get_Y(), 2.0));
                double num3 = (num2 * t.HeadOffset) / ((element.Length == 0) ? ((double) 1) : ((double) element.Length));
                double num4 = 1.0;
                double num5 = 0.0;
                if (!(connectionSourcePoint.get_Y() == point4.get_Y()))
                {
                    num4 = Math.Abs((double) (point4.get_X() - connectionSourcePoint.get_X())) / num2;
                    num5 = Math.Abs((double) (point4.get_Y() - connectionSourcePoint.get_Y())) / num2;
                }
                double num6 = num3 * num4;
                double num7 = num3 * num5;
                if (t.HeadElementArrivedSource == sources[0])
                {
                    if (connectionSourcePoint.get_X() <= point4.get_X())
                    {
                        connectionSourcePoint.set_X(connectionSourcePoint.get_X() + num6);
                    }
                    else
                    {
                        connectionSourcePoint.set_X(connectionSourcePoint.get_X() - num6);
                    }
                    if (connectionSourcePoint.get_Y() <= point4.get_Y())
                    {
                        connectionSourcePoint.set_Y(connectionSourcePoint.get_Y() + num7);
                    }
                    else
                    {
                        connectionSourcePoint.set_Y(connectionSourcePoint.get_Y() - num7);
                    }
                    point2 = connectionSourcePoint;
                }
                else
                {
                    if (point4.get_X() <= connectionSourcePoint.get_X())
                    {
                        point4.set_X(point4.get_X() + num6);
                    }
                    else
                    {
                        point4.set_X(point4.get_X() - num6);
                    }
                    if (point4.get_Y() <= connectionSourcePoint.get_Y())
                    {
                        point4.set_Y(point4.get_Y() + num7);
                    }
                    else
                    {
                        point4.set_Y(point4.get_Y() - num7);
                    }
                    point2 = point4;
                }
                trainPoint = point2;
                if (index != -1)
                {
                    if (connectionSourcePoint.get_Y() == point4.get_Y())
                    {
                        trainPoint.set_Y(trainPoint.get_Y() - 6.0);
                        trainPoint.set_Y(trainPoint.get_Y() + (13 * index));
                    }
                    else
                    {
                        trainPoint.set_X(trainPoint.get_X() - 6.0);
                        trainPoint.set_X(trainPoint.get_X() + (13 * index));
                    }
                }
            }
            return trainPoint;
        }

        public abstract bool HasTrain(TrainBase t);
        public abstract bool HasTrains();
        public override void Invalidate()
        {
            this.RemoveTrains();
            base.Invalidate();
        }

        public virtual bool IsFree()
        {
            return !this.IsOccupiedB();
        }

        public virtual bool IsLocked()
        {
            return true;
        }

        public ArrayList IsOccupied()
        {
            return this.IsOccupied(false);
        }

        public abstract ArrayList IsOccupied(bool bStrict);
        public bool IsOccupiedB()
        {
            return this.IsOccupiedB(false);
        }

        public bool IsOccupiedB(bool bStrict)
        {
            return (this.IsOccupied(bStrict).Count != 0);
        }

        public bool IsOccupiedWith(PathElement pe, bool bCheckMySourceOccupation)
        {
            foreach (Connection connection in base.m_Connections)
            {
                int num;
                int num2;
                Element element;
                if (connection.GetConnectedElement(this, out num2, out element, out num) && (pe == element))
                {
                    if (!pe.IsSourceFree(num))
                    {
                        if (bCheckMySourceOccupation)
                        {
                            return !this.IsSourceFree(num2);
                        }
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        protected virtual bool IsPathBlack(PathElementPassive.PathState ps)
        {
            return (!this.IsPathOccupied(ps) && !this.IsPathSelected(ps));
        }

        protected virtual bool IsPathOccupied(PathElementPassive.PathState ps)
        {
            return ((ps == PathElementPassive.PathState.Red) || (ps == PathElementPassive.PathState.Invalid));
        }

        protected virtual bool IsPathSelected(PathElementPassive.PathState ps)
        {
            return (ps == PathElementPassive.PathState.White);
        }

        public abstract ArrayList IsSelected();
        public bool IsSelectedB()
        {
            return (this.IsSelected().Count != 0);
        }

        public virtual bool IsSourceFree(int Source)
        {
            return this.IsFree();
        }

        public bool IsSourceLightOpen(int nSource)
        {
            foreach (Connection connection in base.m_Connections)
            {
                Light light = (Light) connection.IsConnectedTo(this, nSource, typeof(Light));
                if (light != null)
                {
                    return light.IsOpen();
                }
            }
            return true;
        }

        public virtual bool IsSourceOpen(int nSource)
        {
            return (nSource != -1);
        }

        public abstract void RemoveTrain(TrainBase t);
        public abstract void RemoveTrains();
        public abstract ArrayList Trains();

        [XmlIgnore]
        public bool Defect
        {
            get
            {
                return this.m_bDefect;
            }
            set
            {
                this.m_bDefect = value;
            }
        }
    }
}

