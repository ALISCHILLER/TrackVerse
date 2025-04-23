using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SharedKernel.Exceptions;

namespace SharedKernel.Extensions
{
    /// <summary>
    /// کلاس کمکی برای افزودن قابلیت‌های پردازش رشته.
    /// </summary>
    public static class StringExtensions
    {
        #region Validation Extensions

        /// <summary>
        /// بررسی خالی یا Whitespace بودن رشته.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string? str)
            => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// بررسی خالی بودن رشته (بدون در نظر گرفتن Whitespace).
        /// </summary>
        public static bool IsNullOrEmpty(this string? str)
            => string.IsNullOrEmpty(str);

        /// <summary>
        /// اعتبارسنجی فرمت ایمیل.
        /// </summary>
        public static bool IsValidEmail(this string? email)
        {
            if (email.IsNullOrWhiteSpace())
                return false;

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// اعتبارسنجی شماره تلفن.
        /// </summary>
        public static bool IsValidPhoneNumber(this string? phoneNumber)
        {
            if (phoneNumber.IsNullOrWhiteSpace())
                return false;

            return Regex.IsMatch(phoneNumber, @"^\+?[0-9]{10,15}$");
        }

        #endregion

        #region Formatting Extensions

        /// <summary>
        /// تبدیل رشته به حالت Title Case (مثال: "hello world" → "Hello World").
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            if (str.IsNullOrWhiteSpace())
                return string.Empty;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// تبدیل رشته به حالت Snake Case (مثال: "HelloWorld" → "hello_world").
        /// </summary>
        public static string ToSnakeCase(this string str)
        {
            if (str.IsNullOrWhiteSpace())
                return string.Empty;

            return string.Concat(
                str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())
            ).ToLower();
        }

        /// <summary>
        /// تبدیل رشته به حالت Camel Case (مثال: "hello_world" → "helloWorld").
        /// </summary>
        public static string ToCamelCase(this string str)
        {
            if (str.IsNullOrWhiteSpace())
                return string.Empty;

            return Regex.Replace(str, "([a-z])([A-Z])", "$1_$2").ToLower();
        }

        #endregion

        #region Masking Extensions

        /// <summary>
        /// مخفی کردن بخشی از ایمیل (مثال: "john.doe@example.com" → "j***.d***@example.com").
        /// </summary>
        public static string MaskEmail(this string email)
        {
            if (!email.IsValidEmail())
                throw new ValidationException("Invalid email format.");

            var parts = email.Split('@');
            var localPart = parts[0];
            var domainPart = parts[1];

            return $"{localPart[..1]}***{localPart[^1]}@{domainPart}";
        }

        /// <summary>
        /// مخفی کردن شماره تلفن (مثال: "+989123456789" → "+989*** ***789").
        /// </summary>
        public static string MaskPhoneNumber(this string phoneNumber)
        {
            if (!phoneNumber.IsValidPhoneNumber())
                throw new ValidationException("Invalid phone number format.");

            return Regex.Replace(phoneNumber, @"(\d{3})(\d{3})(\d{4})", "$1*** ***$3");
        }

        #endregion

        #region Hashing Extensions

        /// <summary>
        /// تبدیل رشته به هش SHA256.
        /// </summary>
        public static string ToSha256Hash(this string input)
        {
            if (input.IsNullOrWhiteSpace())
                return string.Empty;

            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        #endregion

        #region Truncation Extensions

        /// <summary>
        /// محدود کردن طول رشته به حداکثر تعداد کاراکتر.
        /// </summary>
        public static string Truncate(this string value, int maxLength, string suffix = "...")
        {
            if (value.IsNullOrWhiteSpace())
                return string.Empty;

            if (value.Length <= maxLength)
                return value;

            return value[..maxLength] + suffix;
        }

        #endregion

        #region Localization Extensions

        /// <summary>
        /// تبدیل رشته به فرمت موردنظر در Localization.
        /// </summary>
        public static string Localize(this string key)
        {
            // در اینجا می‌توانید از منابع Localization استفاده کنید
            // مثال: return LocalizationResources.ResourceManager.GetString(key);
            return key; // برای سادگی، مستقیم کلید را برمی‌گرداند
        }

        #endregion
    }
}