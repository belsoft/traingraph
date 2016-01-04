namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), DataContract(Namespace="http://tempuri.org/")]
    public class GetGPSDeviceStatisticsInitUIResponseBody
    {
        [DataMember(EmitDefaultValue=false, Order=2)]
        public ArrayOfString availableDevicesInHistory;
        [DataMember(EmitDefaultValue=false, Order=3)]
        public string dateTimeFrom;
        [DataMember(EmitDefaultValue=false, Order=4)]
        public string dateTimeTo;
        [DataMember(Order=0)]
        public bool GetGPSDeviceStatisticsInitUIResult;
        [DataMember(Order=1)]
        public bool historyExists;
        [DataMember(EmitDefaultValue=false, Order=5)]
        public string strError;

        public GetGPSDeviceStatisticsInitUIResponseBody()
        {
        }

        public GetGPSDeviceStatisticsInitUIResponseBody(bool GetGPSDeviceStatisticsInitUIResult, bool historyExists, ArrayOfString availableDevicesInHistory, string dateTimeFrom, string dateTimeTo, string strError)
        {
            this.GetGPSDeviceStatisticsInitUIResult = GetGPSDeviceStatisticsInitUIResult;
            this.historyExists = historyExists;
            this.availableDevicesInHistory = availableDevicesInHistory;
            this.dateTimeFrom = dateTimeFrom;
            this.dateTimeTo = dateTimeTo;
            this.strError = strError;
        }
    }
}

