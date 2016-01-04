namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    [Serializable]
    public class PointSimple : PointSwitchBase
    {
        protected bool m_bStraightMinus;
        protected bool m_bStraightPlus;
        private PathElementPassive.PathState m_State;
        private byte Red;
        private byte White;

        public PointSimple()
        {
            this.m_bStraightPlus = false;
            this.m_bStraightMinus = true;
            this.m_State = PathElementPassive.PathState.Undefined;
            this.White = 0;
            this.Red = 0;
            this.Init();
        }

        public PointSimple(string strID) : base(strID)
        {
            this.m_bStraightPlus = false;
            this.m_bStraightMinus = true;
            this.m_State = PathElementPassive.PathState.Undefined;
            this.White = 0;
            this.Red = 0;
            this.Init();
        }

        public Point CalcNonStraightCenter(Point pTo)
        {
            Point point = new Point();
            point.set_X(base.PointCore.get_X() + (Math.Sign((double) (pTo.get_X() - base.PointCore.get_X())) * 6));
            point.set_Y(base.PointCore.get_Y() + (Math.Sign((double) (pTo.get_Y() - base.PointCore.get_Y())) * 6));
            return point;
        }

        public override void Draw(Grid g)
        {
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("SIMPLEPOINT");
        }

        public override PathElement GetNextPathThrough(bool bCheckOccupied, ref int nSourceFrom, out int nForeignSource)
        {
            nForeignSource = -1;
            if (!bCheckOccupied || base.IsOccupiedB())
            {
                int num;
                switch (((PointSwitchBase.ConnectionSource) nSourceFrom))
                {
                    case PointSwitchBase.ConnectionSource.Plus:
                    case PointSwitchBase.ConnectionSource.Minus:
                        nSourceFrom = num = 1;
                        return base.GetConnectedPath(num, out nForeignSource);
                }
                if (this.m_bStraightPlus)
                {
                    nSourceFrom = num = 2;
                    return base.GetConnectedPath(num, out nForeignSource);
                }
                if (this.m_bStraightMinus)
                {
                    nSourceFrom = num = 3;
                    return base.GetConnectedPath(num, out nForeignSource);
                }
                Debug.Assert(false);
            }
            return null;
        }

        public override int GetOppositeSource(int nSource)
        {
            switch (((PointSwitchBase.ConnectionSource) nSource))
            {
                case PointSwitchBase.ConnectionSource.Plus:
                case PointSwitchBase.ConnectionSource.Minus:
                    return 1;
            }
            if (this.m_bStraightPlus)
            {
                return 2;
            }
            if (this.m_bStraightMinus)
            {
                return 3;
            }
            Debug.Assert(false);
            return -1;
        }

        public override Form GetPropertiesForm()
        {
            return null;
        }

        private void Init()
        {
            base.PointPeak = new Point(100.0, 50.0);
            base.PointCore = new Point(150.0, 50.0);
            base.PointPlus = new Point(200.0, 50.0);
            base.PointMinus = new Point(200.0, 100.0);
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
                list.Add(3);
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
                list.Add(3);
            }
            return list;
        }

        public override void Reflect(int width, bool h)
        {
            base.PointCore = h ? Element.GetPointRotatedByY(width, base.PointCore) : Element.GetPointRotatedByX(width, base.PointCore);
            base.PointMinus = h ? Element.GetPointRotatedByY(width, base.PointMinus) : Element.GetPointRotatedByX(width, base.PointMinus);
            base.PointPeak = h ? Element.GetPointRotatedByY(width, base.PointPeak) : Element.GetPointRotatedByX(width, base.PointPeak);
            base.PointPlus = h ? Element.GetPointRotatedByY(width, base.PointPlus) : Element.GetPointRotatedByX(width, base.PointPlus);
        }

        public override bool SetSegmentState(PathElementPassive.PathState st)
        {
            this.m_State = st;
            return true;
        }

        public override bool SetState(string strData, bool bState)
        {
            byte num2;
            string[] strArray = strData.Split(new char[] { ' ' });
            if (strArray.Length >= 3)
            {
                Debug.Assert(strArray[1] == base.Address);
                string str = strArray[2];
                byte num = bState ? ((byte) 1) : ((byte) 0);
                if (str.IndexOf("ws") == 0)
                {
                    this.White = num;
                    goto Label_009D;
                }
                if (str.IndexOf("rt") == 0)
                {
                    this.Red = num;
                    goto Label_009D;
                }
            }
            return false;
        Label_009D:
            num2 = (byte) (this.White + this.Red);
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

        public bool StraightMinus
        {
            get
            {
                return this.m_bStraightMinus;
            }
            set
            {
                this.m_bStraightMinus = value;
            }
        }

        public bool StraightPlus
        {
            get
            {
                return this.m_bStraightPlus;
            }
            set
            {
                this.m_bStraightPlus = value;
            }
        }
    }
}

