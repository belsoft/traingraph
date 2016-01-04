namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Browser;
    using UICommon;

    public class App : Application
    {
        private bool _contentLoaded;
        public static TISLayoutParameters LayoutParameters;

        public App()
        {
            base.add_Startup(new StartupEventHandler(this, (IntPtr) this.Application_Startup));
            base.add_Exit(new EventHandler(this.Application_Exit));
            base.add_UnhandledException(new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.Application_UnhandledException));
            this.InitializeComponent();
        }

        private void Application_Exit(object sender, EventArgs e)
        {
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            int nTrainGraphHourHeight = 15;
            int nTrainGraphAutoScrollTimeout = 30;
            int nMaxPositionGap = 100;
            int nMaxTimeGap = 15;
            string strPartOfVersionName = "";
            string userName = "";
            LayoutParameters = TISLayoutParameters.GetLayoutParameters(new Dictionary<string, string>(e.get_InitParams()));
            try
            {
                nTrainGraphHourHeight = Convert.ToInt32(e.get_InitParams()["TrainGraphHourHeight"]);
            }
            catch
            {
            }
            try
            {
                nTrainGraphAutoScrollTimeout = Convert.ToInt32(e.get_InitParams()["TrainGraphScrollTimeout"]);
            }
            catch
            {
            }
            try
            {
                nMaxPositionGap = Convert.ToInt32(e.get_InitParams()["MaxPositionGap"]);
            }
            catch
            {
            }
            try
            {
                nMaxTimeGap = Convert.ToInt32(e.get_InitParams()["MaxTimeGap"]);
            }
            catch
            {
            }
            try
            {
                strPartOfVersionName = e.get_InitParams()["PartOfVersionName"].ToString();
            }
            catch
            {
            }
            try
            {
                userName = e.get_InitParams()["UserName"].ToString();
            }
            catch
            {
            }
            base.set_RootVisual(new MainPage(nTrainGraphHourHeight, nTrainGraphAutoScrollTimeout, nMaxTimeGap, nMaxPositionGap, strPartOfVersionName, userName));
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            Action action = null;
            if (!Debugger.IsAttached)
            {
                e.set_Handled(true);
                if (action == null)
                {
                    action = () => this.ReportErrorToDOM(e);
                }
                Deployment.get_Current().get_Dispatcher().BeginInvoke(action);
            }
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/App.xaml", UriKind.Relative));
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string str = (e.get_ExceptionObject().Message + e.get_ExceptionObject().StackTrace).Replace('"', '\'').Replace("\r\n", @"\n");
                HtmlPage.get_Window().Eval("throw new Error(\"Unhandled Error in Silverlight Application " + str + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}

