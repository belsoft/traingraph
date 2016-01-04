namespace TISMonitor
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable]
    public class TrackedTrainListEx : TrackedTrainList
    {
        protected int m_nTotalTrainsDetected;

        public TrackedTrainListEx()
        {
            this.m_nTotalTrainsDetected = 0;
        }

        public TrackedTrainListEx(int nTrainLifeTimeMinutes) : base(nTrainLifeTimeMinutes)
        {
            this.m_nTotalTrainsDetected = 0;
        }

        public override void Clear()
        {
            lock (this)
            {
                base.Clear();
                this.m_nTotalTrainsDetected = 0;
            }
        }

        public int IncTotalTrainsDetected()
        {
            lock (this)
            {
                return ++this.m_nTotalTrainsDetected;
            }
        }

        public void Reset(out Hashtable htTrains, out int nTotalTrainsDetected)
        {
            lock (this)
            {
                htTrains = base.m_htTrain2TimeFinished.Clone();
                nTotalTrainsDetected = this.m_nTotalTrainsDetected;
                this.Clear();
            }
        }
    }
}

