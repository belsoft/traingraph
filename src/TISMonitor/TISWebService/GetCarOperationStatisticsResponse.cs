namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), MessageContract(IsWrapped=false)]
    public class GetCarOperationStatisticsResponse
    {
        [MessageBodyMember(Name="GetCarOperationStatisticsResponse", Namespace="http://tempuri.org/", Order=0)]
        public GetCarOperationStatisticsResponseBody Body;

        public GetCarOperationStatisticsResponse()
        {
        }

        public GetCarOperationStatisticsResponse(GetCarOperationStatisticsResponseBody Body)
        {
            this.Body = Body;
        }
    }
}

