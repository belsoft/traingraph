namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough]
    public class TISWebServiceSoapClient : ClientBase<TISWebServiceSoap>, TISWebServiceSoap
    {
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginCloseDelegate;
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginGetCarOperationStatisticsDelegate;
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginGetDevicesDelegate;
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginGetGPSDeviceStatisticsDelegate;
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginGetGPSDeviceStatisticsInitUIDelegate;
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginGetStateDelegate;
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginGetTrainRouteHtmlDelegate;
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginGetTrainsOnlineDelegate;
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginOpenDelegate;
        private ClientBase<TISWebServiceSoap>.BeginOperationDelegate onBeginSetTrainDataDelegate;
        private SendOrPostCallback onCloseCompletedDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndCloseDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndGetCarOperationStatisticsDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndGetDevicesDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndGetGPSDeviceStatisticsDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndGetGPSDeviceStatisticsInitUIDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndGetStateDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndGetTrainRouteHtmlDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndGetTrainsOnlineDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndOpenDelegate;
        private ClientBase<TISWebServiceSoap>.EndOperationDelegate onEndSetTrainDataDelegate;
        private SendOrPostCallback onGetCarOperationStatisticsCompletedDelegate;
        private SendOrPostCallback onGetDevicesCompletedDelegate;
        private SendOrPostCallback onGetGPSDeviceStatisticsCompletedDelegate;
        private SendOrPostCallback onGetGPSDeviceStatisticsInitUICompletedDelegate;
        private SendOrPostCallback onGetStateCompletedDelegate;
        private SendOrPostCallback onGetTrainRouteHtmlCompletedDelegate;
        private SendOrPostCallback onGetTrainsOnlineCompletedDelegate;
        private SendOrPostCallback onOpenCompletedDelegate;
        private SendOrPostCallback onSetTrainDataCompletedDelegate;

        public event EventHandler<AsyncCompletedEventArgs> CloseCompleted;

        public event EventHandler<GetCarOperationStatisticsCompletedEventArgs> GetCarOperationStatisticsCompleted;

        public event EventHandler<GetDevicesCompletedEventArgs> GetDevicesCompleted;

        public event EventHandler<GetGPSDeviceStatisticsCompletedEventArgs> GetGPSDeviceStatisticsCompleted;

        public event EventHandler<GetGPSDeviceStatisticsInitUICompletedEventArgs> GetGPSDeviceStatisticsInitUICompleted;

        public event EventHandler<GetStateCompletedEventArgs> GetStateCompleted;

        public event EventHandler<GetTrainRouteHtmlCompletedEventArgs> GetTrainRouteHtmlCompleted;

        public event EventHandler<GetTrainsOnlineCompletedEventArgs> GetTrainsOnlineCompleted;

        public event EventHandler<AsyncCompletedEventArgs> OpenCompleted;

        public event EventHandler<SetTrainDataCompletedEventArgs> SetTrainDataCompleted;

        public TISWebServiceSoapClient()
        {
        }

        public TISWebServiceSoapClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        public TISWebServiceSoapClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        public TISWebServiceSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        public TISWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private IAsyncResult BeginGetCarOperationStatistics(string strFrom, string strTo, AsyncCallback callback, object asyncState)
        {
            GetCarOperationStatisticsRequest request = new GetCarOperationStatisticsRequest {
                Body = new GetCarOperationStatisticsRequestBody()
            };
            request.Body.strFrom = strFrom;
            request.Body.strTo = strTo;
            return ((TISWebServiceSoap) this).BeginGetCarOperationStatistics(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private IAsyncResult BeginGetDevices(AsyncCallback callback, object asyncState)
        {
            GetDevicesRequest request = new GetDevicesRequest {
                Body = new GetDevicesRequestBody()
            };
            return ((TISWebServiceSoap) this).BeginGetDevices(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private IAsyncResult BeginGetGPSDeviceStatistics(string deviceId, string dateTimeFrom, string dateTimeTo, int maxOutputRange, AsyncCallback callback, object asyncState)
        {
            GetGPSDeviceStatisticsRequest request = new GetGPSDeviceStatisticsRequest {
                Body = new GetGPSDeviceStatisticsRequestBody()
            };
            request.Body.deviceId = deviceId;
            request.Body.dateTimeFrom = dateTimeFrom;
            request.Body.dateTimeTo = dateTimeTo;
            request.Body.maxOutputRange = maxOutputRange;
            return ((TISWebServiceSoap) this).BeginGetGPSDeviceStatistics(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private IAsyncResult BeginGetGPSDeviceStatisticsInitUI(AsyncCallback callback, object asyncState)
        {
            GetGPSDeviceStatisticsInitUIRequest request = new GetGPSDeviceStatisticsInitUIRequest {
                Body = new GetGPSDeviceStatisticsInitUIRequestBody()
            };
            return ((TISWebServiceSoap) this).BeginGetGPSDeviceStatisticsInitUI(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private IAsyncResult BeginGetState(string strIn, AsyncCallback callback, object asyncState)
        {
            GetStateRequest request = new GetStateRequest {
                Body = new GetStateRequestBody()
            };
            request.Body.strIn = strIn;
            return ((TISWebServiceSoap) this).BeginGetState(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private IAsyncResult BeginGetTrainRouteHtml(string trainId, AsyncCallback callback, object asyncState)
        {
            GetTrainRouteHtmlRequest request = new GetTrainRouteHtmlRequest {
                Body = new GetTrainRouteHtmlRequestBody()
            };
            request.Body.trainId = trainId;
            return ((TISWebServiceSoap) this).BeginGetTrainRouteHtml(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private IAsyncResult BeginGetTrainsOnline(AsyncCallback callback, object asyncState)
        {
            GetTrainsOnlineRequest request = new GetTrainsOnlineRequest {
                Body = new GetTrainsOnlineRequestBody()
            };
            return ((TISWebServiceSoap) this).BeginGetTrainsOnline(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private IAsyncResult BeginSetTrainData(string trainIdOld, string trainIdNew, bool goodsTrain, string trainDescription, string userName, AsyncCallback callback, object asyncState)
        {
            SetTrainDataRequest request = new SetTrainDataRequest {
                Body = new SetTrainDataRequestBody()
            };
            request.Body.trainIdOld = trainIdOld;
            request.Body.trainIdNew = trainIdNew;
            request.Body.goodsTrain = goodsTrain;
            request.Body.trainDescription = trainDescription;
            request.Body.userName = userName;
            return ((TISWebServiceSoap) this).BeginSetTrainData(request, callback, asyncState);
        }

        public void CloseAsync()
        {
            this.CloseAsync(null);
        }

        public void CloseAsync(object userState)
        {
            if (this.onBeginCloseDelegate == null)
            {
                this.onBeginCloseDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginClose);
            }
            if (this.onEndCloseDelegate == null)
            {
                this.onEndCloseDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndClose);
            }
            if (this.onCloseCompletedDelegate == null)
            {
                this.onCloseCompletedDelegate = new SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }

        protected override TISWebServiceSoap CreateChannel()
        {
            return new TISWebServiceSoapClientChannel(this);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private bool EndGetCarOperationStatistics(IAsyncResult result, out ObservableCollection<OnlineOfflineStatisticsEntry> webList, out string reportRangeTitle, out string strError)
        {
            GetCarOperationStatisticsResponse response = ((TISWebServiceSoap) this).EndGetCarOperationStatistics(result);
            webList = response.Body.webList;
            reportRangeTitle = response.Body.reportRangeTitle;
            strError = response.Body.strError;
            return response.Body.GetCarOperationStatisticsResult;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private bool EndGetDevices(IAsyncResult result, out ObservableCollection<Car> cars, out string strError)
        {
            GetDevicesResponse response = ((TISWebServiceSoap) this).EndGetDevices(result);
            cars = response.Body.cars;
            strError = response.Body.strError;
            return response.Body.GetDevicesResult;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private bool EndGetGPSDeviceStatistics(IAsyncResult result, out ObservableCollection<TrainGPSStatisticsWebData> listTrainGPSStatistic, out string strError)
        {
            GetGPSDeviceStatisticsResponse response = ((TISWebServiceSoap) this).EndGetGPSDeviceStatistics(result);
            listTrainGPSStatistic = response.Body.listTrainGPSStatistic;
            strError = response.Body.strError;
            return response.Body.GetGPSDeviceStatisticsResult;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private bool EndGetGPSDeviceStatisticsInitUI(IAsyncResult result, out bool historyExists, out ArrayOfString availableDevicesInHistory, out string dateTimeFrom, out string dateTimeTo, out string strError)
        {
            GetGPSDeviceStatisticsInitUIResponse response = ((TISWebServiceSoap) this).EndGetGPSDeviceStatisticsInitUI(result);
            historyExists = response.Body.historyExists;
            availableDevicesInHistory = response.Body.availableDevicesInHistory;
            dateTimeFrom = response.Body.dateTimeFrom;
            dateTimeTo = response.Body.dateTimeTo;
            strError = response.Body.strError;
            return response.Body.GetGPSDeviceStatisticsInitUIResult;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private bool EndGetState(IAsyncResult result, out string strOut, out string onlineTrainsData, out string offlineTrainsData, out string onlineButNotOnTheMapTrainsData, out string trainGraphData, out string strError)
        {
            GetStateResponse response = ((TISWebServiceSoap) this).EndGetState(result);
            strOut = response.Body.strOut;
            onlineTrainsData = response.Body.onlineTrainsData;
            offlineTrainsData = response.Body.offlineTrainsData;
            onlineButNotOnTheMapTrainsData = response.Body.onlineButNotOnTheMapTrainsData;
            trainGraphData = response.Body.trainGraphData;
            strError = response.Body.strError;
            return response.Body.GetStateResult;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private bool EndGetTrainRouteHtml(IAsyncResult result, out string html, out string strError)
        {
            GetTrainRouteHtmlResponse response = ((TISWebServiceSoap) this).EndGetTrainRouteHtml(result);
            html = response.Body.html;
            strError = response.Body.strError;
            return response.Body.GetTrainRouteHtmlResult;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private bool EndGetTrainsOnline(IAsyncResult result, out ObservableCollection<TrainWebData> listTrains, out string strError)
        {
            GetTrainsOnlineResponse response = ((TISWebServiceSoap) this).EndGetTrainsOnline(result);
            listTrains = response.Body.listTrains;
            strError = response.Body.strError;
            return response.Body.GetTrainsOnlineResult;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private bool EndSetTrainData(IAsyncResult result, out string strError)
        {
            SetTrainDataResponse response = ((TISWebServiceSoap) this).EndSetTrainData(result);
            strError = response.Body.strError;
            return response.Body.SetTrainDataResult;
        }

        public void GetCarOperationStatisticsAsync(string strFrom, string strTo)
        {
            this.GetCarOperationStatisticsAsync(strFrom, strTo, null);
        }

        public void GetCarOperationStatisticsAsync(string strFrom, string strTo, object userState)
        {
            if (this.onBeginGetCarOperationStatisticsDelegate == null)
            {
                this.onBeginGetCarOperationStatisticsDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginGetCarOperationStatistics);
            }
            if (this.onEndGetCarOperationStatisticsDelegate == null)
            {
                this.onEndGetCarOperationStatisticsDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndGetCarOperationStatistics);
            }
            if (this.onGetCarOperationStatisticsCompletedDelegate == null)
            {
                this.onGetCarOperationStatisticsCompletedDelegate = new SendOrPostCallback(this.OnGetCarOperationStatisticsCompleted);
            }
            base.InvokeAsync(this.onBeginGetCarOperationStatisticsDelegate, new object[] { strFrom, strTo }, this.onEndGetCarOperationStatisticsDelegate, this.onGetCarOperationStatisticsCompletedDelegate, userState);
        }

        public void GetDevicesAsync()
        {
            this.GetDevicesAsync(null);
        }

        public void GetDevicesAsync(object userState)
        {
            if (this.onBeginGetDevicesDelegate == null)
            {
                this.onBeginGetDevicesDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginGetDevices);
            }
            if (this.onEndGetDevicesDelegate == null)
            {
                this.onEndGetDevicesDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndGetDevices);
            }
            if (this.onGetDevicesCompletedDelegate == null)
            {
                this.onGetDevicesCompletedDelegate = new SendOrPostCallback(this.OnGetDevicesCompleted);
            }
            base.InvokeAsync(this.onBeginGetDevicesDelegate, null, this.onEndGetDevicesDelegate, this.onGetDevicesCompletedDelegate, userState);
        }

        public void GetGPSDeviceStatisticsAsync(string deviceId, string dateTimeFrom, string dateTimeTo, int maxOutputRange)
        {
            this.GetGPSDeviceStatisticsAsync(deviceId, dateTimeFrom, dateTimeTo, maxOutputRange, null);
        }

        public void GetGPSDeviceStatisticsAsync(string deviceId, string dateTimeFrom, string dateTimeTo, int maxOutputRange, object userState)
        {
            if (this.onBeginGetGPSDeviceStatisticsDelegate == null)
            {
                this.onBeginGetGPSDeviceStatisticsDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginGetGPSDeviceStatistics);
            }
            if (this.onEndGetGPSDeviceStatisticsDelegate == null)
            {
                this.onEndGetGPSDeviceStatisticsDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndGetGPSDeviceStatistics);
            }
            if (this.onGetGPSDeviceStatisticsCompletedDelegate == null)
            {
                this.onGetGPSDeviceStatisticsCompletedDelegate = new SendOrPostCallback(this.OnGetGPSDeviceStatisticsCompleted);
            }
            base.InvokeAsync(this.onBeginGetGPSDeviceStatisticsDelegate, new object[] { deviceId, dateTimeFrom, dateTimeTo, maxOutputRange }, this.onEndGetGPSDeviceStatisticsDelegate, this.onGetGPSDeviceStatisticsCompletedDelegate, userState);
        }

        public void GetGPSDeviceStatisticsInitUIAsync()
        {
            this.GetGPSDeviceStatisticsInitUIAsync(null);
        }

        public void GetGPSDeviceStatisticsInitUIAsync(object userState)
        {
            if (this.onBeginGetGPSDeviceStatisticsInitUIDelegate == null)
            {
                this.onBeginGetGPSDeviceStatisticsInitUIDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginGetGPSDeviceStatisticsInitUI);
            }
            if (this.onEndGetGPSDeviceStatisticsInitUIDelegate == null)
            {
                this.onEndGetGPSDeviceStatisticsInitUIDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndGetGPSDeviceStatisticsInitUI);
            }
            if (this.onGetGPSDeviceStatisticsInitUICompletedDelegate == null)
            {
                this.onGetGPSDeviceStatisticsInitUICompletedDelegate = new SendOrPostCallback(this.OnGetGPSDeviceStatisticsInitUICompleted);
            }
            base.InvokeAsync(this.onBeginGetGPSDeviceStatisticsInitUIDelegate, null, this.onEndGetGPSDeviceStatisticsInitUIDelegate, this.onGetGPSDeviceStatisticsInitUICompletedDelegate, userState);
        }

        public void GetStateAsync(string strIn)
        {
            this.GetStateAsync(strIn, null);
        }

        public void GetStateAsync(string strIn, object userState)
        {
            if (this.onBeginGetStateDelegate == null)
            {
                this.onBeginGetStateDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginGetState);
            }
            if (this.onEndGetStateDelegate == null)
            {
                this.onEndGetStateDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndGetState);
            }
            if (this.onGetStateCompletedDelegate == null)
            {
                this.onGetStateCompletedDelegate = new SendOrPostCallback(this.OnGetStateCompleted);
            }
            base.InvokeAsync(this.onBeginGetStateDelegate, new object[] { strIn }, this.onEndGetStateDelegate, this.onGetStateCompletedDelegate, userState);
        }

        public void GetTrainRouteHtmlAsync(string trainId)
        {
            this.GetTrainRouteHtmlAsync(trainId, null);
        }

        public void GetTrainRouteHtmlAsync(string trainId, object userState)
        {
            if (this.onBeginGetTrainRouteHtmlDelegate == null)
            {
                this.onBeginGetTrainRouteHtmlDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginGetTrainRouteHtml);
            }
            if (this.onEndGetTrainRouteHtmlDelegate == null)
            {
                this.onEndGetTrainRouteHtmlDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndGetTrainRouteHtml);
            }
            if (this.onGetTrainRouteHtmlCompletedDelegate == null)
            {
                this.onGetTrainRouteHtmlCompletedDelegate = new SendOrPostCallback(this.OnGetTrainRouteHtmlCompleted);
            }
            base.InvokeAsync(this.onBeginGetTrainRouteHtmlDelegate, new object[] { trainId }, this.onEndGetTrainRouteHtmlDelegate, this.onGetTrainRouteHtmlCompletedDelegate, userState);
        }

        public void GetTrainsOnlineAsync()
        {
            this.GetTrainsOnlineAsync(null);
        }

        public void GetTrainsOnlineAsync(object userState)
        {
            if (this.onBeginGetTrainsOnlineDelegate == null)
            {
                this.onBeginGetTrainsOnlineDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginGetTrainsOnline);
            }
            if (this.onEndGetTrainsOnlineDelegate == null)
            {
                this.onEndGetTrainsOnlineDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndGetTrainsOnline);
            }
            if (this.onGetTrainsOnlineCompletedDelegate == null)
            {
                this.onGetTrainsOnlineCompletedDelegate = new SendOrPostCallback(this.OnGetTrainsOnlineCompleted);
            }
            base.InvokeAsync(this.onBeginGetTrainsOnlineDelegate, null, this.onEndGetTrainsOnlineDelegate, this.onGetTrainsOnlineCompletedDelegate, userState);
        }

        private IAsyncResult OnBeginClose(object[] inValues, AsyncCallback callback, object asyncState)
        {
            return this.BeginClose(callback, asyncState);
        }

        private IAsyncResult OnBeginGetCarOperationStatistics(object[] inValues, AsyncCallback callback, object asyncState)
        {
            string strFrom = (string) inValues[0];
            string strTo = (string) inValues[1];
            return this.BeginGetCarOperationStatistics(strFrom, strTo, callback, asyncState);
        }

        private IAsyncResult OnBeginGetDevices(object[] inValues, AsyncCallback callback, object asyncState)
        {
            return this.BeginGetDevices(callback, asyncState);
        }

        private IAsyncResult OnBeginGetGPSDeviceStatistics(object[] inValues, AsyncCallback callback, object asyncState)
        {
            string deviceId = (string) inValues[0];
            string dateTimeFrom = (string) inValues[1];
            string dateTimeTo = (string) inValues[2];
            int maxOutputRange = (int) inValues[3];
            return this.BeginGetGPSDeviceStatistics(deviceId, dateTimeFrom, dateTimeTo, maxOutputRange, callback, asyncState);
        }

        private IAsyncResult OnBeginGetGPSDeviceStatisticsInitUI(object[] inValues, AsyncCallback callback, object asyncState)
        {
            return this.BeginGetGPSDeviceStatisticsInitUI(callback, asyncState);
        }

        private IAsyncResult OnBeginGetState(object[] inValues, AsyncCallback callback, object asyncState)
        {
            string strIn = (string) inValues[0];
            return this.BeginGetState(strIn, callback, asyncState);
        }

        private IAsyncResult OnBeginGetTrainRouteHtml(object[] inValues, AsyncCallback callback, object asyncState)
        {
            string trainId = (string) inValues[0];
            return this.BeginGetTrainRouteHtml(trainId, callback, asyncState);
        }

        private IAsyncResult OnBeginGetTrainsOnline(object[] inValues, AsyncCallback callback, object asyncState)
        {
            return this.BeginGetTrainsOnline(callback, asyncState);
        }

        private IAsyncResult OnBeginOpen(object[] inValues, AsyncCallback callback, object asyncState)
        {
            return this.BeginOpen(callback, asyncState);
        }

        private IAsyncResult OnBeginSetTrainData(object[] inValues, AsyncCallback callback, object asyncState)
        {
            string trainIdOld = (string) inValues[0];
            string trainIdNew = (string) inValues[1];
            bool goodsTrain = (bool) inValues[2];
            string trainDescription = (string) inValues[3];
            string userName = (string) inValues[4];
            return this.BeginSetTrainData(trainIdOld, trainIdNew, goodsTrain, trainDescription, userName, callback, asyncState);
        }

        private void OnCloseCompleted(object state)
        {
            if (this.CloseCompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.CloseCompleted(this, new AsyncCompletedEventArgs(args.Error, args.Cancelled, args.UserState));
            }
        }

        private object[] OnEndClose(IAsyncResult result)
        {
            this.EndClose(result);
            return null;
        }

        private object[] OnEndGetCarOperationStatistics(IAsyncResult result)
        {
            ObservableCollection<OnlineOfflineStatisticsEntry> defaultValueForInitialization = base.GetDefaultValueForInitialization<ObservableCollection<OnlineOfflineStatisticsEntry>>();
            string reportRangeTitle = base.GetDefaultValueForInitialization<string>();
            string strError = base.GetDefaultValueForInitialization<string>();
            bool flag = this.EndGetCarOperationStatistics(result, out defaultValueForInitialization, out reportRangeTitle, out strError);
            return new object[] { defaultValueForInitialization, reportRangeTitle, strError, flag };
        }

        private object[] OnEndGetDevices(IAsyncResult result)
        {
            ObservableCollection<Car> defaultValueForInitialization = base.GetDefaultValueForInitialization<ObservableCollection<Car>>();
            string strError = base.GetDefaultValueForInitialization<string>();
            bool flag = this.EndGetDevices(result, out defaultValueForInitialization, out strError);
            return new object[] { defaultValueForInitialization, strError, flag };
        }

        private object[] OnEndGetGPSDeviceStatistics(IAsyncResult result)
        {
            ObservableCollection<TrainGPSStatisticsWebData> defaultValueForInitialization = base.GetDefaultValueForInitialization<ObservableCollection<TrainGPSStatisticsWebData>>();
            string strError = base.GetDefaultValueForInitialization<string>();
            bool flag = this.EndGetGPSDeviceStatistics(result, out defaultValueForInitialization, out strError);
            return new object[] { defaultValueForInitialization, strError, flag };
        }

        private object[] OnEndGetGPSDeviceStatisticsInitUI(IAsyncResult result)
        {
            bool defaultValueForInitialization = base.GetDefaultValueForInitialization<bool>();
            ArrayOfString availableDevicesInHistory = base.GetDefaultValueForInitialization<ArrayOfString>();
            string dateTimeFrom = base.GetDefaultValueForInitialization<string>();
            string dateTimeTo = base.GetDefaultValueForInitialization<string>();
            string strError = base.GetDefaultValueForInitialization<string>();
            bool flag2 = this.EndGetGPSDeviceStatisticsInitUI(result, out defaultValueForInitialization, out availableDevicesInHistory, out dateTimeFrom, out dateTimeTo, out strError);
            return new object[] { defaultValueForInitialization, availableDevicesInHistory, dateTimeFrom, dateTimeTo, strError, flag2 };
        }

        private object[] OnEndGetState(IAsyncResult result)
        {
            string defaultValueForInitialization = base.GetDefaultValueForInitialization<string>();
            string onlineTrainsData = base.GetDefaultValueForInitialization<string>();
            string offlineTrainsData = base.GetDefaultValueForInitialization<string>();
            string onlineButNotOnTheMapTrainsData = base.GetDefaultValueForInitialization<string>();
            string trainGraphData = base.GetDefaultValueForInitialization<string>();
            string strError = base.GetDefaultValueForInitialization<string>();
            bool flag = this.EndGetState(result, out defaultValueForInitialization, out onlineTrainsData, out offlineTrainsData, out onlineButNotOnTheMapTrainsData, out trainGraphData, out strError);
            return new object[] { defaultValueForInitialization, onlineTrainsData, offlineTrainsData, onlineButNotOnTheMapTrainsData, trainGraphData, strError, flag };
        }

        private object[] OnEndGetTrainRouteHtml(IAsyncResult result)
        {
            string defaultValueForInitialization = base.GetDefaultValueForInitialization<string>();
            string strError = base.GetDefaultValueForInitialization<string>();
            bool flag = this.EndGetTrainRouteHtml(result, out defaultValueForInitialization, out strError);
            return new object[] { defaultValueForInitialization, strError, flag };
        }

        private object[] OnEndGetTrainsOnline(IAsyncResult result)
        {
            ObservableCollection<TrainWebData> defaultValueForInitialization = base.GetDefaultValueForInitialization<ObservableCollection<TrainWebData>>();
            string strError = base.GetDefaultValueForInitialization<string>();
            bool flag = this.EndGetTrainsOnline(result, out defaultValueForInitialization, out strError);
            return new object[] { defaultValueForInitialization, strError, flag };
        }

        private object[] OnEndOpen(IAsyncResult result)
        {
            this.EndOpen(result);
            return null;
        }

        private object[] OnEndSetTrainData(IAsyncResult result)
        {
            string defaultValueForInitialization = base.GetDefaultValueForInitialization<string>();
            bool flag = this.EndSetTrainData(result, out defaultValueForInitialization);
            return new object[] { defaultValueForInitialization, flag };
        }

        private void OnGetCarOperationStatisticsCompleted(object state)
        {
            if (this.GetCarOperationStatisticsCompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.GetCarOperationStatisticsCompleted(this, new GetCarOperationStatisticsCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetDevicesCompleted(object state)
        {
            if (this.GetDevicesCompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.GetDevicesCompleted(this, new GetDevicesCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetGPSDeviceStatisticsCompleted(object state)
        {
            if (this.GetGPSDeviceStatisticsCompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.GetGPSDeviceStatisticsCompleted(this, new GetGPSDeviceStatisticsCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetGPSDeviceStatisticsInitUICompleted(object state)
        {
            if (this.GetGPSDeviceStatisticsInitUICompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.GetGPSDeviceStatisticsInitUICompleted(this, new GetGPSDeviceStatisticsInitUICompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetStateCompleted(object state)
        {
            if (this.GetStateCompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.GetStateCompleted(this, new GetStateCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetTrainRouteHtmlCompleted(object state)
        {
            if (this.GetTrainRouteHtmlCompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.GetTrainRouteHtmlCompleted(this, new GetTrainRouteHtmlCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetTrainsOnlineCompleted(object state)
        {
            if (this.GetTrainsOnlineCompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.GetTrainsOnlineCompleted(this, new GetTrainsOnlineCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnOpenCompleted(object state)
        {
            if (this.OpenCompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.OpenCompleted(this, new AsyncCompletedEventArgs(args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnSetTrainDataCompleted(object state)
        {
            if (this.SetTrainDataCompleted != null)
            {
                ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs args = (ClientBase<TISWebServiceSoap>.InvokeAsyncCompletedEventArgs) state;
                this.SetTrainDataCompleted(this, new SetTrainDataCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        public void OpenAsync()
        {
            this.OpenAsync(null);
        }

        public void OpenAsync(object userState)
        {
            if (this.onBeginOpenDelegate == null)
            {
                this.onBeginOpenDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginOpen);
            }
            if (this.onEndOpenDelegate == null)
            {
                this.onEndOpenDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndOpen);
            }
            if (this.onOpenCompletedDelegate == null)
            {
                this.onOpenCompletedDelegate = new SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }

        public void SetTrainDataAsync(string trainIdOld, string trainIdNew, bool goodsTrain, string trainDescription, string userName)
        {
            this.SetTrainDataAsync(trainIdOld, trainIdNew, goodsTrain, trainDescription, userName, null);
        }

        public void SetTrainDataAsync(string trainIdOld, string trainIdNew, bool goodsTrain, string trainDescription, string userName, object userState)
        {
            if (this.onBeginSetTrainDataDelegate == null)
            {
                this.onBeginSetTrainDataDelegate = new ClientBase<TISWebServiceSoap>.BeginOperationDelegate(this.OnBeginSetTrainData);
            }
            if (this.onEndSetTrainDataDelegate == null)
            {
                this.onEndSetTrainDataDelegate = new ClientBase<TISWebServiceSoap>.EndOperationDelegate(this.OnEndSetTrainData);
            }
            if (this.onSetTrainDataCompletedDelegate == null)
            {
                this.onSetTrainDataCompletedDelegate = new SendOrPostCallback(this.OnSetTrainDataCompleted);
            }
            base.InvokeAsync(this.onBeginSetTrainDataDelegate, new object[] { trainIdOld, trainIdNew, goodsTrain, trainDescription, userName }, this.onEndSetTrainDataDelegate, this.onSetTrainDataCompletedDelegate, userState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IAsyncResult TISWebServiceSoap.BeginGetCarOperationStatistics(GetCarOperationStatisticsRequest request, AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetCarOperationStatistics(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IAsyncResult TISWebServiceSoap.BeginGetDevices(GetDevicesRequest request, AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetDevices(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IAsyncResult TISWebServiceSoap.BeginGetGPSDeviceStatistics(GetGPSDeviceStatisticsRequest request, AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetGPSDeviceStatistics(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IAsyncResult TISWebServiceSoap.BeginGetGPSDeviceStatisticsInitUI(GetGPSDeviceStatisticsInitUIRequest request, AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetGPSDeviceStatisticsInitUI(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IAsyncResult TISWebServiceSoap.BeginGetState(GetStateRequest request, AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetState(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IAsyncResult TISWebServiceSoap.BeginGetTrainRouteHtml(GetTrainRouteHtmlRequest request, AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetTrainRouteHtml(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IAsyncResult TISWebServiceSoap.BeginGetTrainsOnline(GetTrainsOnlineRequest request, AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetTrainsOnline(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IAsyncResult TISWebServiceSoap.BeginSetTrainData(SetTrainDataRequest request, AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginSetTrainData(request, callback, asyncState);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetCarOperationStatisticsResponse TISWebServiceSoap.EndGetCarOperationStatistics(IAsyncResult result)
        {
            return base.Channel.EndGetCarOperationStatistics(result);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetDevicesResponse TISWebServiceSoap.EndGetDevices(IAsyncResult result)
        {
            return base.Channel.EndGetDevices(result);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetGPSDeviceStatisticsResponse TISWebServiceSoap.EndGetGPSDeviceStatistics(IAsyncResult result)
        {
            return base.Channel.EndGetGPSDeviceStatistics(result);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetGPSDeviceStatisticsInitUIResponse TISWebServiceSoap.EndGetGPSDeviceStatisticsInitUI(IAsyncResult result)
        {
            return base.Channel.EndGetGPSDeviceStatisticsInitUI(result);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetStateResponse TISWebServiceSoap.EndGetState(IAsyncResult result)
        {
            return base.Channel.EndGetState(result);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetTrainRouteHtmlResponse TISWebServiceSoap.EndGetTrainRouteHtml(IAsyncResult result)
        {
            return base.Channel.EndGetTrainRouteHtml(result);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetTrainsOnlineResponse TISWebServiceSoap.EndGetTrainsOnline(IAsyncResult result)
        {
            return base.Channel.EndGetTrainsOnline(result);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        SetTrainDataResponse TISWebServiceSoap.EndSetTrainData(IAsyncResult result)
        {
            return base.Channel.EndSetTrainData(result);
        }

        public System.Net.CookieContainer CookieContainer
        {
            get
            {
                IHttpCookieContainerManager property = base.InnerChannel.GetProperty<IHttpCookieContainerManager>();
                if (property != null)
                {
                    return property.get_CookieContainer();
                }
                return null;
            }
            set
            {
                IHttpCookieContainerManager property = base.InnerChannel.GetProperty<IHttpCookieContainerManager>();
                if (property == null)
                {
                    throw new InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpCookieContainerBindingElement.");
                }
                property.set_CookieContainer(value);
            }
        }

        private class TISWebServiceSoapClientChannel : ClientBase<TISWebServiceSoap>.ChannelBase<TISWebServiceSoap>, TISWebServiceSoap
        {
            public TISWebServiceSoapClientChannel(ClientBase<TISWebServiceSoap> client) : base(client)
            {
            }

            public IAsyncResult BeginGetCarOperationStatistics(GetCarOperationStatisticsRequest request, AsyncCallback callback, object asyncState)
            {
                object[] args = new object[] { request };
                return base.BeginInvoke("GetCarOperationStatistics", args, callback, asyncState);
            }

            public IAsyncResult BeginGetDevices(GetDevicesRequest request, AsyncCallback callback, object asyncState)
            {
                object[] args = new object[] { request };
                return base.BeginInvoke("GetDevices", args, callback, asyncState);
            }

            public IAsyncResult BeginGetGPSDeviceStatistics(GetGPSDeviceStatisticsRequest request, AsyncCallback callback, object asyncState)
            {
                object[] args = new object[] { request };
                return base.BeginInvoke("GetGPSDeviceStatistics", args, callback, asyncState);
            }

            public IAsyncResult BeginGetGPSDeviceStatisticsInitUI(GetGPSDeviceStatisticsInitUIRequest request, AsyncCallback callback, object asyncState)
            {
                object[] args = new object[] { request };
                return base.BeginInvoke("GetGPSDeviceStatisticsInitUI", args, callback, asyncState);
            }

            public IAsyncResult BeginGetState(GetStateRequest request, AsyncCallback callback, object asyncState)
            {
                object[] args = new object[] { request };
                return base.BeginInvoke("GetState", args, callback, asyncState);
            }

            public IAsyncResult BeginGetTrainRouteHtml(GetTrainRouteHtmlRequest request, AsyncCallback callback, object asyncState)
            {
                object[] args = new object[] { request };
                return base.BeginInvoke("GetTrainRouteHtml", args, callback, asyncState);
            }

            public IAsyncResult BeginGetTrainsOnline(GetTrainsOnlineRequest request, AsyncCallback callback, object asyncState)
            {
                object[] args = new object[] { request };
                return base.BeginInvoke("GetTrainsOnline", args, callback, asyncState);
            }

            public IAsyncResult BeginSetTrainData(SetTrainDataRequest request, AsyncCallback callback, object asyncState)
            {
                object[] args = new object[] { request };
                return base.BeginInvoke("SetTrainData", args, callback, asyncState);
            }

            public GetCarOperationStatisticsResponse EndGetCarOperationStatistics(IAsyncResult result)
            {
                object[] args = new object[0];
                return (GetCarOperationStatisticsResponse) base.EndInvoke("GetCarOperationStatistics", args, result);
            }

            public GetDevicesResponse EndGetDevices(IAsyncResult result)
            {
                object[] args = new object[0];
                return (GetDevicesResponse) base.EndInvoke("GetDevices", args, result);
            }

            public GetGPSDeviceStatisticsResponse EndGetGPSDeviceStatistics(IAsyncResult result)
            {
                object[] args = new object[0];
                return (GetGPSDeviceStatisticsResponse) base.EndInvoke("GetGPSDeviceStatistics", args, result);
            }

            public GetGPSDeviceStatisticsInitUIResponse EndGetGPSDeviceStatisticsInitUI(IAsyncResult result)
            {
                object[] args = new object[0];
                return (GetGPSDeviceStatisticsInitUIResponse) base.EndInvoke("GetGPSDeviceStatisticsInitUI", args, result);
            }

            public GetStateResponse EndGetState(IAsyncResult result)
            {
                object[] args = new object[0];
                return (GetStateResponse) base.EndInvoke("GetState", args, result);
            }

            public GetTrainRouteHtmlResponse EndGetTrainRouteHtml(IAsyncResult result)
            {
                object[] args = new object[0];
                return (GetTrainRouteHtmlResponse) base.EndInvoke("GetTrainRouteHtml", args, result);
            }

            public GetTrainsOnlineResponse EndGetTrainsOnline(IAsyncResult result)
            {
                object[] args = new object[0];
                return (GetTrainsOnlineResponse) base.EndInvoke("GetTrainsOnline", args, result);
            }

            public SetTrainDataResponse EndSetTrainData(IAsyncResult result)
            {
                object[] args = new object[0];
                return (SetTrainDataResponse) base.EndInvoke("SetTrainData", args, result);
            }
        }
    }
}

