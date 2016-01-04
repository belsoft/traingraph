namespace TISMonitor
{
    using System;
    using WebdataLoader;

    public class TrainGraphCacheStorage : IsolatedStorageHelper
    {
        public override string GetFileName()
        {
            return "TrainGraphCacheStorage.cache";
        }
    }
}

