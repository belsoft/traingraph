namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Xml.Serialization;
    using TISWebServiceHelper;

    [Serializable]
    public class Layout : LayoutBase
    {
        private static Color elBaseColor = Colors.get_LightGray();
        [XmlIgnore]
        public Canvas g;
        [XmlIgnore]
        public ArrayList m_alPathElements = new ArrayList();
        protected TISMonitor.Stations m_alStations = new TISMonitor.Stations();
        public static FontFamily m_fontStation;
        [XmlIgnore]
        public Hashtable m_htAddress2StateElement = new Hashtable();
        protected Hashtable m_htPerrons = new Hashtable();
        [XmlIgnore]
        public Hashtable m_htSegment2PathElementPassive = new Hashtable();
        protected Hashtable m_htTrainID2Train = new Hashtable();
        private SolidColorBrush m_penBase = new SolidColorBrush(elBaseColor);
        protected TrackedTrainListEx m_TrackedTrainList = new TrackedTrainListEx();
        [XmlIgnore]
        public string SQLConnectionString = "";
        private static int[] TextOrientationVectorHor;
        private static int[] TextOrientationVectorVer;

        public event LegendClickedHandler OnLegendClicked;

        public event TrainChangeNumberHandler OnTrainChangeNumber;

        public event TrainPropertiesHandler OnTrainProperties;

        public event TrainRouteHandler OnTrainRoute;

        static Layout()
        {
            int[] numArray = new int[2];
            numArray[1] = 90;
            TextOrientationVectorHor = numArray;
            numArray = new int[2];
            numArray[0] = -100;
            TextOrientationVectorVer = numArray;
            m_fontStation = new FontFamily("Arial");
        }

        public override void ClearContent()
        {
            base.ClearContent();
            this.m_TrackedTrainList.Clear();
            this.m_htAddress2StateElement.Clear();
            this.m_htSegment2PathElementPassive.Clear();
            this.m_htPerrons.Clear();
            this.m_alStations.Clear();
            this.m_htTrainID2Train.Clear();
            this.m_alPathElements.Clear();
        }

        private void ClearPerronsFromCanvas(Canvas canvasRoot)
        {
            List<UIElement> list = new List<UIElement>();
            foreach (UIElement element in canvasRoot.get_Children())
            {
                if (element is PerronControl)
                {
                    PerronControl control = element as PerronControl;
                    list.Add(element);
                }
            }
            foreach (UIElement element in list)
            {
                canvasRoot.get_Children().Remove(element);
            }
        }

        public void ClearTrainsFromCanvas()
        {
            Canvas g = this.g;
            List<UIElement> list = new List<UIElement>();
            foreach (UIElement element in g.get_Children())
            {
                if (((element is TrainControl) || (element is TrainOfflinePositionControl)) || (element is TrainOnlinePositionControl))
                {
                    list.Add(element);
                }
            }
            foreach (UIElement element in list)
            {
                g.get_Children().Remove(element);
            }
        }

        public static double ConvertRadians2Degrees(double d)
        {
            return ((d * 180.0) / 3.1415926535897931);
        }

        private static Brush CreateHatchBrush(Color clr)
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.set_MappingMode(0);
            brush.set_SpreadMethod(2);
            brush.set_StartPoint(new Point(0.0, 0.0));
            brush.set_EndPoint(new Point(3.0, 3.0));
            GradientStop stop = new GradientStop();
            stop.set_Color(clr);
            brush.get_GradientStops().Add(stop);
            stop = new GradientStop();
            stop.set_Color(clr);
            stop.set_Offset(0.5);
            brush.get_GradientStops().Add(stop);
            stop = new GradientStop();
            stop.set_Color(Colors.get_Transparent());
            stop.set_Offset(0.5);
            brush.get_GradientStops().Add(stop);
            stop = new GradientStop();
            stop.set_Color(Colors.get_Transparent());
            stop.set_Offset(1.0);
            brush.get_GradientStops().Add(stop);
            return brush;
        }

        private Line CreateLineForDrawing(double x1, double y1, double x2, double y2)
        {
            Line line = new Line();
            line.set_StrokeThickness(3.0);
            line.set_Stroke(this.m_penBase);
            line.set_Width(1280.0);
            line.set_Height(1280.0);
            line.set_X1(x1);
            line.set_Y1(y1);
            line.set_X2(x2);
            line.set_Y2(y2);
            return line;
        }

        public void DrawElements()
        {
            ArrayList list = new ArrayList();
            foreach (Element element in base.m_alElements)
            {
                if (element is Station)
                {
                    this.DrawStation(element as Station);
                }
                else if (element is Track)
                {
                    this.DrawTrack(element as Track);
                }
                else if (element is Perron)
                {
                    this.DrawPerron(element as Perron, this.g, false);
                }
                else if (element is TextLabel)
                {
                    this.DrawTextLabel(element as TextLabel);
                }
                else if (element is PointSwitchBase)
                {
                    this.DrawPointSwitch(element as PointSwitchBase);
                }
            }
        }

        public void DrawOfflineTrains(List<TrainWebData> listTrains, LegendUILoader legendLoader)
        {
            MouseButtonEventHandler handler = null;
            foreach (TrainWebData data in listTrains)
            {
                Track peHead = base.GetElement(data.HeadElement) as Track;
                if (peHead != null)
                {
                    Point trainPoint = peHead.GetTrainPoint();
                    Track.GetOffsettedValue(peHead, (double) data.HeadOffset, data.HeadElementArrivedSource, ref trainPoint);
                    FrameworkElement control = legendLoader.GetControl();
                    control.set_Tag(data);
                    double num = 0.0;
                    Point point2 = new Point(0.5, 1.0);
                    if (peHead.PointPlus.get_X() == peHead.PointMinus.get_X())
                    {
                        point2 = new Point(1.0, 0.5);
                        num = 90.0;
                    }
                    control.SetValue(Canvas.LeftProperty, trainPoint.get_X() - (point2.get_X() * 16.0));
                    control.SetValue(Canvas.TopProperty, trainPoint.get_Y() - (point2.get_Y() * 16.0));
                    this.g.get_Children().Add(control);
                    RotateTransform transform = new RotateTransform();
                    transform.set_Angle(-num);
                    control.set_RenderTransform(transform);
                    ToolTipService.SetToolTip(control, TrainControl.CreateTooltipControl(data.Tooltip));
                    if (handler == null)
                    {
                        handler = new MouseButtonEventHandler(this, (IntPtr) this.<DrawOfflineTrains>b__1);
                    }
                    control.add_MouseLeftButtonDown(handler);
                }
            }
        }

        public void DrawOnlineTrains(List<TrainWebData> listTrains)
        {
            if (listTrains != null)
            {
                List<Train> list;
                Dictionary<Train, List<Train>> dictionary;
                this.DrawTrainsRemoveOldAndAssignNew(listTrains);
                this.DrawTrainsGetIntersected(out list, out dictionary);
                foreach (Element element in base.m_alElements)
                {
                    if (element is PathElement)
                    {
                        PathElement element2 = element as PathElement;
                        ArrayList collection = element2.Trains();
                        if (collection.Count > 0)
                        {
                            foreach (Train train in collection)
                            {
                                Point trainPoint;
                                Rectangle rectangle;
                                bool small = list.Contains(train);
                                if (small)
                                {
                                    ArrayList alTrains = new ArrayList();
                                    alTrains.AddRange(collection);
                                    if (dictionary.ContainsKey(train))
                                    {
                                        foreach (Train train2 in dictionary[train])
                                        {
                                            if (!alTrains.Contains(train2))
                                            {
                                                alTrains.Add(train2);
                                            }
                                        }
                                    }
                                    alTrains.Sort(new Comparison<object>(this.SortTrainsForDrawing));
                                    trainPoint = element2.GetTrainPoint(alTrains, train);
                                }
                                else
                                {
                                    ArrayList list4 = new ArrayList();
                                    list4.Add(train);
                                    trainPoint = element2.GetTrainPoint(list4, train);
                                }
                                this.DrawTrain(this.g, train, trainPoint, small, true, out rectangle);
                            }
                        }
                    }
                }
            }
        }

        private void DrawPerron(Perron p, Canvas g, bool withTrain)
        {
            PerronControl control = new PerronControl(p);
            control.SetValue(Canvas.LeftProperty, p.BasePoint.get_X());
            control.SetValue(Canvas.TopProperty, p.BasePoint.get_Y());
            control.Boarding = withTrain;
            g.get_Children().Add(control);
        }

        public void DrawPerrons(List<TrainWebData> trains)
        {
            this.ClearPerronsFromCanvas(this.g);
            List<string> list = new List<string>();
            foreach (TrainWebData data in trains)
            {
                if (data.AtPerron)
                {
                    list.Add(data.HeadElement);
                    Element element = base.GetElement(data.HeadElement);
                    if ((element != null) && (element is Track))
                    {
                        Track track = element as Track;
                        if (track.Perron != null)
                        {
                            list.Add(track.Perron.ID);
                        }
                    }
                }
            }
            foreach (Element element in base.m_alElements)
            {
                if (element is Perron)
                {
                    bool withTrain = list.Contains(element.ID);
                    this.DrawPerron(element as Perron, this.g, withTrain);
                }
            }
        }

        private void DrawPointSwitch(PointSwitchBase ps)
        {
            this.g.get_Children().Add(this.CreateLineForDrawing(ps.PointCore.get_X(), ps.PointCore.get_Y(), ps.PointMinus.get_X(), ps.PointMinus.get_Y()));
            this.g.get_Children().Add(this.CreateLineForDrawing(ps.PointCore.get_X(), ps.PointCore.get_Y(), ps.PointPlus.get_X(), ps.PointPlus.get_Y()));
            this.g.get_Children().Add(this.CreateLineForDrawing(ps.PointCore.get_X(), ps.PointCore.get_Y(), ps.PointPeak.get_X(), ps.PointPeak.get_Y()));
        }

        private void DrawStation(Station e)
        {
            TextBlock block = new TextBlock();
            block.set_Text((e.ShortName.Length > 0) ? e.ShortName : e.ID);
            block.set_FontSize(12.0);
            block.set_FontFamily(m_fontStation);
            block.set_FontWeight(FontWeights.get_Bold());
            Color midnightBlue = ColorConstants.MidnightBlue;
            block.set_Foreground(new SolidColorBrush(midnightBlue));
            RotateTransform transform = new RotateTransform();
            transform.set_Angle((double) e.Escapement);
            block.set_RenderTransform(transform);
            block.SetValue(Canvas.LeftProperty, e.PointBase.get_X());
            block.SetValue(Canvas.TopProperty, (e.PointBase.get_Y() + 15.0) - (block.get_ActualHeight() / 2.0));
            this.g.get_Children().Add(block);
        }

        private void DrawTextLabel(TextLabel tl)
        {
            TextLabelControl control = new TextLabelControl(tl);
            control.SetValue(Canvas.LeftProperty, tl.PointBase.get_X());
            control.SetValue(Canvas.TopProperty, tl.PointBase.get_Y());
            control.Draw();
            this.g.get_Children().Add(control);
        }

        private void DrawTrack(Track e)
        {
            Line line = this.CreateLineForDrawing(e.PointMinus.get_X(), e.PointMinus.get_Y(), e.PointPlus.get_X(), e.PointPlus.get_Y());
            this.g.get_Children().Add(line);
        }

        private void DrawTrain(Canvas g, Train train, Point pt, bool small, bool allowToDraw, out Rectangle rectangle)
        {
            Color color;
            Color color2;
            rectangle = new Rectangle();
            TrainControl control = new TrainControl(train);
            control.OnTrainProperties += new TrainControl.TrainPropertiesHandler(this.TrainControl_OnTrainProperties);
            Train.GetTrainColors(train, out color, out color2);
            control.BodyColorHi = color2;
            control.BodyColorLo = color;
            Point connectionSourcePoint = new Point(0.0, 0.0);
            Point point2 = new Point(0.0, 0.0);
            PathElement headElement = train.HeadElement;
            if ((train.NextElement != null) && (train.NextElement != headElement))
            {
                connectionSourcePoint = train.NextElement.GetConnectionSourcePoint(train.m_nNextElementSource);
                point2 = train.HeadElement.GetConnectionSourcePoint(train.m_nHeadElementArrivedSource);
            }
            else if (train.m_nHeadElementArrivedSource != -1)
            {
                connectionSourcePoint = headElement.GetConnectionSourcePoint(headElement.GetOppositeSource(train.m_nHeadElementArrivedSource));
                point2 = headElement.GetConnectionSourcePoint(train.m_nHeadElementArrivedSource);
            }
            double rotationAngle = 0.0;
            if ((((connectionSourcePoint.get_X() != 0.0) || (connectionSourcePoint.get_Y() != 0.0)) && ((point2.get_X() != 0.0) || (point2.get_Y() != 0.0))) && (point2 != connectionSourcePoint))
            {
                rotationAngle = _SilverlightHelper.GetRotationAngle(point2, connectionSourcePoint);
            }
            else if (train.HeadElement != null)
            {
                int[] sources = train.HeadElement.GetSources();
                if ((sources != null) && (sources.Length > 1))
                {
                    point2 = train.HeadElement.GetConnectionSourcePoint(sources[0]);
                    connectionSourcePoint = train.HeadElement.GetConnectionSourcePoint(sources[1]);
                    rotationAngle = _SilverlightHelper.GetRotationAngle(point2, connectionSourcePoint);
                }
            }
            control.ID = train.ID;
            control.Tooltip = train.GetToolTipText();
            control.IDToDisplay = train.IDToDisplay;
            control.Small = small;
            control.Direction = 1;
            rectangle = new Rectangle();
            rectangle.X = (int) pt.get_X();
            rectangle.Y = (int) pt.get_Y();
            rectangle.Width = (int) control.WIDTH_ACTUAL;
            rectangle.Height = (int) control.HEIGHT_ACTUAL;
            if (allowToDraw)
            {
                control.SetValue(Canvas.LeftProperty, (double) rectangle.X);
                control.SetValue(Canvas.TopProperty, (double) rectangle.Y);
                control.SetValue(FrameworkElement.WidthProperty, (double) rectangle.Width);
                control.SetValue(FrameworkElement.HeightProperty, (double) rectangle.Height);
                double num2 = point2.get_X() - connectionSourcePoint.get_X();
                double num3 = point2.get_Y() - connectionSourcePoint.get_Y();
                int[] textOrientationVectorHor = TextOrientationVectorHor;
                if (train.HeadElement is Track)
                {
                    Track track = train.HeadElement as Track;
                    if (track.PointMinus.get_Y() == track.PointPlus.get_Y())
                    {
                        textOrientationVectorHor = TextOrientationVectorVer;
                    }
                }
                double num4 = _SilverlightHelper.GetAngleBetweenVercors((double) textOrientationVectorHor[0], (double) textOrientationVectorHor[1], num2, num3);
                control.Draw(num4 >= 90.0);
                RotateTransform transform = new RotateTransform();
                transform.set_Angle(-rotationAngle);
                control.set_RenderTransform(transform);
                g.get_Children().Add(control);
            }
            rectangle.Width += (int) (2.0 * control.NOSE_LEN);
            rectangle.Height = rectangle.Width = Math.Max(rectangle.Width, rectangle.Height);
        }

        public void DrawTrainsGetIntersected(out List<Train> intersectedTrains, out Dictionary<Train, List<Train>> train2IntersectedTrains)
        {
            intersectedTrains = new List<Train>();
            train2IntersectedTrains = new Dictionary<Train, List<Train>>();
            Dictionary<Train, TrainPossiblePlaces> dictionary = new Dictionary<Train, TrainPossiblePlaces>();
            foreach (Element element in base.m_alElements)
            {
                if (element is PathElement)
                {
                    PathElement element2 = element as PathElement;
                    ArrayList alTrains = element2.Trains();
                    if (alTrains.Count > 0)
                    {
                        foreach (Train train in alTrains)
                        {
                            Rectangle rectangle;
                            ArrayList list2 = new ArrayList {
                                train
                            };
                            Point trainPoint = element2.GetTrainPoint(list2, train);
                            TrainPossiblePlaces places = new TrainPossiblePlaces();
                            this.DrawTrain(this.g, train, trainPoint, false, false, out rectangle);
                            places.Places.Add(rectangle);
                            trainPoint = element2.GetTrainPoint(alTrains, train);
                            this.DrawTrain(this.g, train, trainPoint, true, false, out rectangle);
                            places.Places.Add(rectangle);
                            dictionary.Add(train, places);
                        }
                    }
                }
            }
            foreach (Train train2 in dictionary.Keys)
            {
                TrainPossiblePlaces places2 = dictionary[train2];
                foreach (Train train3 in dictionary.Keys)
                {
                    if (train2.ID != train3.ID)
                    {
                        TrainPossiblePlaces placesB = dictionary[train3];
                        if (places2.IntersectsWith(placesB))
                        {
                            if (!train2IntersectedTrains.ContainsKey(train2))
                            {
                                train2IntersectedTrains[train2] = new List<Train>();
                            }
                            train2IntersectedTrains[train2].Add(train3);
                            if (!intersectedTrains.Contains(train2))
                            {
                                intersectedTrains.Add(train2);
                            }
                            if (!intersectedTrains.Contains(train3))
                            {
                                intersectedTrains.Add(train3);
                            }
                        }
                    }
                }
            }
        }

        public void DrawTrainsRemoveOldAndAssignNew(List<TrainWebData> listTrains)
        {
            foreach (Element element in base.m_alElements)
            {
                if (element is PathElement)
                {
                    PathElement element2 = element as PathElement;
                    if (element2.Trains().Count > 0)
                    {
                        element2.RemoveTrains();
                    }
                }
            }
            foreach (TrainWebData data in listTrains)
            {
                Track track = base.GetElement(data.HeadElement) as Track;
                if (track != null)
                {
                    Train t = TrainWebData2Train(data);
                    t.HeadElement = track;
                    t.NextElement = (data.NextElement.Length > 0) ? (base.GetElement(data.NextElement) as PathElement) : null;
                    track.AddTrain(t, t.HeadElementArrivedSource);
                }
            }
        }

        public int GetActiveElementsCountInSegment(string strSegment)
        {
            return this.GetActiveElementsInSegment(strSegment).Count;
        }

        public ArrayList GetActiveElementsInSegment(string strSegment)
        {
            ArrayList list = new ArrayList();
            foreach (PathElementPassive passive in this.GetElementsInSegment(strSegment))
            {
                if ((passive is PointSwitchBase) || (passive is Track))
                {
                    list.Add(passive);
                }
            }
            return list;
        }

        public static double GetAngleBetweenVectors(double x1, double y1, double x2, double y2)
        {
            double d = ((x1 * x2) + (y1 * y2)) / (Math.Sqrt((x1 * x1) + (y1 * y1)) * Math.Sqrt((x2 * x2) + (y2 * y2)));
            return Math.Acos(d);
        }

        public ArrayList GetElementsInSegment(string strSegment)
        {
            ArrayList list = new ArrayList();
            if (this.m_htSegment2PathElementPassive.ContainsKey(strSegment))
            {
                list = (ArrayList) this.m_htSegment2PathElementPassive[strSegment];
            }
            return list;
        }

        public Perron GetPerron(string strID)
        {
            return (Perron) this.m_htPerrons[strID];
        }

        public SIDISServerBase GetServer()
        {
            return null;
        }

        public ArrayList GetStateElements(string strAddress)
        {
            ArrayList list = new ArrayList();
            if (this.m_htAddress2StateElement.ContainsKey(strAddress))
            {
                list = (ArrayList) this.m_htAddress2StateElement[strAddress];
            }
            return list;
        }

        public TrainBase GetTrain(string strID)
        {
            return (TrainBase) this.m_htTrainID2Train[strID];
        }

        public bool HasTrain(string strTrainID)
        {
            return this.m_htTrainID2Train.ContainsKey(strTrainID);
        }

        public bool IsTrainTracked(string strTTTrainId, DateTime dtNow)
        {
            return this.m_TrackedTrainList.TrainTracked(strTTTrainId, dtNow);
        }

        public override bool LoadFile(string strFileName, string strConnect)
        {
            return false;
        }

        private Point RotateVector(Point o, double angleRadian)
        {
            Point point = new Point();
            point.set_X((o.get_X() * Math.Cos(angleRadian)) - (o.get_Y() * Math.Sin(angleRadian)));
            point.set_Y((o.get_X() * Math.Sin(angleRadian)) + (o.get_Y() * Math.Cos(angleRadian)));
            point.set_X((double) ((int) point.get_X()));
            point.set_Y((double) ((int) point.get_Y()));
            return point;
        }

        public bool SetPathElementSegmentState(string strAddress, PathElementPassive.PathState ps)
        {
            bool flag = true;
            if (this.m_htAddress2StateElement.ContainsKey(strAddress))
            {
                ArrayList list = (ArrayList) this.m_htAddress2StateElement[strAddress];
                foreach (StateElement element in list)
                {
                    if (element is PathElement)
                    {
                        PathElement element2 = element as PathElement;
                        element2.Defined = true;
                        flag &= element2.SetSegmentState(ps);
                    }
                }
                return flag;
            }
            return false;
        }

        public void SetTrainTracked(string strTTTrainId)
        {
            this.m_TrackedTrainList.AddTrain(strTTTrainId, this.GetServer().GetDateTimeNow());
        }

        public void SetTrainTracked(string strTTTrainId, DateTime dtNow)
        {
            this.m_TrackedTrainList.AddTrain(strTTTrainId, dtNow);
        }

        public int SortTrainsForDrawing(object obj1, object obj2)
        {
            Train train = obj1 as Train;
            Train train2 = obj2 as Train;
            if ((train == null) || (train2 == null))
            {
                return 0;
            }
            return train.IDToDisplay.CompareTo(train2.IDToDisplay);
        }

        private void TrainControl_OnTrainChangeNumber(string trainId)
        {
            if (this.OnTrainChangeNumber != null)
            {
                this.OnTrainChangeNumber(trainId);
            }
        }

        private void TrainControl_OnTrainProperties(Train t)
        {
            if (this.OnTrainProperties != null)
            {
                this.OnTrainProperties(t);
            }
        }

        private void TrainControl_OnTrainRoute(string trainId)
        {
            if (this.OnTrainRoute != null)
            {
                this.OnTrainRoute(trainId);
            }
        }

        public static Train TrainWebData2Train(TrainWebData twd)
        {
            return new Train { HeadOffset = twd.HeadOffset, MoveState = twd.AtPerron ? MoveState.WaitAtPerron : MoveState.MoveToPerron, Delay = TimeSpan.FromSeconds((double) twd.DelaySec), Direction = twd.Direction, HeadElementArrivedSource = twd.HeadElementArrivedSource, m_nNextElementSource = twd.NextElementSource, IDToDisplay = twd.IDToDisplay, ID = twd.ID, Locomotive = twd.Locomotive, Tooltip = twd.Tooltip, Description = twd.Description, HasTimetable = twd.HasTimetable, GPSLat = twd.GPSLat, GPSLon = twd.GPSLon, CarId = twd.CarId };
        }

        [XmlIgnore]
        public Hashtable Perrons
        {
            get
            {
                return this.m_htPerrons;
            }
        }

        [XmlIgnore]
        public TISMonitor.Stations Stations
        {
            get
            {
                return this.m_alStations;
            }
        }

        [XmlIgnore]
        public Hashtable Trains
        {
            get
            {
                return this.m_htTrainID2Train;
            }
        }

        public delegate void LegendClickedHandler(FrameworkElement twd);

        public delegate void TrainChangeNumberHandler(string trainID);

        private class TrainPossiblePlaces
        {
            public List<Rectangle> Places = new List<Rectangle>();

            public bool IntersectsWith(Layout.TrainPossiblePlaces placesB)
            {
                foreach (Rectangle rectangle in this.Places)
                {
                    foreach (Rectangle rectangle2 in placesB.Places)
                    {
                        if (rectangle.IntersectsWith(rectangle2))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public delegate void TrainPropertiesHandler(Train t);

        public delegate void TrainRouteHandler(string trainID);
    }
}

