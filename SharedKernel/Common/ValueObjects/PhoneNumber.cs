using System;
using System.Text.RegularExpressions;

namespace SharedKernel.Common.ValueObjects
{
    /// <summary>
    /// Value Object برای نمایش شماره تلفن.
    /// </summary>
    public sealed class PhoneNumber : ValueObject
    {
        /// <summary>
        /// مقدار شماره تلفن.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// سازنده برای اعتبارسنجی و مقداردهی اولیه.
        /// </summary>
        /// <param name="value">مقدار شماره تلفن.</param>
        public PhoneNumber(string value)
        {
            if (!Regex.IsMatch(value, @"^\+?[0-9]{10,15}$"))
                throw new ArgumentException("Invalid phone number format");

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
        /// نمایش رشته‌ای شماره تلفن.
        /// </summary>
        public override string ToString() => Value;
    }
}