namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [EditorBrowsable(EditorBrowsableState.Advanced), DataContract(Namespace="http://tempuri.org/"), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class GetTrainRouteHtmlRequestBody
    {
        [DataMember(EmitDefaultValue=false, Order=0)]
        public string trainId;

        public GetTrainRouteHtmlRequestBody()
        {
        }

        public GetTrainRouteHtmlRequestBody(string trainId)
        {
            this.trainId = trainId;
        }
    }
}

