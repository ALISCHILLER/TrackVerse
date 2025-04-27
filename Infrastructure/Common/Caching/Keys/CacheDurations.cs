using System;

namespace Infrastructure.Common.Caching.Keys
{
    /// <summary>
    /// این کلاس زمان انقضا (TTL) کش‌ها را برای کلیدهای مختلف سیستم مدیریت می‌کند.
    /// </summary>
    public static class CacheDurations
    {
        /// <summary>
        /// مدت زمان انقضای کش برای داده‌های کاربران (به ثانیه)
        /// </summary>
        public static readonly TimeSpan UserCacheExpiration = TimeSpan.FromMinutes(30); // 30 دقیقه

        /// <summary>
        /// مدت زمان انقضای کش برای داده‌های محصولات (به ثانیه)
        /// </summary>
        public static readonly TimeSpan ProductCacheExpiration = TimeSpan.FromHours(1); // 1 ساعت

        /// <summary>
        /// مدت زمان انقضای کش برای لیست تمام کاربران (به ثانیه)
        /// </summary>
        public static readonly TimeSpan UserListCacheExpiration = TimeSpan.FromHours(6); // 6 ساعت

        /// <summary>
        /// مدت زمان انقضای کش برای لیست محصولات (به ثانیه)
        /// </summary>
        public static readonly TimeSpan ProductListCacheExpiration = TimeSpan.FromHours(6); // 6 ساعت

        /// <summary>
        /// مدت زمان انقضای کش برای تنظیمات سیستم (به ثانیه)
        /// </summary>
        public static readonly TimeSpan SystemSettingsCacheExpiration = TimeSpan.FromDays(1); // 1 روز

        /// <summary>
        /// مدت زمان انقضای کش برای مقادیر دیکشنری (به ثانیه)
        /// </summary>
        public static readonly TimeSpan LookupValuesCacheExpiration = TimeSpan.FromDays(7); // 7 روز

        /// <summary>
        /// مدت زمان انقضای کش برای زبان‌بندی و لوکالایزیشن (به ثانیه)
        /// </summary>
        public static readonly TimeSpan LocalizationCacheExpiration = TimeSpan.FromDays(7); // 7 روز

        /// <summary>
        /// این متد زمان انقضای کش مناسب را برای کلید خاص دریافت می‌کند.
        /// </summary>
        /// <param name="cacheKey">کلید کش</param>
        /// <returns>زمان انقضا برای کلید</returns>
        public static TimeSpan GetCacheExpiration(string cacheKey)
        {
            return cacheKey switch
            {
                string key when key.Contains("Cache:User") => UserCacheExpiration,
                string key when key.Contains("Cache:Product") => ProductCacheExpiration,
                string key when key.Contains("Cache:List") => ProductListCacheExpiration,
                string key when key.Contains("Cache:Settings") => SystemSettingsCacheExpiration,
                string key when key.Contains("Cache:Common:LookupValues") => LookupValuesCacheExpiration,
                string key when key.Contains("Cache:Common:Localization") => LocalizationCacheExpiration,
                _ => TimeSpan.FromMinutes(10) // مقدار پیش‌فرض در صورت عدم تطابق
            };
        }
    }
}
