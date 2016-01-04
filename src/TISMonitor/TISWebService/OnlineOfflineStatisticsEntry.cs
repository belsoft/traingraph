namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using System.Threading;

    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0"), DataContract(Name="OnlineOfflineStatisticsEntry", Namespace="http://tempuri.org/"), DebuggerStepThrough]
    public class OnlineOfflineStatisticsEntry : INotifyPropertyChanged
    {
        private string ActuelGPSLinkField;
        private string ActuelGPSTimeField;
        private string CarField;
        private string DisconnectionsField;
        private string LastOfflineLinkField;
        private string LastOfflineTimeField;
        private string LastOnlineLinkField;
        private string LastOnlineTimeField;
        private string OnlineTimeField;
        private double OnlineTimeMinutesField;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [DataMember(EmitDefaultValue=false, Order=9)]
        public string ActuelGPSLink
        {
            get
            {
                return this.ActuelGPSLinkField;
            }
            set
            {
                if (!object.ReferenceEquals(this.ActuelGPSLinkField, value))
                {
                    this.ActuelGPSLinkField = value;
                    this.RaisePropertyChanged("ActuelGPSLink");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=8)]
        public string ActuelGPSTime
        {
            get
            {
                return this.ActuelGPSTimeField;
            }
            set
            {
                if (!object.ReferenceEquals(this.ActuelGPSTimeField, value))
                {
                    this.ActuelGPSTimeField = value;
                    this.RaisePropertyChanged("ActuelGPSTime");
                }
            }
        }

        [DataMember(EmitDefaultValue=false)]
        public string Car
        {
            get
            {
                return this.CarField;
            }
            set
            {
                if (!object.ReferenceEquals(this.CarField, value))
                {
                    this.CarField = value;
                    this.RaisePropertyChanged("Car");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=3)]
        public string Disconnections
        {
            get
            {
                return this.DisconnectionsField;
            }
            set
            {
                if (!object.ReferenceEquals(this.DisconnectionsField, value))
                {
                    this.DisconnectionsField = value;
                    this.RaisePropertyChanged("Disconnections");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=5)]
        public string LastOfflineLink
        {
            get
            {
                return this.LastOfflineLinkField;
            }
            set
            {
                if (!object.ReferenceEquals(this.LastOfflineLinkField, value))
                {
                    this.LastOfflineLinkField = value;
                    this.RaisePropertyChanged("LastOfflineLink");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=4)]
        public string LastOfflineTime
        {
            get
            {
                return this.LastOfflineTimeField;
            }
            set
            {
                if (!object.ReferenceEquals(this.LastOfflineTimeField, value))
                {
                    this.LastOfflineTimeField = value;
                    this.RaisePropertyChanged("LastOfflineTime");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=7)]
        public string LastOnlineLink
        {
            get
            {
                return this.LastOnlineLinkField;
            }
            set
            {
                if (!object.ReferenceEquals(this.LastOnlineLinkField, value))
                {
                    this.LastOnlineLinkField = value;
                    this.RaisePropertyChanged("LastOnlineLink");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=6)]
        public string LastOnlineTime
        {
            get
            {
                return this.LastOnlineTimeField;
            }
            set
            {
                if (!object.ReferenceEquals(this.LastOnlineTimeField, value))
                {
                    this.LastOnlineTimeField = value;
                    this.RaisePropertyChanged("LastOnlineTime");
                }
            }
        }

        [DataMember(EmitDefaultValue=false)]
        public string OnlineTime
        {
            get
            {
                return this.OnlineTimeField;
            }
            set
            {
                if (!object.ReferenceEquals(this.OnlineTimeField, value))
                {
                    this.OnlineTimeField = value;
                    this.RaisePropertyChanged("OnlineTime");
                }
            }
        }

        [DataMember(IsRequired=true)]
        public double OnlineTimeMinutes
        {
            get
            {
                return this.OnlineTimeMinutesField;
            }
            set
            {
                if (!this.OnlineTimeMinutesField.Equals(value))
                {
                    this.OnlineTimeMinutesField = value;
                    this.RaisePropertyChanged("OnlineTimeMinutes");
                }
            }
        }
    }
}

