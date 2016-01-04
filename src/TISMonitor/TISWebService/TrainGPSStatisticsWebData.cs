namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using System.Threading;

    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0"), DataContract(Name="TrainGPSStatisticsWebData", Namespace="http://tempuri.org/"), DebuggerStepThrough]
    public class TrainGPSStatisticsWebData : INotifyPropertyChanged
    {
        private string GPSTimeField;
        private float LatitudeField;
        private float LongitudeField;
        private int SpeedField;
        private string TrainIDField;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [DataMember(EmitDefaultValue=false)]
        public string GPSTime
        {
            get
            {
                return this.GPSTimeField;
            }
            set
            {
                if (!object.ReferenceEquals(this.GPSTimeField, value))
                {
                    this.GPSTimeField = value;
                    this.RaisePropertyChanged("GPSTime");
                }
            }
        }

        [DataMember(IsRequired=true, Order=2)]
        public float Latitude
        {
            get
            {
                return this.LatitudeField;
            }
            set
            {
                if (!this.LatitudeField.Equals(value))
                {
                    this.LatitudeField = value;
                    this.RaisePropertyChanged("Latitude");
                }
            }
        }

        [DataMember(IsRequired=true, Order=3)]
        public float Longitude
        {
            get
            {
                return this.LongitudeField;
            }
            set
            {
                if (!this.LongitudeField.Equals(value))
                {
                    this.LongitudeField = value;
                    this.RaisePropertyChanged("Longitude");
                }
            }
        }

        [DataMember(IsRequired=true, Order=4)]
        public int Speed
        {
            get
            {
                return this.SpeedField;
            }
            set
            {
                if (!this.SpeedField.Equals(value))
                {
                    this.SpeedField = value;
                    this.RaisePropertyChanged("Speed");
                }
            }
        }

        [DataMember(EmitDefaultValue=false)]
        public string TrainID
        {
            get
            {
                return this.TrainIDField;
            }
            set
            {
                if (!object.ReferenceEquals(this.TrainIDField, value))
                {
                    this.TrainIDField = value;
                    this.RaisePropertyChanged("TrainID");
                }
            }
        }
    }
}

