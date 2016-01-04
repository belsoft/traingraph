namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [MessageContract(IsWrapped=false), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class SetTrainDataRequest
    {
        [MessageBodyMember(Name="SetTrainData", Namespace="http://tempuri.org/", Order=0)]
        public SetTrainDataRequestBody Body;

        public SetTrainDataRequest()
        {
        }

        public SetTrainDataRequest(SetTrainDataRequestBody Body)
        {
            this.Body = Body;
        }
    }
}

