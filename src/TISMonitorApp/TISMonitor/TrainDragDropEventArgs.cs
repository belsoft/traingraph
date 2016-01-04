namespace TISMonitor
{
    using System;

    public class TrainDragDropEventArgs : EventArgs
    {
        public TrainBase Destination;
        public TrainBase Source;

        public TrainDragDropEventArgs(TrainBase s, TrainBase d)
        {
            this.Source = s;
            this.Destination = d;
        }
    }
}

