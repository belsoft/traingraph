namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [EditorBrowsable(EditorBrowsableState.Advanced), GeneratedCode("System.ServiceModel", "4.0.0.0"), MessageContract(IsWrapped=false), DebuggerStepThrough]
    public class GetGPSDeviceStatisticsResponse
    {
        [MessageBodyMember(Name="GetGPSDeviceStatisticsResponse", Namespace="http://tempuri.org/", Order=0)]
        public GetGPSDeviceStatisticsResponseBody Body;

        public GetGPSDeviceStatisticsResponse()
        {
        }

        public GetGPSDeviceStatisticsResponse(GetGPSDeviceStatisticsResponseBody Body)
        {
            this.Body = Body;
        }
    }
}

