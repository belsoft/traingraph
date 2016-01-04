using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace Traingraph.Web.TISMonitor
{
    [ServiceKnownType(typeof(int)), ServiceKnownType(typeof(double)), DataContract, ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples"), ServiceKnownType(typeof(string)), ServiceKnownType(typeof(TISWebServiceGetStateSupportIN))]
    public class TISWebServiceGetStateSupportIN
    {
        [DataMember]
        public string clientLastRealTimeTrainGraphTimeValue = "01.01.0001 00:00:00.00";
        [DataMember]
        public bool getRegularTraingraphData = false;
        [DataMember]
        public bool getServerTimes = false;
        [DataMember]
        public string traingraphStartValue = "01.01.0001 00:00:00.00";
        [DataMember]
        public string traingraphStopValue = "01.01.0001 00:00:00.00";

        public DateTime clientLastRealTimeTrainGraphTime
        {
            get
            {
                return DTHelper.GetDate(this.clientLastRealTimeTrainGraphTimeValue);
            }
            set
            {
                this.clientLastRealTimeTrainGraphTimeValue = DTHelper.GetStr(value);
            }
        }

        public DateTime traingraphStart
        {
            get
            {
                return DTHelper.GetDate(this.traingraphStartValue);
            }
            set
            {
                this.traingraphStartValue = DTHelper.GetStr(value);
            }
        }

        public DateTime traingraphStop
        {
            get
            {
                return DTHelper.GetDate(this.traingraphStopValue);
            }
            set
            {
                this.traingraphStopValue = DTHelper.GetStr(value);
            }
        }
    }
}