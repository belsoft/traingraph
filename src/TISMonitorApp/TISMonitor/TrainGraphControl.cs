namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using TISWebServiceHelper;

    public class TrainGraphControl : UserControl
    {
        private bool _contentLoaded;
        internal Canvas CanvasRoot;
        private double CanvasRoot_ActualHeight = 4000.0;
        private const double CanvasRoot_ActualWidth = 1251.0;
        internal ScaleTransform CanvasScale;
        internal Grid LayoutRoot;
        private DateTime m_actualTime = DateTime.MinValue;
        private Line m_actualTimeLine = null;
        private bool m_bAllowToUpdateScroll = true;
        private Dictionary<string, TrainCurveData> m_name2RealtimeCurve = new Dictionary<string, TrainCurveData>();
        private int m_nMaxPositionGap;
        private int m_nMaxTimeGap;
        private int m_nTrainGraphAutoScrollTimeout = 30;
        private int m_nTrainGraphHourHeight = 100;
        private List<TrainCurveData> m_regularCurves = new List<TrainCurveData>();
        private List<MarkerData> m_stations = new List<MarkerData>();
        private TISWebServiceHelper.TrainGraphData m_tgd = null;
        private DispatcherTimer m_timerUserScroll = null;
        public double SCREEN_FACTOR = 15.0;
        internal ScrollViewer scrollViewer;

        public TrainGraphControl(int nTrainGraphHourHeight, int nTrainGraphAutoScrollTimeout, int nMaxTimeGap, int nMaxPositionGap)
        {
            this.m_nTrainGraphHourHeight = nTrainGraphHourHeight;
            this.m_nMaxPositionGap = nMaxPositionGap;
            this.m_nMaxTimeGap = nMaxTimeGap;
            this.InitializeComponent();
            this.UpdateHeight();
            this.m_nTrainGraphAutoScrollTimeout = nTrainGraphAutoScrollTimeout;
            Helper.RegisterForNotification("VerticalOffset", this.scrollViewer, new PropertyChangedCallback(this, (IntPtr) this.OnVerticalOffsetChanged));
            this.m_timerUserScroll = new DispatcherTimer();
            this.m_timerUserScroll.add_Tick(new EventHandler(this.TimerUserScroll_Tick));
            Application.get_Current().get_Host().get_Content().add_Resized(new EventHandler(this.Content_Resized));
        }

        private void Content_Resized(object sender, EventArgs e)
        {
            double num = Application.get_Current().get_Host().get_Content().get_ActualHeight();
            double num2 = Application.get_Current().get_Host().get_Content().get_ActualWidth();
            if (num2 < 1260.0)
            {
                this.CanvasScale.set_ScaleX(num2 / 1260.0);
                this.CanvasScale.set_ScaleY(this.CanvasScale.get_ScaleX());
            }
            else
            {
                this.CanvasScale.set_ScaleX(1.0);
                this.CanvasScale.set_ScaleY(1.0);
            }
        }

        private double DateTime2PointY(DateTime dt, Rectangle rc)
        {
            if (this.m_stations.Count >= 2)
            {
                TimeSpan span = (TimeSpan) (this.m_tgd.TimeStop - this.m_tgd.TimeStart);
                TimeSpan span2 = (TimeSpan) (dt - this.m_tgd.TimeStart);
                return (double) (rc.Top + ((int) ((span2.TotalSeconds * (rc.Height - (rc.Top * 2))) / span.TotalSeconds)));
            }
            return -1.0;
        }

        public void Draw(bool full)
        {
            Rectangle rectangle;
            SilverlightHelper.Debug(">>> Draw");
            if (full)
            {
                this.UpdateHeight();
            }
            if (this.m_tgd.Markers.Count > 0)
            {
                this.m_stations.Clear();
                this.m_tgd.Markers.Sort(new Comparison<MarkerData>(this.MySortFunction));
                this.m_stations.AddRange(this.m_tgd.Markers);
            }
            this.DrawGetRectangle(this.m_stations, out rectangle);
            this.DrawActualTime(rectangle);
            if (((this.m_tgd != null) && (this.m_tgd.Markers != null)) && (this.m_tgd.Curves != null))
            {
                if (full)
                {
                    double num;
                    double num2;
                    this.CanvasRoot.get_Children().Clear();
                    this.DrawStations(this.m_stations, rectangle, out num, out num2);
                    this.DrawScales(rectangle, this.m_stations, num, num2);
                    this.DrawCurves(rectangle, full);
                    this.DrawActualTime(rectangle);
                }
                else
                {
                    this.DrawCurves(rectangle, full);
                }
                if (this.m_bAllowToUpdateScroll || full)
                {
                    double num3 = this.DateTime2PointY(this.m_actualTime, rectangle) - (this.scrollViewer.get_ActualHeight() / 2.0);
                    this.scrollViewer.UpdateLayout();
                    this.scrollViewer.ScrollToVerticalOffset(num3);
                }
                SilverlightHelper.Debug("<<< Draw");
            }
        }

        private void DrawActualTime(Rectangle rc)
        {
            int num = -100;
            if (this.m_actualTimeLine == null)
            {
                num = 100;
                this.m_actualTimeLine = new Line();
            }
            if (this.m_actualTimeLine.get_Parent() == null)
            {
                this.CanvasRoot.get_Children().Add(this.m_actualTimeLine);
            }
            DateTime actualTime = this.m_actualTime;
            if (this.m_tgd != null)
            {
                if (actualTime < this.m_tgd.TimeStart)
                {
                    actualTime = this.m_tgd.TimeStart;
                }
                if (actualTime > this.m_tgd.TimeStop)
                {
                    actualTime = this.m_tgd.TimeStop;
                }
            }
            double num2 = this.DateTime2PointY(actualTime.AddMinutes(1.0), rc) - this.DateTime2PointY(actualTime, rc);
            Line actualTimeLine = this.m_actualTimeLine;
            int x = rc.X;
            actualTimeLine.set_X1((double) x);
            actualTimeLine.set_Y1(this.DateTime2PointY(actualTime, rc));
            actualTimeLine.set_X2(this.Position2PointX(rc.Width, rc) + x);
            actualTimeLine.set_Y2(actualTimeLine.get_Y1());
            actualTimeLine.set_Width(1251.0);
            actualTimeLine.set_Height(this.CanvasRoot_ActualHeight);
            actualTimeLine.set_StrokeThickness(num2);
            actualTimeLine.SetValue(Canvas.ZIndexProperty, num);
            actualTimeLine.set_Stroke(new SolidColorBrush(Color.FromArgb(0xff, 0x80, 0xff, 0x80)));
        }

        private void DrawCurve(TrainCurveData c, Rectangle rc, List<Line> rectangles, int startIndex)
        {
            if ((c != null) && (c.Points != null))
            {
                PointData data;
                PointData data2;
                int num = 0;
                int num2 = -1;
                Line item = new Line();
                Color color = Colors.get_Black();
                double widthRequires = 0.0;
                if (c.Points.Count > 0)
                {
                    widthRequires = this.DrawCurveTitle(rc, c.Points[0], c.Points[0], c.Caption, false);
                }
                for (int i = startIndex; i < (c.Points.Count - 1); i++)
                {
                    data = c.Points[i];
                    data2 = c.Points[i + 1];
                    if (!c.Regular)
                    {
                        TimeSpan span = (TimeSpan) (data.DateTime - data2.DateTime);
                        if (this.IsGap((double) (data.Position - data2.Position), span.TotalMinutes))
                        {
                            c.CaptionHasDrawn = false;
                            c.CaptionStartIndex = i + 1;
                            continue;
                        }
                    }
                    color = SilverlightHelper.ConvertToColor(data2.Color);
                    Line line2 = new Line();
                    line2.set_X1(this.Position2PointX(data.Position, rc));
                    line2.set_Y1(this.DateTime2PointY(data.DateTime, rc));
                    line2.set_X2(this.Position2PointX(data2.Position, rc));
                    line2.set_Y2(this.DateTime2PointY(data2.DateTime, rc));
                    line2.set_Width(1251.0);
                    line2.set_Height(this.CanvasRoot_ActualHeight);
                    line2.set_StrokeThickness(2.0);
                    line2.set_Stroke(new SolidColorBrush(color));
                    this.CanvasRoot.get_Children().Add(line2);
                    if (c.Regular && (c.Caption.Length > 0))
                    {
                        if (!this.HasInsersection(rectangles, line2))
                        {
                            int num5 = data2.Position - data.Position;
                            if (num5 > num)
                            {
                                num = num5;
                                num2 = i;
                                item = this.LineClone(line2);
                            }
                        }
                    }
                    else if (!c.CaptionHasDrawn)
                    {
                        int num6;
                        int num7;
                        int num8;
                        bool flag;
                        if (this.GetGoodPointsForTitle(c, c.CaptionStartIndex, rc, widthRequires, out num6, out num7, out num8, out flag))
                        {
                            c.CaptionHasDrawn = true;
                            this.DrawCurveTitle(rc, c.Points[num6], c.Points[num7], c.Caption, true);
                        }
                        else if (flag)
                        {
                            c.CaptionStartIndex = num8;
                        }
                    }
                }
                if ((c.Regular && (c.Caption.Length > 0)) && (num2 != -1))
                {
                    data = c.Points[num2];
                    data2 = c.Points[num2 + 1];
                    color = SilverlightHelper.ConvertToColor(data2.Color);
                    if (rectangles != null)
                    {
                        rectangles.Add(item);
                    }
                    Point point = new Point();
                    Point point2 = new Point();
                    point.set_X(this.Position2PointX(data.Position, rc));
                    point.set_Y(this.DateTime2PointY(data.DateTime, rc));
                    point2.set_X(this.Position2PointX(data2.Position, rc));
                    point2.set_Y(this.DateTime2PointY(data2.DateTime, rc));
                    double rotationAngle = SilverlightHelper.GetRotationAngle(point, point2);
                    double num10 = point2.get_X() - point.get_X();
                    TextBlock block = new TextBlock();
                    block.SetValue(Canvas.TopProperty, point.get_Y() + 5.0);
                    block.SetValue(Canvas.LeftProperty, point.get_X());
                    block.SetValue(FrameworkElement.WidthProperty, num10);
                    block.SetValue(FrameworkElement.HeightProperty, 50.0);
                    block.set_Width(num10);
                    block.set_Text(c.Caption);
                    block.set_FontSize(17.0);
                    block.set_FontFamily(new FontFamily("Arial"));
                    block.set_Foreground(new SolidColorBrush(color));
                    block.set_FontStyle(FontStyles.get_Normal());
                    block.set_FontWeight(FontWeights.get_Bold());
                    RotateTransform transform = new RotateTransform();
                    transform.set_Angle(-rotationAngle);
                    block.set_RenderTransform(transform);
                    block.set_TextAlignment(0);
                    this.CanvasRoot.get_Children().Add(block);
                }
            }
        }

        private void DrawCurves(Rectangle rc, bool full)
        {
            SilverlightHelper.Debug("DrawCurves started");
            List<TrainCurveData> list = new List<TrainCurveData>();
            List<TrainCurveData> list2 = new List<TrainCurveData>();
            foreach (CurveData data in this.m_tgd.Curves)
            {
                TrainCurveData item = new TrainCurveData {
                    CaptionHasDrawn = false,
                    Caption = data.Caption,
                    Name = data.Name,
                    Points = data.Points,
                    Regular = data.Regular
                };
                if (item.Regular)
                {
                    list.Add(item);
                }
                else
                {
                    list2.Add(item);
                }
            }
            if (full)
            {
                SilverlightHelper.Debug("Regular curves draw started");
                if (list.Count > 0)
                {
                    this.m_regularCurves = list;
                }
                List<Line> rectangles = new List<Line>();
                foreach (TrainCurveData data2 in this.m_regularCurves)
                {
                    this.DrawCurve(data2, rc, rectangles, 0);
                }
                SilverlightHelper.Debug("Regular curves draw finished");
            }
            SilverlightHelper.Debug("Realtime curves draw started");
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (TrainCurveData data2 in list2)
            {
                if (!this.m_name2RealtimeCurve.ContainsKey(data2.Name))
                {
                    this.m_name2RealtimeCurve[data2.Name] = new TrainCurveData();
                }
                this.m_name2RealtimeCurve[data2.Name].Caption = data2.Caption;
                this.m_name2RealtimeCurve[data2.Name].Name = data2.Name;
                this.m_name2RealtimeCurve[data2.Name].Regular = data2.Regular;
                dictionary[data2.Name] = (this.m_name2RealtimeCurve[data2.Name].Points.Count > 0) ? (this.m_name2RealtimeCurve[data2.Name].Points.Count - 1) : 0;
                this.m_name2RealtimeCurve[data2.Name].Points.AddRange(data2.Points);
            }
            foreach (string str in dictionary.Keys)
            {
                int startIndex = dictionary[str];
                this.DrawCurve(this.m_name2RealtimeCurve[str], rc, null, startIndex);
            }
            SilverlightHelper.Debug("Realtime curves draw finished");
            SilverlightHelper.Debug("DrawCurves finished");
        }

        private double DrawCurveTitle(Rectangle rc, PointData dpt1, PointData dpt2, string title, bool draw)
        {
            Point point3;
            Point point4;
            PointData data;
            Point point = this.PointData2Point(dpt1, rc);
            Point point2 = this.PointData2Point(dpt2, rc);
            if (point.get_X() < point2.get_X())
            {
                point3 = point;
                point4 = point2;
                data = dpt1;
            }
            else
            {
                point3 = point2;
                point4 = point;
                data = dpt2;
            }
            Color color = SilverlightHelper.ConvertToColor(dpt2.Color);
            if (dpt1.DateTime > dpt2.DateTime)
            {
                color = SilverlightHelper.ConvertToColor(dpt1.Color);
            }
            double rotationAngle = SilverlightHelper.GetRotationAngle(point3, point4);
            TextBlock block = new TextBlock();
            int num2 = 100;
            double num3 = 5.0;
            block.SetValue(Canvas.TopProperty, point3.get_Y() + num3);
            block.SetValue(Canvas.LeftProperty, point3.get_X() + num3);
            block.SetValue(FrameworkElement.WidthProperty, (double) num2);
            block.SetValue(FrameworkElement.HeightProperty, 50.0);
            block.set_Width((double) num2);
            block.set_Text(title);
            block.set_FontSize(12.0);
            block.set_FontFamily(new FontFamily("Arial"));
            block.set_Foreground(new SolidColorBrush(color));
            block.set_FontStyle(FontStyles.get_Normal());
            block.set_FontWeight(FontWeights.get_Bold());
            RotateTransform transform = new RotateTransform();
            transform.set_Angle(-rotationAngle);
            block.set_RenderTransform(transform);
            block.set_TextAlignment(1);
            if (draw)
            {
                this.CanvasRoot.get_Children().Add(block);
            }
            return (block.get_ActualWidth() + num3);
        }

        private void DrawGetRectangle(List<MarkerData> stations, out Rectangle rc)
        {
            rc = new Rectangle(0, 7, 0x4e3, (int) this.CanvasRoot_ActualHeight);
            int position = 0x7fffffff;
            int num2 = -2147483648;
            MarkerData data = null;
            foreach (MarkerData data2 in stations)
            {
                if (data2.Position < position)
                {
                    position = data2.Position;
                }
                if (data2.Position > num2)
                {
                    num2 = data2.Position;
                    data = data2;
                }
            }
            if (position != 0x7fffffff)
            {
                rc.X = position;
            }
            if (num2 != -2147483648)
            {
                rc.Width = num2 - rc.X;
            }
        }

        private void DrawHorScaleLine(Rectangle rc, double y)
        {
            Line line = new Line();
            int x = rc.X;
            line.set_X1((double) x);
            line.set_Y1(y);
            line.set_X2(this.Position2PointX(rc.Width, rc) + x);
            line.set_Y2(y);
            line.set_Width(1251.0);
            line.set_Height(this.CanvasRoot_ActualHeight);
            line.set_StrokeThickness(1.0);
            line.set_Stroke(new SolidColorBrush(ColorConstants.LightGray));
            this.CanvasRoot.get_Children().Add(line);
        }

        private void DrawScales(Rectangle rc, List<MarkerData> s, double lastRightStationPosition, double firstLeftStationXPos)
        {
            DateTime timeStart = this.m_tgd.TimeStart;
            TimeSpan span = TimeSpan.FromMinutes(1.0);
            double num = 16.0;
            double num2 = 18.0;
            double num3 = 5.0;
            while (timeStart <= this.m_tgd.TimeStop)
            {
                double y = this.DateTime2PointY(timeStart, rc);
                if ((timeStart.Minute == 0) || ((timeStart.Minute % 10) == 0))
                {
                    this.DrawHorScaleLine(rc, y);
                    this.DrawStationTimeScale(y, s, true);
                    TextBlock block = new TextBlock();
                    block.SetValue(Canvas.TopProperty, y - (num2 / 2.0));
                    block.SetValue(Canvas.LeftProperty, ((firstLeftStationXPos - num) - num3) - 1.0);
                    block.SetValue(FrameworkElement.WidthProperty, num);
                    block.SetValue(FrameworkElement.HeightProperty, num2);
                    block.set_TextAlignment(0);
                    TextBlock block2 = new TextBlock();
                    block2.SetValue(Canvas.TopProperty, y - (num2 / 2.0));
                    block2.SetValue(Canvas.LeftProperty, (lastRightStationPosition + num3) + 2.0);
                    block2.SetValue(FrameworkElement.WidthProperty, num);
                    block2.SetValue(FrameworkElement.HeightProperty, num2);
                    block2.set_TextAlignment(0);
                    if (timeStart.Minute == 0)
                    {
                        block.set_Text(timeStart.Hour.ToString("00"));
                        block.set_FontSize(15.0);
                        block.set_FontFamily(new FontFamily("Arial"));
                        block.set_Foreground(new SolidColorBrush(Colors.get_Black()));
                        block.set_FontStyle(FontStyles.get_Normal());
                        block.set_FontWeight(FontWeights.get_Bold());
                    }
                    else if ((timeStart.Minute % 10) == 0)
                    {
                        block.set_Text(timeStart.Minute.ToString());
                        block.set_FontSize(13.0);
                        block.set_FontFamily(new FontFamily("Arial"));
                        block.set_Foreground(new SolidColorBrush(Colors.get_Gray()));
                        block.set_FontStyle(FontStyles.get_Normal());
                        block.set_FontWeight(FontWeights.get_Normal());
                    }
                    block2.set_Text(block.get_Text());
                    block2.set_FontStyle(block.get_FontStyle());
                    block2.set_FontWeight(block.get_FontWeight());
                    block2.set_FontSize(block.get_FontSize());
                    block2.set_FontFamily(block.get_FontFamily());
                    block2.set_Foreground(block.get_Foreground());
                    this.CanvasRoot.get_Children().Add(block);
                    this.CanvasRoot.get_Children().Add(block2);
                }
                else
                {
                    this.DrawStationTimeScale(y, s, false);
                }
                timeStart = timeStart.Add(span);
            }
        }

        private void DrawStation(MarkerData s, Rectangle rc)
        {
            if (s.Visible)
            {
                Line l = new Line();
                l.set_X1((double) s.Position);
                l.set_X2((double) s.Position);
                l.set_Y1((double) rc.Y);
                l.set_Y2(this.CanvasRoot_ActualHeight - rc.Y);
                l.set_Width(1251.0);
                l.set_Height(this.CanvasRoot_ActualHeight);
                if (s.Style == PositionMarkerStyle.MarkerStation)
                {
                    l.set_StrokeThickness(2.0);
                }
                else if (s.Style == PositionMarkerStyle.MarkerIntermediateStop)
                {
                    l.get_StrokeDashArray().Add(2.0);
                    l.get_StrokeDashArray().Add(2.0);
                    l.set_StrokeThickness(2.0);
                }
                else if (s.Style == PositionMarkerStyle.MarkerSegmentBorder)
                {
                    l.set_StrokeThickness(1.0);
                }
                else
                {
                    l.set_StrokeThickness(2.0);
                }
                l.set_Stroke(new SolidColorBrush(SilverlightHelper.ConvertToColor(s.Color)));
                this.LineAdjust1PixelsWidth(l);
                this.CanvasRoot.get_Children().Add(l);
            }
        }

        private void DrawStations(List<MarkerData> stations, Rectangle rc, out double lastRightStationXPos, out double firstLeftStationXPos)
        {
            lastRightStationXPos = double.MinValue;
            firstLeftStationXPos = double.MaxValue;
            foreach (MarkerData data in stations)
            {
                lastRightStationXPos = Math.Max(lastRightStationXPos, (double) data.Position);
                firstLeftStationXPos = Math.Min(firstLeftStationXPos, (double) data.Position);
                this.DrawStation(data, rc);
            }
        }

        private void DrawStationTimeScale(double y, List<MarkerData> ls, bool big)
        {
            double num = big ? 4.5 : 2.0;
            foreach (MarkerData data in ls)
            {
                if (data.Style == PositionMarkerStyle.MarkerStation)
                {
                    Line l = new Line();
                    l.set_X1(data.Position - num);
                    l.set_Y1(y);
                    l.set_X2(data.Position + num);
                    l.set_Y2(y);
                    this.LineAdjust1PixelsWidth(l);
                    l.set_Width(1251.0);
                    l.set_Height(this.CanvasRoot_ActualHeight);
                    l.set_StrokeThickness(1.0);
                    l.set_Stroke(new SolidColorBrush(SilverlightHelper.ConvertToColor(data.Color)));
                    this.CanvasRoot.get_Children().Add(l);
                }
            }
        }

        private int FindBetterPointForCurveTitleDrawing(TrainCurveData c, int startIndex, int endIndex, Rectangle rc)
        {
            Point point = this.PointData2Point(c.Points[endIndex], rc);
            for (int i = startIndex; (i < c.Points.Count) && (i <= endIndex); i++)
            {
                if (Math.Abs(SilverlightHelper.GetRotationAngle(this.PointData2Point(c.Points[i], rc), point)) < 30.0)
                {
                    return i;
                }
            }
            return startIndex;
        }

        private bool GetGoodPointsForTitle(TrainCurveData c, int startIndex, Rectangle rc, double widthRequires, out int i1, out int i2, out int nextStartIndex, out bool resetNextStartIndex)
        {
            i1 = startIndex;
            i2 = startIndex;
            nextStartIndex = startIndex;
            resetNextStartIndex = false;
            Point point = this.PointData2Point(c.Points[startIndex], rc);
            double num = 0.0;
            for (int i = startIndex + 1; i < (c.Points.Count - 1); i++)
            {
                PointData data = c.Points[i];
                PointData data2 = c.Points[i + 1];
                Point point2 = this.PointData2Point(data, rc);
                Point point3 = this.PointData2Point(data2, rc);
                TimeSpan span = (TimeSpan) (data.DateTime - data2.DateTime);
                if (this.IsGap((double) (data.Position - data2.Position), span.TotalMinutes))
                {
                    nextStartIndex = i + 1;
                    resetNextStartIndex = true;
                    return false;
                }
                if (i == (startIndex + 1))
                {
                    num = point2.get_X() - point.get_X();
                }
                else
                {
                    double num3 = point3.get_X() - point2.get_X();
                    if ((num * num3) < 0.0)
                    {
                        nextStartIndex = i + 1;
                        resetNextStartIndex = true;
                        return false;
                    }
                }
                if (Math.Abs((double) (point.get_X() - point2.get_X())) >= widthRequires)
                {
                    i2 = i;
                    i1 = this.FindBetterPointForCurveTitleDrawing(c, startIndex, i, rc);
                    return true;
                }
            }
            return false;
        }

        private bool HasInsersection(List<Line> l, Line rc)
        {
            if (l != null)
            {
                foreach (Line line in l)
                {
                    if (IntersectionOf(this.LineClone(line), this.LineClone(rc)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool HasInsersection(List<Rectangle> l, Rectangle rc)
        {
            if (l != null)
            {
                foreach (Rectangle rectangle in l)
                {
                    if (rectangle.IntersectsWith(rc))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/TrainGraphControl.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.CanvasScale = (ScaleTransform) base.FindName("CanvasScale");
                this.scrollViewer = (ScrollViewer) base.FindName("scrollViewer");
                this.CanvasRoot = (Canvas) base.FindName("CanvasRoot");
            }
        }

        public static bool IntersectionOf(Line line1, Line line2)
        {
            if (((line1.get_X1() == line1.get_X2()) && (line1.get_Y1() == line1.get_Y2())) || ((line2.get_X1() == line2.get_X2()) && (line2.get_Y1() == line2.get_Y2())))
            {
                return false;
            }
            if (((line1.get_X1() != line2.get_X1()) || (line1.get_Y1() != line2.get_Y1())) && ((line1.get_X2() != line2.get_X1()) || (line1.get_Y2() != line2.get_Y1())))
            {
                if (((line1.get_X1() == line2.get_X2()) && (line1.get_Y1() == line2.get_Y2())) || ((line1.get_X2() == line2.get_X2()) && (line1.get_Y2() == line2.get_Y2())))
                {
                    return true;
                }
                line1.set_X2(line1.get_X2() - line1.get_X1());
                line1.set_Y2(line1.get_Y2() - line1.get_Y1());
                line2.set_X1(line2.get_X1() - line1.get_X1());
                line2.set_Y1(line2.get_Y1() - line1.get_Y1());
                line2.set_X2(line2.get_X2() - line1.get_X1());
                line2.set_Y2(line2.get_Y2() - line1.get_Y1());
                double num = Math.Sqrt((line1.get_X2() * line1.get_X2()) + (line1.get_Y2() * line1.get_Y2()));
                double num2 = line1.get_X2() / num;
                double num3 = line1.get_Y2() / num;
                double num4 = (line2.get_X1() * num2) + (line2.get_Y1() * num3);
                line2.set_Y1((line2.get_Y1() * num2) - (line2.get_X1() * num3));
                line2.set_X1(num4);
                num4 = (line2.get_X2() * num2) + (line2.get_Y2() * num3);
                line2.set_Y2((line2.get_Y2() * num2) - (line2.get_X2() * num3));
                line2.set_X2(num4);
                if (((line2.get_Y1() < 0.0) && (line2.get_Y2() < 0.0)) || ((line2.get_Y1() >= 0.0) && (line2.get_Y2() >= 0.0)))
                {
                    return false;
                }
                double num5 = line2.get_X2() + (((line2.get_X1() - line2.get_X2()) * line2.get_Y2()) / (line2.get_Y2() - line2.get_Y1()));
                if ((num5 < 0.0) || (num5 > num))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsGap(double dx, double dy)
        {
            bool flag = true;
            if ((this.m_nMaxPositionGap > 0) && (this.m_nMaxPositionGap < Math.Abs(dx)))
            {
                flag = false;
            }
            if ((flag && (this.m_nMaxTimeGap > 0)) && (this.m_nMaxTimeGap < Math.Abs(dy)))
            {
                flag = false;
            }
            return !flag;
        }

        private void LineAdjust1PixelsWidth(Line l)
        {
            l.set_X1(l.get_X1() + 0.5);
            l.set_X2(l.get_X2() + 0.5);
            l.set_Y1(l.get_Y1() + 0.5);
            l.set_Y2(l.get_Y2() + 0.5);
        }

        private Line LineClone(Line line)
        {
            Line line2 = new Line();
            line2.set_X1(line.get_X1());
            line2.set_X2(line.get_X2());
            line2.set_Y1(line.get_Y1());
            line2.set_Y2(line.get_Y2());
            return line2;
        }

        public int MySortFunction(MarkerData obj1, MarkerData obj2)
        {
            return obj1.Position.CompareTo(obj2.Position);
        }

        public void OnVerticalOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.m_timerUserScroll != null)
            {
                this.m_bAllowToUpdateScroll = false;
                this.m_timerUserScroll.Stop();
                this.m_timerUserScroll.set_Interval(TimeSpan.FromSeconds((double) this.m_nTrainGraphAutoScrollTimeout));
                this.m_timerUserScroll.Start();
            }
        }

        private Point PointData2Point(PointData pd1, Rectangle rc)
        {
            Point point = new Point();
            point.set_X(this.Position2PointX(pd1.Position, rc));
            point.set_Y(this.DateTime2PointY(pd1.DateTime, rc));
            return point;
        }

        private double Position2PointX(int nPosition, Rectangle rc)
        {
            if (this.m_stations.Count >= 2)
            {
                long position = this.m_stations[0].Position;
                long num2 = this.m_stations[this.m_stations.Count - 1].Position;
                return (double) (rc.Left + (((nPosition - position) * rc.Width) / (num2 - position)));
            }
            return -1.0;
        }

        public void ResetData()
        {
            this.m_stations.Clear();
            this.m_regularCurves.Clear();
            this.m_name2RealtimeCurve.Clear();
            this.m_tgd = null;
        }

        public void SetScrollViewerHeight(double h)
        {
            this.scrollViewer.set_Height(h);
        }

        private void TimerUserScroll_Tick(object sender, EventArgs e)
        {
            if (this.m_timerUserScroll.get_IsEnabled())
            {
                this.m_bAllowToUpdateScroll = true;
            }
            this.m_timerUserScroll.Stop();
        }

        private void UpdateHeight()
        {
            double num = 1000.0;
            if (this.m_tgd != null)
            {
                TimeSpan span = (TimeSpan) (this.m_tgd.TimeStop - this.m_tgd.TimeStart);
                if (span.TotalHours > 0.0)
                {
                    num = span.TotalHours * this.m_nTrainGraphHourHeight;
                }
            }
            this.CanvasRoot.set_Height(this.CanvasRoot_ActualHeight = num);
        }

        public DateTime ActualTime
        {
            get
            {
                return this.m_actualTime;
            }
            set
            {
                this.m_actualTime = value;
            }
        }

        public List<TrainCurveData> RegularCurves
        {
            get
            {
                return this.m_regularCurves;
            }
        }

        public TISWebServiceHelper.TrainGraphData TrainGraphData
        {
            get
            {
                return this.m_tgd;
            }
            set
            {
                this.m_tgd = value;
            }
        }
    }
}

