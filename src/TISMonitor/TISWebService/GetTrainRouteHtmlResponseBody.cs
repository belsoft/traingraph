namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), DataContract(Namespace="http://tempuri.org/")]
    public class GetTrainRouteHtmlResponseBody
    {
        [DataMember(Order=0)]
        public bool GetTrainRouteHtmlResult;
        [DataMember(EmitDefaultValue=false, Order=1)]
        public string html;
        [DataMember(EmitDefaultValue=false, Order=2)]
        public string strError;

        public GetTrainRouteHtmlResponseBody()
        {
        }

        public GetTrainRouteHtmlResponseBody(bool GetTrainRouteHtmlResult, string html, string strError)
        {
            this.GetTrainRouteHtmlResult = GetTrainRouteHtmlResult;
            this.html = html;
            this.strError = strError;
        }
    }
}

