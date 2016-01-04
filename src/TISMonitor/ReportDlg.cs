namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    public class ReportDlg : ChildWindow
    {
        private bool _contentLoaded;
        internal Button CancelButton;
        internal Label label1;
        internal Grid LayoutRoot;
        internal Button OKButton;

        public ReportDlg()
        {
            this.InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = false;
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/ReportDlg.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.CancelButton = (Button) base.FindName("CancelButton");
                this.OKButton = (Button) base.FindName("OKButton");
                this.label1 = (Label) base.FindName("label1");
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = true;
        }
    }
}

