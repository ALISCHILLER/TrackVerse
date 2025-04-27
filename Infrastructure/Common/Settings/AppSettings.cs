namespace Infrastructure.Common.Settings
{
    public class AppSettings
    {
        // تنظیمات JWT برای احراز هویت
        public JwtSetting Jwt { get; set; }

        // تنظیمات کش (برای Redis یا سایر کش‌ها)
        public CacheSettings Cache { get; set; }

        // تنظیمات رمزنگاری
        public EncryptionSettings Encryption { get; set; }

        // تنظیمات ذخیره‌سازی فایل‌ها
        public FileStorageSettings FileStorage { get; set; }

        // تنظیمات ایمیل
        public EmailSettings Email { get; set; }

        // سایر تنظیمات عمومی مورد نیاز
        public string Environment { get; set; }
    }

    public class JwtSetting
    {
        // کلید رمزنگاری JWT
        public string SecretKey { get; set; }

        // مدت زمان انقضا Access Token
        public int AccessTokenExpirationMinutes { get; set; }

        // مدت زمان انقضا Refresh Token
        public int RefreshTokenExpirationDays { get; set; }

        // الگوریتم امضای توکن
        public string Algorithm { get; set; }
    }

    public class CacheSetting
    {
        // نوع کش (مثلاً Redis یا MemoryCache)
        public string CacheType { get; set; }

        // مدت زمان انقضا کش در دقیقه
        public int CacheExpirationMinutes { get; set; }

        // تنظیمات مربوط به اتصال به Redis (در صورت استفاده از Redis)
        public RedisSettings Redis { get; set; }
    }

    public class RedisSetting
    {
        // آدرس سرور Redis
        public string Host { get; set; }

        // پورت Redis
        public int Port { get; set; }

        // رمز عبور Redis (در صورت نیاز)
        public string Password { get; set; }

        // مدت زمان انقضا کلیدهای کش در Redis
        public int DefaultExpirationMinutes { get; set; }
    }

    public class EncryptionSetting
    {
        // کلید رمزنگاری برای داده‌ها
        public string EncryptionKey { get; set; }

        // الگوریتم رمزنگاری (مثلاً AES)
        public string Algorithm { get; set; }
    }

    public class FileStorageSetting
    {
        // نوع ذخیره‌سازی فایل (مثلاً Local یا Cloud)
        public string StorageType { get; set; }

        // مسیر ذخیره‌سازی فایل‌ها برای ذخیره‌سازی محلی
        public string LocalPath { get; set; }

        // تنظیمات مربوط به فضای ابری (اگر از Cloud استفاده می‌شود)
        public CloudStorageSettings Cloud { get; set; }
    }

    public class CloudStorageSettings
    {
        // نوع فضای ابری (مثلاً AWS, Azure)
        public string Provider { get; set; }

        // شناسه دسترسی (Access Key)
        public string AccessKey { get; set; }

        // کلید مخفی دسترسی (Secret Key)
        public string SecretKey { get; set; }

        // نام منطقه (Region)
        public string Region { get; set; }

        // نام باکت (Bucket Name)
        public string BucketName { get; set; }
    }

    public class EmailSetting
    {
        // آدرس ایمیل ارسال‌کننده
        public string FromAddress { get; set; }

        // نام ایمیل ارسال‌کننده
        public string FromName { get; set; }

        // سرور SMTP
        public string SmtpServer { get; set; }

        // پورت SMTP
        public int SmtpPort { get; set; }

        // نام کاربری برای ورود به SMTP
        public string SmtpUser { get; set; }

        // رمز عبور برای SMTP
        public string SmtpPassword { get; set; }
    }
}
