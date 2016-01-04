using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TrainGraph.WebA.TISWebService;
using TISWebServiceHelper;

namespace TISServiceHelper
{
    public class StateDataViewModel
    {
        public bool StateResult { get; set; }
        public TISWebServiceGetStateSupportOUT strOut { get; set; }
        public List<TrainWebData> onlineTrains { get; set; }

        public List<TrainWebData> offlineTrains { get; set; }

        public List<TrainWebData> onlineButNotOnTheMap { get; set; }

        public TrainGraphData trainGraphData { get; set; }

        public string Error { get; set; }
    }
}