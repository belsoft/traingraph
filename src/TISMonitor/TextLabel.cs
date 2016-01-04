namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [Serializable]
    public class TextLabel : Element
    {
        public const int HEIGHT = 30;
        protected static Font m_fFont;
        private int m_FontStyle;
        private int m_nEscapement;
        private float m_nFontSize;
        private int m_nForeColor;
        private Point m_PointBase;
        private string m_strFontName;
        private string m_strText;
        public string TextBackColor;
        public const int WIDTH = 200;

        public TextLabel()
        {
            this.m_strText = "";
            this.m_strFontName = "Arial";
            this.m_nFontSize = 9f;
            this.m_nEscapement = 0;
            this.m_nForeColor = -5658199;
            this.m_FontStyle = 0;
            this.Init();
        }

        public TextLabel(string strID) : base(strID)
        {
            this.m_strText = "";
            this.m_strFontName = "Arial";
            this.m_nFontSize = 9f;
            this.m_nEscapement = 0;
            this.m_nForeColor = -5658199;
            this.m_FontStyle = 0;
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
            Debug.Assert(e is TextLabel);
            if (e is TextLabel)
            {
                this.PointBase = (e as TextLabel).PointBase;
            }
        }

        public override void Draw(Grid g)
        {
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("TextLabel");
        }

        private void Init()
        {
        }

        public override void Move(Size size)
        {
            this.m_PointBase.set_X(this.m_PointBase.get_X() + size.get_Width());
            this.m_PointBase.set_Y(this.m_PointBase.get_Y() + size.get_Height());
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
            double num = this.m_PointBase.get_X();
            double num2 = this.m_PointBase.get_Y();
            double num3 = (this.m_PointBase.get_X() - 200.0) - 20.0;
            double num4 = this.m_PointBase.get_Y() - 15.0;
            this.Bounds.X = (int) num;
            this.Bounds.Y = (int) num2;
            this.Bounds.Size = new Size(num - num, num2 - num4);
            base.Center = this.m_PointBase;
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

        public string FontName
        {
            get
            {
                return this.m_strFontName;
            }
            set
            {
                this.m_strFontName = value;
            }
        }

        public float FontSize
        {
            get
            {
                return this.m_nFontSize;
            }
            set
            {
                this.m_nFontSize = value;
            }
        }

        public int FontStyle
        {
            get
            {
                return this.m_FontStyle;
            }
            set
            {
                this.m_FontStyle = value;
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

        public string Text
        {
            get
            {
                return this.m_strText;
            }
            set
            {
                this.m_strText = value;
            }
        }

        public string TextForeColor { get; set; }
    }
}

