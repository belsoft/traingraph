namespace TISMonitor
{
    using System;

    [Serializable]
    public class TrainCourse
    {
        public DateTime ArrivalTime;
        public int Distance;
        public int DistancePerron;
        public PathElement Element;
        public int ElementSource;
        public TISMonitor.Station Station = null;
        public TimeSpan tsToEnterElement;
        public TimeSpan tsToEnterPerron;

        public string Dump()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", new object[] { this.Element.ID, (this.Station == null) ? "" : this.Station.ID, this.Distance, this.DistancePerron, this.tsToEnterElement.ToString(), this.tsToEnterPerron.ToString() });
        }
    }
}

