namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [DataContract(Namespace="http://tempuri.org/"), GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough]
    public class GetCarOperationStatisticsResponseBody
    {
        [DataMember(Order=0)]
        public bool GetCarOperationStatisticsResult;
        [DataMember(EmitDefaultValue=false, Order=2)]
        public string reportRangeTitle;
        [DataMember(EmitDefaultValue=false, Order=3)]
        public string strError;
        [DataMember(EmitDefaultValue=false, Order=1)]
        public ObservableCollection<OnlineOfflineStatisticsEntry> webList;

        public GetCarOperationStatisticsResponseBody()
        {
        }

        public GetCarOperationStatisticsResponseBody(bool GetCarOperationStatisticsResult, ObservableCollection<OnlineOfflineStatisticsEntry> webList, string reportRangeTitle, string strError)
        {
            this.GetCarOperationStatisticsResult = GetCarOperationStatisticsResult;
            this.webList = webList;
            this.reportRangeTitle = reportRangeTitle;
            this.strError = strError;
        }
    }
}

