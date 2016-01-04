namespace TISMonitor
{
    using System;

    [Serializable]
    public class TrackedTrainList
    {
        protected Hashtable m_htTrain2TimeFinished;
        protected int m_nTrainLifeTimeMinutes;

        public TrackedTrainList()
        {
            this.m_htTrain2TimeFinished = new Hashtable();
            this.m_nTrainLifeTimeMinutes = 720;
        }

        public TrackedTrainList(int nTrainLifeTimeMinutes)
        {
            this.m_htTrain2TimeFinished = new Hashtable();
            this.m_nTrainLifeTimeMinutes = 720;
            this.m_nTrainLifeTimeMinutes = nTrainLifeTimeMinutes;
        }

        public void AddTrain(string strTrainId, DateTime dt)
        {
            lock (this)
            {
                this.m_htTrain2TimeFinished[strTrainId] = dt;
            }
        }

        public virtual void Clear()
        {
            lock (this)
            {
                this.m_htTrain2TimeFinished.Clear();
            }
        }

        public void ClearOld(DateTime dt)
        {
            lock (this)
            {
                ArrayList list = new ArrayList();
                foreach (string str in this.m_htTrain2TimeFinished.Keys)
                {
                    if (!this.TrainTracked(str, dt))
                    {
                        list.Add(str);
                    }
                }
                foreach (string str in list)
                {
                    this.m_htTrain2TimeFinished.Remove(str);
                }
            }
        }

        public Hashtable GetHashtable()
        {
            lock (this)
            {
                return this.m_htTrain2TimeFinished.Clone();
            }
        }

        public bool TrainTracked(string strTrainId, DateTime dt)
        {
            lock (this)
            {
                if (this.m_htTrain2TimeFinished.Contains(strTrainId))
                {
                    DateTime time = (DateTime) this.m_htTrain2TimeFinished[strTrainId];
                    TimeSpan span = (TimeSpan) (dt - time);
                    return (span.Duration().TotalMinutes <= this.m_nTrainLifeTimeMinutes);
                }
                return false;
            }
        }

        public int TrainLifeTimeMinutes
        {
            get
            {
                return this.m_nTrainLifeTimeMinutes;
            }
            set
            {
                this.m_nTrainLifeTimeMinutes = value;
            }
        }
    }
}

