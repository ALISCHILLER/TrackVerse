using System;

namespace Infrastructure.Common.Settings
{
    /// <summary>
    /// تنظیمات مربوط به ذخیره‌سازی فایل‌ها.
    /// </summary>
    public class FileStorageSettings
    {
        /// <summary>
        /// مسیر ذخیره‌سازی فایل‌های محلی (در صورت استفاده از ذخیره‌سازی محلی).
        /// </summary>
        public string LocalStoragePath { get; set; }

        /// <summary>
        /// آدرس ذخیره‌سازی ابری (در صورت استفاده از ذخیره‌سازی ابری، مانند Azure یا AWS).
        /// </summary>
        public string CloudStorageUrl { get; set; }

        /// <summary>
        /// نوع فضای ذخیره‌سازی برای فایل‌ها (محلی یا ابری).
        /// </summary>
        public StorageType StorageType { get; set; }

        /// <summary>
        /// مدت زمان انقضای فایل‌ها در فضای ذخیره‌سازی.
        /// </summary>
        public TimeSpan FileExpiration { get; set; }

        /// <summary>
        /// تعداد فایل‌هایی که باید همزمان بارگذاری شوند (برای بهینه‌سازی عملکرد).
        /// </summary>
        public int MaxConcurrentUploads { get; set; }

        /// <summary>
        /// حداکثر اندازه مجاز برای فایل‌های آپلود شده (در مگابایت).
        /// </summary>
        public int MaxFileSizeInMB { get; set; }

        /// <summary>
        /// فرمت‌های مجاز برای فایل‌های آپلود شده (مانند .jpg, .png, .pdf).
        /// </summary>
        public string[] AllowedFileExtensions { get; set; }

        /// <summary>
        /// تنظیمات مربوط به ذخیره‌سازی فایل‌ها.
        /// </summary>
        public FileStorageSettings()
        {
            // مقادیر پیش‌فرض برای تنظیمات ذخیره‌سازی
            StorageType = StorageType.Local;
            FileExpiration = TimeSpan.FromDays(30); // فایل‌ها بعد از ۳۰ روز منقضی می‌شوند
            MaxConcurrentUploads = 5; // حداکثر ۵ فایل همزمان بارگذاری می‌شود
            MaxFileSizeInMB = 100; // حداکثر اندازه فایل ۱۰۰ مگابایت است
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" }; // فرمت‌های مجاز
        }
    }

    /// <summary>
    /// نوع فضای ذخیره‌سازی.
    /// </summary>
    public enum StorageType
    {
        /// <summary>
        /// ذخیره‌سازی محلی (مثلاً در سرور یا دیسک محلی)
        /// </summary>
        Local,

        /// <summary>
        /// ذخیره‌سازی ابری (مثلاً در AWS, Azure یا Google Cloud)
        /// </summary>
        Cloud
    }
}
