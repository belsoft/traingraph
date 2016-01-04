namespace TISMonitor
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class ArrayList : List<object>
    {
        public ArrayList Clone()
        {
            ArrayList list = new ArrayList();
            foreach (object obj2 in this)
            {
                list.Add(obj2);
            }
            return list;
        }
    }
}

