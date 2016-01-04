using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace TISWebServiceHelper
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples"), 
    ServiceKnownType(typeof(CurveData)), DataContract(Name = "cd"), 
    ServiceKnownType(typeof(int)), ServiceKnownType(typeof(double)), 
    ServiceKnownType(typeof(string)), ServiceKnownType(typeof(PointData))]
    public class CurveData
    {
        [DataMember(Name = "c")]
        public string Caption = "";
        private Dictionary<string, int> m_caption2Number = new Dictionary<string, int>();
        [DataMember(Name = "n")]
        public string Name = "";
        [DataMember(Name = "s")]
        public List<PointData> Points = new List<PointData>();
        [DataMember(Name = "r")]
        public bool Regular = true;

        public void CaptionSetFromStatistics()
        {
            this.Caption = "";
            int num = 0;
            foreach (KeyValuePair<string, int> pair in this.m_caption2Number)
            {
                if (pair.Value > num)
                {
                    num = pair.Value;
                    this.Caption = pair.Key;
                }
            }
            this.m_caption2Number.Clear();
        }

        public void CaptionUpdateStatistics(string caption)
        {
            int num = 0;
            if (this.m_caption2Number.ContainsKey(caption))
            {
                num = this.m_caption2Number[caption];
            }
            num++;
            this.m_caption2Number[caption] = num;
        }

        public CurveData Clone()
        {
            CurveData data = new CurveData
            {
                Caption = this.Caption,
                Name = this.Name,
                Regular = this.Regular
            };
            foreach (PointData data2 in this.Points)
            {
                data.Points.Add(data2.Clone());
            }
            return data;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public string Key
        {
            get
            {
                return string.Format("{0},{1}", this.Regular, this.Name);
            }
        }
    }
}