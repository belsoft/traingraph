namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class TextLabelControl : UserControl
    {
        private bool _contentLoaded;
        internal Grid LayoutRoot;
        private TextLabel m_tl;
        internal TextBlock Text;

        public TextLabelControl()
        {
            this.m_tl = null;
            this.InitializeComponent();
        }

        public TextLabelControl(TextLabel tl)
        {
            this.m_tl = null;
            this.m_tl = tl;
            this.InitializeComponent();
            this.Draw();
        }

        public void Draw()
        {
            this.Text.set_Text("");
            if (this.m_tl != null)
            {
                this.Text.set_Text((this.m_tl.Text.Length > 0) ? this.m_tl.Text : this.m_tl.ID);
                this.Text.set_Foreground(new SolidColorBrush(_SilverlightHelper.ConvertToColor(this.m_tl.TextForeColor)));
                this.Text.set_FontFamily(new FontFamily(this.m_tl.FontName));
                this.Text.set_FontSize((double) (this.m_tl.FontSize + 3f));
                if ((this.m_tl.FontStyle & 1) == 1)
                {
                    this.Text.set_FontWeight(FontWeights.get_Bold());
                }
                if ((this.m_tl.FontStyle & 2) == 2)
                {
                    this.Text.set_FontStyle(FontStyles.get_Italic());
                }
                if ((this.m_tl.FontStyle & 4) == 4)
                {
                    this.Text.set_TextDecorations(TextDecorations.get_Underline());
                }
                RotateTransform transform = new RotateTransform();
                transform.set_Angle((double) this.m_tl.Escapement);
                this.Text.set_RenderTransform(transform);
            }
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/TextLabelControl.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.Text = (TextBlock) base.FindName("Text");
            }
        }
    }
}

