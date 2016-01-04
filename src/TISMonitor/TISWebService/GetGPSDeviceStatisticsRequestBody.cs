namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), DataContract(Namespace="http://tempuri.org/")]
    public class GetGPSDeviceStatisticsRequestBody
    {
        [DataMember(EmitDefaultValue=false, Order=1)]
        public string dateTimeFrom;
        [DataMember(EmitDefaultValue=false, Order=2)]
        public string dateTimeTo;
        [DataMember(EmitDefaultValue=false, Order=0)]
        public string deviceId;
        [DataMember(Order=3)]
        public int maxOutputRange;

        public GetGPSDeviceStatisticsRequestBody()
        {
        }

        public GetGPSDeviceStatisticsRequestBody(string deviceId, string dateTimeFrom, string dateTimeTo, int maxOutputRange)
        {
            this.deviceId = deviceId;
            this.dateTimeFrom = dateTimeFrom;
            this.dateTimeTo = dateTimeTo;
            this.maxOutputRange = maxOutputRange;
        }
    }
}

