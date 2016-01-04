namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [MessageContract(IsWrapped=false), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class GetTrainRouteHtmlResponse
    {
        [MessageBodyMember(Name="GetTrainRouteHtmlResponse", Namespace="http://tempuri.org/", Order=0)]
        public GetTrainRouteHtmlResponseBody Body;

        public GetTrainRouteHtmlResponse()
        {
        }

        public GetTrainRouteHtmlResponse(GetTrainRouteHtmlResponseBody Body)
        {
            this.Body = Body;
        }
    }
}

