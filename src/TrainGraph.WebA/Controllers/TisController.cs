using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web.Http;
using TISServiceHelper;
using TISWebServiceHelper;
using TrainGraph.WebA.Helpers;
//using TISWebServiceHelper;
using TrainGraph.WebA.TISWebService;

namespace TrainGraph.WebA.Controllers
{
    public class TisController : ApiController
    {
        ///// <summary>
        ///// Create default Get request
        ///// </summary>
        ///// <returns>StateData</returns>
        //public async Task<StateDataViewModel> Get()
        //{
        //    var result = await GetStateDataAsync(CreateDefaultStateIn());

        //    return result;
        //}

        /// <summary>
        /// Create Get request
        /// </summary>
        /// <returns>StateData</returns>
        public async Task<StateDataViewModel> Get([FromUri]TISWebServiceGetStateSupportIN stateIn)
        {
            var result = await GetStateDataAsync(stateIn);

            return result;
        }

        /**
         *  Create Regular Data StateIn
         *  <TISWebServiceGetStateSupportIN xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/TISServiceHelper">
         *      <clientLastRealTimeTrainGraphTimeValue>01.01.0001 00:00:00.00</clientLastRealTimeTrainGraphTimeValue>
         *      <getRegularTraingraphData>true</getRegularTraingraphData>
         *      <getServerTimes>false</getServerTimes>
         *      <traingraphStartValue>01.01.0001 00:00:00.00</traingraphStartValue>
         *      <traingraphStopValue>01.01.0001 00:00:00.00</traingraphStopValue>
         *  </TISWebServiceGetStateSupportIN>
         */
        private TISWebServiceGetStateSupportIN CreateRegularDataStateIn()
        {
            return new TISWebServiceGetStateSupportIN
            {
                getRegularTraingraphData = true,
                clientLastRealTimeTrainGraphTimeValue = DTHelper.GetStr(DateTime.MinValue),
                getServerTimes = false,
                traingraphStartValue = DTHelper.CNT_MINVALUE,
                traingraphStopValue = DTHelper.CNT_MINVALUE
            };
        }

        /**
         *  Create default StateIn
         *  <TISWebServiceGetStateSupportIN xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/TISServiceHelper">
         *       <clientLastRealTimeTrainGraphTimeValue>16.11.2015 07:42:50.19</clientLastRealTimeTrainGraphTimeValue>
         *       <getRegularTraingraphData>false</getRegularTraingraphData>
         *      <getServerTimes>false</getServerTimes>
         *      <traingraphStartValue>16.11.2015 05:00:00.00</traingraphStartValue>
         *      <traingraphStopValue>17.11.2015 01:00:00.00</traingraphStopValue>
         *  </TISWebServiceGetStateSupportIN>
         */
        private TISWebServiceGetStateSupportIN CreateDefaultStateIn()
        {
            return new TISWebServiceGetStateSupportIN
            {
                getRegularTraingraphData = false,
                clientLastRealTimeTrainGraphTimeValue = DTHelper.GetStr(DateTime.Now.AddHours(-3)),
                getServerTimes = false,
                traingraphStartValue = DTHelper.GetStr(DateTime.Today.AddHours(5)),
                traingraphStopValue = DTHelper.GetStr(DateTime.Today.AddDays(1).AddHours(1))
            };
        }

        private async Task<StateDataViewModel> GetStateDataAsync(TISWebServiceGetStateSupportIN stateIn)
        {
            GetStateResponse result = new GetStateResponse();

            using (TISWebService.TISWebServiceSoapClient client = GetClient())
            using (var scope = new FlowingOperationContextScope(client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = CreateRequestMessageProperty();
                string strIn = DCSerializer.SerializeWithDCS(stateIn);
                try
                {
                    var webResponse = await client.GetStateAsync(strIn).ContinueOnScope(scope);
                    return GetStateData(webResponse.Body);
                }
                catch (Exception ex)
                {
                    return new StateDataViewModel { Error = ex.Message };
                }

            }
        }

        private StateDataViewModel GetStateData(GetStateResponseBody webResponse) 
        {
            var model = new StateDataViewModel();

            model.StateResult = webResponse.GetStateResult;
            model.strOut = DCSerializer.DeserializeWithDCS<TISWebServiceGetStateSupportOUT>(webResponse.strOut);
            model.onlineTrains = DCSerializer.DeserializeWithDCS<List<TrainWebData>>(webResponse.onlineTrainsData);
            model.offlineTrains = DCSerializer.DeserializeWithDCS<List<TrainWebData>>(webResponse.offlineTrainsData);
            model.onlineButNotOnTheMap = DCSerializer.DeserializeWithDCS<List<TrainWebData>>(webResponse.onlineButNotOnTheMapTrainsData);
            model.trainGraphData = DCSerializer.DeserializeWithDCS<TrainGraphData>(webResponse.trainGraphData);
            model.Error = webResponse.strError;

            return model;
        }

        private HttpRequestMessageProperty CreateRequestMessageProperty() 
        {
            HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
            requestMessage.Headers["Referer"] = "http://blb.csie-data.com/ClientBin/TISMonitor.xap?version22=16010101010000000";
            requestMessage.Headers["Content-Type"] = "text/xml; charset=utf-8";
            requestMessage.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
            requestMessage.Headers["Accept"] = "*/*";
            requestMessage.Headers["Accept-Language"] = "en-US,en;q=0.7,ru;q=0.3";
         
            return requestMessage;
        }

        private TISWebServiceSoapClient GetClient()
        {
            TISWebService.TISWebServiceSoapClient client = new TISWebService.TISWebServiceSoapClient();
            // Increase Message Size Quota
            (client.Endpoint.Binding as BasicHttpBinding).MaxReceivedMessageSize = 2147483647;
            (client.Endpoint.Binding as BasicHttpBinding).MaxBufferSize = 2147483647;
           return client;
        }
    }
}