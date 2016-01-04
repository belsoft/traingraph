namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using System.Threading;

    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0"), DebuggerStepThrough, DataContract(Name="Car", Namespace="http://tempuri.org/")]
    public class Car : INotifyPropertyChanged
    {
        private string CarNumberField;
        private string DeviceIdField;
        private float? OfflineLatitudeField;
        private float? OfflineLongitudeField;
        private DateTime? OfflineTimeField;
        private float? OnlineLatitudeField;
        private float? OnlineLongitudeField;
        private DateTime? OnlineTimeField;
        private int RecIdField;
        private byte? StateField;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [DataMember(EmitDefaultValue=false, Order=1)]
        public string CarNumber
        {
            get
            {
                return this.CarNumberField;
            }
            set
            {
                if (!object.ReferenceEquals(this.CarNumberField, value))
                {
                    this.CarNumberField = value;
                    this.RaisePropertyChanged("CarNumber");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=2)]
        public string DeviceId
        {
            get
            {
                return this.DeviceIdField;
            }
            set
            {
                if (!object.ReferenceEquals(this.DeviceIdField, value))
                {
                    this.DeviceIdField = value;
                    this.RaisePropertyChanged("DeviceId");
                }
            }
        }

        [DataMember(IsRequired=true, Order=7)]
        public float? OfflineLatitude
        {
            get
            {
                return this.OfflineLatitudeField;
            }
            set
            {
                if (!this.OfflineLatitudeField.Equals(value))
                {
                    this.OfflineLatitudeField = value;
                    this.RaisePropertyChanged("OfflineLatitude");
                }
            }
        }

        [DataMember(IsRequired=true, Order=8)]
        public float? OfflineLongitude
        {
            get
            {
                return this.OfflineLongitudeField;
            }
            set
            {
                if (!this.OfflineLongitudeField.Equals(value))
                {
                    this.OfflineLongitudeField = value;
                    this.RaisePropertyChanged("OfflineLongitude");
                }
            }
        }

        [DataMember(IsRequired=true, Order=6)]
        public DateTime? OfflineTime
        {
            get
            {
                return this.OfflineTimeField;
            }
            set
            {
                if (!this.OfflineTimeField.Equals(value))
                {
                    this.OfflineTimeField = value;
                    this.RaisePropertyChanged("OfflineTime");
                }
            }
        }

        [DataMember(IsRequired=true, Order=4)]
        public float? OnlineLatitude
        {
            get
            {
                return this.OnlineLatitudeField;
            }
            set
            {
                if (!this.OnlineLatitudeField.Equals(value))
                {
                    this.OnlineLatitudeField = value;
                    this.RaisePropertyChanged("OnlineLatitude");
                }
            }
        }

        [DataMember(IsRequired=true, Order=5)]
        public float? OnlineLongitude
        {
            get
            {
                return this.OnlineLongitudeField;
            }
            set
            {
                if (!this.OnlineLongitudeField.Equals(value))
                {
                    this.OnlineLongitudeField = value;
                    this.RaisePropertyChanged("OnlineLongitude");
                }
            }
        }

        [DataMember(IsRequired=true, Order=3)]
        public DateTime? OnlineTime
        {
            get
            {
                return this.OnlineTimeField;
            }
            set
            {
                if (!this.OnlineTimeField.Equals(value))
                {
                    this.OnlineTimeField = value;
                    this.RaisePropertyChanged("OnlineTime");
                }
            }
        }

        [DataMember(IsRequired=true)]
        public int RecId
        {
            get
            {
                return this.RecIdField;
            }
            set
            {
                if (!this.RecIdField.Equals(value))
                {
                    this.RecIdField = value;
                    this.RaisePropertyChanged("RecId");
                }
            }
        }

        [DataMember(IsRequired=true, Order=9)]
        public byte? State
        {
            get
            {
                return this.StateField;
            }
            set
            {
                if (!this.StateField.Equals(value))
                {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
    }
}

