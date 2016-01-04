namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Advanced), MessageContract(IsWrapped=false)]
    public class GetTrainsOnlineResponse
    {
        [MessageBodyMember(Name="GetTrainsOnlineResponse", Namespace="http://tempuri.org/", Order=0)]
        public GetTrainsOnlineResponseBody Body;

        public GetTrainsOnlineResponse()
        {
        }

        public GetTrainsOnlineResponse(GetTrainsOnlineResponseBody Body)
        {
            this.Body = Body;
        }
    }
}

