namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    [Serializable]
    public class PositionMarker : Element
    {
        private bool m_bVisible;
        protected static Font m_fFont;
        private int m_nColor;
        private int m_nStyle;
        private Point m_PointBase;

        public PositionMarker()
        {
            this.m_nStyle = 0;
            this.m_nColor = -5658199;
            this.m_bVisible = true;
            this.Init();
        }

        public PositionMarker(string strID) : base(strID)
        {
            this.m_nStyle = 0;
            this.m_nColor = -5658199;
            this.m_bVisible = true;
            this.Init();
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
            Debug.Assert(e is PositionMarker);
            if (e is PositionMarker)
            {
                this.PointBase = (e as PositionMarker).PointBase;
            }
        }

        public override void Draw(Grid g)
        {
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("PositionMarker");
        }

        public override Form GetPropertiesForm()
        {
            return null;
        }

        private void Init()
        {
        }

        public override void Move(Size size)
        {
            this.UpdateBounds();
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

        public int Color
        {
            get
            {
                return this.m_nColor;
            }
            set
            {
                this.m_nColor = value;
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

        public int Style
        {
            get
            {
                return this.m_nStyle;
            }
            set
            {
                this.m_nStyle = value;
            }
        }

        public bool Visible
        {
            get
            {
                return this.m_bVisible;
            }
            set
            {
                this.m_bVisible = value;
            }
        }
    }
}

