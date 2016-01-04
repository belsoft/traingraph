using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using System.Xml.Serialization;

namespace TISWebServiceHelper
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples"), ServiceKnownType(typeof(string)), ServiceKnownType(typeof(int)), ServiceKnownType(typeof(double)), ServiceKnownType(typeof(PointData)), DataContract(Name = "pd")]
    public class PointData
    {
        [DataMember(Name = "c"), XmlElement("c")]
        public int Color;
        [XmlElement("d"), DataMember(Name = "d")]
        public System.DateTime DateTime;
        [DataMember(Name = "p"), XmlElement("p")]
        public int Position;
        [XmlElement("t"), DataMember(Name = "t")]
        public int Type;

        public PointData Clone()
        {
            return new PointData { Color = this.Color, DateTime = this.DateTime, Position = this.Position, Type = this.Type };
        }
    }
}