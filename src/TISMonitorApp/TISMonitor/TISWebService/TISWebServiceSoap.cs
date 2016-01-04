namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ServiceModel;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), ServiceContract(ConfigurationName="TISWebService.TISWebServiceSoap")]
    public interface TISWebServiceSoap
    {
        [OperationContract(AsyncPattern=true, Action="http://tempuri.org/GetCarOperationStatistics", ReplyAction="*")]
        IAsyncResult BeginGetCarOperationStatistics(GetCarOperationStatisticsRequest request, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true, Action="http://tempuri.org/GetDevices", ReplyAction="*")]
        IAsyncResult BeginGetDevices(GetDevicesRequest request, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true, Action="http://tempuri.org/GetGPSDeviceStatistics", ReplyAction="*")]
        IAsyncResult BeginGetGPSDeviceStatistics(GetGPSDeviceStatisticsRequest request, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true, Action="http://tempuri.org/GetGPSDeviceStatisticsInitUI", ReplyAction="*")]
        IAsyncResult BeginGetGPSDeviceStatisticsInitUI(GetGPSDeviceStatisticsInitUIRequest request, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true, Action="http://tempuri.org/GetState", ReplyAction="*")]
        IAsyncResult BeginGetState(GetStateRequest request, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true, Action="http://tempuri.org/GetTrainRouteHtml", ReplyAction="*")]
        IAsyncResult BeginGetTrainRouteHtml(GetTrainRouteHtmlRequest request, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true, Action="http://tempuri.org/GetTrainsOnline", ReplyAction="*")]
        IAsyncResult BeginGetTrainsOnline(GetTrainsOnlineRequest request, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true, Action="http://tempuri.org/SetTrainData", ReplyAction="*")]
        IAsyncResult BeginSetTrainData(SetTrainDataRequest request, AsyncCallback callback, object asyncState);
        GetCarOperationStatisticsResponse EndGetCarOperationStatistics(IAsyncResult result);
        GetDevicesResponse EndGetDevices(IAsyncResult result);
        GetGPSDeviceStatisticsResponse EndGetGPSDeviceStatistics(IAsyncResult result);
        GetGPSDeviceStatisticsInitUIResponse EndGetGPSDeviceStatisticsInitUI(IAsyncResult result);
        GetStateResponse EndGetState(IAsyncResult result);
        GetTrainRouteHtmlResponse EndGetTrainRouteHtml(IAsyncResult result);
        GetTrainsOnlineResponse EndGetTrainsOnline(IAsyncResult result);
        SetTrainDataResponse EndSetTrainData(IAsyncResult result);
    }
}

