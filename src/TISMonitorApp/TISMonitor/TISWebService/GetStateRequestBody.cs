namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), DataContract(Namespace="http://tempuri.org/")]
    public class GetStateRequestBody
    {
        [DataMember(EmitDefaultValue=false, Order=0)]
        public string strIn;

        public GetStateRequestBody()
        {
        }

        public GetStateRequestBody(string strIn)
        {
            this.strIn = strIn;
        }
    }
}

