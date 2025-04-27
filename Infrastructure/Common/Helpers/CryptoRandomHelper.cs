using System;
using System.Security.Cryptography;

namespace Infrastructure.Common.Helpers
{
    /// <summary>
    /// کلاس CryptoRandomHelper شامل متدهای کمکی برای تولید مقادیر تصادفی رمزنگاری شده.
    /// </summary>
    public static class CryptoRandomHelper
    {
        private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        /// <summary>
        /// تولید یک عدد تصادفی بین دو مقدار مشخص شده.
        /// </summary>
        /// <param name="minValue">حداقل مقدار.</param>
        /// <param name="maxValue">حداکثر مقدار.</param>
        /// <returns>عدد تصادفی تولید شده.</returns>
        public static int GetRandomInt(int minValue, int maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue باید کمتر از maxValue باشد.");
            }

            byte[] randomBytes = new byte[4];
            _rng.GetBytes(randomBytes);
            int randomValue = BitConverter.ToInt32(randomBytes, 0);

            // مقیاس دهی به محدوده
            return Math.Abs(randomValue % (maxValue - minValue)) + minValue;
        }

        /// <summary>
        /// تولید یک رشته تصادفی از حروف و اعداد.
        /// </summary>
        /// <param name="length">طول رشته تصادفی.</param>
        /// <returns>رشته تصادفی تولید شده.</returns>
        public static string GetRandomString(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("طول رشته باید مثبت باشد.");
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[length];
            byte[] randomBytes = new byte[length];

            _rng.GetBytes(randomBytes);

            for (int i = 0; i < length; i++)
            {
                // مقیاس دهی برای انتخاب از مجموعه حروف و اعداد
                stringChars[i] = chars[randomBytes[i] % chars.Length];
            }

            return new string(stringChars);
        }

        /// <summary>
        /// تولید یک رشته تصادفی از حروف الفبا (بدون اعداد).
        /// </summary>
        /// <param name="length">طول رشته تصادفی.</param>
        /// <returns>رشته تصادفی تولید شده.</returns>
        public static string GetRandomAlphabeticString(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("طول رشته باید مثبت باشد.");
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            char[] stringChars = new char[length];
            byte[] randomBytes = new byte[length];

            _rng.GetBytes(randomBytes);

            for (int i = 0; i < length; i++)
            {
                // مقیاس دهی برای انتخاب از مجموعه حروف الفبا
                stringChars[i] = chars[randomBytes[i] % chars.Length];
            }

            return new string(stringChars);
        }

        /// <summary>
        /// تولید یک عدد تصادفی بین 0 و 1 (مقدار شناور).
        /// </summary>
        /// <returns>عدد تصادفی بین 0 و 1.</returns>
        public static double GetRandomDouble()
        {
            byte[] randomBytes = new byte[8];
            _rng.GetBytes(randomBytes);
            ulong randomValue = BitConverter.ToUInt64(randomBytes, 0);

            // تبدیل به یک عدد شناور بین 0 و 1
            return (randomValue / (double)ulong.MaxValue);
        }

        /// <summary>
        /// تولید یک آرایه از بایت‌های تصادفی.
        /// </summary>
        /// <param name="length">طول آرایه بایت‌ها.</param>
        /// <returns>آرایه بایت‌های تصادفی تولید شده.</returns>
        public static byte[] GetRandomBytes(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("طول آرایه باید مثبت باشد.");
            }

            byte[] randomBytes = new byte[length];
            _rng.GetBytes(randomBytes);

            return randomBytes;
        }

        /// <summary>
        /// تولید یک GUID تصادفی.
        /// </summary>
        /// <returns>GUID تصادفی تولید شده.</returns>
        public static Guid GetRandomGuid()
        {
            byte[] randomBytes = new byte[16];
            _rng.GetBytes(randomBytes);
            return new Guid(randomBytes);
        }

        /// <summary>
        /// تولید یک عدد تصادفی در محدوده خاص به صورت شناور (float).
        /// </summary>
        /// <param name="minValue">حداقل مقدار.</param>
        /// <param name="maxValue">حداکثر مقدار.</param>
        /// <returns>عدد تصادفی شناور تولید شده.</returns>
        public static float GetRandomFloat(float minValue, float maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue باید کمتر از maxValue باشد.");
            }

            byte[] randomBytes = new byte[4];
            _rng.GetBytes(randomBytes);
            int randomValue = BitConverter.ToInt32(randomBytes, 0);

            // مقیاس دهی به محدوده شناور
            return (float)(minValue + (randomValue / (double)int.MaxValue) * (maxValue - minValue));
        }
    }
}
