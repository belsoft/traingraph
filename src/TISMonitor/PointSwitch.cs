namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    [Serializable]
    public class PointSwitch : PointSwitchBase
    {
        private bool LockedDefined;
        private bool m_bLimitedSignalSet;
        private bool m_StateLocked;
        private byte[] m_StateMinus;
        private bool m_StateNotControlled;
        private byte[] m_StatePeak;
        private byte[] m_StatePlus;
        private byte[] m_StatePreMinus;
        private byte[] m_StatePrePeak;
        private byte[] m_StatePrePlus;
        private PathElementPassive.PathState Minus;
        private bool NotControlledDefined;
        private PathElementPassive.PathState Peak;
        private PathElementPassive.PathState Plus;
        private PathElementPassive.PathState PreMinus;
        private PathElementPassive.PathState PrePeak;
        private PathElementPassive.PathState PrePlus;

        public PointSwitch()
        {
            this.Plus = PathElementPassive.PathState.Undefined;
            this.PrePlus = PathElementPassive.PathState.Undefined;
            this.Minus = PathElementPassive.PathState.Undefined;
            this.PreMinus = PathElementPassive.PathState.Undefined;
            this.Peak = PathElementPassive.PathState.Undefined;
            this.PrePeak = PathElementPassive.PathState.Undefined;
            this.LockedDefined = false;
            this.m_StateLocked = false;
            this.NotControlledDefined = false;
            this.m_StateNotControlled = false;
            this.m_bLimitedSignalSet = false;
            this.m_StatePlus = new byte[2];
            this.m_StatePrePlus = new byte[2];
            this.m_StateMinus = new byte[2];
            this.m_StatePreMinus = new byte[2];
            this.m_StatePeak = new byte[2];
            this.m_StatePrePeak = new byte[2];
            this.Init();
        }

        public PointSwitch(string strID) : base(strID)
        {
            this.Plus = PathElementPassive.PathState.Undefined;
            this.PrePlus = PathElementPassive.PathState.Undefined;
            this.Minus = PathElementPassive.PathState.Undefined;
            this.PreMinus = PathElementPassive.PathState.Undefined;
            this.Peak = PathElementPassive.PathState.Undefined;
            this.PrePeak = PathElementPassive.PathState.Undefined;
            this.LockedDefined = false;
            this.m_StateLocked = false;
            this.NotControlledDefined = false;
            this.m_StateNotControlled = false;
            this.m_bLimitedSignalSet = false;
            this.m_StatePlus = new byte[2];
            this.m_StatePrePlus = new byte[2];
            this.m_StateMinus = new byte[2];
            this.m_StatePreMinus = new byte[2];
            this.m_StatePeak = new byte[2];
            this.m_StatePrePeak = new byte[2];
            this.Init();
        }

        public override void Draw(Grid g)
        {
        }

        private Color GetColor4State(PathElementPassive.PathState p)
        {
            return base.ForeColor;
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("POINT");
        }

        public override PathInfo[] GetNextPathsThrough(int nSourceFrom)
        {
            PathInfo info;
            switch (((PointSwitchBase.ConnectionSource) nSourceFrom))
            {
                case PointSwitchBase.ConnectionSource.Plus:
                case PointSwitchBase.ConnectionSource.Minus:
                    return base.GetNextPathsThrough(nSourceFrom);
            }
            info = new PathInfo {
                FromElement = this,
                FromSource = nSourceFrom,
                ToElement = this.GetNextPathThrough(false, ref info.FromSource, out info.ToSource)
            };
            PathInfo info2 = new PathInfo {
                FromElement = this,
                FromSource = nSourceFrom
            };
            if (info.ToElement != null)
            {
                info2.ToElement = base.GetConnectedPath((info.FromSource == 2) ? 3 : 2, out info2.ToSource);
            }
            else if (this.IsPathSelected(this.Plus))
            {
                info.ToElement = base.GetConnectedPath(2, out info.ToSource);
                info2.ToElement = base.GetConnectedPath(3, out info2.ToSource);
            }
            else
            {
                info2.ToElement = base.GetConnectedPath(3, out info2.ToSource);
                info.ToElement = base.GetConnectedPath(2, out info.ToSource);
            }
            return new PathInfo[] { info, info2 };
        }

        public override PathElement GetNextPathThrough(bool bCheckOccupied, ref int nSourceFrom, out int nForeignSource)
        {
            ArrayList list;
            int num;
            nForeignSource = -1;
            PointSwitchBase.ConnectionSource source = (PointSwitchBase.ConnectionSource) nSourceFrom;
            if (bCheckOccupied)
            {
                list = base.IsOccupied();
                if (list.Count != 0)
                {
                    if (nSourceFrom == ((int) list[0]))
                    {
                        nSourceFrom = num = (int) list[1];
                        return base.GetConnectedPath(num, out nForeignSource);
                    }
                    if (nSourceFrom == ((int) list[1]))
                    {
                        nSourceFrom = num = (int) list[0];
                        return base.GetConnectedPath(num, out nForeignSource);
                    }
                }
                return null;
            }
            switch (source)
            {
                case PointSwitchBase.ConnectionSource.Plus:
                case PointSwitchBase.ConnectionSource.Minus:
                    nSourceFrom = num = 1;
                    return base.GetConnectedPath(num, out nForeignSource);
            }
            list = this.IsSelected();
            if (list.Count == 0)
            {
                list = base.IsOccupied();
                if (list.Count == 0)
                {
                    return null;
                }
            }
            nSourceFrom = num = (int) list[1];
            return base.GetConnectedPath(num, out nForeignSource);
        }

        public override int GetOppositeSource(int nSource)
        {
            PointSwitchBase.ConnectionSource source = (PointSwitchBase.ConnectionSource) nSource;
            ArrayList list = base.IsOccupied();
            if (list.Count == 0)
            {
                list = this.IsSelected();
                if (list.Count == 0)
                {
                    return -1;
                }
            }
            switch (source)
            {
                case PointSwitchBase.ConnectionSource.Plus:
                case PointSwitchBase.ConnectionSource.Minus:
                    return 1;
            }
            return (int) list[1];
        }

        public override int[] GetOppositeSources(int nSource)
        {
            PointSwitchBase.ConnectionSource source = (PointSwitchBase.ConnectionSource) nSource;
            int oppositeSource = this.GetOppositeSource(nSource);
            if (oppositeSource == -1)
            {
                if (source == PointSwitchBase.ConnectionSource.Peak)
                {
                    return new int[] { 2, 3 };
                }
                return new int[] { 1 };
            }
            switch (((PointSwitchBase.ConnectionSource) oppositeSource))
            {
                case PointSwitchBase.ConnectionSource.Peak:
                    return new int[] { 1 };

                case PointSwitchBase.ConnectionSource.Plus:
                    return new int[] { oppositeSource, 3 };
            }
            return new int[] { oppositeSource, 2 };
        }

        public override Form GetPropertiesForm()
        {
            return null;
        }

        private void Init()
        {
            base.m_nAverageSpeed = 40;
            base.PointPeak = new Point(100.0, 50.0);
            base.PointCore = new Point(150.0, 50.0);
            base.PointPlus = new Point(200.0, 50.0);
            base.PointMinus = new Point(200.0, 100.0);
        }

        public override void Invalidate()
        {
            this.Plus = this.PrePlus = this.Minus = this.PreMinus = this.Peak = this.PrePeak = PathElementPassive.PathState.Undefined;
            base.Invalidate();
        }

        public override bool IsFree()
        {
            return ((this.IsSourceFree(1) && this.IsSourceFree(2)) && this.IsSourceFree(3));
        }

        public override bool IsLocked()
        {
            return (this.LockedDefined && this.m_StateLocked);
        }

        public override ArrayList IsOccupied(bool bStrict)
        {
            ArrayList list = new ArrayList();
            if (!bStrict)
            {
                if (this.IsPathOccupied(this.Peak) || this.IsPathOccupied(this.PrePeak))
                {
                    if (((this.IsPathOccupied(this.Plus) || this.IsPathOccupied(this.PrePlus)) || this.IsPathSelected(this.Plus)) || this.IsPathSelected(this.PrePlus))
                    {
                        list.Add(1);
                        list.Add(2);
                        return list;
                    }
                    if (((this.IsPathOccupied(this.Minus) || this.IsPathOccupied(this.PreMinus)) || this.IsPathSelected(this.Minus)) || this.IsPathSelected(this.PreMinus))
                    {
                        list.Add(1);
                        list.Add(3);
                        return list;
                    }
                    return list;
                }
                if (this.IsPathSelected(this.Peak) || this.IsPathSelected(this.PrePeak))
                {
                    if (this.IsPathOccupied(this.Plus) || this.IsPathOccupied(this.PrePlus))
                    {
                        list.Add(1);
                        list.Add(2);
                        return list;
                    }
                    if (this.IsPathOccupied(this.Minus) || this.IsPathOccupied(this.PreMinus))
                    {
                        list.Add(1);
                        list.Add(3);
                        return list;
                    }
                }
                return list;
            }
            if (((this.IsPathOccupied(this.Peak) && this.IsPathOccupied(this.PrePeak)) && this.LockedDefined) && this.m_StateLocked)
            {
                if (this.IsPathOccupied(this.Plus) && this.IsPathOccupied(this.PrePlus))
                {
                    list.Add(1);
                    list.Add(2);
                }
                else if (this.IsPathOccupied(this.Minus) && this.IsPathOccupied(this.PreMinus))
                {
                    list.Add(1);
                    list.Add(3);
                }
            }
            return list;
        }

        public override ArrayList IsSelected()
        {
            ArrayList list = new ArrayList();
            int num = 0;
            int num2 = 0;
            if (this.LockedDefined && this.m_StateLocked)
            {
                num2 += this.IsPathSelected(this.Peak) ? 1 : 0;
                num2 += this.IsPathSelected(this.PrePeak) ? 1 : 0;
                Debug.Assert((num2 == 0) || (num2 == 2));
                if (num2 >= 1)
                {
                    num = num2;
                    num += this.IsPathSelected(this.Plus) ? 1 : 0;
                    num += this.IsPathSelected(this.PrePlus) ? 1 : 0;
                    if (num >= 3)
                    {
                        list.Add(1);
                        list.Add(2);
                    }
                    else
                    {
                        num = num2;
                        num += this.IsPathSelected(this.Minus) ? 1 : 0;
                        num += this.IsPathSelected(this.PreMinus) ? 1 : 0;
                        if (num >= 3)
                        {
                            list.Add(1);
                            list.Add(3);
                        }
                    }
                }
            }
            return list;
        }

        public override bool IsSourceFree(int nSource)
        {
            switch (((PointSwitchBase.ConnectionSource) nSource))
            {
                case PointSwitchBase.ConnectionSource.Peak:
                    return (!this.IsPathOccupied(this.Peak) && !this.IsPathOccupied(this.PrePeak));

                case PointSwitchBase.ConnectionSource.Plus:
                    return (!this.IsPathOccupied(this.Plus) && !this.IsPathOccupied(this.PrePlus));

                case PointSwitchBase.ConnectionSource.Minus:
                    return (!this.IsPathOccupied(this.Minus) && !this.IsPathOccupied(this.PreMinus));
            }
            Debug.Assert(false);
            return true;
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
            this.Peak = this.PrePeak = st;
            return true;
        }

        private void SetSmartStatePeak()
        {
            this.PrePeak = this.Peak = PathElementPassive.PathState.Black;
            if ((this.IsPathSelected(this.Plus) || this.IsPathOccupied(this.Plus)) && this.IsPathBlack(this.Minus))
            {
                this.PrePeak = this.Peak = this.Plus;
            }
            else if ((this.IsPathSelected(this.Minus) || this.IsPathOccupied(this.Minus)) && this.IsPathBlack(this.Plus))
            {
                this.PrePeak = this.Peak = this.Minus;
            }
            ArrayList list = (ArrayList) base.m_Layout.m_htSegment2PathElementPassive[base.Segment];
            Debug.Assert(list != null);
            if (list != null)
            {
                foreach (PathElementPassive passive in list)
                {
                    if (!(!(passive.ID != base.ID) || (passive is PointSwitch)))
                    {
                        passive.SetSegmentState(this.Peak);
                    }
                }
            }
            this.m_StateLocked = !this.IsPathBlack(this.Peak);
            this.LockedDefined = true;
        }

        public override bool SetState(string strData, bool bState)
        {
            int num;
            byte num2;
            string[] strArray = strData.Split(new char[] { ' ' });
            if (strArray.Length >= 3)
            {
                if (!strArray[0].ToUpper().StartsWith("WEI"))
                {
                    return false;
                }
                Debug.Assert(strArray[1] == base.ID);
                string str = strArray[2];
                if (str.IndexOf("verschl") != -1)
                {
                    this.m_StateLocked = bState;
                    this.LockedDefined = true;
                    return this.UpdateState(strData, bState);
                }
                if (str.IndexOf("gesperrt") != -1)
                {
                    this.m_StateNotControlled = bState;
                    this.NotControlledDefined = true;
                    return this.UpdateState(strData, bState);
                }
                if (str.IndexOf("rt") != -1)
                {
                    num = 1;
                    goto Label_0108;
                }
                if (str.IndexOf("ws") != -1)
                {
                    num = 0;
                    goto Label_0108;
                }
            }
            return false;
        Label_0108:
            num2 = bState ? ((byte) 1) : ((byte) 0);
            if (strData.IndexOf("+") != -1)
            {
                if (strData.IndexOf("vor") != -1)
                {
                    this.SetState(num2, num, ref this.m_StatePrePlus, ref this.PrePlus);
                }
                else
                {
                    this.SetState(num2, num, ref this.m_StatePlus, ref this.Plus);
                    if (this.m_bLimitedSignalSet)
                    {
                        this.SetState(num2, num, ref this.m_StatePrePlus, ref this.PrePlus);
                        this.SetSmartStatePeak();
                    }
                }
            }
            else if (strData.IndexOf("-") != -1)
            {
                if (strData.IndexOf("vor") != -1)
                {
                    this.SetState(num2, num, ref this.m_StatePreMinus, ref this.PreMinus);
                }
                else
                {
                    this.SetState(num2, num, ref this.m_StateMinus, ref this.Minus);
                    if (this.m_bLimitedSignalSet)
                    {
                        this.SetState(num2, num, ref this.m_StatePreMinus, ref this.PreMinus);
                        this.SetSmartStatePeak();
                    }
                }
            }
            else if (strData.IndexOf("Sp.") != -1)
            {
                this.SetState(num2, num, ref this.m_StatePrePeak, ref this.PrePeak);
                this.SetState(num2, num, ref this.m_StatePeak, ref this.Peak);
                ArrayList list = (ArrayList) base.m_Layout.m_htSegment2PathElementPassive[base.Segment];
                Debug.Assert(list != null);
                if (list != null)
                {
                    foreach (PathElementPassive passive in list)
                    {
                        if (!(!(passive.ID != base.ID) || (passive is PointSwitch)))
                        {
                            passive.SetSegmentState(this.Peak);
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return this.UpdateState(strData, bState);
        }

        private void SetState(byte nState, int nIndex, ref byte[] state, ref PathElementPassive.PathState pa)
        {
            state[nIndex] = nState;
            byte num = (byte) (state[0] + state[1]);
            if (num == 0)
            {
                pa = PathElementPassive.PathState.Black;
            }
            else if (num == 1)
            {
                pa = (state[0] == 0) ? PathElementPassive.PathState.Red : PathElementPassive.PathState.White;
            }
            else
            {
                pa = PathElementPassive.PathState.Invalid;
            }
        }

        private bool UpdateState(string strData, bool bState)
        {
            base.Valid = ((((this.Plus != PathElementPassive.PathState.Invalid) && (this.PrePlus != PathElementPassive.PathState.Invalid)) && ((this.Minus != PathElementPassive.PathState.Invalid) && (this.PreMinus != PathElementPassive.PathState.Invalid))) && (this.Peak != PathElementPassive.PathState.Invalid)) && (this.PrePeak != PathElementPassive.PathState.Invalid);
            base.Defined = ((((this.Plus != PathElementPassive.PathState.Undefined) && (this.PrePlus != PathElementPassive.PathState.Undefined)) && ((this.Minus != PathElementPassive.PathState.Undefined) && (this.PreMinus != PathElementPassive.PathState.Undefined))) && ((this.Peak != PathElementPassive.PathState.Undefined) && (this.PrePeak != PathElementPassive.PathState.Undefined))) && this.LockedDefined;
            if ((((base.Valid && (this.Plus != PathElementPassive.PathState.Red)) && ((this.PrePlus != PathElementPassive.PathState.Red) && (this.Minus != PathElementPassive.PathState.Red))) && ((this.PreMinus != PathElementPassive.PathState.Red) && (this.Peak != PathElementPassive.PathState.Red))) && (this.PrePeak != PathElementPassive.PathState.Red))
            {
                base.Defect = false;
            }
            return base.SetState(strData, bState);
        }

        public bool LimitedSignalSet
        {
            get
            {
                return this.m_bLimitedSignalSet;
            }
            set
            {
                this.m_bLimitedSignalSet = value;
            }
        }
    }
}

