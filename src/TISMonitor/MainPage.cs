namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.ServiceModel;
    using System.Windows;
    using System.Windows.Browser;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using TISMonitor.TISWebService;
    using TISServiceHelper;
    using TISWebServiceHelper;
    using UICommon;
    using WebdataLoader;

    public class MainPage : UserControl
    {
        private bool _contentLoaded;
        internal BusyIndicator busyIndicator;
        internal Canvas CanvasRoot;
        internal ScaleTransform CanvasScale;
        internal ScaleTransform CanvasScale2;
        internal Grid LayoutRoot;
        private bool m_bInsideTimerForTrains = false;
        private List<string> m_eventLog = new List<string>();
        private bool m_EventLogDialogOpened = false;
        private TrainGraphCache m_firstTrainGraphCache = null;
        private bool m_isFirstIteration = true;
        private DateTime m_LastShiftPressed = DateTime.MinValue;
        private TISLayout m_layout = null;
        private Size m_layoutSize = new Size(1200.0, 400.0);
        private bool m_needToFillTrainGraph = true;
        private ClientServerInfo m_serverInfo = new ClientServerInfo();
        private string m_strPartOfVersionName = "";
        private string m_strUserName = "";
        private TrainGraphData m_tgd = null;
        private DispatcherTimer m_timer = null;
        private TrainGraphCache m_trainGraphCache = new TrainGraphCache();
        private TrainsCache m_trainsCache = new TrainsCache();
        internal TextBlock textBlockApplicationViersion;
        private TrainGraphControl trainGraphControl = null;

        public MainPage(int nTrainGraphHourHeight, int nTrainGraphAutoScrollTimeout, int nMaxTimeGap, int nMaxPositionGap, string strPartOfVersionName, string userName)
        {
            this.m_strUserName = userName;
            this.m_strPartOfVersionName = strPartOfVersionName;
            base.add_MouseRightButtonDown(new MouseButtonEventHandler(this, (IntPtr) this.MainPage_MouseRightButtonDown));
            base.add_KeyDown(new KeyEventHandler(this, (IntPtr) this.MainPage_KeyDown));
            this.m_timer = new DispatcherTimer();
            this.m_timer.set_Interval(TimeSpan.FromSeconds(10.0));
            this.m_timer.add_Tick(new EventHandler(this.Timer_Tick));
            this.InitializeComponent();
            this.AddLog("Application started");
            this.trainGraphControl = new TrainGraphControl(nTrainGraphHourHeight, nTrainGraphAutoScrollTimeout, nMaxTimeGap, nMaxPositionGap);
            this.trainGraphControl.SetValue(Grid.RowProperty, 3);
            this.trainGraphControl.SetValue(Grid.ColumnProperty, 0);
            this.trainGraphControl.set_Margin(new Thickness(-4.0, 0.0, 0.0, 0.0));
            this.LayoutRoot.get_Children().Add(this.trainGraphControl);
            AssemblyName name = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
            this.textBlockApplicationViersion.set_Text(string.Format(" v.{0}", name.Version));
            this.TrainGraphStateLoadFromCache(this.CacheVersion);
            this.m_layoutSize = App.LayoutParameters.GetLayoutSize(this.m_layoutSize);
            Application.get_Current().get_Host().get_Content().add_Resized(new EventHandler(this.Content_Resized));
        }

        private void AddLog(string logMessage)
        {
            this.m_eventLog.Insert(0, string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), logMessage));
            if (this.m_eventLog.Count > 0x3e8)
            {
                while (this.m_eventLog.Count > 0x3e8)
                {
                    this.m_eventLog.RemoveAt(this.m_eventLog.Count - 1);
                }
            }
        }

        private void CanvasHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ShowEventLog();
        }

        private void CanvasRoot_Loaded(object sender, RoutedEventArgs e)
        {
            this.CanvasRoot.get_Children().Clear();
        }

        private void Content_Resized(object sender, EventArgs e)
        {
            double num = Application.get_Current().get_Host().get_Content().get_ActualHeight();
            double num2 = Application.get_Current().get_Host().get_Content().get_ActualWidth();
            if ((num != 0.0) && (num2 != 0.0))
            {
                if (num2 < this.m_layoutSize.get_Width())
                {
                    this.CanvasScale.set_ScaleX(num2 / this.m_layoutSize.get_Width());
                    this.CanvasScale.set_ScaleY(this.CanvasScale.get_ScaleX());
                    this.CanvasScale2.set_ScaleX(this.CanvasScale.get_ScaleX());
                    this.CanvasScale2.set_ScaleY(this.CanvasScale.get_ScaleX());
                }
                else
                {
                    this.CanvasScale.set_ScaleX(1.0);
                    this.CanvasScale.set_ScaleY(1.0);
                    this.CanvasScale2.set_ScaleX(1.0);
                    this.CanvasScale2.set_ScaleY(1.0);
                }
                this.LayoutRoot.get_RowDefinitions().get_Item(0).set_Height(new GridLength(5.0 * this.CanvasScale.get_ScaleX()));
                this.LayoutRoot.get_RowDefinitions().get_Item(1).set_Height(new GridLength(this.m_layoutSize.get_Height() * this.CanvasScale.get_ScaleX()));
                this.LayoutRoot.get_RowDefinitions().get_Item(2).set_Height(new GridLength(5.0 * this.CanvasScale.get_ScaleX()));
                double num3 = (num - ((this.m_layoutSize.get_Height() + 10.0) * this.CanvasScale.get_ScaleX())) / this.CanvasScale.get_ScaleX();
                if (num3 > 0.0)
                {
                    this.LayoutRoot.get_RowDefinitions().get_Item(3).set_Height(new GridLength(num3));
                    this.trainGraphControl.SetScrollViewerHeight(this.LayoutRoot.get_RowDefinitions().get_Item(3).get_Height().get_Value());
                }
                this.LayoutRoot.set_Height(num / this.CanvasScale.get_ScaleX());
            }
        }

        private void DrawTrains(List<TrainWebData> onlineTrains, List<TrainWebData> offlineTrains, List<TrainWebData> onlineButOnTheMapTrains)
        {
            this.m_layout.ClearTrainsFromCanvas();
            this.m_layout.DrawOfflineTrains(offlineTrains, new LegendLoaderOffline());
            this.m_layout.DrawOfflineTrains(onlineButOnTheMapTrains, new LegendLoaderOnline());
            this.m_layout.DrawOnlineTrains(onlineTrains);
            this.m_layout.DrawPerrons(onlineTrains);
        }

        private void EventLogDialog_Closed(object sender, EventArgs e)
        {
            this.m_EventLogDialogOpened = false;
        }

        private TISWebServiceSoapClient GetClient()
        {
            TISWebServiceSoapClient client = new TISWebServiceSoapClient();
            if (Application.get_Current().get_Host().get_Source().AbsoluteUri.StartsWith("https") && client.Endpoint.Address.Uri.AbsoluteUri.StartsWith("http"))
            {
                client.Endpoint.Address = new EndpointAddress(client.Endpoint.Address.Uri.AbsoluteUri.Replace("http", "https"));
                (client.Endpoint.Binding as BasicHttpBinding).Security.Mode = BasicHttpSecurityMode.Transport;
            }
            return client;
        }

        private List<string> GetPossibleTrainIds()
        {
            List<string> list = new List<string>();
            if ((this.trainGraphControl != null) && (this.trainGraphControl.RegularCurves != null))
            {
                foreach (CurveData data in this.trainGraphControl.RegularCurves)
                {
                    list.Add(data.Name);
                }
            }
            list.Sort();
            return list;
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/MainPage.xaml", UriKind.Relative));
                this.busyIndicator = (BusyIndicator) base.FindName("busyIndicator");
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.textBlockApplicationViersion = (TextBlock) base.FindName("textBlockApplicationViersion");
                this.CanvasRoot = (Canvas) base.FindName("CanvasRoot");
                this.CanvasScale = (ScaleTransform) base.FindName("CanvasScale");
                this.CanvasScale2 = (ScaleTransform) base.FindName("CanvasScale2");
            }
        }

        private void Layout_OnLayoutXML(string xml)
        {
            this.LoadLayoutFromString(xml);
        }

        private void Layout_OnLog(string xml)
        {
            this.AddLog(xml);
        }

        private void Layout_OnTrainChangeNumber(string trainId)
        {
            MessageBox.Show("Not implemented!");
        }

        private void Layout_OnTrainProperties(Train t)
        {
            List<string> possibleTrainIds = this.GetPossibleTrainIds();
            if (!possibleTrainIds.Contains(t.ID))
            {
                possibleTrainIds.Insert(0, t.ID);
            }
            Uri u = new Uri(string.Format("/TrainRoute.aspx?TrainId={0}", HttpUtility.UrlEncode(t.ID)), UriKind.Relative);
            TimeSpan span = (TimeSpan) (DateTime.Now - this.m_LastShiftPressed);
            bool enable = (span.TotalSeconds < 2.0) || (this.m_strUserName.Length > 0);
            TrainPropertiesDlg dlg = new TrainPropertiesDlg(t, possibleTrainIds, u, enable);
            dlg.Closed += new EventHandler(this.TrainPropertiesDlg_Closed);
            dlg.Show();
        }

        private void Layout_OnTrainRoute(string trainId)
        {
            new OpenLinkDlg(new Uri(string.Format("/TrainRoute.aspx?TrainId={0}", trainId), UriKind.Relative)).Show();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            LayoutLoaderHelper helper = new LayoutLoaderHelper(this.CacheVersion, App.LayoutParameters.GetLayoutFile());
            helper.OnLog += new LayoutLoaderHelper.LogHandler(this.Layout_OnLog);
            helper.OnLayoutXML += new LayoutLoaderHelper.LayoutXMLHandler(this.Layout_OnLayoutXML);
            helper.LoadLayout();
        }

        private void LoadLayoutFromString(string xmlFile)
        {
            try
            {
                this.m_layout = new TISLayout();
                this.m_layout.g = this.CanvasRoot;
                this.m_layout.OnTrainChangeNumber += new Layout.TrainChangeNumberHandler(this.Layout_OnTrainChangeNumber);
                this.m_layout.OnTrainRoute += new Layout.TrainRouteHandler(this.Layout_OnTrainRoute);
                this.m_layout.OnTrainProperties += new Layout.TrainPropertiesHandler(this.Layout_OnTrainProperties);
                if (this.m_layout.LoadFromString(xmlFile))
                {
                    this.m_layout.DrawElements();
                    this.TrainsStateLoadFromCache(this.CacheVersion);
                    this.OnTimerGetState();
                    this.m_timer.Start();
                    this.Content_Resized(null, null);
                }
                else
                {
                    MessageBox.Show("Failed to load layout..." + this.m_layout.LastError);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.get_Key() == 4)
            {
                this.m_LastShiftPressed = DateTime.Now;
            }
        }

        private void MainPage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.set_Handled(true);
        }

        private List<CurveData> MergeCachedAndRealTimesCurves(List<CurveData> cache, List<CurveData> realtime)
        {
            List<CurveData> list = new List<CurveData>();
            Dictionary<string, CurveData> dictionary = new Dictionary<string, CurveData>();
            Dictionary<string, CurveData> dictionary2 = new Dictionary<string, CurveData>();
            foreach (CurveData data in realtime)
            {
                dictionary[data.Key] = data;
            }
            foreach (CurveData data in cache)
            {
                dictionary2[data.Key] = data;
                if (dictionary.ContainsKey(data.Key))
                {
                    CurveData data2 = dictionary[data.Key];
                    data.Points.AddRange(data2.Points);
                }
                list.Add(data);
            }
            foreach (CurveData data in realtime)
            {
                if (!dictionary2.ContainsKey(data.Key))
                {
                    list.Add(data);
                }
            }
            return list;
        }

        private void OnTimerGetState()
        {
            if (!this.m_bInsideTimerForTrains)
            {
                this.m_bInsideTimerForTrains = true;
                TISWebServiceSoapClient client = this.GetClient();
                client.GetStateCompleted += new EventHandler<GetStateCompletedEventArgs>(this.TISWebService_GetStateCompleted);
                TISWebServiceGetStateSupportIN tin = new TISWebServiceGetStateSupportIN {
                    clientLastRealTimeTrainGraphTime = this.m_serverInfo.LastTrainGraphTime,
                    getRegularTraingraphData = this.m_needToFillTrainGraph,
                    traingraphStart = (this.m_tgd != null) ? this.m_tgd.TimeStart : DateTime.MinValue,
                    traingraphStop = (this.m_tgd != null) ? this.m_tgd.TimeStop : DateTime.MinValue
                };
                string strIn = DCSerializer.SerializeWithDCS(tin);
                if (Application.get_Current().get_Host().get_Source().AbsoluteUri.StartsWith("https") && client.Endpoint.Address.Uri.AbsoluteUri.StartsWith("http"))
                {
                    client.Endpoint.Address = new EndpointAddress(client.Endpoint.Address.Uri.AbsoluteUri.Replace("http", "https"));
                    (client.Endpoint.Binding as BasicHttpBinding).Security.Mode = BasicHttpSecurityMode.Transport;
                }
                client.GetStateAsync(strIn);
            }
        }

        private void ResetTrainGraph()
        {
            this.m_serverInfo = new ClientServerInfo();
            this.m_tgd = null;
            this.m_needToFillTrainGraph = true;
            this.m_isFirstIteration = true;
            this.ResetTrainGraphCache();
        }

        private void ResetTrainGraphCache()
        {
            this.m_trainGraphCache = new TrainGraphCache();
            this.m_trainGraphCache.Version = this.CacheVersion;
            this.m_firstTrainGraphCache = null;
        }

        private void SetOperationDate(DateTime dtOperationDate, bool allowToReset)
        {
            if (allowToReset && (this.m_serverInfo.OperationDate != dtOperationDate))
            {
                this.ResetTrainGraph();
            }
            this.m_serverInfo.OperationDate = dtOperationDate;
        }

        private void ShowEventLog()
        {
            if (!this.m_EventLogDialogOpened)
            {
                this.m_EventLogDialogOpened = true;
                EventLogDialog dialog = new EventLogDialog(this.m_eventLog);
                dialog.Closed += new EventHandler(this.EventLogDialog_Closed);
                dialog.Show();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.OnTimerGetState();
        }

        private void TISWebService_GetStateCompleted(object sender, GetStateCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    throw new SLException(e.Error.Message + " TISMonitor from server which failed");
                }
                if (!e.Result)
                {
                    throw new SLException(e.strError + " TISMonitor from server");
                }
                TISWebServiceGetStateSupportOUT tout = DCSerializer.DeserializeWithDCS(typeof(TISWebServiceGetStateSupportOUT), e.strOut) as TISWebServiceGetStateSupportOUT;
                this.m_serverInfo.LastTrainGraphTime = tout.actualRealTimeTrainGraphTime;
                this.m_serverInfo.ServerTime = tout.ServerTime;
                List<TrainWebData> onlineTrains = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), e.onlineTrainsData) as List<TrainWebData>;
                List<TrainWebData> onlineButOnTheMapTrains = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), e.onlineButNotOnTheMapTrainsData) as List<TrainWebData>;
                List<TrainWebData> offlineTrains = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), e.offlineTrainsData) as List<TrainWebData>;
                this.DrawTrains(onlineTrains, offlineTrains, onlineButOnTheMapTrains);
                TrainGraphData data = DCSerializer.DeserializeWithDCS(typeof(TrainGraphData), e.trainGraphData) as TrainGraphData;
                if (this.m_needToFillTrainGraph)
                {
                    this.trainGraphControl.ResetData();
                }
                bool needToFillTrainGraph = this.m_needToFillTrainGraph;
                bool flag2 = false;
                if (this.m_firstTrainGraphCache != null)
                {
                    flag2 = true;
                    needToFillTrainGraph = true;
                    data.Curves = this.MergeCachedAndRealTimesCurves(this.m_firstTrainGraphCache.Curves, data.Curves);
                    data.Markers.Clear();
                    data.Markers.AddRange(this.m_firstTrainGraphCache.Markers);
                    this.m_firstTrainGraphCache = null;
                }
                this.m_tgd = data;
                this.trainGraphControl.TrainGraphData = this.m_tgd;
                this.trainGraphControl.ActualTime = this.m_serverInfo.ServerTime;
                this.trainGraphControl.Draw(needToFillTrainGraph);
                if (this.m_needToFillTrainGraph)
                {
                    this.m_needToFillTrainGraph = false;
                }
                this.UpdateTrainGraphCache(this.m_tgd, this.m_serverInfo);
                this.UpdateTrainsCache(onlineTrains);
                this.SetOperationDate(tout.ServerOperationDate, !this.m_isFirstIteration || flag2);
                this.m_isFirstIteration = false;
                ErrorDlg.HelperClose();
            }
            catch (SLException exception)
            {
                this.AddLog("TISMonitor GetState error custom: " + exception.Message);
                ErrorDlg.HelperShow("", exception.Message);
            }
            catch (Exception exception2)
            {
                this.AddLog("TISMonitor GetState error: " + exception2.Message);
                ErrorDlg.HelperShow("", exception2.Message);
                this.ResetTrainGraph();
            }
            this.busyIndicator.IsBusy = false;
            this.m_bInsideTimerForTrains = false;
        }

        private void TISWebService_SetTrainDataCompleted(object sender, SetTrainDataCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    throw new Exception(e.Error.Message);
                }
                if (!e.Result)
                {
                    throw new Exception(e.strError);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("SetTrainData failed: " + exception.Message, "", 0);
                this.AddLog("SetTrainData failed: " + exception.Message);
            }
        }

        private void TrainGraphStateLoadFromCache(string version)
        {
            TrainGraphCache cache = new TrainGraphCache();
            if (cache.Load(version))
            {
                TrainGraphCache cache2 = cache;
                if (cache2 == null)
                {
                    this.AddLog("TrainGraphCache was not loaded: Points.Cout == 0");
                }
                else
                {
                    this.m_needToFillTrainGraph = false;
                    this.m_tgd = new TrainGraphData();
                    this.m_tgd.TimeStart = cache2.TimeStart;
                    this.m_tgd.TimeStop = cache2.TimeStop;
                    this.m_serverInfo.OperationDate = cache2.OperationDate;
                    this.m_serverInfo.ServerTime = cache2.ServerTime;
                    this.m_serverInfo.LastTrainGraphTime = cache2.LastTrainGraphTime;
                    this.m_firstTrainGraphCache = cache2;
                }
            }
            else
            {
                this.AddLog("TrainGraphCache was not loaded: " + cache.LastError);
            }
        }

        private void TrainPropertiesDlg_Closed(object sender, EventArgs e)
        {
            TrainPropertiesDlg dlg = sender as TrainPropertiesDlg;
            if ((dlg != null) && !(!dlg.DialogResult.HasValue ? true : !dlg.DialogResult.Value))
            {
                TISWebServiceSoapClient client = this.GetClient();
                client.SetTrainDataCompleted += new EventHandler<SetTrainDataCompletedEventArgs>(this.TISWebService_SetTrainDataCompleted);
                client.SetTrainDataAsync(dlg.Train.ID, dlg.TrainIdNew, dlg.Locomotive, dlg.Description, this.m_strUserName);
            }
        }

        private void TrainsStateLoadFromCache(string version)
        {
            TrainsCache cache = new TrainsCache();
            if (cache.Load(version))
            {
                this.DrawTrains(cache.Trains, new List<TrainWebData>(), new List<TrainWebData>());
            }
            else
            {
                this.AddLog("TrainsCache was not loaded: " + cache.LastError);
            }
        }

        private void UpdateTrainGraphCache(TrainGraphData tgd, ClientServerInfo serverInfo)
        {
            this.m_trainGraphCache.Version = this.CacheVersion;
            this.m_trainGraphCache.CacheTime = DateTime.Now;
            this.m_trainGraphCache.TimeStart = tgd.TimeStart;
            this.m_trainGraphCache.TimeStop = tgd.TimeStop;
            if (tgd.Markers.Count > 0)
            {
                this.m_trainGraphCache.Markers = tgd.Markers;
            }
            if (tgd.Curves.Count > 0)
            {
                this.m_trainGraphCache.Curves = this.MergeCachedAndRealTimesCurves(this.m_trainGraphCache.Curves, tgd.Curves);
            }
            this.m_trainGraphCache.LastTrainGraphTime = serverInfo.LastTrainGraphTime;
            this.m_trainGraphCache.OperationDate = serverInfo.OperationDate;
            this.m_trainGraphCache.ServerTime = serverInfo.ServerTime;
            if (!this.m_trainGraphCache.Save())
            {
                this.AddLog("TisMonitor did not save TrainGraphCache: " + this.m_trainGraphCache.LastError);
                IsolatedStorageHelper.IncreaseTo(10);
            }
        }

        private void UpdateTrainsCache(List<TrainWebData> l)
        {
            this.m_trainsCache.Version = this.CacheVersion;
            this.m_trainsCache.CacheTime = DateTime.Now;
            this.m_trainsCache.Trains = l;
            if (!this.m_trainsCache.Save())
            {
                this.AddLog("TisMonitor did not save TrainsCache: " + this.m_trainsCache.LastError);
                IsolatedStorageHelper.IncreaseTo(10);
            }
        }

        private string CacheVersion
        {
            get
            {
                return (this.m_strPartOfVersionName + this.textBlockApplicationViersion.get_Text());
            }
        }
    }
}

