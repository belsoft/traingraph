namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [EditorBrowsable(EditorBrowsableState.Advanced), MessageContract(IsWrapped=false), GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough]
    public class GetGPSDeviceStatisticsInitUIRequest
    {
        [MessageBodyMember(Name="GetGPSDeviceStatisticsInitUI", Namespace="http://tempuri.org/", Order=0)]
        public GetGPSDeviceStatisticsInitUIRequestBody Body;

        public GetGPSDeviceStatisticsInitUIRequest()
        {
        }

        public GetGPSDeviceStatisticsInitUIRequest(GetGPSDeviceStatisticsInitUIRequestBody Body)
        {
            this.Body = Body;
        }
    }
}

