namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class PerronControl : UserControl
    {
        private bool _contentLoaded;
        internal Image imageGeneral;
        internal Image imageYellow;
        internal Grid LayoutRoot;
        private bool m_Boarding = false;
        private Perron m_p = new Perron();
        public const double WIDTH = 22.0;

        public PerronControl(Perron p)
        {
            this.m_p = p;
            this.InitializeComponent();
            this.Draw();
        }

        private void Draw()
        {
            this.imageGeneral.set_Visibility(this.m_Boarding ? ((Visibility) 1) : ((Visibility) 0));
            this.imageYellow.set_Visibility(!this.m_Boarding ? ((Visibility) 1) : ((Visibility) 0));
            RotateTransform transform = new RotateTransform();
            transform.set_Angle((double) this.m_p.Escapement);
            base.set_RenderTransform(transform);
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/PerronControl.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.imageYellow = (Image) base.FindName("imageYellow");
                this.imageGeneral = (Image) base.FindName("imageGeneral");
            }
        }

        public bool Boarding
        {
            get
            {
                return this.m_Boarding;
            }
            set
            {
                if (this.m_Boarding != value)
                {
                    this.m_Boarding = value;
                    this.Draw();
                }
            }
        }
    }
}

