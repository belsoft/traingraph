namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using WebdataLoader;

    public class EventLogDialog : ChildWindow
    {
        private bool _contentLoaded;
        internal Button buttonClearCache;
        internal Button buttonIncreaseQuota;
        internal DataGrid dataGridEvents;
        internal Grid LayoutRoot;
        private List<string> m_logEntries = new List<string>();
        internal Button OKButton;

        public EventLogDialog(List<string> l)
        {
            this.InitializeComponent();
            this.m_logEntries = l;
            List<Data> list = new List<Data>();
            foreach (string str in l)
            {
                Data item = new Data {
                    Entry = str
                };
                list.Add(item);
            }
            this.dataGridEvents.ItemsSource = list;
        }

        private void buttonClearCache_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            string str = "";
            TrainsCacheStorage storage = new TrainsCacheStorage();
            if (!storage.Clear())
            {
                flag = false;
                str = str + string.Format("\n\r {0}", storage.LastError);
            }
            TrainGraphCacheStorage storage2 = new TrainGraphCacheStorage();
            if (!storage2.Clear())
            {
                flag = false;
                str = str + string.Format("\n\r {0}", storage2.LastError);
            }
            LayoutCacheStorage storage3 = new LayoutCacheStorage();
            if (!storage3.Clear())
            {
                flag = false;
                str = str + string.Format("\n\r {0}", storage3.LastError);
            }
            if (flag)
            {
                MessageBox.Show("Cache removed successfully");
            }
            else
            {
                MessageBox.Show("Cache removing failed..." + str);
            }
        }

        private void buttonIncreaseQuota_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageHelper.IncreaseTo(10);
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Application.LoadComponent(this, new Uri("/TISMonitor;component/EventLogDialog.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.OKButton = (Button) base.FindName("OKButton");
                this.dataGridEvents = (DataGrid) base.FindName("dataGridEvents");
                this.buttonClearCache = (Button) base.FindName("buttonClearCache");
                this.buttonIncreaseQuota = (Button) base.FindName("buttonIncreaseQuota");
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = true;
        }
    }
}

