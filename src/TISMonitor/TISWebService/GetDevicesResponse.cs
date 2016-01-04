namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Advanced), MessageContract(IsWrapped=false), GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class GetDevicesResponse
    {
        [MessageBodyMember(Name="GetDevicesResponse", Namespace="http://tempuri.org/", Order=0)]
        public GetDevicesResponseBody Body;

        public GetDevicesResponse()
        {
        }

        public GetDevicesResponse(GetDevicesResponseBody Body)
        {
            this.Body = Body;
        }
    }
}

