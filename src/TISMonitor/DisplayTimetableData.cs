namespace TISMonitor
{
    using System;

    [Serializable]
    public class DisplayTimetableData
    {
        public string Destination = "";
        public string DestinationDisplay = "";
        public string Line = "";
        public string StartStationDBID = "";
        public DateTime TimetableDate = new DateTime();
        public DateTime TimetableTime = new DateTime();
        public int TrackNo = 1;
        public string Train = "";

        public object Clone()
        {
            return (DisplayTimetableData) base.MemberwiseClone();
        }

        public virtual int CompareTo(object obj)
        {
            if (!(obj is DisplayTimetableData))
            {
                throw new ArgumentException("Object is not an DisplayTimetableData");
            }
            DisplayTimetableData data = (DisplayTimetableData) obj;
            int num = this.TimetableTime.CompareTo(data.TimetableTime);
            if (num != 0)
            {
                return num;
            }
            num = this.Train.CompareTo(data.Train);
            return ((num == 0) ? this.Destination.CompareTo(data.Destination) : num);
        }

        public virtual string Dump()
        {
            return string.Format("{0} {1}", this.Destination, this.TimetableTime.ToString("HH:mm:ss"));
        }

        public override bool Equals(object obj)
        {
            DisplayTimetableData data = (DisplayTimetableData) obj;
            return ((((this.Train == data.Train) && (this.Destination == data.Destination)) && ((this.TimetableTime == data.TimetableTime) && (this.TrackNo == data.TrackNo))) && (this.Line == data.Line));
        }

        public virtual DateTime GetDepartTime()
        {
            return this.TimetableTime;
        }

        public override int GetHashCode()
        {
            return ((((this.Train.GetHashCode() ^ this.Destination.GetHashCode()) ^ this.TimetableTime.GetHashCode()) ^ this.TrackNo.GetHashCode()) ^ this.Line.GetHashCode());
        }
    }
}

