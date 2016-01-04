namespace TISMonitor
{
    using System.Windows;

    public class LegendLoaderOnline : LegendUILoader
    {
        public override FrameworkElement GetControl()
        {
            return new TrainOnlinePositionControl();
        }
    }
}

