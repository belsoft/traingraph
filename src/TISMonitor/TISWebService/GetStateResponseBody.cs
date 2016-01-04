namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), DataContract(Namespace="http://tempuri.org/")]
    public class GetStateResponseBody
    {
        [DataMember(Order=0)]
        public bool GetStateResult;
        [DataMember(EmitDefaultValue=false, Order=3)]
        public string offlineTrainsData;
        [DataMember(EmitDefaultValue=false, Order=4)]
        public string onlineButNotOnTheMapTrainsData;
        [DataMember(EmitDefaultValue=false, Order=2)]
        public string onlineTrainsData;
        [DataMember(EmitDefaultValue=false, Order=6)]
        public string strError;
        [DataMember(EmitDefaultValue=false, Order=1)]
        public string strOut;
        [DataMember(EmitDefaultValue=false, Order=5)]
        public string trainGraphData;

        public GetStateResponseBody()
        {
        }

        public GetStateResponseBody(bool GetStateResult, string strOut, string onlineTrainsData, string offlineTrainsData, string onlineButNotOnTheMapTrainsData, string trainGraphData, string strError)
        {
            this.GetStateResult = GetStateResult;
            this.strOut = strOut;
            this.onlineTrainsData = onlineTrainsData;
            this.offlineTrainsData = offlineTrainsData;
            this.onlineButNotOnTheMapTrainsData = onlineButNotOnTheMapTrainsData;
            this.trainGraphData = trainGraphData;
            this.strError = strError;
        }
    }
}

