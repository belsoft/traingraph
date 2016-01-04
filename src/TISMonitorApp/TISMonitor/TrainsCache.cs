namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using TISWebServiceHelper;

    [DataContract]
    public class TrainsCache : CachableObject
    {
        [DataMember]
        public List<TrainWebData> Trains = new List<TrainWebData>();

        public override int CacheValidTimeSec()
        {
            return 60;
        }

        public override bool IsValid(string requiredVersion, out string strError)
        {
            if (!base.IsValid(requiredVersion, out strError))
            {
                return false;
            }
            return true;
        }

        public override bool Load(string versionReuired)
        {
            try
            {
                string str;
                TrainsCacheStorage storage = new TrainsCacheStorage();
                if (storage.Load(out str, out this.LastError))
                {
                    TrainsCache cache = DCSerializer.DeserializeWithDCS(typeof(TrainsCache), str) as TrainsCache;
                    base.CacheTime = cache.CacheTime;
                    this.Trains = cache.Trains;
                    base.Version = cache.Version;
                    if (!cache.IsValid(versionReuired, out this.LastError))
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception exception)
            {
                base.LastError = exception.Message;
            }
            return false;
        }

        public override bool Save()
        {
            TrainsCacheStorage storage = new TrainsCacheStorage();
            string data = DCSerializer.SerializeWithDCS(this);
            storage.Clear();
            bool flag = storage.Save(data, out this.LastError);
            base.LastError = base.LastError + string.Format(" Len={0}", data.Length);
            return flag;
        }
    }
}

