namespace TISMonitor
{
    using System;
    using System.Windows;

    [Serializable]
    public class IconLANSwitch : IconElement
    {
        private ArrayList m_Cameras;
        private static Icon m_Icon = null;
        private int m_nPort;
        private int m_nSoftwareId;
        private string m_strAddress;
        private string m_strCameras;

        public IconLANSwitch()
        {
            this.m_strAddress = "";
            this.m_nPort = 0;
            this.m_strCameras = "";
            this.m_Cameras = new ArrayList();
            this.m_nSoftwareId = 0;
        }

        public IconLANSwitch(string strID) : base(strID)
        {
            this.m_strAddress = "";
            this.m_nPort = 0;
            this.m_strCameras = "";
            this.m_Cameras = new ArrayList();
            this.m_nSoftwareId = 0;
        }

        public override string GetAPIName()
        {
            return "LANSWITCH";
        }

        protected override Icon GetIcon()
        {
            if (m_Icon == null)
            {
            }
            return m_Icon;
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("LANSWITCH");
        }

        public override Form GetPropertiesForm()
        {
            return null;
        }

        public override string GetToolTipText(bool bDetailed)
        {
            string str;
            if (bDetailed)
            {
                str = "{0} {1} - {2}";
                return string.Format(str, this.GetName(), base.ID, this.State2Description());
            }
            str = "{0} {1} - {2}";
            return string.Format(str, this.GetName(), base.Station, this.State2Description());
        }

        public override void InitAfterLoad(Layout l)
        {
            if (this.m_strCameras.Length > 0)
            {
                foreach (string str in this.m_strCameras.Split(new char[] { ';' }))
                {
                    IconCamera element = base.m_Layout.GetElement(str) as IconCamera;
                    if (element != null)
                    {
                        this.m_Cameras.Add(element);
                    }
                }
            }
            base.InitAfterLoad(l);
        }

        public override void Reflect(int width, bool h)
        {
            base.BasePoint = h ? Element.GetPointRotatedByY(width, base.BasePoint) : Element.GetPointRotatedByX(width, base.BasePoint);
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

        public string Cameras
        {
            get
            {
                return this.m_strCameras;
            }
            set
            {
                this.m_strCameras = value;
            }
        }

        public int Port
        {
            get
            {
                return this.m_nPort;
            }
            set
            {
                this.m_nPort = value;
            }
        }

        public int SoftwareId
        {
            get
            {
                return this.m_nSoftwareId;
            }
            set
            {
                this.m_nSoftwareId = value;
            }
        }
    }
}

