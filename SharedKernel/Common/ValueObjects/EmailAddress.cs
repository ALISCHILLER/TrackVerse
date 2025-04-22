using System;
using System.Text.RegularExpressions;

namespace SharedKernel.Common.ValueObjects
{
    /// <summary>
    /// Value Object برای نمایش آدرس ایمیل.
    /// </summary>
    public sealed class EmailAddress : ValueObject
    {
        /// <summary>
        /// مقدار ایمیل.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// سازنده برای اعتبارسنجی و مقداردهی اولیه.
        /// </summary>
        /// <param name="value">مقدار ایمیل.</param>
        public EmailAddress(string value)
        {
            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid email format");

            Value = value;
        }

        /// <summary>
        /// پیاده‌سازی GetEqualityComponents برای مقایسه.
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// نمایش رشته‌ای ایمیل.
        /// </summary>
        public override string ToString() => Value;
    }
}