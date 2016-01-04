namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), DataContract(Namespace="http://tempuri.org/"), DebuggerStepThrough]
    public class GetCarOperationStatisticsRequestBody
    {
        [DataMember(EmitDefaultValue=false, Order=0)]
        public string strFrom;
        [DataMember(EmitDefaultValue=false, Order=1)]
        public string strTo;

        public GetCarOperationStatisticsRequestBody()
        {
        }

        public GetCarOperationStatisticsRequestBody(string strFrom, string strTo)
        {
            this.strFrom = strFrom;
            this.strTo = strTo;
        }
    }
}

