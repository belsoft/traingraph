namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [DebuggerStepThrough, DataContract(Namespace="http://tempuri.org/"), EditorBrowsable(EditorBrowsableState.Advanced), GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class GetGPSDeviceStatisticsResponseBody
    {
        [DataMember(Order=0)]
        public bool GetGPSDeviceStatisticsResult;
        [DataMember(EmitDefaultValue=false, Order=1)]
        public ObservableCollection<TrainGPSStatisticsWebData> listTrainGPSStatistic;
        [DataMember(EmitDefaultValue=false, Order=2)]
        public string strError;

        public GetGPSDeviceStatisticsResponseBody()
        {
        }

        public GetGPSDeviceStatisticsResponseBody(bool GetGPSDeviceStatisticsResult, ObservableCollection<TrainGPSStatisticsWebData> listTrainGPSStatistic, string strError)
        {
            this.GetGPSDeviceStatisticsResult = GetGPSDeviceStatisticsResult;
            this.listTrainGPSStatistic = listTrainGPSStatistic;
            this.strError = strError;
        }
    }
}

