using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Settings
{
    public class JwtSettings
    {
        // کلید مخفی برای امضا و تأیید JWT
        public string SecretKey { get; set; }

        // الگوریتمی که برای امضا و تأیید JWT استفاده می‌شود
        public string Algorithm { get; set; }

        // مدت زمان انقضای توکن دسترسی (Access Token) به دقیقه
        public int AccessTokenExpirationMinutes { get; set; }

        // مدت زمان انقضای توکن تجدید (Refresh Token) به دقیقه
        public int RefreshTokenExpirationMinutes { get; set; }

        // URL که بعد از اعتبارسنجی موفقیت‌آمیز توکن، درخواست‌ها به آن هدایت می‌شوند
        public string Issuer { get; set; }

        // URL که توکن‌ها از آنجا صادر می‌شوند
        public string Audience { get; set; }

        // نوع الگوریتم برای رمزگذاری JWT (مثلاً HMACSHA256)
        public string JwtType { get; set; }

        // آیا توکن‌ها باید چرخش کنند یا خیر (برای امنیت بیشتر)
        public bool EnableTokenRotation { get; set; }

        // آیا توکن‌ها باید در لیست سیاه قرار بگیرند بعد از چرخش
        public bool EnableTokenBlacklisting { get; set; }

        // کلید‌های رمزنگاری برای عملیات‌های امن اضافی (در صورت نیاز)
        public string EncryptionKey { get; set; }
    }
}
