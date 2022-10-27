using System.Globalization;

namespace Store.Common
{
    public static class PersianConvertor
    {
        public static string ToLongShamsi(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime)}/{pc.GetDayOfMonth(dateTime)} {dateTime.Hour}:{dateTime.Minute}";
        }
        public static string ToShortShamsi(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime)}/{pc.GetDayOfMonth(dateTime)}";
        }
    }
}
