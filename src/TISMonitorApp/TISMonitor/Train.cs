namespace TISMonitor
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public class Train : TrainBase
    {
        public bool AtPerron = false;
        public string CarId = "";
        public double GPSLat = 0.0;
        public double GPSLon = 0.0;
        public bool HasTimetable = false;
        public string Tooltip;

        public static void AssignBlack(out Color clrLo, out Color clrHi)
        {
            clrHi = Colors.get_Black();
            clrLo = Color.FromArgb(0xff, 0x80, 0x80, 0x80);
        }

        public override string GetToolTipText()
        {
            return this.Tooltip;
        }

        public static void GetTrainColors(Train train, out Color clrLo, out Color clrHi)
        {
            if (train.Locomotive)
            {
                clrLo = Color.FromArgb(0xff, 0xc0, 0xc0, 0xc0);
                clrHi = Color.FromArgb(0xff, 0x80, 0x80, 0x80);
            }
            else if (!train.TimeTableAssigned())
            {
                AssignBlack(out clrLo, out clrHi);
            }
            else if (train.ID.IndexOf('?') >= 0)
            {
                AssignBlack(out clrLo, out clrHi);
            }
            else
            {
                TrainBase.GetDelayColor(TimeSpan.FromSeconds(train.Delay.TotalSeconds), out clrLo, out clrHi);
            }
        }

        public override bool TimeTableAssigned()
        {
            return this.HasTimetable;
        }
    }
}

