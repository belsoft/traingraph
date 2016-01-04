namespace TISMonitor
{
    using System.Windows;

    public class LegendLoaderOffline : LegendUILoader
    {
        public override FrameworkElement GetControl()
        {
            return new TrainOfflinePositionControl();
        }
    }
}

