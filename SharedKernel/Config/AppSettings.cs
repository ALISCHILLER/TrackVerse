using System.ComponentModel.DataAnnotations;

namespace SharedKernel.Config
{
    /// <summary>
    /// کلاس اصلی برای مدیریت تنظیمات برنامه.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// تنظیمات اتصال به دیتابیس.
        /// </summary>
        [Required(ErrorMessage = "Database connection settings are required.")]
        public ConnectionStrings ConnectionStrings { get; set; }

        /// <summary>
        /// تنظیمات Redis.
        /// </summary>
        public RedisSettings Redis { get; set; }

        /// <summary>
        /// تنظیمات Kafka.
        /// </summary>
        public KafkaSettings Kafka { get; set; }

        /// <summary>
        /// تنظیمات لاگ‌زنی.
        /// </summary>
        public LoggingSettings Logging { get; set; }

        /// <summary>
        /// تنظیمات عمومی برنامه.
        /// </summary>
        public GeneralSettings General { get; set; }
    }

    /// <summary>
    /// تنظیمات اتصال به دیتابیس.
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// رشته اتصال به دیتابیس اصلی.
        /// </summary>
        [Required(ErrorMessage = "DefaultConnection string is required.")]
        public string DefaultConnection { get; set; }
    }

    /// <summary>
    /// تنظیمات Redis.
    /// </summary>
    public class RedisSettings
    {
        /// <summary>
        /// آدرس اتصال به Redis.
        /// </summary>
        [Required(ErrorMessage = "Redis connection string is required.")]
        public string ConnectionString { get; set; }
    }

    /// <summary>
    /// تنظیمات Kafka.
    /// </summary>
    public class KafkaSettings
    {
        /// <summary>
        /// آدرس سرورهای Kafka.
        /// </summary>
        [Required(ErrorMessage = "Kafka bootstrap servers are required.")]
        public string BootstrapServers { get; set; }
    }

    /// <summary>
    /// تنظیمات لاگ‌زنی.
    /// </summary>
    public class LoggingSettings
    {
        /// <summary>
        /// سطح لاگ‌زنی پیش‌فرض.
        /// </summary>
        public string LogLevel { get; set; } = "Information";

        /// <summary>
        /// سطح لاگ‌زنی برای ASP.NET Core.
        /// </summary>
        public string MicrosoftAspNetCoreLogLevel { get; set; } = "Warning";
    }

    /// <summary>
    /// تنظیمات عمومی برنامه.
    /// </summary>
    public class GeneralSettings
    {
        /// <summary>
        /// حداکثر تعداد تلاش برای ارسال پیام‌ها.
        /// </summary>
        public int MaxRetryAttempts { get; set; } = 3;

        /// <summary>
        /// زمان تاخیر بین تلاش‌ها (به میلی‌ثانیه).
        /// </summary>
        public int RetryDelayMilliseconds { get; set; } = 1000;
    }
}