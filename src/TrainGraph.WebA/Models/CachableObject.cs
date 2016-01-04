using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TISWebServiceHelper
{
    [DataContract]
    public class CachableObject
    {
        [DataMember]
        public DateTime CacheTime;
        public string LastError = "";
        [DataMember]
        public string Version;

        public virtual int CacheValidTimeSec()
        {
            return 0;
        }

        public virtual bool IsValid(string requiredVersion, out string strError)
        {
            strError = "";
            if (this.Version != requiredVersion)
            {
                strError = string.Format("Invalid version '{0}', has to be '{1}'", this.Version, requiredVersion);
                return false;
            }
            TimeSpan span = (TimeSpan)(DateTime.Now - this.CacheTime);
            double totalSeconds = span.TotalSeconds;
            if ((totalSeconds >= 0.0) && (totalSeconds >= this.CacheValidTimeSec()))
            {
                strError = "CacheTime is " + this.CacheTime.ToString();
                return false;
            }
            return true;
        }

        public virtual bool Load(string versionReuired)
        {
            return false;
        }

        public virtual bool Save()
        {
            this.CacheTime = DateTime.Now;
            return false;
        }
    }
}