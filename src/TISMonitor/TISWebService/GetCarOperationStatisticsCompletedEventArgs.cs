namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;

    [DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class GetCarOperationStatisticsCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        public GetCarOperationStatisticsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public string reportRangeTitle
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[1];
            }
        }

        public bool Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (bool) this.results[3];
            }
        }

        public string strError
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[2];
            }
        }

        public ObservableCollection<OnlineOfflineStatisticsEntry> webList
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (ObservableCollection<OnlineOfflineStatisticsEntry>) this.results[0];
            }
        }
    }
}

