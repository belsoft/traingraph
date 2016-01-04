namespace TISMonitor
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class Hashtable : Dictionary<object, object>
    {
        public Hashtable Clone()
        {
            return this;
        }

        public bool Contains(object o)
        {
            return this.Contains(o);
        }
    }
}

