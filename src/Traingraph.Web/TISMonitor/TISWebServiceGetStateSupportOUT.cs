using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace Traingraph.Web.TISMonitor
{
    [ServiceKnownType(typeof(TISWebServiceGetStateSupportOUT)), ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples"), DataContract, ServiceKnownType(typeof(string)), ServiceKnownType(typeof(int)), ServiceKnownType(typeof(double))]
    public class TISWebServiceGetStateSupportOUT
    {
        [DataMember]
        public string actualRealTimeTrainGraphTimeValue = "01.01.0001 00:00:00.00";
        [DataMember]
        public string ServerOperationDateValue = "01.01.0001 00:00:00.00";
        [DataMember]
        public string ServerTimeValue = "01.01.0001 00:00:00.00";

        public DateTime actualRealTimeTrainGraphTime
        {
            get
            {
                return DTHelper.GetDate(this.actualRealTimeTrainGraphTimeValue);
            }
            set
            {
                this.actualRealTimeTrainGraphTimeValue = DTHelper.GetStr(value);
            }
        }

        public DateTime ServerOperationDate
        {
            get
            {
                return DTHelper.GetDate(this.ServerOperationDateValue);
            }
            set
            {
                this.ServerOperationDateValue = DTHelper.GetStr(value);
            }
        }

        public DateTime ServerTime
        {
            get
            {
                return DTHelper.GetDate(this.ServerTimeValue);
            }
            set
            {
                this.ServerTimeValue = DTHelper.GetStr(value);
            }
        }
    }
}