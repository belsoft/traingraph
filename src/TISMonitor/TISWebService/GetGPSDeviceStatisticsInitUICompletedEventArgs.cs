namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough]
    public class GetGPSDeviceStatisticsInitUICompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        public GetGPSDeviceStatisticsInitUICompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public ArrayOfString availableDevicesInHistory
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (ArrayOfString) this.results[1];
            }
        }

        public string dateTimeFrom
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[2];
            }
        }

        public string dateTimeTo
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[3];
            }
        }

        public bool historyExists
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (bool) this.results[0];
            }
        }

        public bool Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (bool) this.results[5];
            }
        }

        public string strError
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[4];
            }
        }
    }
}

