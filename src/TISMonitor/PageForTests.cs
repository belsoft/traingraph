namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    public class PageForTests : Page
    {
        private bool _contentLoaded;
        internal Grid LayoutRoot;
        internal TextBlock textBlock1;

        public PageForTests()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/PageForTests.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.textBlock1 = (TextBlock) base.FindName("textBlock1");
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}

