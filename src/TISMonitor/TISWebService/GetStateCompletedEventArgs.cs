namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough]
    public class GetStateCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        public GetStateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public string offlineTrainsData
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[2];
            }
        }

        public string onlineButNotOnTheMapTrainsData
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[3];
            }
        }

        public string onlineTrainsData
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
                return (bool) this.results[6];
            }
        }

        public string strError
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[5];
            }
        }

        public string strOut
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[0];
            }
        }

        public string trainGraphData
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[4];
            }
        }
    }
}

