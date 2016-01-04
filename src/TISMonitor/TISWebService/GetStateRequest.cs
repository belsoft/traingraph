namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), MessageContract(IsWrapped=false)]
    public class GetStateRequest
    {
        [MessageBodyMember(Name="GetState", Namespace="http://tempuri.org/", Order=0)]
        public GetStateRequestBody Body;

        public GetStateRequest()
        {
        }

        public GetStateRequest(GetStateRequestBody Body)
        {
            this.Body = Body;
        }
    }
}

