namespace SharedKernel.Constants
{
    /// <summary>
    /// کلاس ثابت‌ها برای استفاده در سراسر پروژه.
    /// </summary>
    public static class AppConstants
    {
        /// <summary>
        /// نام پیش‌فرض رشته اتصال به دیتابیس.
        /// </summary>
        public const string DefaultConnectionStringName = "DefaultConnection";

        /// <summary>
        /// مسیر پیش‌فرض لاگ‌ها.
        /// </summary>
        public const string DefaultLogPath = "Logs/app-log.txt";

        /// <summary>
        /// حداکثر تعداد تلاش برای ارسال پیام‌ها.
        /// </summary>
        public const int MaxRetryAttempts = 3;

        /// <summary>
        /// زمان تاخیر بین تلاش‌ها (به میلی‌ثانیه).
        /// </summary>
        public const int RetryDelayMilliseconds = 1000;

        /// <summary>
        /// مدت زمان اعتبار توکن JWT (به دقیقه).
        /// </summary>
        public const int JwtTokenExpirationMinutes = 60;

        /// <summary>
        /// نام کلید خاص برای ذخیره‌سازی توکن‌ها در Redis.
        /// </summary>
        public const string RedisTokenKeyPrefix = "AuthToken:";

        /// <summary>
        /// نام کلید خاص برای ذخیره‌سازی Session در Redis.
        /// </summary>
        public const string RedisSessionKeyPrefix = "UserSession:";

        /// <summary>
        /// آدرس پیش‌فرض سرور Kafka.
        /// </summary>
        public const string KafkaBootstrapServers = "localhost:9092";

        /// <summary>
        /// نام Topic پیش‌فرض در Kafka.
        /// </summary>
        public const string KafkaDefaultTopic = "default-topic";

        /// <summary>
        /// حداکثر اندازه فایل‌های آپلودی (به بایت).
        /// </summary>
        public const long MaxUploadFileSizeBytes = 5 * 1024 * 1024; // 5MB

        /// <summary>
        /// لیست فرمت‌های فایل مجاز برای آپلود.
        /// </summary>
        public static readonly string[] AllowedFileExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };

        /// <summary>
        /// نام کلید خاص برای ذخیره‌سازی تنظیمات کاربر در Cache.
        /// </summary>
        public const string UserSettingsCacheKey = "UserSettings:";

        /// <summary>
        /// مدت زمان اعتبار داده‌ها در Cache (به دقیقه).
        /// </summary>
        public const int CacheExpirationMinutes = 30;

        /// <summary>
        /// نام سرویس‌های داخلی (Microservices).
        /// </summary>
        public static class ServiceNames
        {
            public const string AuthService = "AuthService";
            public const string DeviceService = "DeviceService";
            public const string NotificationService = "NotificationService";
            public const string BillingService = "BillingService";
        }

        /// <summary>
        /// تنظیمات مربوط به API Gateway.
        /// </summary>
        public static class ApiGateway
        {
            public const string BaseUrl = "https://api.example.com";
            public const string HealthCheckEndpoint = "/health";
            public const string StatusEndpoint = "/status";
        }
    }
}