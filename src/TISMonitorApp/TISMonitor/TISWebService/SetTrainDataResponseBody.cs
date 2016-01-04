namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [DataContract(Namespace="http://tempuri.org/"), EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class SetTrainDataResponseBody
    {
        [DataMember(Order=0)]
        public bool SetTrainDataResult;
        [DataMember(EmitDefaultValue=false, Order=1)]
        public string strError;

        public SetTrainDataResponseBody()
        {
        }

        public SetTrainDataResponseBody(bool SetTrainDataResult, string strError)
        {
            this.SetTrainDataResult = SetTrainDataResult;
            this.strError = strError;
        }
    }
}

