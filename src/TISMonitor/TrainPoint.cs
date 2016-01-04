namespace TISMonitor
{
    using System;

    [Serializable]
    public class TrainPoint
    {
        public DateTime m_dt = DateTime.MinValue;
        public long m_nPosition;
        public TimeSpan m_tsDelay = new TimeSpan(0L);
    }
}

