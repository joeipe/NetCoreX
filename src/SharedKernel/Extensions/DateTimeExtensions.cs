using System.Globalization;

namespace SharedKernel.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly string dateFormat = "dd/MM/yyyy";

        public static DateTime ParseDate(this string date)
        {
            DateTime res = DateTime.ParseExact(date, dateFormat, CultureInfo.CurrentUICulture.DateTimeFormat);
            return res;
        }

        public static string ParseDate(this DateTime date)
        {
            string res = date.ToString(dateFormat);
            return res;
        }

        public static bool IsValidDate(this string date)
        {
            var res = DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsed);
            return res;
        }

        public static DateTime SetKind(this DateTime DT, DateTimeKind DTKind)
        {
            return DateTime.SpecifyKind(DT, DTKind);
        }
    }
}