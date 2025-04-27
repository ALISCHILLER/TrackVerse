using System;

namespace Infrastructure.Common.Settings
{
    /// <summary>
    /// تنظیمات مربوط به ارسال ایمیل.
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// آدرس ایمیل ارسال‌کننده.
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// رمز عبور یا توکن ایمیل ارسال‌کننده.
        /// </summary>
        public string SenderPassword { get; set; }

        /// <summary>
        /// سرور SMTP برای ارسال ایمیل.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// پورت مورد استفاده برای اتصال به سرور SMTP.
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// استفاده از ارتباط امن (SSL/TLS) برای اتصال به سرور SMTP.
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// آدرس ایمیل برای پاسخ‌دهی (در صورت نیاز).
        /// </summary>
        public string ReplyToEmail { get; set; }

        /// <summary>
        /// نام ارسال‌کننده ایمیل.
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// تنظیمات ایمیل
        /// </summary>
        public EmailSettings()
        {
            // مقادیر پیش‌فرض برای تنظیمات ایمیل
            SmtpServer = "smtp.example.com"; // سرور SMTP پیش‌فرض
            SmtpPort = 587; // پورت پیش‌فرض SMTP برای اتصال امن (TLS)
            UseSsl = true; // استفاده از SSL/TLS به صورت پیش‌فرض
            SenderName = "YourAppName"; // نام ارسال‌کننده پیش‌فرض
        }
    }
}
