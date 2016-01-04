namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    [Serializable]
    public class Light : StateElement
    {
        private bool Green1;
        private bool Green1Defined;
        private bool Green2;
        private bool Green2Defined;
        private const int HEIGHT = 0x24;
        private const int LEG = 7;
        private Point m_PointLeg;
        private bool Red;
        private bool RedDefined;
        private bool White;
        private bool WhiteDefined;
        private const int WIDTH = 7;
        private bool Yellow;
        private bool YellowDefined;

        public Light()
        {
            this.White = false;
            this.Green1 = false;
            this.Green2 = false;
            this.Red = false;
            this.Yellow = false;
            this.WhiteDefined = false;
            this.Green1Defined = false;
            this.Green2Defined = false;
            this.RedDefined = false;
            this.YellowDefined = false;
            this.Init();
        }

        public Light(string strID) : base(strID)
        {
            this.White = false;
            this.Green1 = false;
            this.Green2 = false;
            this.Red = false;
            this.Yellow = false;
            this.WhiteDefined = false;
            this.Green1Defined = false;
            this.Green2Defined = false;
            this.RedDefined = false;
            this.YellowDefined = false;
            this.Init();
        }

        public override int CanConnect(Point p, Element e, int nSource, out Point pDest)
        {
            pDest = this.m_PointLeg;
            return -1;
        }

        public override OnElementMoveDelegate CanMove(Point p)
        {
            if (Element.IsPtCaptured(p, this.PointLeg))
            {
                return new OnElementMoveDelegate(this.OnMove);
            }
            return null;
        }

        public override void CopyLocationInfoFrom(Element e)
        {
            Debug.Assert(e is Light);
            if (e is Light)
            {
                this.PointLeg = (e as Light).PointLeg;
            }
        }

        public override void Draw(Grid g)
        {
        }

        public override int GetConnectionSourceID(Point p)
        {
            if (Element.IsPtCaptured(p, this.PointLeg))
            {
                return 0;
            }
            return -1;
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("LIGHT");
        }

        public static float GetRotationAngle(Point p1, Point p2)
        {
            return 0f;
        }

        private void Init()
        {
            this.PointLeg = new Point(100.0, 100.0);
        }

        public bool IsOpen()
        {
            return (((this.Green1 || this.Green2) || (this.Yellow || this.White)) && base.Defined);
        }

        public override void Move(Size size)
        {
        }

        private Rectangle OnMove(Point pt)
        {
            this.PointLeg = pt;
            this.UpdateBounds();
            return base.Bounds;
        }

        public override void Reflect(int width, bool h)
        {
            base.Center = h ? Element.GetPointRotatedByY(width, base.Center) : Element.GetPointRotatedByX(width, base.Center);
        }

        public override void SetLocation(Rectangle r)
        {
            this.PointLeg = new Point((double) r.X, (double) r.Y);
        }

        public override bool SetState(string strData, bool bState)
        {
            string[] strArray = strData.Split(new char[] { ' ' });
            if ((strArray.Length == 3) || (strArray.Length == 4))
            {
                if (strArray[0].ToUpper() != "HAUPTSIGNAL")
                {
                    return false;
                }
                Debug.Assert(strArray[1] == base.ID);
                string str = strArray[2].ToUpper();
                if (str.IndexOf("SSP") != -1)
                {
                    this.White = bState;
                    this.WhiteDefined = true;
                    goto Label_015F;
                }
                if (str.IndexOf("RT") != -1)
                {
                    this.Red = bState;
                    this.RedDefined = true;
                    goto Label_015F;
                }
                if (str.IndexOf("GN1") != -1)
                {
                    this.Green1 = bState;
                    this.Green1Defined = true;
                    goto Label_015F;
                }
                if (str.IndexOf("GN2") != -1)
                {
                    this.Green2 = bState;
                    this.Green2Defined = true;
                    goto Label_015F;
                }
                if (str.IndexOf("GE") != -1)
                {
                    this.Yellow = bState;
                    this.YellowDefined = true;
                    goto Label_015F;
                }
                if (str.IndexOf("GN") != -1)
                {
                    this.Green1 = bState;
                    this.Green1Defined = true;
                    goto Label_015F;
                }
            }
            return false;
        Label_015F:
            base.Defined = true;
            base.Valid = true;
            return base.SetState(strData, bState);
        }

        public override void UpdateBounds()
        {
        }

        public Point PointLeg
        {
            get
            {
                return this.m_PointLeg;
            }
            set
            {
                if (this.m_PointLeg != value)
                {
                    this.m_PointLeg = value;
                    this.UpdateBounds();
                }
            }
        }
    }
}

