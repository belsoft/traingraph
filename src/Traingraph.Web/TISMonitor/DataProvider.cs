//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel;
//using System.Web;

//using System.Threading.Tasks;
//using Traingraph.Web.TISMonitorWebService;

//namespace Traingraph.Web.TISMonitor
//{
//    public class DataProvider
//    {
//        private TISWebServiceGetStateSupportIN m_stateIn = null;
//        public DataProvider()
//        {
//            this.Init();
//        }

//        private void Init()
//        { 
        
//        }

//        public void SetParameters(TISWebServiceGetStateSupportIN stateIn)
//        {
//            this.m_stateIn = stateIn;
//        }

//        public void SetStateIn(TISWebServiceGetStateSupportIN state)
//        {
//            this.m_stateIn = state;
//        }

//        private TISMonitorWebService.TISWebServiceSoapClient GetClient()
//        {
//            TISMonitorWebService.TISWebServiceSoapClient client = new TISMonitorWebService.TISWebServiceSoapClient();
//            if (client.Endpoint.Address.Uri.AbsoluteUri.StartsWith("http"))
//            {
//                client.Endpoint.Address = new EndpointAddress(client.Endpoint.Address.Uri.AbsoluteUri.Replace("http", "https"));
//                (client.Endpoint.Binding as BasicHttpBinding).Security.Mode = BasicHttpSecurityMode.Transport;
//            }
//            return client;
//        }

//        private Task<GetStateResponse> OnTimerGetState()
//        {
//            TISMonitorWebService.TISWebServiceSoapClient client = this.GetClient();

//            //client. += new EventHandler<GetStateCompletedEventArgs>(this.TISWebService_GetStateCompleted);
//            if (this.m_stateIn != null)
//            {
//                string strIn = DCSerializer.SerializeWithDCS(this.m_stateIn);
//                return client.GetStateAsync(strIn);
//                this.m_stateIn = null;

//                //result.ContinueWith(t => {
//                //    TISWebService_GetStateCompleted(t.Result.Body);
//                //});
//            }
//        }

//        private void TISWebService_GetStateCompleted(GetStateResponseBody response)
//        {
//            try
//            {
//                    TISWebServiceGetStateSupportOUT result =
//                        DCSerializer.DeserializeWithDCS(typeof(TISWebServiceGetStateSupportOUT), response.strOut) as TISWebServiceGetStateSupportOUT;
//                    List<TrainWebData> onlineTrains = DCSerializer.DeserializeWithDCS(
//                        typeof(List<TrainWebData>), 
//                        response.onlineTrainsData) as List<TrainWebData>;
//                    List<TrainWebData> offlineTrains = DCSerializer.DeserializeWithDCS(
//                        typeof(List<TrainWebData>), 
//                        response.offlineTrainsData) as List<TrainWebData>;
//                    List<TrainWebData> onlineButNotOnTheMap = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), 
//                        response.onlineButNotOnTheMapTrainsData) as List<TrainWebData>;
                  
                
//                this.OnData(result, onlineTrains, offlineTrains, onlineButNotOnTheMap);
                
//            }
//            catch (Exception exception2)
//            {
        
//            }
//        }
//    }
//}