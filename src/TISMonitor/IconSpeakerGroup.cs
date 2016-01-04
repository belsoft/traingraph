namespace TISMonitor
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class IconSpeakerGroup : IconElement
    {
        public const string ELEMENT_NAME = "SPEAKERGROUP";
        private static Font m_fFont = null;
        private static Icon m_Icon = null;
        private string m_strText;

        public IconSpeakerGroup(string strID) : base(strID)
        {
            this.m_strText = "";
            base.APIID = strID;
        }

        public override void Draw(Grid g)
        {
            base.Draw(g);
            Rectangle bounds = base.Bounds;
            bounds.X += 0x12;
            bounds.Width = 400;
        }

        public override bool Equals(object obj)
        {
            IconSpeakerGroup group = (IconSpeakerGroup) obj;
            return (base.APIID == group.APIID);
        }

        public override string GetAPIName()
        {
            return "SPEAKERGROUP";
        }

        public override Font GetFont()
        {
            return m_fFont;
        }

        public override int GetHashCode()
        {
            return base.APIID.GetHashCode();
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
            return XMLResourceLoaderUtils.GetString("SPEAKERGROUP");
        }

        public override string GetToolTipText(bool bDetailed)
        {
            string str;
            if (bDetailed)
            {
                str = "{0} \"{1}\" {2} - {3}";
                return string.Format(str, new object[] { this.GetName(), this.Text, base.ID, this.State2Description() });
            }
            str = "{0} \"{1}\" - {2}";
            return string.Format(str, this.GetName(), this.Text, this.State2Description());
        }

        public void Reclect(int wodth)
        {
        }

        public override void Reflect(int width, bool h)
        {
            base.BasePoint = h ? Element.GetPointRotatedByY(width, base.BasePoint) : Element.GetPointRotatedByX(width, base.BasePoint);
        }

        public string Text
        {
            get
            {
                return this.m_strText;
            }
            set
            {
                this.m_strText = value;
            }
        }
    }
}

