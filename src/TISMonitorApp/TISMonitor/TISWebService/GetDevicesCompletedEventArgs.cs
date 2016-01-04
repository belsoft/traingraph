namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;

    [DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class GetDevicesCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        public GetDevicesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public ObservableCollection<Car> cars
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (ObservableCollection<Car>) this.results[0];
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

