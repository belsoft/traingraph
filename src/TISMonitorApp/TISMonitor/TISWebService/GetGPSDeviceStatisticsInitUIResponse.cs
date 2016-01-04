namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), MessageContract(IsWrapped=false)]
    public class GetGPSDeviceStatisticsInitUIResponse
    {
        [MessageBodyMember(Name="GetGPSDeviceStatisticsInitUIResponse", Namespace="http://tempuri.org/", Order=0)]
        public GetGPSDeviceStatisticsInitUIResponseBody Body;

        public GetGPSDeviceStatisticsInitUIResponse()
        {
        }

        public GetGPSDeviceStatisticsInitUIResponse(GetGPSDeviceStatisticsInitUIResponseBody Body)
        {
            this.Body = Body;
        }
    }
}

