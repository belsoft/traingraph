namespace TISMonitor
{
    using System;

    public class TrainPropEventArgs : EventArgs
    {
        public TrainBase Train;

        public TrainPropEventArgs(TrainBase t)
        {
            this.Train = t;
        }
    }
}

