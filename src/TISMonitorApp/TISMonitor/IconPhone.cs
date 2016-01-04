namespace TISMonitor
{
    using System;
    using System.Windows;

    [Serializable]
    public class IconPhone : IconElement
    {
        public IconPhone()
        {
        }

        public IconPhone(string strID) : base(strID)
        {
        }

        public override string GetAPIName()
        {
            return "PHONE";
        }

        protected override Icon GetIcon()
        {
            return null;
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("PHONE");
        }

        public override void Reflect(int width, bool h)
        {
            base.BasePoint = h ? Element.GetPointRotatedByY(width, base.BasePoint) : Element.GetPointRotatedByX(width, base.BasePoint);
        }
    }
}

