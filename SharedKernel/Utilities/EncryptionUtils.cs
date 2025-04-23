using System;
using System.Security.Cryptography;
using System.Text;

namespace SharedKernel.Utilities
{
    /// <summary>
    /// ابزارهای کمکی برای رمزگذاری و امنیت.
    /// </summary>
    public static class EncryptionUtils
    {
        /// <summary>
        /// هش کردن یک رشته با الگوریتم SHA256.
        /// </summary>
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// تولید یک رشته تصادفی برای استفاده به عنوان Salt.
        /// </summary>
        public static string GenerateSalt(int length = 16)
        {
            var random = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }
    }
}