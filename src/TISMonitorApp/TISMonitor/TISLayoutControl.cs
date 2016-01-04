namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    public class TISLayoutControl : UserControl
    {
        private bool _contentLoaded;
        internal Canvas CanvasRoot;
        internal Grid LayoutRoot;
        private TISLayout m_layout = new TISLayout();

        public TISLayoutControl()
        {
            this.InitializeComponent();
            this.m_layout.g = this.CanvasRoot;
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/TISLayoutControl.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.CanvasRoot = (Canvas) base.FindName("CanvasRoot");
            }
        }

        public TISLayout Layout
        {
            get
            {
                return this.m_layout;
            }
            set
            {
                this.m_layout = value;
            }
        }
    }
}

