namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), MessageContract(IsWrapped=false), DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Advanced)]
    public class GetTrainRouteHtmlRequest
    {
        [MessageBodyMember(Name="GetTrainRouteHtml", Namespace="http://tempuri.org/", Order=0)]
        public GetTrainRouteHtmlRequestBody Body;

        public GetTrainRouteHtmlRequest()
        {
        }

        public GetTrainRouteHtmlRequest(GetTrainRouteHtmlRequestBody Body)
        {
            this.Body = Body;
        }
    }
}

