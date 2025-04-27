using System;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Common.Helpers
{
    /// <summary>
    /// کلاس PasswordGenerator برای تولید رمز عبور ایمن.
    /// </summary>
    public static class PasswordGenerator
    {
        private static readonly string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string Digits = "0123456789";
        private static readonly string SpecialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/";

        /// <summary>
        /// تولید یک رمز عبور ایمن به طول مشخص.
        /// </summary>
        /// <param name="length">طول رمز عبور مورد نظر.</param>
        /// <param name="requireUppercase">آیا باید حروف بزرگ شامل شوند؟</param>
        /// <param name="requireLowercase">آیا باید حروف کوچک شامل شوند؟</param>
        /// <param name="requireDigits">آیا باید اعداد شامل شوند؟</param>
        /// <param name="requireSpecialChars">آیا باید کاراکترهای خاص شامل شوند؟</param>
        /// <returns>رمز عبور ایمن تولید شده.</returns>
        public static string GeneratePassword(int length, bool requireUppercase = true, bool requireLowercase = true,
                                               bool requireDigits = true, bool requireSpecialChars = true)
        {
            if (length < 8)
            {
                throw new ArgumentException("رمز عبور باید حداقل 8 کاراکتر باشد.", nameof(length));
            }

            StringBuilder password = new StringBuilder();
            string allowedChars = string.Empty;

            // اضافه کردن کاراکترهای مجاز به رمز عبور
            if (requireUppercase)
                allowedChars += UppercaseChars;
            if (requireLowercase)
                allowedChars += LowercaseChars;
            if (requireDigits)
                allowedChars += Digits;
            if (requireSpecialChars)
                allowedChars += SpecialChars;

            if (string.IsNullOrEmpty(allowedChars))
            {
                throw new ArgumentException("باید حداقل یک نوع کاراکتر برای رمز عبور انتخاب شود.");
            }

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                // ایجاد رمز عبور با استفاده از کاراکترهای تصادفی
                for (int i = 0; i < length; i++)
                {
                    int index = randomBytes[i] % allowedChars.Length;
                    password.Append(allowedChars[index]);
                }
            }

            return password.ToString();
        }

        /// <summary>
        /// تولید یک رمز عبور ایمن و تصادفی با استانداردهای معمول.
        /// </summary>
        /// <returns>رمز عبور ایمن و تصادفی.</returns>
        public static string GenerateDefaultPassword()
        {
            // تولید رمز عبور 12 کاراکتری با شرایط استاندارد
            return GeneratePassword(12, true, true, true, true);
        }
    }
}
