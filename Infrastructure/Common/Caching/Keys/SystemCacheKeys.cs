using System;

namespace Infrastructure.Common.Caching.Keys
{
    /// <summary>
    /// این کلاس شامل کلیدهای کش برای داده‌های سیستم می‌باشد.
    /// این داده‌ها می‌توانند شامل تنظیمات عمومی سیستم، آمار سیستم، وضعیت‌ها و اطلاعات عمومی دیگر باشند.
    /// </summary>
    public static class SystemCacheKeys
    {
        // ------------------ System Settings ------------------

        /// <summary>
        /// کلید کش برای تنظیمات سیستم عمومی.
        /// </summary>
        public const string SystemSettings = "Cache:System:Settings";

        /// <summary>
        /// کلید کش برای تنظیمات امنیتی سیستم.
        /// </summary>
        public const string SecuritySettings = "Cache:System:SecuritySettings";

        /// <summary>
        /// کلید کش برای تنظیمات ایمیل سیستم.
        /// </summary>
        public const string EmailSettings = "Cache:System:EmailSettings";

        // ------------------ System Metrics ------------------

        /// <summary>
        /// کلید کش برای آمار کلی سیستم.
        /// </summary>
        public const string SystemMetrics = "Cache:System:Metrics";

        /// <summary>
        /// کلید کش برای تعداد کاربران فعال سیستم.
        /// </summary>
        public const string ActiveUsersCount = "Cache:System:ActiveUsersCount";

        /// <summary>
        /// کلید کش برای تعداد خطاهای رخ داده در سیستم.
        /// </summary>
        public const string SystemErrorCount = "Cache:System:SystemErrorCount";

        /// <summary>
        /// کلید کش برای وضعیت سلامت سیستم.
        /// </summary>
        public const string SystemHealthStatus = "Cache:System:HealthStatus";

        // ------------------ Cache for Logs ------------------

        /// <summary>
        /// کلید کش برای آخرین لاگ‌های خطا.
        /// </summary>
        public const string LatestErrorLogs = "Cache:System:LatestErrorLogs";

        /// <summary>
        /// کلید کش برای آخرین لاگ‌های عمومی.
        /// </summary>
        public const string LatestSystemLogs = "Cache:System:LatestSystemLogs";

        // ------------------ User Management ------------------

        /// <summary>
        /// کلید کش برای لیست تمام نقش‌های سیستم.
        /// </summary>
        public const string AllRoles = "Cache:System:AllRoles";

        /// <summary>
        /// کلید کش برای لیست تمام دسترسی‌های سیستم.
        /// </summary>
        public const string AllPermissions = "Cache:System:AllPermissions";

        // ------------------ Dynamic Cache Keys ------------------

        /// <summary>
        /// تولید کلید کش برای یک تنظیمات سیستم خاص.
        /// </summary>
        /// <param name="settingKey">کلید تنظیمات</param>
        public static string GetSystemSettingKey(string settingKey)
        {
            return string.Format("{0}:{1}", SystemSettings, settingKey);
        }

        /// <summary>
        /// تولید کلید کش برای آمار سیستم بر اساس یک فیلد خاص.
        /// </summary>
        /// <param name="metricType">نوع آمار</param>
        public static string GetSystemMetricKey(string metricType)
        {
            return string.Format("{0}:{1}", SystemMetrics, metricType);
        }

        /// <summary>
        /// تولید کلید کش برای لاگ‌های خطا برای یک روز خاص.
        /// </summary>
        /// <param name="date">تاریخ مورد نظر</param>
        public static string GetErrorLogsByDateKey(DateTime date)
        {
            return string.Format("{0}:{1}", LatestErrorLogs, date.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// تولید کلید کش برای آمار خطاهای سیستم در یک بازه زمانی خاص.
        /// </summary>
        /// <param name="startDate">تاریخ شروع</param>
        /// <param name="endDate">تاریخ پایان</param>
        public static string GetSystemErrorCountByDateKey(DateTime startDate, DateTime endDate)
        {
            return string.Format("{0}:{1}-{2}", SystemErrorCount, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// تولید کلید کش برای وضعیت سلامت سیستم بر اساس وضعیت خاص.
        /// </summary>
        /// <param name="status">وضعیت سلامت</param>
        public static string GetSystemHealthStatusKey(string status)
        {
            return string.Format("{0}:{1}", SystemHealthStatus, status.ToLower());
        }

        // ------------------ Common Area ------------------

        /// <summary>
        /// تولید کلید کش برای تعداد کاربران فعال.
        /// </summary>
        public static string GetActiveUsersCountKey()
        {
            return ActiveUsersCount;
        }

        /// <summary>
        /// تولید کلید کش برای آمار کلی سیستم.
        /// </summary>
        public static string GetSystemMetricsKey()
        {
            return SystemMetrics;
        }

        /// <summary>
        /// تولید کلید کش برای تنظیمات امنیتی سیستم.
        /// </summary>
        public static string GetSecuritySettingsKey()
        {
            return SecuritySettings;
        }

        /// <summary>
        /// تولید کلید کش برای تنظیمات ایمیل سیستم.
        /// </summary>
        public static string GetEmailSettingsKey()
        {
            return EmailSettings;
        }
    }
}
