namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), MessageContract(IsWrapped=false)]
    public class SetTrainDataResponse
    {
        [MessageBodyMember(Name="SetTrainDataResponse", Namespace="http://tempuri.org/", Order=0)]
        public SetTrainDataResponseBody Body;

        public SetTrainDataResponse()
        {
        }

        public SetTrainDataResponse(SetTrainDataResponseBody Body)
        {
            this.Body = Body;
        }
    }
}

