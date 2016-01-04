namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;

    [MessageContract(IsWrapped=false), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class GetCarOperationStatisticsRequest
    {
        [MessageBodyMember(Name="GetCarOperationStatistics", Namespace="http://tempuri.org/", Order=0)]
        public GetCarOperationStatisticsRequestBody Body;

        public GetCarOperationStatisticsRequest()
        {
        }

        public GetCarOperationStatisticsRequest(GetCarOperationStatisticsRequestBody Body)
        {
            this.Body = Body;
        }
    }
}

