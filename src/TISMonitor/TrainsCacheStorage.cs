namespace TISMonitor
{
    using System;
    using WebdataLoader;

    public class TrainsCacheStorage : IsolatedStorageHelper
    {
        public override string GetFileName()
        {
            return "TrainsCacheStorage.cache";
        }
    }
}

