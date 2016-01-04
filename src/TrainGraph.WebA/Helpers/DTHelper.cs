using System;
using System.Globalization;

namespace TISServiceHelper
{
    public class DTHelper
    {
        public const string CNT_FORMAT = "dd.MM.yyyy HH:mm:ss.ff";
        public const string CNT_MINVALUE = "01.01.0001 00:00:00.00";

        public static DateTime GetDate(string str)
        {
            IFormatProvider provider = new CultureInfo("en-US");
            try
            {
                return DateTime.ParseExact(str, "dd.MM.yyyy HH:mm:ss.ff", provider);
            }
            catch
            {
            }
            return DateTime.MinValue;
        }

        public static string GetStr(DateTime dt)
        {
            return dt.ToString("dd.MM.yyyy HH:mm:ss.ff");
        }
    }
}