namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), MessageContract(IsWrapped=false), DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Advanced)]
    public class GetGPSDeviceStatisticsRequest
    {
        [MessageBodyMember(Name="GetGPSDeviceStatistics", Namespace="http://tempuri.org/", Order=0)]
        public GetGPSDeviceStatisticsRequestBody Body;

        public GetGPSDeviceStatisticsRequest()
        {
        }

        public GetGPSDeviceStatisticsRequest(GetGPSDeviceStatisticsRequestBody Body)
        {
            this.Body = Body;
        }
    }
}

