namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [DebuggerStepThrough, DataContract(Namespace="http://tempuri.org/"), GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class GetDevicesResponseBody
    {
        [DataMember(EmitDefaultValue=false, Order=1)]
        public ObservableCollection<Car> cars;
        [DataMember(Order=0)]
        public bool GetDevicesResult;
        [DataMember(EmitDefaultValue=false, Order=2)]
        public string strError;

        public GetDevicesResponseBody()
        {
        }

        public GetDevicesResponseBody(bool GetDevicesResult, ObservableCollection<Car> cars, string strError)
        {
            this.GetDevicesResult = GetDevicesResult;
            this.cars = cars;
            this.strError = strError;
        }
    }
}

