namespace TISMonitor
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using TISWebServiceHelper;

    [ServiceContract(Namespace="http://Microsoft.ServiceModel.Samples"), ServiceKnownType(typeof(double)), ServiceKnownType(typeof(CurveData)), ServiceKnownType(typeof(PointData)), ServiceKnownType(typeof(string)), ServiceKnownType(typeof(int)), ServiceKnownType(typeof(MarkerData)), ServiceKnownType(typeof(TrainGraphData)), DataContract]
    public class TrainGraphCache : TrainGraphData
    {
        [DataMember]
        public DateTime LastTrainGraphTime;
        [DataMember]
        public DateTime OperationDate;
        [DataMember]
        public DateTime ServerTime;

        public override int CacheValidTimeSec()
        {
            return 0xa8c0;
        }

        public TrainGraphCache Clone()
        {
            TrainGraphCache cache = new TrainGraphCache {
                CacheTime = base.CacheTime,
                LastTrainGraphTime = this.LastTrainGraphTime,
                OperationDate = this.OperationDate,
                ServerTime = this.ServerTime,
                Version = base.Version,
                TimeStart = base.TimeStart,
                TimeStop = base.TimeStop
            };
            foreach (MarkerData data in base.Markers)
            {
                cache.Markers.Add(data.Clone());
            }
            foreach (CurveData data2 in base.Curves)
            {
                cache.Curves.Add(data2.Clone());
            }
            return cache;
        }

        public override bool IsValid(string requiredVersion, out string strError)
        {
            strError = "";
            if (!base.IsValid(requiredVersion, out strError))
            {
                return false;
            }
            if (base.Markers.Count == 0)
            {
                strError = "Markers.Count == 0";
                return false;
            }
            return true;
        }

        public override bool Load(string versionReuired)
        {
            try
            {
                byte[] buffer;
                TrainGraphCacheStorage storage = new TrainGraphCacheStorage();
                if (storage.Load(out buffer, out this.LastError))
                {
                    TrainGraphCache cache = DCSerializer.DeserializeWithDCSMS(typeof(TrainGraphCache), buffer) as TrainGraphCache;
                    base.CacheTime = cache.CacheTime;
                    base.Curves = cache.Curves;
                    base.Markers = cache.Markers;
                    this.LastTrainGraphTime = cache.LastTrainGraphTime;
                    this.OperationDate = cache.OperationDate;
                    this.ServerTime = cache.ServerTime;
                    base.TimeStart = cache.TimeStart;
                    base.TimeStop = cache.TimeStop;
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
            TrainGraphCacheStorage storage = new TrainGraphCacheStorage();
            byte[] data = DCSerializer.SerializeWithDCSMS(this);
            storage.Clear();
            return storage.Save(data, out this.LastError);
        }
    }
}

