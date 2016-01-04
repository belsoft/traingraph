namespace TISMonitor
{
    using System;
    using System.Collections;

    [Serializable]
    public class DisplayData : DisplayTimetableData
    {
        public TimeSpan TimeToWait;
        public DisplayDataType Type;

        public DisplayData(string strTrainID, string strDestination)
        {
            this.TimeToWait = new TimeSpan(0L);
            this.Type = DisplayDataType.Regular;
            base.Train = strTrainID;
            base.Destination = strDestination;
        }

        public DisplayData(string strTrainID, DisplayDataType type)
        {
            this.TimeToWait = new TimeSpan(0L);
            this.Type = DisplayDataType.Regular;
            base.Train = strTrainID;
            this.Type = type;
        }

        public override int CompareTo(object obj)
        {
            if (!(obj is DisplayData))
            {
                throw new ArgumentException("Object is not an DisplayData");
            }
            DisplayData data = (DisplayData) obj;
            return this.TimeToWait.CompareTo(data.TimeToWait);
        }

        public override string Dump()
        {
            return string.Format("{0}: {1} {2} {3} {4}", new object[] { this.Type.ToString(), base.Train, base.Destination, this.TimetableTime.ToString("HH:mm:ss"), string.Format("{0}:{1}:{0}", this.TimeToWait.Hours, this.TimeToWait.Minutes, this.TimeToWait.Seconds) });
        }

        public override DateTime GetDepartTime()
        {
            return (base.GetDepartTime() + this.TimeToWait);
        }

        public int GetMinutesToWait()
        {
            return (int) Math.Round(this.TimeToWait.TotalMinutes);
        }

        public class TimeToWaitDescComparer : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                return ((x as DisplayData).CompareTo(y) * -1);
            }
        }
    }
}

