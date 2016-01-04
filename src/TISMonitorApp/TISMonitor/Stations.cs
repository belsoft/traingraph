namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using TISWebServiceHelper;

    [Serializable]
    public class Stations : ArrayList
    {
        private Hashtable m_htDBID2Station = new Hashtable();
        private Hashtable m_htName2Station = new Hashtable();

        public void AddRude(Layout l, Station s)
        {
            base.Add(s);
            this.m_htDBID2Station[s.ID] = s;
            this.m_htName2Station[s.ID] = s;
            s.InitAfterLoad(l);
        }

        public Station GetStationByDBID(string str)
        {
            if (this.m_htDBID2Station.ContainsKey(str))
            {
                return (Station) this.m_htDBID2Station[str];
            }
            return null;
        }

        public Station GetStationByID(string str)
        {
            if (this.m_htName2Station.ContainsKey(str))
            {
                return (Station) this.m_htName2Station[str];
            }
            return null;
        }

        public void InitAfterLoad(Layout l, List<StationWebData> listStations)
        {
            foreach (StationWebData data in listStations)
            {
                Station item = new Station();
                base.Add(item);
                item.ID = data.ID;
                item.DBID = data.DBID;
                item.ShortName = data.ShortName;
                this.m_htName2Station[item.ID] = item;
                item.InitAfterLoad(l);
            }
        }

        public void Initialize()
        {
            base.Clear();
            this.m_htName2Station.Clear();
            this.m_htDBID2Station.Clear();
        }
    }
}

