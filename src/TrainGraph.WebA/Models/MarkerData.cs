using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace TISWebServiceHelper
{

    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples"), DataContract(Name = "md"), 
    ServiceKnownType(typeof(int)), ServiceKnownType(typeof(MarkerData)), 
    ServiceKnownType(typeof(string)), ServiceKnownType(typeof(double))]
    public class MarkerData
    {
        [DataMember(Name = "c")]
        public int Color = 0x808080;
        [DataMember(Name = "i")]
        public string ID = "";
        [DataMember(Name = "p")]
        public int Position = 0;
        [DataMember(Name = "s")]
        public PositionMarkerStyle Style = PositionMarkerStyle.MarkerStation;
        [DataMember(Name = "v")]
        public bool Visible = true;

        public MarkerData Clone()
        {
            return new MarkerData { Color = this.Color, ID = this.ID, Position = this.Position, Style = this.Style, Visible = this.Visible };
        }
    }
}