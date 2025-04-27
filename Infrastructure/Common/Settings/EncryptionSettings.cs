using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Settings
{
    
        /// <summary>
        /// تنظیمات مربوط به رمزنگاری داده‌ها.
        /// </summary>
        public class EncryptionSettings
        {
            /// <summary>
            /// کلید رمزنگاری برای استفاده در الگوریتم‌های رمزنگاری.
            /// </summary>
            public string EncryptionKey { get; set; }

            /// <summary>
            /// نوع الگوریتم رمزنگاری که باید استفاده شود (مانند AES، RSA و غیره).
            /// </summary>
            public string EncryptionAlgorithm { get; set; }

            /// <summary>
            /// زمان انقضای کلید رمزنگاری (برای مثال، به مدت چند روز یا ماه قابل استفاده است).
            /// </summary>
            public TimeSpan KeyExpiration { get; set; }

            /// <summary>
            /// مسیر یا آدرس فایل ذخیره‌سازی کلیدهای رمزنگاری (در صورت استفاده از ذخیره‌سازی محلی).
            /// </summary>
            public string KeyStorageLocation { get; set; }

            /// <summary>
            /// آیا از رمزنگاری دوطرفه استفاده می‌شود یا خیر (برای مثال در RSA).
            /// </summary>
            public bool UseSymmetricEncryption { get; set; }

            /// <summary>
            /// تنظیمات مربوط به نحوه رمزنگاری داده‌ها در سیستم.
            /// </summary>
            public EncryptionSettings()
            {
                // مقادیر پیش‌فرض برای تنظیمات رمزنگاری
                EncryptionAlgorithm = "AES";
                KeyExpiration = TimeSpan.FromDays(30);
                UseSymmetricEncryption = true;
            }
        }
    }

