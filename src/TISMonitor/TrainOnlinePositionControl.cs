namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using TISWebServiceHelper;

    public class TrainOnlinePositionControl : UserControl
    {
        private bool _contentLoaded;
        internal Image image1;
        internal Grid LayoutRoot;
        public TrainWebData Train = null;
        public const double WIDTH = 16.0;

        public TrainOnlinePositionControl()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/TrainOnlinePositionControl.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.image1 = (Image) base.FindName("image1");
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}

