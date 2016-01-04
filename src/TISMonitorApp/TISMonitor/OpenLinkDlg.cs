namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    public class OpenLinkDlg : ChildWindow
    {
        private bool _contentLoaded;
        internal Button CancelButton;
        internal HyperlinkButton hyperlinkButton1;
        internal Grid LayoutRoot;
        private Uri m_u = null;

        public OpenLinkDlg(Uri u)
        {
            this.InitializeComponent();
            this.m_u = u;
            this.hyperlinkButton1.set_TargetName("_blank");
            this.hyperlinkButton1.set_NavigateUri(u);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = false;
        }

        private void hyperlinkButton1_Click(object sender, RoutedEventArgs e)
        {
            this.hyperlinkButton1.set_TargetName("_blank");
            this.hyperlinkButton1.set_NavigateUri(this.m_u);
            base.Close();
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/OpenLinkDlg.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.CancelButton = (Button) base.FindName("CancelButton");
                this.hyperlinkButton1 = (HyperlinkButton) base.FindName("hyperlinkButton1");
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = true;
        }
    }
}

