using System;
using System.Threading.Tasks;
using Infrastructure.Common.Caching.Interfaces;
using StackExchange.Redis;  // فرض بر این است که از Redis به عنوان سیستم کش استفاده می‌کنیم.

namespace Infrastructure.Common.Caching
{
    /// <summary>
    /// پیاده‌سازی سرویس کش برای مدیریت عملیات ذخیره‌سازی و بازیابی داده‌ها از کش.
    /// این سرویس از Redis برای کش استفاده می‌کند.
    /// </summary>
    public class CacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            _database = _redisConnection.GetDatabase();
        }

        /// <summary>
        /// داده‌ای را در کش ذخیره می‌کند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <param name="value">مقدار داده‌ای که باید ذخیره شود</param>
        /// <param name="absoluteExpiration">زمان انقضای مطلق کش</param>
        /// <param name="slidingExpiration">انقضا بر اساس مدت زمان اخیر استفاده</param>
        /// <returns>یک تسک که تکمیل فرآیند ذخیره‌سازی کش را نشان می‌دهد</returns>
        public async Task SetAsync(string key, object value, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "مقدار نمی‌تواند null باشد");

            var serializedValue = System.Text.Json.JsonSerializer.Serialize(value);

            // تنظیم زمان انقضا کش
            TimeSpan? expiry = absoluteExpiration ?? slidingExpiration;

            await _database.StringSetAsync(key, serializedValue, expiry);
        }

        /// <summary>
        /// داده‌ای را از کش بر اساس کلید آن می‌خواند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <returns>داده‌ای که از کش خوانده شده است یا null اگر داده موجود نباشد</returns>
        public async Task<object> GetAsync(string key)
        {
            var serializedValue = await _database.StringGetAsync(key);
            if (serializedValue.IsNullOrEmpty)
                return null;

            return System.Text.Json.JsonSerializer.Deserialize<object>(serializedValue);
        }

        /// <summary>
        /// داده‌ای را از کش حذف می‌کند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده‌ای که باید حذف شود</param>
        /// <returns>یک تسک که تکمیل فرآیند حذف کش را نشان می‌دهد</returns>
        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        /// <summary>
        /// بررسی می‌کند که آیا داده‌ای با کلید خاص در کش موجود است یا خیر.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <returns>حقیقت (True) اگر داده وجود داشته باشد و نادرست (False) در غیر این صورت</returns>
        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        /// <summary>
        /// کش را به‌طور کامل پاک می‌کند.
        /// </summary>
        /// <returns>یک تسک که تکمیل فرآیند پاک‌سازی کش را نشان می‌دهد</returns>
        public async Task ClearAsync()
        {
            var server = _redisConnection.GetServer(_redisConnection.GetEndPoints()[0]);
            await foreach (var key in server.KeysAsync())
            {
                await _database.KeyDeleteAsync(key);
            }
        }

        /// <summary>
        /// کش را به‌صورت اجباری برای یک کلید خاص منقضی می‌کند.
        /// </summary>
        /// <param name="key">کلید کش که باید منقضی شود</param>
        /// <returns>یک تسک که تکمیل فرآیند منقضی کردن کش را نشان می‌دهد</returns>
        public async Task ExpireAsync(string key, TimeSpan? ttl = null)
        {
            if (ttl.HasValue)
            {
                // انقضای با تأخیر
                await _database.KeyExpireAsync(key, ttl.Value);
            }
            else
            {
                // انقضای فوری
                await _database.KeyExpireAsync(key, TimeSpan.Zero);
            }
        }

        /// <summary>
        /// داده‌ای را در کش ذخیره می‌کند و مقدار را بر اساس TTL خاص تنظیم می‌کند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <param name="value">مقدار داده‌ای که باید ذخیره شود</param>
        /// <param name="ttl">مدت زمان انقضای کش</param>
        /// <returns>یک تسک که تکمیل فرآیند ذخیره‌سازی کش را نشان می‌دهد</returns>
        public async Task SetWithTTLAsync(string key, object value, TimeSpan ttl)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "مقدار نمی‌تواند null باشد");

            var serializedValue = System.Text.Json.JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, serializedValue, ttl);
        }

        /// <summary>
        /// داده‌ای را از کش می‌خواند و اگر داده موجود نبود، آن را با تابعی که مشخص می‌شود به‌طور دینامیک بارگذاری می‌کند.
        /// </summary>
        /// <param name="key">کلید کش برای شناسایی داده</param>
        /// <param name="fetchDataFunction">تابعی که برای بارگذاری داده در صورت عدم وجود در کش فراخوانی می‌شود</param>
        /// <param name="absoluteExpiration">زمان انقضای مطلق کش</param>
        /// <param name="slidingExpiration">انقضا بر اساس مدت زمان اخیر استفاده</param>
        /// <returns>داده‌ای که از کش یا تابع بارگذاری خوانده شده است</returns>
        public async Task<object> GetOrAddAsync(string key, Func<Task<object>> fetchDataFunction, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            var cachedValue = await GetAsync(key);

            if (cachedValue != null)
                return cachedValue;

            var fetchedData = await fetchDataFunction();
            await SetAsync(key, fetchedData, absoluteExpiration, slidingExpiration);

            return fetchedData;
        }

        public Task ExpireAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
