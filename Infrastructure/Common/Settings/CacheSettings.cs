namespace Infrastructure.Common.Settings
{
    public class CacheSettings
    {
        // نوع کش (مثلاً Redis یا MemoryCache)
        public string CacheType { get; set; }

        // مدت زمان انقضا کش در دقیقه
        public int CacheExpirationMinutes { get; set; }

        // تنظیمات مربوط به اتصال به Redis (در صورت استفاده از Redis)
        public RedisSettings Redis { get; set; }
    }

    public class RedisSettings
    {
        // آدرس سرور Redis
        public string Host { get; set; }

        // پورت Redis
        public int Port { get; set; }

        // رمز عبور Redis (در صورت نیاز)
        public string Password { get; set; }

        // مدت زمان انقضا کلیدهای کش در Redis
        public int DefaultExpirationMinutes { get; set; }

        // اگر از SSL استفاده می‌کنید
        public bool UseSsl { get; set; }

        // تنظیمات مربوط به اتصال مجدد
        public RedisConnectionSettings Connection { get; set; }
    }

    public class RedisConnectionSettings
    {
        // مدت زمان اتصال مجدد در صورت قطع ارتباط
        public int RetryCount { get; set; }

        // مدت زمان بین تلاش‌ها برای اتصال مجدد
        public int RetryIntervalSeconds { get; set; }

        // مدت زمان انقضا اتصال
        public int ConnectTimeoutMilliseconds { get; set; }

        // مدت زمان برای عملیات‌های Redis
        public int OperationTimeoutMilliseconds { get; set; }
    }
}
