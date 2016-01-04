namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    public class TrainPropertiesDlg : ChildWindow
    {
        private bool _contentLoaded;
        internal Button CancelButton;
        internal CheckBox checkBoxGoodsTrain;
        internal ComboBox comboBoxTrainNumbers;
        internal HyperlinkButton hyperlinkButton;
        internal Label labelTrainDescription;
        internal Label labelTrainNumber;
        internal Grid LayoutRoot;
        private bool m_enable = false;
        private TISMonitor.Train m_train = null;
        internal Button OKButton;
        internal TextBox textBoxTrainDescription;

        public TrainPropertiesDlg(TISMonitor.Train t, List<string> possibleTrainIds, Uri u, bool enable)
        {
            this.InitializeComponent();
            this.OKButton.Focus();
            this.m_enable = enable;
            this.m_train = t;
            base.Title = string.Format("Zug '{0}' Eigenschaften", this.m_train.ID);
            this.hyperlinkButton.set_TargetName("_blank");
            this.hyperlinkButton.set_NavigateUri(u);
            this.comboBoxTrainNumbers.set_ItemsSource(possibleTrainIds);
            this.comboBoxTrainNumbers.set_SelectedItem(this.m_train.ID);
            this.checkBoxGoodsTrain.set_IsChecked(new bool?(this.m_train.Locomotive));
            this.textBoxTrainDescription.set_Text(this.m_train.Description);
            if (!this.m_enable)
            {
                this.comboBoxTrainNumbers.set_IsEnabled(false);
                this.textBoxTrainDescription.set_IsEnabled(false);
                this.checkBoxGoodsTrain.set_IsEnabled(false);
                this.OKButton.set_IsEnabled(false);
            }
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
                Application.LoadComponent(this, new Uri("/TISMonitor;component/TrainPropertiesDlg.xaml", UriKind.Relative));
                this.LayoutRoot = (Grid) base.FindName("LayoutRoot");
                this.CancelButton = (Button) base.FindName("CancelButton");
                this.OKButton = (Button) base.FindName("OKButton");
                this.comboBoxTrainNumbers = (ComboBox) base.FindName("comboBoxTrainNumbers");
                this.labelTrainNumber = (Label) base.FindName("labelTrainNumber");
                this.checkBoxGoodsTrain = (CheckBox) base.FindName("checkBoxGoodsTrain");
                this.textBoxTrainDescription = (TextBox) base.FindName("textBoxTrainDescription");
                this.labelTrainDescription = (Label) base.FindName("labelTrainDescription");
                this.hyperlinkButton = (HyperlinkButton) base.FindName("hyperlinkButton");
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = true;
        }

        public string Description
        {
            get
            {
                return this.textBoxTrainDescription.get_Text();
            }
        }

        public bool Locomotive
        {
            get
            {
                return this.checkBoxGoodsTrain.get_IsChecked().Value;
            }
        }

        public TISMonitor.Train Train
        {
            get
            {
                return this.m_train;
            }
        }

        public string TrainIdNew
        {
            get
            {
                if (this.comboBoxTrainNumbers.get_SelectionBoxItem() != null)
                {
                    return this.comboBoxTrainNumbers.get_SelectionBoxItem().ToString();
                }
                return "";
            }
        }
    }
}

