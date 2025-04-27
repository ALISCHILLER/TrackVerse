using System;
using System.Collections.Generic;

namespace Infrastructure.Common.Helpers
{
    /// <summary>
    /// کلاس Guard برای انجام اعتبارسنجی بر روی آرگومان‌ها و مقادیر ورودی.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// بررسی می‌کند که مقدار ورودی نباید نال باشد.
        /// </summary>
        /// <param name="value">مقدار ورودی.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentNullException">اگر مقدار ورودی نال باشد.</exception>
        public static void AgainstNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, "Value cannot be null.");
            }
        }

        /// <summary>
        /// بررسی می‌کند که یک رشته نباید نال یا خالی باشد.
        /// </summary>
        /// <param name="value">مقدار ورودی.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر رشته ورودی نال یا خالی باشد.</exception>
        public static void AgainstNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value cannot be null or empty.", paramName);
            }
        }

        /// <summary>
        /// بررسی می‌کند که یک رشته نباید نال یا فقط فضای خالی باشد.
        /// </summary>
        /// <param name="value">مقدار ورودی.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر رشته ورودی نال یا فقط فضای خالی باشد.</exception>
        public static void AgainstNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null, empty or whitespace.", paramName);
            }
        }

        /// <summary>
        /// بررسی می‌کند که یک عدد باید بزرگتر از مقدار مشخص شده باشد.
        /// </summary>
        /// <param name="value">مقدار ورودی.</param>
        /// <param name="minValue">مقدار حداقل مجاز.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentOutOfRangeException">اگر مقدار ورودی کمتر از حداقل مجاز باشد.</exception>
        public static void AgainstLessThan(int value, int minValue, string paramName)
        {
            if (value < minValue)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value cannot be less than {minValue}.");
            }
        }

        /// <summary>
        /// بررسی می‌کند که یک عدد باید کوچکتر از مقدار مشخص شده باشد.
        /// </summary>
        /// <param name="value">مقدار ورودی.</param>
        /// <param name="maxValue">مقدار حداکثر مجاز.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentOutOfRangeException">اگر مقدار ورودی بیشتر از حداقل مجاز باشد.</exception>
        public static void AgainstGreaterThan(int value, int maxValue, string paramName)
        {
            if (value > maxValue)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value cannot be greater than {maxValue}.");
            }
        }

        /// <summary>
        /// بررسی می‌کند که مقدار ورودی باید یکی از مقادیر موجود در لیست باشد.
        /// </summary>
        /// <typeparam name="T">نوع داده برای بررسی.</typeparam>
        /// <param name="value">مقدار ورودی.</param>
        /// <param name="validValues">لیست مقادیر معتبر.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر مقدار ورودی در لیست معتبر نباشد.</exception>
        public static void AgainstInvalidEnumValue<T>(T value, List<T> validValues, string paramName)
        {
            if (!validValues.Contains(value))
            {
                throw new ArgumentException($"Value is not a valid {typeof(T).Name}.", paramName);
            }
        }

        /// <summary>
        /// بررسی می‌کند که یک تاریخ باید در گذشته باشد.
        /// </summary>
        /// <param name="date">تاریخ ورودی.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر تاریخ وارد شده بعد از تاریخ کنونی باشد.</exception>
        public static void AgainstFutureDate(DateTime date, string paramName)
        {
            if (date > DateTime.Now)
            {
                throw new ArgumentException("Date cannot be in the future.", paramName);
            }
        }

        /// <summary>
        /// بررسی می‌کند که یک تاریخ باید در گذشته یا هم‌اکنون باشد.
        /// </summary>
        /// <param name="date">تاریخ ورودی.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر تاریخ وارد شده بعد از تاریخ کنونی باشد.</exception>
        public static void AgainstFutureOrNowDate(DateTime date, string paramName)
        {
            if (date >= DateTime.Now)
            {
                throw new ArgumentException("Date cannot be in the future or now.", paramName);
            }
        }

        /// <summary>
        /// بررسی می‌کند که یک تاریخ باید در آینده باشد.
        /// </summary>
        /// <param name="date">تاریخ ورودی.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر تاریخ وارد شده قبل از تاریخ کنونی باشد.</exception>
        public static void AgainstPastDate(DateTime date, string paramName)
        {
            if (date < DateTime.Now)
            {
                throw new ArgumentException("Date cannot be in the past.", paramName);
            }
        }

        /// <summary>
        /// بررسی می‌کند که مقدار ورودی باید یک آدرس ایمیل معتبر باشد.
        /// </summary>
        /// <param name="email">آدرس ایمیل ورودی.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر آدرس ایمیل ورودی معتبر نباشد.</exception>
        public static void AgainstInvalidEmail(string email, string paramName)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                throw new ArgumentException("Invalid email address.", paramName);
            }
        }

        /// <summary>
        /// بررسی می‌کند که یک مقدار ورودی باید بزرگتر از صفر باشد.
        /// </summary>
        /// <param name="value">مقدار ورودی.</param>
        /// <param name="paramName">نام پارامتر برای پرتاب استثنا.</param>
        /// <exception cref="ArgumentException">اگر مقدار ورودی کمتر از یا برابر با صفر باشد.</exception>
        public static void AgainstZeroOrLess(int value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Value cannot be zero or less.", paramName);
            }
        }
    }
}
