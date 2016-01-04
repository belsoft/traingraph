namespace TISMonitor.TISWebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using System.Threading;

    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0"), DataContract(Name="TrainWebData", Namespace="http://tempuri.org/"), DebuggerStepThrough]
    public class TrainWebData : INotifyPropertyChanged
    {
        private string ActualOrLastStationField;
        private bool AtPerronField;
        private string CarIdField;
        private string ColorHiField;
        private string ColorLoField;
        private int DelaySecField;
        private string DescriptionField;
        private int DirectionField;
        private bool GPSEnabledField;
        private double GPSLatField;
        private double GPSLonField;
        private bool HasTimetableField;
        private int HeadElementArrivedSourceField;
        private string HeadElementField;
        private int HeadOffsetField;
        private string IDField;
        private string IDToDisplayField;
        private bool LocomotiveField;
        private string NextElementField;
        private int NextElementSourceField;
        private string TooltipField;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [DataMember(EmitDefaultValue=false, Order=0x11)]
        public string ActualOrLastStation
        {
            get
            {
                return this.ActualOrLastStationField;
            }
            set
            {
                if (!object.ReferenceEquals(this.ActualOrLastStationField, value))
                {
                    this.ActualOrLastStationField = value;
                    this.RaisePropertyChanged("ActualOrLastStation");
                }
            }
        }

        [DataMember(IsRequired=true, Order=9)]
        public bool AtPerron
        {
            get
            {
                return this.AtPerronField;
            }
            set
            {
                if (!this.AtPerronField.Equals(value))
                {
                    this.AtPerronField = value;
                    this.RaisePropertyChanged("AtPerron");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=20)]
        public string CarId
        {
            get
            {
                return this.CarIdField;
            }
            set
            {
                if (!object.ReferenceEquals(this.CarIdField, value))
                {
                    this.CarIdField = value;
                    this.RaisePropertyChanged("CarId");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=0x12)]
        public string ColorHi
        {
            get
            {
                return this.ColorHiField;
            }
            set
            {
                if (!object.ReferenceEquals(this.ColorHiField, value))
                {
                    this.ColorHiField = value;
                    this.RaisePropertyChanged("ColorHi");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=0x13)]
        public string ColorLo
        {
            get
            {
                return this.ColorLoField;
            }
            set
            {
                if (!object.ReferenceEquals(this.ColorLoField, value))
                {
                    this.ColorLoField = value;
                    this.RaisePropertyChanged("ColorLo");
                }
            }
        }

        [DataMember(IsRequired=true, Order=3)]
        public int DelaySec
        {
            get
            {
                return this.DelaySecField;
            }
            set
            {
                if (!this.DelaySecField.Equals(value))
                {
                    this.DelaySecField = value;
                    this.RaisePropertyChanged("DelaySec");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=12)]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                if (!object.ReferenceEquals(this.DescriptionField, value))
                {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }

        [DataMember(IsRequired=true, Order=7)]
        public int Direction
        {
            get
            {
                return this.DirectionField;
            }
            set
            {
                if (!this.DirectionField.Equals(value))
                {
                    this.DirectionField = value;
                    this.RaisePropertyChanged("Direction");
                }
            }
        }

        [DataMember(IsRequired=true, Order=0x10)]
        public bool GPSEnabled
        {
            get
            {
                return this.GPSEnabledField;
            }
            set
            {
                if (!this.GPSEnabledField.Equals(value))
                {
                    this.GPSEnabledField = value;
                    this.RaisePropertyChanged("GPSEnabled");
                }
            }
        }

        [DataMember(IsRequired=true, Order=14)]
        public double GPSLat
        {
            get
            {
                return this.GPSLatField;
            }
            set
            {
                if (!this.GPSLatField.Equals(value))
                {
                    this.GPSLatField = value;
                    this.RaisePropertyChanged("GPSLat");
                }
            }
        }

        [DataMember(IsRequired=true, Order=15)]
        public double GPSLon
        {
            get
            {
                return this.GPSLonField;
            }
            set
            {
                if (!this.GPSLonField.Equals(value))
                {
                    this.GPSLonField = value;
                    this.RaisePropertyChanged("GPSLon");
                }
            }
        }

        [DataMember(IsRequired=true, Order=13)]
        public bool HasTimetable
        {
            get
            {
                return this.HasTimetableField;
            }
            set
            {
                if (!this.HasTimetableField.Equals(value))
                {
                    this.HasTimetableField = value;
                    this.RaisePropertyChanged("HasTimetable");
                }
            }
        }

        [DataMember(EmitDefaultValue=false)]
        public string HeadElement
        {
            get
            {
                return this.HeadElementField;
            }
            set
            {
                if (!object.ReferenceEquals(this.HeadElementField, value))
                {
                    this.HeadElementField = value;
                    this.RaisePropertyChanged("HeadElement");
                }
            }
        }

        [DataMember(IsRequired=true, Order=6)]
        public int HeadElementArrivedSource
        {
            get
            {
                return this.HeadElementArrivedSourceField;
            }
            set
            {
                if (!this.HeadElementArrivedSourceField.Equals(value))
                {
                    this.HeadElementArrivedSourceField = value;
                    this.RaisePropertyChanged("HeadElementArrivedSource");
                }
            }
        }

        [DataMember(IsRequired=true)]
        public int HeadOffset
        {
            get
            {
                return this.HeadOffsetField;
            }
            set
            {
                if (!this.HeadOffsetField.Equals(value))
                {
                    this.HeadOffsetField = value;
                    this.RaisePropertyChanged("HeadOffset");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=5)]
        public string ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                if (!object.ReferenceEquals(this.IDField, value))
                {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=4)]
        public string IDToDisplay
        {
            get
            {
                return this.IDToDisplayField;
            }
            set
            {
                if (!object.ReferenceEquals(this.IDToDisplayField, value))
                {
                    this.IDToDisplayField = value;
                    this.RaisePropertyChanged("IDToDisplay");
                }
            }
        }

        [DataMember(IsRequired=true, Order=10)]
        public bool Locomotive
        {
            get
            {
                return this.LocomotiveField;
            }
            set
            {
                if (!this.LocomotiveField.Equals(value))
                {
                    this.LocomotiveField = value;
                    this.RaisePropertyChanged("Locomotive");
                }
            }
        }

        [DataMember(EmitDefaultValue=false)]
        public string NextElement
        {
            get
            {
                return this.NextElementField;
            }
            set
            {
                if (!object.ReferenceEquals(this.NextElementField, value))
                {
                    this.NextElementField = value;
                    this.RaisePropertyChanged("NextElement");
                }
            }
        }

        [DataMember(IsRequired=true, Order=11)]
        public int NextElementSource
        {
            get
            {
                return this.NextElementSourceField;
            }
            set
            {
                if (!this.NextElementSourceField.Equals(value))
                {
                    this.NextElementSourceField = value;
                    this.RaisePropertyChanged("NextElementSource");
                }
            }
        }

        [DataMember(EmitDefaultValue=false, Order=8)]
        public string Tooltip
        {
            get
            {
                return this.TooltipField;
            }
            set
            {
                if (!object.ReferenceEquals(this.TooltipField, value))
                {
                    this.TooltipField = value;
                    this.RaisePropertyChanged("Tooltip");
                }
            }
        }
    }
}

