using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Infrastructure.Common.Helpers
{
    /// <summary>
    /// کلاس StringHelper شامل متدهای کمکی برای کار با رشته‌ها.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// بررسی می‌کند که آیا رشته ورودی خالی یا فقط شامل فضای سفید است.
        /// </summary>
        /// <param name="input">رشته ورودی برای بررسی.</param>
        /// <returns>برمی‌گرداند true اگر رشته خالی یا فقط فضای سفید باشد، در غیر این صورت false.</returns>
        public static bool IsNullOrWhitespace(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// بررسی می‌کند که آیا رشته ورودی فقط شامل اعداد است.
        /// </summary>
        /// <param name="input">رشته ورودی برای بررسی.</param>
        /// <returns>برمی‌گرداند true اگر رشته فقط شامل اعداد باشد، در غیر این صورت false.</returns>
        public static bool IsNumeric(string input)
        {
            return input.All(char.IsDigit);
        }

        /// <summary>
        /// تبدیل یک رشته به حروف بزرگ.
        /// </summary>
        /// <param name="input">رشته ورودی.</param>
        /// <returns>رشته تبدیل شده به حروف بزرگ.</returns>
        public static string ToUpperCase(string input)
        {
            return input?.ToUpper();
        }

        /// <summary>
        /// تبدیل یک رشته به حروف کوچک.
        /// </summary>
        /// <param name="input">رشته ورودی.</param>
        /// <returns>رشته تبدیل شده به حروف کوچک.</returns>
        public static string ToLowerCase(string input)
        {
            return input?.ToLower();
        }

        /// <summary>
        /// حذف فاصله‌های اضافی از ابتدا و انتهای رشته.
        /// </summary>
        /// <param name="input">رشته ورودی.</param>
        /// <returns>رشته با فاصله‌های حذف شده از ابتدا و انتها.</returns>
        public static string TrimWhitespace(string input)
        {
            return input?.Trim();
        }

        /// <summary>
        /// تبدیل رشته به فرمت Title Case (حروف اول کلمات بزرگ می‌شوند).
        /// </summary>
        /// <param name="input">رشته ورودی.</param>
        /// <returns>رشته تبدیل شده به فرمت Title Case.</returns>
        public static string ToTitleCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            var words = input.Split(' ');
            var titleCaseWords = words.Select(word =>
                word.Length > 0
                    ? char.ToUpper(word[0]) + word.Substring(1).ToLower()
                    : word);

            return string.Join(" ", titleCaseWords);
        }

        /// <summary>
        /// بررسی می‌کند که آیا رشته ورودی حاوی یک الگوی خاص (regex) است.
        /// </summary>
        /// <param name="input">رشته ورودی.</param>
        /// <param name="pattern">الگوی regex برای جستجو.</param>
        /// <returns>برمی‌گرداند true اگر الگوی regex در رشته پیدا شود، در غیر این صورت false.</returns>
        public static bool MatchesPattern(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// حذف کاراکترهای خاص یا حروف اضافی از رشته ورودی.
        /// </summary>
        /// <param name="input">رشته ورودی.</param>
        /// <param name="charsToRemove">کاراکترهایی که باید از رشته حذف شوند.</param>
        /// <returns>رشته با حذف کاراکترهای خاص.</returns>
        public static string RemoveCharacters(string input, string charsToRemove)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(charsToRemove))
                return input;

            return new string(input.Where(c => !charsToRemove.Contains(c)).ToArray());
        }

        /// <summary>
        /// تقسیم یک رشته به آرایه‌ای از رشته‌ها با استفاده از جداکننده خاص.
        /// </summary>
        /// <param name="input">رشته ورودی.</param>
        /// <param name="separator">جداکننده برای تقسیم رشته.</param>
        /// <returns>آرایه‌ای از رشته‌ها.</returns>
        public static string[] SplitBySeparator(string input, char separator)
        {
            return input?.Split(separator);
        }

        /// <summary>
        /// تبدیل یک رشته به Base64.
        /// </summary>
        /// <param name="input">رشته ورودی.</param>
        /// <returns>رشته ورودی به فرمت Base64 تبدیل شده.</returns>
        public static string ToBase64(string input)
        {
            if (input == null) return null;

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// تبدیل یک رشته Base64 به رشته اصلی.
        /// </summary>
        /// <param name="input">رشته Base64.</param>
        /// <returns>رشته اصلی پس از تبدیل از Base64.</returns>
        public static string FromBase64(string input)
        {
            if (input == null) return null;

            byte[] bytes = Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
