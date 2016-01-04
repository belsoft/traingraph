namespace TISMonitor
{
    using System;
    using System.Windows;

    public abstract class LegendUILoader
    {
        protected LegendUILoader()
        {
        }

        public abstract FrameworkElement GetControl();
    }
}

