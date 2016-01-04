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
        /*
         
         //   POST http://blb.csie-data.com/TISWebService/TISWebService.asmx HTTP/1.1
         //   Referer: http://blb.csie-data.com/ClientBin/TISMonitor.xap?version22=16010101010000000
         //   Content-Type: text/xml; charset=utf-8
         //   User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko
         //   Accept: *//*
         //   Accept-Language: en-US,en;q=0.7,ru;q=0.3
         //   SOAPAction: "http://tempuri.org/GetState"
         //   Host: blb.csie-data.com
         //   Content-Length: 894
         //   Expect: 100-continue
         //   Accept-Encoding: gzip, deflate
         //   Connection: Keep-Alive

         //   <s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/"><s:Body><GetState xmlns="http://tempuri.org/" xmlns:i="http://www.w3.org/2001/XMLSchema-instance"><strIn>PFRJU1dlYlNlcnZpY2VHZXRTdGF0ZVN1cHBvcnRJTiB4bWxucz0iaHR0cDovL3NjaGVtYXMuZGF0YWNvbnRyYWN0Lm9yZy8yMDA0LzA3L1RJU1NlcnZpY2VIZWxwZXIiIHhtbG5zOmk9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hLWluc3RhbmNlIj48Y2xpZW50TGFzdFJlYWxUaW1lVHJhaW5HcmFwaFRpbWVWYWx1ZT4xNy4xMS4yMDE1IDE1OjE1OjQ3LjAyPC9jbGllbnRMYXN0UmVhbFRpbWVUcmFpbkdyYXBoVGltZVZhbHVlPjxnZXRSZWd1bGFyVHJhaW5ncmFwaERhdGE+ZmFsc2U8L2dldFJlZ3VsYXJUcmFpbmdyYXBoRGF0YT48Z2V0U2VydmVyVGltZXM+ZmFsc2U8L2dldFNlcnZlclRpbWVzPjx0cmFpbmdyYXBoU3RhcnRWYWx1ZT4xNy4xMS4yMDE1IDA1OjAwOjAwLjAwPC90cmFpbmdyYXBoU3RhcnRWYWx1ZT48dHJhaW5ncmFwaFN0b3BWYWx1ZT4xOC4xMS4yMDE1IDAxOjAwOjAwLjAwPC90cmFpbmdyYXBoU3RvcFZhbHVlPjwvVElTV2ViU2VydmljZUdldFN0YXRlU3VwcG9ydElOPg==</strIn></GetState></s:Body></s:Envelope>
         //*/
        //<TISWebServiceGetStateSupportIN xmlns="http://schemas.datacontract.org/2004/07/TISServiceHelper" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
        //<clientLastRealTimeTrainGraphTimeValue>17.11.2015 15:15:47.02</clientLastRealTimeTrainGraphTimeValue><getRegularTraingraphData>false</getRegularTraingraphData>
        //<getServerTimes>false</getServerTimes><traingraphStartValue>17.11.2015 05:00:00.00</traingraphStartValue>
        //<traingraphStopValue>18.11.2015 01:00:00.00</traingraphStopValue></TISWebServiceGetStateSupportIN>
   
            //POST http://blb.csie-data.com/TISWebService/TISWebService.asmx HTTP/1.1
            //Accept: */*
            //Referer: http://blb.csie-data.com/ClientBin/TISMonitor.xap?version22=16010101010000000
            //Accept-Language: en-US,en;q=0.7,ru;q=0.3
            //Content-Length: 894
            //Content-Type: text/xml; charset=utf-8
            //SOAPAction: "http://tempuri.org/GetState"
            //Accept-Encoding: gzip, deflate
            //User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko
            //Host: blb.csie-data.com
            //Connection: Keep-Alive
            //Pragma: no-cache
            //Cookie: __utma=214335556.2105611086.1447327866.1447758517.1447762699.8; __utmb=214335556.1.10.1447762699; __utmz=214335556.1447327866.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); __utmt=1; ASP.NET_SessionId=viakdpvbfewktx5dubpt4j3f; blbcounter=1; __utmc=214335556

            //<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/"><s:Body><GetState xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://tempuri.org/"><strIn>PFRJU1dlYlNlcnZpY2VHZXRTdGF0ZVN1cHBvcnRJTiB4bWxuczppPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYS1pbnN0YW5jZSIgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLmRhdGFjb250cmFjdC5vcmcvMjAwNC8wNy9USVNTZXJ2aWNlSGVscGVyIj48Y2xpZW50TGFzdFJlYWxUaW1lVHJhaW5HcmFwaFRpbWVWYWx1ZT4xNy4xMS4yMDE1IDEyOjMyOjQ1LjM4PC9jbGllbnRMYXN0UmVhbFRpbWVUcmFpbkdyYXBoVGltZVZhbHVlPjxnZXRSZWd1bGFyVHJhaW5ncmFwaERhdGE+ZmFsc2U8L2dldFJlZ3VsYXJUcmFpbmdyYXBoRGF0YT48Z2V0U2VydmVyVGltZXM+ZmFsc2U8L2dldFNlcnZlclRpbWVzPjx0cmFpbmdyYXBoU3RhcnRWYWx1ZT4xNy4xMS4yMDE1IDA1OjAwOjAwLjAwPC90cmFpbmdyYXBoU3RhcnRWYWx1ZT48dHJhaW5ncmFwaFN0b3BWYWx1ZT4xOC4xMS4yMDE1IDAxOjAwOjAwLjAwPC90cmFpbmdyYXBoU3RvcFZhbHVlPjwvVElTV2ViU2VydmljZUdldFN0YXRlU3VwcG9ydElOPg==</strIn></GetState></s:Body></s:Envelope>
        //    <TISWebServiceGetStateSupportIN xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/TISServiceHelper">
        //<clientLastRealTimeTrainGraphTimeValue>17.11.2015 12:32:45.38</clientLastRealTimeTrainGraphTimeValue><getRegularTraingraphData>false</getRegularTraingraphData>
        //<getServerTimes>false</getServerTimes>
        //<traingraphStartValue>17.11.2015 05:00:00.00</traingraphStartValue><traingraphStopValue>18.11.2015 01:00:00.00</traingraphStopValue></TISWebServiceGetStateSupportIN>
         
        public async Task<StateDataViewModel> Get()
        {
            /**
             * Fiddler
             *  <TISWebServiceGetStateSupportIN xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/TISServiceHelper">
             *      <clientLastRealTimeTrainGraphTimeValue>16.11.2015 07:42:50.19</clientLastRealTimeTrainGraphTimeValue>
             *      <getRegularTraingraphData>false</getRegularTraingraphData>
             *      <getServerTimes>false</getServerTimes>
             *      <traingraphStartValue>16.11.2015 05:00:00.00</traingraphStartValue>
             *      <traingraphStopValue>17.11.2015 01:00:00.00</traingraphStopValue>
             *  </TISWebServiceGetStateSupportIN>
            */
            var result = await Post(CreateDefaultStatIn());

            return result;
        }

        private TISWebServiceGetStateSupportIN CreateDefaultStatIn()
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

        public async Task<StateDataViewModel> Post(TISWebServiceGetStateSupportIN stateIn)
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