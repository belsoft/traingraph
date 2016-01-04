namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [DebuggerStepThrough, MessageContract(IsWrapped=false), GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class GetDevicesRequest
    {
        [MessageBodyMember(Name="GetDevices", Namespace="http://tempuri.org/", Order=0)]
        public GetDevicesRequestBody Body;

        public GetDevicesRequest()
        {
        }

        public GetDevicesRequest(GetDevicesRequestBody Body)
        {
            this.Body = Body;
        }
    }
}

