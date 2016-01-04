using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Traingraph.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TISMonitorWebService.TISWebServiceSoap s;
            TisController t = new TisController();
             t.Get();
            //s.GetStateAsync()
            /*
                                 TISWebServiceGetStateSupportOUT result = DCSerializer.DeserializeWithDCS(typeof(TISWebServiceGetStateSupportOUT), e.strOut) as TISWebServiceGetStateSupportOUT;
                    List<TrainWebData> onlineTrains = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), e.onlineTrainsData) as List<TrainWebData>;
                    List<TrainWebData> offlineTrains = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), e.offlineTrainsData) as List<TrainWebData>;
                    List<TrainWebData> onlineButNotOnTheMap = DCSerializer.DeserializeWithDCS(typeof(List<TrainWebData>), e.onlineButNotOnTheMapTrainsData) as List<TrainWebData>;
                    this.OnData(result, onlineTrains, offlineTrains, onlineButNotOnTheMap);

             */
            //TISMonitorWebService.GetStateResponse fsd = new TISMonitorWebService.GetStateResponse(new TISMonitorWebService.GetStateResponseBody({ 
              
            //});

            return View();
        }

        public ActionResult Statistik()
        {
            ViewBag.Message = "Statistik";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}