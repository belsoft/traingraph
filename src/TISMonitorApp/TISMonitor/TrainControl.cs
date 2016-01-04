namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class TrainControl : UserControl
    {
        private bool _contentLoaded;
        internal Canvas Base;
        internal Canvas CanvasRoot;
        private const double CNT_HEIGHT = 24.0;
        private const double CNT_HEIGHT_SMALL = 12.0;
        private const string CNT_Tooltip_name = "Tooltip";
        internal Grid LayoutRoot;
        private Color m_BodyColorHi;
        private Color m_BodyColorLo;
        private int m_Direction;
        private string m_DisplayID;
        protected FontFamily m_fntTrainBigImpact;
        protected int m_fntTrainBigImpactSize;
        protected FontWeight m_fntTrainBigImpactStyle;
        protected FontFamily m_fntTrainBigNormal;
        protected int m_fntTrainBigNormalSize;
        protected FontWeight m_fntTrainBigNormalStyle;
        protected FontFamily m_fntTrainSmall;
        protected int m_fntTrainSmallSize;
        protected FontWeight m_fntTrainSmallStyle;
        private string m_ID;
        private bool m_Small;
        private string m_Tooltip;
        private Train m_train;
        private const double y0 = 0.0;

        public event TrainChangeNumberHandler OnTrainChangeNumber;

        public event TrainPropertiesHandler OnTrainProperties;

        public event TrainRouteHandler OnTrainRoute;

        public TrainControl()
        {
            this.m_ID = "";
            this.m_DisplayID = "";
            this.m_Tooltip = "";
            this.m_BodyColorHi = Colors.get_Blue();
            this.m_BodyColorLo = Colors.get_Blue();
            this.m_Direction = 0;
            this.m_Small = false;
            this.m_train = null;
            this.m_fntTrainSmall = new FontFamily("Arial");
            this.m_fntTrainBigImpact = new FontFamily("Impact");
            this.m_fntTrainBigNormal = new FontFamily("Arial");
            this.m_fntTrainSmallSize = 11;
            this.m_fntTrainBigImpactSize = 13;
            this.m_fntTrainBigNormalSize = 15;
            this.m_fntTrainSmallStyle = FontWeights.get_Bold();
            this.m_fntTrainBigImpactStyle = FontWeights.get_Normal();
            this.m_fntTrainBigNormalStyle = FontWeights.get_Bold();
            this.InitializeComponent();
        }

        public TrainControl(Train train)
        {
            this.m_ID = "";
            this.m_DisplayID = "";
            this.m_Tooltip = "";
            this.m_BodyColorHi = Colors.get_Blue();
            this.m_BodyColorLo = Colors.get_Blue();
            this.m_Direction = 0;
            this.m_Small = false;
            this.m_train = null;
            this.m_fntTrainSmall = new FontFamily("Arial");
            this.m_fntTrainBigImpact = new FontFamily("Impact");
            this.m_fntTrainBigNormal = new FontFamily("Arial");
            this.m_fntTrainSmallSize = 11;
            this.m_fntTrainBigImpactSize = 13;
            this.m_fntTrainBigNormalSize = 15;
            this.m_fntTrainSmallStyle = FontWeights.get_Bold();
            this.m_fntTrainBigImpactStyle = FontWeights.get_Normal();
            this.m_fntTrainBigNormalStyle = FontWeights.get_Bold();
            this.InitializeComponent();
            this.m_train = train;
        }

        private static Brush CreateHatchBrush(Color clrLo, Color clrHi)
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.set_MappingMode(1);
            brush.set_SpreadMethod(0);
            brush.set_StartPoint(new Point(0.0, 0.0));
            brush.set_EndPoint(new Point(0.0, 1.0));
            GradientStop stop = new GradientStop();
            stop.set_Color(clrLo);
            brush.get_GradientStops().Add(stop);
            stop = new GradientStop();
            stop.set_Color(clrHi);
            stop.set_Offset(1.0);
            brush.get_GradientStops().Add(stop);
            return brush;
        }

        public static TextBlock CreateTooltipControl(string t)
        {
            TextBlock block = new TextBlock();
            block.set_Text(t);
            block.set_FontFamily(new FontFamily("Courier New"));
            block.set_FontSize(12.0);
            return block;
        }

        public void Draw(bool bRotateText)
        {
            this.CanvasRoot.get_Children().Clear();
            base.set_Height(this.HEIGHT_ACTUAL);
            base.set_Width(this.WIDTH_ACTUAL);
            this.CanvasRoot.SetValue(FrameworkElement.WidthProperty, this.WIDTH_ACTUAL);
            this.CanvasRoot.SetValue(FrameworkElement.HeightProperty, this.HEIGHT_ACTUAL);
            base.SetValue(FrameworkElement.WidthProperty, this.WIDTH_ACTUAL);
            base.SetValue(FrameworkElement.HeightProperty, this.HEIGHT_ACTUAL);
            Brush brush = CreateHatchBrush(this.BodyColorHi, this.BodyColorLo);
            Brush brush2 = CreateHatchBrush(this.BodyColorLo, this.BodyColorHi);
            Rectangle rectangle = new Rectangle();
            rectangle.set_StrokeThickness(0.0);
            rectangle.set_Fill(brush);
            rectangle.SetValue(Canvas.LeftProperty, this.x0);
            rectangle.SetValue(Canvas.TopProperty, 0.0);
            rectangle.SetValue(FrameworkElement.HeightProperty, this.HEIGHT_ACTUAL / 2.0);
            rectangle.SetValue(FrameworkElement.WidthProperty, this.WIDTH_ACTUAL);
            Rectangle rectangle2 = new Rectangle();
            rectangle2.set_StrokeThickness(0.0);
            rectangle2.set_Fill(brush2);
            rectangle2.SetValue(Canvas.LeftProperty, this.x0);
            rectangle2.SetValue(Canvas.TopProperty, 0.0 + (this.HEIGHT_ACTUAL / 2.0));
            rectangle2.SetValue(FrameworkElement.HeightProperty, this.HEIGHT_ACTUAL / 2.0);
            rectangle2.SetValue(FrameworkElement.WidthProperty, this.WIDTH_ACTUAL);
            this.CanvasRoot.get_Children().Add(rectangle);
            this.CanvasRoot.get_Children().Add(rectangle2);
            if ((this.Direction == 1) || (this.Direction == -1))
            {
                Polygon polygon = new Polygon();
                polygon.set_Fill(brush);
                polygon.set_StrokeThickness(0.0);
                Polygon polygon2 = new Polygon();
                polygon2.set_Fill(brush2);
                polygon2.set_StrokeThickness(0.0);
                if (this.Direction == 1)
                {
                    polygon.get_Points().Add(new Point(this.x0 + this.WIDTH_ACTUAL, 0.0));
                    polygon.get_Points().Add(new Point(this.x0 + this.WIDTH_ACTUAL, 0.0 + (this.HEIGHT_ACTUAL / 2.0)));
                    polygon.get_Points().Add(new Point((this.x0 + this.WIDTH_ACTUAL) + this.NOSE_LEN, 0.0 + (this.HEIGHT_ACTUAL / 2.0)));
                    polygon2.get_Points().Add(new Point(this.x0 + this.WIDTH_ACTUAL, 0.0 + (this.HEIGHT_ACTUAL / 2.0)));
                    polygon2.get_Points().Add(new Point(this.x0 + this.WIDTH_ACTUAL, 0.0 + this.HEIGHT_ACTUAL));
                    polygon2.get_Points().Add(new Point((this.x0 + this.WIDTH_ACTUAL) + this.NOSE_LEN, 0.0 + (this.HEIGHT_ACTUAL / 2.0)));
                }
                else if (this.Direction == -1)
                {
                    polygon.get_Points().Add(new Point(this.x0, 0.0));
                    polygon.get_Points().Add(new Point(this.x0, 0.0 + (this.HEIGHT_ACTUAL / 2.0)));
                    polygon.get_Points().Add(new Point(0.0, 0.0 + (this.HEIGHT_ACTUAL / 2.0)));
                    polygon2.get_Points().Add(new Point(this.x0, 0.0 + this.HEIGHT_ACTUAL));
                    polygon2.get_Points().Add(new Point(0.0, 0.0 + (this.HEIGHT_ACTUAL / 2.0)));
                    polygon2.get_Points().Add(new Point(this.x0, 0.0 + (this.HEIGHT_ACTUAL / 2.0)));
                }
                this.CanvasRoot.get_Children().Add(polygon);
                this.CanvasRoot.get_Children().Add(polygon2);
            }
            if (this.IDToDisplay.Length > 0)
            {
                TextBlock block = new TextBlock();
                block.set_Text(this.IDToDisplay);
                double num = 16.5;
                block.set_FontFamily((this.IDToDisplay != "?") ? this.m_fntTrainBigImpact : this.m_fntTrainBigNormal);
                block.set_FontSize((this.IDToDisplay != "?") ? ((double) this.m_fntTrainBigImpactSize) : ((double) this.m_fntTrainBigNormalSize));
                block.set_FontWeight((this.IDToDisplay != "?") ? this.m_fntTrainBigImpactStyle : this.m_fntTrainBigNormalStyle);
                block.set_FontStretch(FontStretches.get_Normal());
                block.set_FontStyle(FontStyles.get_Normal());
                if (this.m_Small)
                {
                    block.set_FontFamily(this.m_fntTrainSmall);
                    block.set_FontSize((double) this.m_fntTrainSmallSize);
                    block.set_FontWeight(this.m_fntTrainSmallStyle);
                    num = 12.0;
                }
                block.SetValue(Canvas.LeftProperty, this.x0);
                block.SetValue(Canvas.TopProperty, (0.0 + (this.HEIGHT_ACTUAL / 2.0)) - (num / 2.0));
                block.SetValue(FrameworkElement.WidthProperty, this.WIDTH_ACTUAL);
                block.set_Foreground(new SolidColorBrush(Colors.get_Yellow()));
                block.set_TextAlignment(0);
                if (bRotateText)
                {
                    RotateTransform transform = new RotateTransform();
                    transform.set_Angle(180.0);
                    transform.set_CenterX(this.WIDTH_ACTUAL / 2.0);
                    transform.set_CenterY(block.get_ActualHeight() / 2.0);
                    block.set_RenderTransform(transform);
                }
                this.CanvasRoot.get_Children().Add(block);
            }
            foreach (FrameworkElement element in this.CanvasRoot.get_Children())
            {
                double num2 = Convert.ToDouble(element.GetValue(Canvas.LeftProperty));
                double num3 = Convert.ToDouble(element.GetValue(Canvas.TopProperty));
                num2 = (num2 - (this.WIDTH_ACTUAL / 2.0)) - this.NOSE_LEN;
                num3 -= this.HEIGHT_ACTUAL / 2.0;
                element.SetValue(Canvas.LeftProperty, num2);
                element.SetValue(Canvas.TopProperty, num3);
            }
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/TrainControl.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.Base = (Canvas) base.FindName("Base");
                this.CanvasRoot = (Canvas) base.FindName("CanvasRoot");
            }
        }

        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.OnTrainProperties != null)
            {
                this.OnTrainProperties(this.m_train);
            }
        }

        private void MenuItem_TrainNumberChange(object sender, RoutedEventArgs e)
        {
            if (this.OnTrainChangeNumber != null)
            {
                this.OnTrainChangeNumber(this.ID);
            }
        }

        private void MenuItem_TrainRouteShow(object sender, RoutedEventArgs e)
        {
            if (this.OnTrainRoute != null)
            {
                this.OnTrainRoute(this.ID);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public Color BodyColorHi
        {
            get
            {
                return this.m_BodyColorHi;
            }
            set
            {
                this.m_BodyColorHi = value;
            }
        }

        public Color BodyColorLo
        {
            get
            {
                return this.m_BodyColorLo;
            }
            set
            {
                this.m_BodyColorLo = value;
            }
        }

        public int Direction
        {
            get
            {
                return this.m_Direction;
            }
            set
            {
                this.m_Direction = value;
            }
        }

        public double HEIGHT_ACTUAL
        {
            get
            {
                return (this.Small ? 12.0 : 24.0);
            }
        }

        public string ID
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID = value;
            }
        }

        public string IDToDisplay
        {
            get
            {
                return this.m_DisplayID;
            }
            set
            {
                this.m_DisplayID = value;
            }
        }

        public double NOSE_LEN
        {
            get
            {
                return (this.HEIGHT_ACTUAL / 4.0);
            }
        }

        public bool Small
        {
            get
            {
                return this.m_Small;
            }
            set
            {
                this.m_Small = value;
            }
        }

        public string Tooltip
        {
            get
            {
                return this.m_Tooltip;
            }
            set
            {
                this.m_Tooltip = value;
                ToolTipService.SetToolTip(this, CreateTooltipControl(this.m_Tooltip));
            }
        }

        public double WIDTH_ACTUAL
        {
            get
            {
                return (double) (0x20 + (Math.Max(0, this.IDToDisplay.Length - 4) * 8));
            }
        }

        private double x0
        {
            get
            {
                return this.NOSE_LEN;
            }
        }

        public delegate void TrainChangeNumberHandler(string trainID);

        public delegate void TrainPropertiesHandler(Train t);

        public delegate void TrainRouteHandler(string trainID);
    }
}

