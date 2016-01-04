namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [EditorBrowsable(EditorBrowsableState.Advanced), DataContract(Namespace="http://tempuri.org/"), GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough]
    public class GetTrainsOnlineResponseBody
    {
        [DataMember(Order=0)]
        public bool GetTrainsOnlineResult;
        [DataMember(EmitDefaultValue=false, Order=1)]
        public ObservableCollection<TrainWebData> listTrains;
        [DataMember(EmitDefaultValue=false, Order=2)]
        public string strError;

        public GetTrainsOnlineResponseBody()
        {
        }

        public GetTrainsOnlineResponseBody(bool GetTrainsOnlineResult, ObservableCollection<TrainWebData> listTrains, string strError)
        {
            this.GetTrainsOnlineResult = GetTrainsOnlineResult;
            this.listTrains = listTrains;
            this.strError = strError;
        }
    }
}

