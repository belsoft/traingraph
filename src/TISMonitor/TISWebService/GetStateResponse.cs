namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), MessageContract(IsWrapped=false), EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough]
    public class GetStateResponse
    {
        [MessageBodyMember(Name="GetStateResponse", Namespace="http://tempuri.org/", Order=0)]
        public GetStateResponseBody Body;

        public GetStateResponse()
        {
        }

        public GetStateResponse(GetStateResponseBody Body)
        {
            this.Body = Body;
        }
    }
}

