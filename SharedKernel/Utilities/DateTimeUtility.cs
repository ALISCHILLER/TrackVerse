using System;
using System.Globalization;

namespace SharedKernel.Utilities
{
    /// <summary>
    /// ابزارهای کمکی برای مدیریت تاریخ و زمان.
    /// </summary>
    public static class DateTimeUtility
    {
        private static readonly PersianCalendar _persianCalendar = new PersianCalendar();

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی.
        /// </summary>
        public static string ToPersianDate(DateTime dateTime, string separator = "/")
        {
            return $"{_persianCalendar.GetYear(dateTime)}{separator}{_persianCalendar.GetMonth(dateTime):00}{separator}{_persianCalendar.GetDayOfMonth(dateTime):00}";
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی با زمان.
        /// </summary>
        public static string ToPersianDateTime(DateTime dateTime, string separator = "/")
        {
            return $"{ToPersianDate(dateTime, separator)} {dateTime:HH:mm:ss}";
        }

        /// <summary>
        /// تبدیل تاریخ شمسی به تاریخ میلادی.
        /// </summary>
        public static DateTime FromPersianDate(string persianDate, string separator = "/")
        {
            var parts = persianDate.Split(separator);
            if (parts.Length != 3)
                throw new ArgumentException("فرمت تاریخ شمسی باید 'yyyy/MM/dd' باشد.");

            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            return _persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        /// <summary>
        /// محاسبه سن بر اساس تاریخ تولد.
        /// </summary>
        public static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// بررسی اعتبار رشته تاریخ با فرمت خاص.
        /// </summary>
        public static bool IsValidDate(string dateString, string format = "yyyy-MM-dd")
        {
            return DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        /// <summary>
        /// بدست آوردن اولین روز ماه.
        /// </summary>
        public static DateTime GetFirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// بدست آوردن آخرین روز ماه.
        /// </summary>
        public static DateTime GetLastDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// تفاوت دو تاریخ به صورت فاصله زمانی.
        /// </summary>
        public static TimeSpan GetTimeDifference(DateTime from, DateTime to)
        {
            return to - from;
        }

        /// <summary>
        /// بررسی اینکه آیا تاریخ ورودی امروز است یا خیر.
        /// </summary>
        public static bool IsToday(DateTime date)
        {
            return date.Date == DateTime.Today;
        }

        /// <summary>
        /// بررسی اینکه آیا تاریخ ورودی در آینده است یا خیر.
        /// </summary>
        public static bool IsFuture(DateTime date)
        {
            return date > DateTime.Now;
        }

        /// <summary>
        /// بررسی اینکه آیا تاریخ ورودی در گذشته است یا خیر.
        /// </summary>
        public static bool IsPast(DateTime date)
        {
            return date < DateTime.Now;
        }

        /// <summary>
        /// فرمت کردن تاریخ میلادی به رشته سفارشی.
        /// </summary>
        public static string FormatDate(DateTime date, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}