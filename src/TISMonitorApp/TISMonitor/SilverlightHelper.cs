namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;

    public class SilverlightHelper
    {
        public static Color ConvertToColor(int c)
        {
            byte[] bytes = BitConverter.GetBytes(c);
            return Color.FromArgb(0xff, bytes[2], bytes[1], bytes[0]);
        }

        public static Color ConvertToColor(string colorName)
        {
            Type type = typeof(Colors);
            if (type.GetProperty(colorName) != null)
            {
                object obj2 = type.InvokeMember(colorName, BindingFlags.GetProperty, null, null, null);
                if (obj2 != null)
                {
                    return (Color) obj2;
                }
            }
            return new Color();
        }

        public static void Debug(string str)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff"), str));
        }

        public static double GetAngleBetweenVercors(double x1, double y1, double x2, double y2)
        {
            double d = ((x1 * x2) + (y1 * y2)) / Math.Sqrt(((x1 * x1) + (y1 * y1)) * ((x2 * x2) + (y2 * y2)));
            return ((Math.Acos(d) * 180.0) / 3.1415926535897931);
        }

        public static double GetRotationAngle(Point p1, Point p2)
        {
            double x = Math.Abs((double) (p1.get_X() - p2.get_X()));
            double y = Math.Abs((double) (p1.get_Y() - p2.get_Y()));
            double num3 = 0.0;
            if (x != 0.0)
            {
                if (y == 0.0)
                {
                    if (p1.get_X() > p2.get_X())
                    {
                        num3 = 3.1415926535897931;
                    }
                    else if (p1.get_X() < p2.get_X())
                    {
                        num3 = 0.0;
                    }
                }
                else if (((p2.get_X() > p1.get_X()) && (p2.get_Y() < p1.get_Y())) || ((p2.get_X() < p1.get_X()) && (p2.get_Y() > p1.get_Y())))
                {
                    num3 = Math.Atan2(y, x);
                }
                else
                {
                    num3 = -Math.Atan2(y, x);
                }
            }
            else if (p1.get_Y() > p2.get_Y())
            {
                num3 = 1.5707963267948966;
            }
            else if (p1.get_Y() < p2.get_Y())
            {
                num3 = -1.5707963267948966;
            }
            return ((num3 * 180.0) / 3.1415926535897931);
        }
    }
}

