using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace TISWebServiceHelper
{
    [ServiceKnownType(typeof(string)), DataContract, ServiceKnownType(typeof(CurveData)), ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples"), ServiceKnownType(typeof(TrainGraphData)), ServiceKnownType(typeof(double)), ServiceKnownType(typeof(int)), ServiceKnownType(typeof(PointData)), ServiceKnownType(typeof(MarkerData))]
    public class TrainGraphData : CachableObject
    {
        [DataMember]
        public List<CurveData> Curves = new List<CurveData>();
        [DataMember]
        public List<MarkerData> Markers = new List<MarkerData>();
        [DataMember]
        public DateTime TimeStart;
        [DataMember]
        public DateTime TimeStop;

        public string Dump()
        {
            string str = String.Format("Curves.Count='{0}', Points: ", this.Curves.Count);
            foreach (CurveData data in this.Curves)
            {
                if (data.Points != null)
                {
                    str = str + string.Format("{0},", data.Points.Count);
                }
            }
            return str;
        }
    }
}