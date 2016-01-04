namespace TISMonitor
{
    using System;
    using System.Windows;

    [Serializable]
    public class IconCamera : IconElement
    {
        private int m_nPort;
        private string m_strAddress;
        private string m_strFolderVideo;

        public IconCamera()
        {
            this.m_strAddress = "";
            this.m_strFolderVideo = "";
            this.m_nPort = 0x2711;
        }

        public IconCamera(string strID) : base(strID)
        {
            this.m_strAddress = "";
            this.m_strFolderVideo = "";
            this.m_nPort = 0x2711;
        }

        public override string GetAPIName()
        {
            return "CAMERA";
        }

        protected override Icon GetIcon()
        {
            return null;
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("CAMERA");
        }

        public override Form GetPropertiesForm()
        {
            return null;
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

        public string FolderVideo
        {
            get
            {
                return this.m_strFolderVideo;
            }
            set
            {
                this.m_strFolderVideo = value;
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
    }
}

