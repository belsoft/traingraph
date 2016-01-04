namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), DataContract(Namespace="http://tempuri.org/"), DebuggerStepThrough]
    public class SetTrainDataRequestBody
    {
        [DataMember(Order=2)]
        public bool goodsTrain;
        [DataMember(EmitDefaultValue=false, Order=3)]
        public string trainDescription;
        [DataMember(EmitDefaultValue=false, Order=1)]
        public string trainIdNew;
        [DataMember(EmitDefaultValue=false, Order=0)]
        public string trainIdOld;
        [DataMember(EmitDefaultValue=false, Order=4)]
        public string userName;

        public SetTrainDataRequestBody()
        {
        }

        public SetTrainDataRequestBody(string trainIdOld, string trainIdNew, bool goodsTrain, string trainDescription, string userName)
        {
            this.trainIdOld = trainIdOld;
            this.trainIdNew = trainIdNew;
            this.goodsTrain = goodsTrain;
            this.trainDescription = trainDescription;
            this.userName = userName;
        }
    }
}

