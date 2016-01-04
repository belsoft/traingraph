using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Traingraph.Web.TISMonitor;
using Traingraph.Web.TISMonitorWebService;

namespace Traingraph.Web.Controllers
{
    public class TisController : ApiController
    {

        public void Get()
        {
            /*
      <TISWebServiceGetStateSupportIN xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/TISServiceHelper">
             * <clientLastRealTimeTrainGraphTimeValue>16.11.2015 07:42:50.19</clientLastRealTimeTrainGraphTimeValue>
             * <getRegularTraingraphData>false</getRegularTraingraphData><getServerTimes>false</getServerTimes>
             * <traingraphStartValue>16.11.2015 05:00:00.00</traingraphStartValue>
             * <traingraphStopValue>17.11.2015 01:00:00.00</traingraphStopValue></TISWebServiceGetStateSupportIN>
             
             */

           Post(new TISWebServiceGetStateSupportIN
            { 
                getRegularTraingraphData = false,
                //getServerTimes = false,
                clientLastRealTimeTrainGraphTime = DateTime.Now,
                traingraphStart = DateTime.Today.AddHours(5),
                traingraphStop = DateTime.Today.AddDays(1).AddHours(1)
            });

            //return result;
        }

        private TISMonitorWebService.TISWebServiceSoapClient GetClient()
        {
            TISMonitorWebService.TISWebServiceSoapClient client = new TISMonitorWebService.TISWebServiceSoapClient();
           

            if (client.Endpoint.Address.Uri.AbsoluteUri.StartsWith("http"))
            {
                client.Endpoint.Address = new EndpointAddress(client.Endpoint.Address.Uri.AbsoluteUri.Replace("http", "https"));
                (client.Endpoint.Binding as BasicHttpBinding).Security.Mode = BasicHttpSecurityMode.Transport;
            }

            


            return client;
        }

        public void Post(TISWebServiceGetStateSupportIN model)
        {
             TISMonitorWebService.TISWebServiceSoapClient client = this.GetClient();

            string strOut, strError, strTrainOnlineData, strTrainOfflineData, strTrainGraphData, strOnlineButNotOnMapData;

             using (new OperationContextScope(client.InnerChannel))
             {
                 HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                 requestMessage.Headers["Referer"] = "http://blb.csie-data.com/ClientBin/TISMonitor.xap?version22=16010101010000000";
                 requestMessage.Headers["Content-Type"] = "text/xml; charset=utf-8";
                 OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;

                 client.GetStateCompleted += new EventHandler<GetStateCompletedEventArgs>(this.TISWebService_GetStateCompleted);


                 //var result = client.GetState(DCSerializer.SerializeWithDCS(model), out strOut, out strTrainOnlineData, out strTrainOfflineData, out strOnlineButNotOnMapData, out strTrainOnlineData, out strError);
                 client.GetStateAsync(DCSerializer.SerializeWithDCS(model));
                 //var result = client.GetStateAsync(DCSerializer.SerializeWithDCS(model););
                 //result.ContinueWith(x =>
                 //{
                 //    var x2 = x.Result.Body;
                 //    var xv = x;
                 //});


             }




            //client. += new EventHandler<GetStateCompletedEventArgs>(this.TISWebService_GetStateCompleted);
         
                //string strIn = DCSerializer.SerializeWithDCS(model);

                //var result = new TISWebServiceGetStateSupportOUT();
                //var resultAsync = client.GetStateAsync(strIn);
                //resultAsync.ContinueWith(t =>
                //{
                //    result = TISWebService_GetStateCompleted(t.Result.Body);
                //});

                //return null;
                //this.m_stateIn = null;

        }

        private void TISWebService_GetStateCompleted(object sender, GetStateCompletedEventArgs e)
        {
            try
            {
                //if (e.Error != null)
                //{
                //    throw new SLException(e.Error.Message + " DataProvider from server which failed");
                //}
                //if (!e.Result)
                //{
                //    throw new SLException(e.strError + " DataProvider from server");
                //}
                //if (this.OnData != null)
                //{
                    TISWebServiceGetStateSupportOUT result = DCSerializer.DeserializeWithDCS(typeof(TISWebServiceGetStateSupportOUT), e.strOut) as TISWebServiceGetStateSupportOUT;
                    List<TrainWebData> onlineTrains = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), e.onlineTrainsData) as List<TrainWebData>;
                    List<TrainWebData> offlineTrains = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), e.offlineTrainsData) as List<TrainWebData>;
                    List<TrainWebData> onlineButNotOnTheMap = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), e.onlineButNotOnTheMapTrainsData) as List<TrainWebData>;
                    //this.OnData(result, onlineTrains, offlineTrains, onlineButNotOnTheMap);
                    //this.AddLog("DataProvider GetStateCompleted");
                //}
            }
            //catch (SLException exception)
            //{
            //    this.DataError(exception.Message);
            //    this.AddLog("DataProvider GetState error custom: " + exception.Message);
            //}
            catch (Exception exception2)
            {
                //this.DataError(exception2.Message);
                //this.AddLog("DataProvider GetState error: " + exception2.Message);
            }
        }

        /*
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
         
         */
    }
}