namespace TISMonitor
{
    using System;
    using System.Windows;

    [Serializable]
    public class IconSpeaker : IconElement
    {
        private static Icon m_Icon = null;

        public IconSpeaker()
        {
        }

        public IconSpeaker(string strID) : base(strID)
        {
        }

        public override string GetAPIName()
        {
            return "SPEAKER";
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
            return XMLResourceLoaderUtils.GetString("SPEAKER");
        }

        public override void Reflect(int width, bool h)
        {
            base.BasePoint = h ? Element.GetPointRotatedByY(width, base.BasePoint) : Element.GetPointRotatedByX(width, base.BasePoint);
        }
    }
}

