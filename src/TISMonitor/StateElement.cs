namespace TISMonitor
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class StateElement : Element
    {
        [XmlIgnore]
        public bool Defined;
        protected string m_strAddress;
        [XmlIgnore]
        public bool Valid;

        public StateElement()
        {
            this.m_strAddress = "";
            this.Valid = true;
            this.Defined = false;
        }

        public StateElement(string strID) : base(strID)
        {
            this.m_strAddress = "";
            this.Valid = true;
            this.Defined = false;
            this.m_strAddress = strID;
        }

        public override Form GetPropertiesForm()
        {
            return null;
        }

        public virtual void Invalidate()
        {
            this.Valid = true;
            this.Defined = false;
        }

        public virtual bool SetState(string strData, bool bState)
        {
            return true;
        }

        public string Address
        {
            get
            {
                return this.m_strAddress;
            }
            set
            {
                this.m_strAddress = value;
            }
        }
    }
}

