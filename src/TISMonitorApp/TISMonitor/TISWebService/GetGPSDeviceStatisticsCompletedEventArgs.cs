namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough]
    public class GetGPSDeviceStatisticsCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        public GetGPSDeviceStatisticsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public ObservableCollection<TrainGPSStatisticsWebData> listTrainGPSStatistic
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (ObservableCollection<TrainGPSStatisticsWebData>) this.results[0];
            }
        }

        public bool Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (bool) this.results[2];
            }
        }

        public string strError
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[1];
            }
        }
    }
}

