using System;

namespace Infrastructure.Common.Helpers
{
    /// <summary>
    /// کلاس GuidHelper برای تولید و اعتبارسنجی شناسه‌های GUID.
    /// </summary>
    public static class GuidHelper
    {
        /// <summary>
        /// تولید یک شناسه GUID جدید.
        /// </summary>
        /// <returns>یک شناسه GUID جدید.</returns>
        public static Guid NewGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// اعتبارسنجی اینکه یک رشته معتبر برای تبدیل به GUID است یا خیر.
        /// </summary>
        /// <param name="guidString">رشته ورودی برای بررسی.</param>
        /// <returns>بررسی می‌کند که آیا رشته ورودی یک GUID معتبر است یا خیر.</returns>
        public static bool IsValidGuid(string guidString)
        {
            return Guid.TryParse(guidString, out _);
        }

        /// <summary>
        /// تبدیل یک رشته به شناسه GUID. اگر رشته ورودی نامعتبر باشد، یک استثنا پرتاب می‌شود.
        /// </summary>
        /// <param name="guidString">رشته ورودی برای تبدیل به GUID.</param>
        /// <returns>شناسه GUID مربوط به رشته ورودی.</returns>
        /// <exception cref="ArgumentException">اگر رشته ورودی یک GUID معتبر نباشد.</exception>
        public static Guid ToGuid(string guidString)
        {
            if (Guid.TryParse(guidString, out Guid result))
            {
                return result;
            }

            throw new ArgumentException("The provided string is not a valid GUID.", nameof(guidString));
        }

        /// <summary>
        /// اعتبارسنجی اینکه یک شناسه GUID نباید خالی باشد (شناسه GUID صفر).
        /// </summary>
        /// <param name="guid">شناسه GUID برای بررسی.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر شناسه GUID خالی (صفر) باشد.</exception>
        public static void AgainstEmpty(Guid guid, string paramName)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException("GUID cannot be empty.", paramName);
            }
        }

        /// <summary>
        /// بررسی می‌کند که یک GUID باید برابر با GUID دیگری باشد.
        /// </summary>
        /// <param name="guid">شناسه GUID برای بررسی.</param>
        /// <param name="compareGuid">شناسه GUID مقایسه‌ای.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر شناسه GUID ورودی با شناسه GUID مقایسه‌ای برابر نباشد.</exception>
        public static void AgainstGuidNotEqual(Guid guid, Guid compareGuid, string paramName)
        {
            if (guid != compareGuid)
            {
                throw new ArgumentException($"GUID should be equal to {compareGuid}.", paramName);
            }
        }
    }
}
