using System;
using System.Globalization;

namespace Infrastructure.Common.Helpers
{
    /// <summary>
    /// کلاس برای انجام عملیات‌های کمکی روی تاریخ و زمان.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// تاریخ و زمان فعلی سیستم را برمی‌گرداند.
        /// </summary>
        /// <returns>تاریخ و زمان فعلی.</returns>
        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// تاریخ و زمان فعلی سیستم را به فرمت مشخص شده تبدیل می‌کند.
        /// </summary>
        /// <param name="format">فرمت مورد نظر.</param>
        /// <returns>تاریخ و زمان فرمت‌شده.</returns>
        public static string GetCurrentDateTime(string format)
        {
            return DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// تاریخ و زمان فعلی سیستم را به فرمت تاریخ (yyyy-MM-dd) تبدیل می‌کند.
        /// </summary>
        /// <returns>تاریخ فرمت‌شده.</returns>
        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// تاریخ و زمان فعلی سیستم را به فرمت زمان (HH:mm:ss) تبدیل می‌کند.
        /// </summary>
        /// <returns>زمان فرمت‌شده.</returns>
        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// یک رشته تاریخ را به شیء DateTime تبدیل می‌کند.
        /// </summary>
        /// <param name="dateString">رشته تاریخ به فرمت yyyy-MM-dd.</param>
        /// <returns>شیء DateTime معادل.</returns>
        public static DateTime? ConvertStringToDate(string dateString)
        {
            DateTime date;
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }
            return null;
        }

        /// <summary>
        /// تاریخ را به فرمت yyyy-MM-dd تبدیل می‌کند.
        /// </summary>
        /// <param name="date">تاریخ برای فرمت‌بندی.</param>
        /// <returns>تاریخ فرمت‌شده.</returns>
        public static string ConvertDateToString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// تاریخ و زمان را به فرمت مورد نظر تبدیل می‌کند.
        /// </summary>
        /// <param name="date">تاریخ و زمان برای فرمت‌بندی.</param>
        /// <param name="format">فرمت مورد نظر.</param>
        /// <returns>تاریخ و زمان فرمت‌شده.</returns>
        public static string ConvertDateTimeToString(DateTime date, string format)
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// مدت زمان گذشته از یک تاریخ مشخص را محاسبه می‌کند.
        /// </summary>
        /// <param name="startDate">تاریخ شروع.</param>
        /// <returns>مدت زمان گذشته به صورت رشته (مثلاً 3 روز پیش).</returns>
        public static string GetTimeAgo(DateTime startDate)
        {
            var timeSpan = DateTime.Now - startDate;

            if (timeSpan.Days > 365)
            {
                int years = timeSpan.Days / 365;
                return $"{years} year{(years > 1 ? "s" : "")} ago";
            }
            if (timeSpan.Days > 30)
            {
                int months = timeSpan.Days / 30;
                return $"{months} month{(months > 1 ? "s" : "")} ago";
            }
            if (timeSpan.Days > 0)
            {
                return $"{timeSpan.Days} day{(timeSpan.Days > 1 ? "s" : "")} ago";
            }
            if (timeSpan.Hours > 0)
            {
                return $"{timeSpan.Hours} hour{(timeSpan.Hours > 1 ? "s" : "")} ago";
            }
            if (timeSpan.Minutes > 0)
            {
                return $"{timeSpan.Minutes} minute{(timeSpan.Minutes > 1 ? "s" : "")} ago";
            }
            return "Just now";
        }

        /// <summary>
        /// تاریخ و زمان را به زمان UTC تبدیل می‌کند.
        /// </summary>
        /// <param name="date">تاریخ و زمان محلی.</param>
        /// <returns>تاریخ و زمان به زمان UTC.</returns>
        public static DateTime ConvertToUtc(DateTime date)
        {
            return date.ToUniversalTime();
        }

        /// <summary>
        /// تاریخ و زمان UTC را به زمان محلی تبدیل می‌کند.
        /// </summary>
        /// <param name="utcDate">تاریخ و زمان UTC.</param>
        /// <returns>تاریخ و زمان محلی.</returns>
        public static DateTime ConvertFromUtc(DateTime utcDate)
        {
            return utcDate.ToLocalTime();
        }

        /// <summary>
        /// تفاوت زمانی بین دو تاریخ را به صورت روز، ساعت، دقیقه و ثانیه برمی‌گرداند.
        /// </summary>
        /// <param name="startDate">تاریخ شروع.</param>
        /// <param name="endDate">تاریخ پایان.</param>
        /// <returns>تفاوت زمانی به صورت رشته.</returns>
        public static string GetTimeDifference(DateTime startDate, DateTime endDate)
        {
            var timeSpan = endDate - startDate;

            int days = timeSpan.Days;
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            return $"{days} day{(days > 1 ? "s" : "")}, {hours} hour{(hours > 1 ? "s" : "")}, {minutes} minute{(minutes > 1 ? "s" : "")}, {seconds} second{(seconds > 1 ? "s" : "")}";
        }

        /// <summary>
        /// بررسی می‌کند که آیا تاریخ وارد شده قبل از تاریخ فعلی است یا خیر.
        /// </summary>
        /// <param name="date">تاریخ برای بررسی.</param>
        /// <returns>برگشت مقدار true اگر تاریخ وارد شده قبل از تاریخ فعلی باشد.</returns>
        public static bool IsBeforeCurrentDate(DateTime date)
        {
            return date < DateTime.Now;
        }

        /// <summary>
        /// بررسی می‌کند که آیا تاریخ وارد شده بعد از تاریخ فعلی است یا خیر.
        /// </summary>
        /// <param name="date">تاریخ برای بررسی.</param>
        /// <returns>برگشت مقدار true اگر تاریخ وارد شده بعد از تاریخ فعلی باشد.</returns>
        public static bool IsAfterCurrentDate(DateTime date)
        {
            return date > DateTime.Now;
        }
    }
}
