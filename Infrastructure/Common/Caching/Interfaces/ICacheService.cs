using System;
using System.Threading.Tasks;

namespace Infrastructure.Common.Caching.Interfaces
{
    /// <summary>
    /// این اینترفیس برای مدیریت کش داده‌ها در سیستم طراحی شده است.
    /// از این سرویس می‌توان برای ذخیره‌سازی، خواندن، حذف داده‌ها و مدیریت زمان انقضای کش استفاده کرد.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// داده‌ای را در کش ذخیره می‌کند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <param name="value">مقدار داده‌ای که باید ذخیره شود</param>
        /// <param name="absoluteExpiration">زمان انقضای مطلق کش</param>
        /// <param name="slidingExpiration">انقضا بر اساس مدت زمان اخیر استفاده</param>
        /// <returns>یک تسک که تکمیل فرآیند ذخیره‌سازی کش را نشان می‌دهد</returns>
        Task SetAsync(string key, object value, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null);

        /// <summary>
        /// داده‌ای را از کش بر اساس کلید آن می‌خواند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <returns>داده‌ای که از کش خوانده شده است یا null اگر داده موجود نباشد</returns>
        Task<object> GetAsync(string key);

        /// <summary>
        /// داده‌ای را از کش حذف می‌کند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده‌ای که باید حذف شود</param>
        /// <returns>یک تسک که تکمیل فرآیند حذف کش را نشان می‌دهد</returns>
        Task RemoveAsync(string key);

        /// <summary>
        /// بررسی می‌کند که آیا داده‌ای با کلید خاص در کش موجود است یا خیر.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <returns>حقیقت (True) اگر داده وجود داشته باشد و نادرست (False) در غیر این صورت</returns>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// کش را به‌طور کامل پاک می‌کند.
        /// </summary>
        /// <returns>یک تسک که تکمیل فرآیند پاک‌سازی کش را نشان می‌دهد</returns>
        Task ClearAsync();

        /// <summary>
        /// کش را به‌صورت اجباری برای یک کلید خاص منقضی می‌کند.
        /// </summary>
        /// <param name="key">کلید کش که باید منقضی شود</param>
        /// <returns>یک تسک که تکمیل فرآیند منقضی کردن کش را نشان می‌دهد</returns>
        Task ExpireAsync(string key);

        /// <summary>
        /// داده‌ای را در کش ذخیره می‌کند و مقدار را بر اساس TTL خاص تنظیم می‌کند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <param name="value">مقدار داده‌ای که باید ذخیره شود</param>
        /// <param name="ttl">مدت زمان انقضای کش</param>
        /// <returns>یک تسک که تکمیل فرآیند ذخیره‌سازی کش را نشان می‌دهد</returns>
        Task SetWithTTLAsync(string key, object value, TimeSpan ttl);

        /// <summary>
        /// داده‌ای را از کش می‌خواند و اگر داده موجود نبود، آن را با تابعی که مشخص می‌شود به‌طور دینامیک بارگذاری می‌کند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <param name="fetchDataFunction">تابعی که برای بارگذاری داده در صورت عدم وجود در کش فراخوانی می‌شود</param>
        /// <param name="absoluteExpiration">زمان انقضای مطلق کش</param>
        /// <param name="slidingExpiration">انقضا بر اساس مدت زمان اخیر استفاده</param>
        /// <returns>داده‌ای که از کش یا تابع بارگذاری خوانده شده است</returns>
        Task<object> GetOrAddAsync(string key, Func<Task<object>> fetchDataFunction, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null);
    }
}
