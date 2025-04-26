using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace SharedKernel.Validation
{
    /// <summary>
    /// استثناء اختصاصی برای خطاهای اعتبارسنجی.
    /// </summary>
    public class CustomValidationException : Exception
    {
        /// <summary>
        /// لیست خطاهای اعتبارسنجی.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; }

        public CustomValidationException()
            : base("یک یا چند خطای اعتبارسنجی رخ داده است.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(f => f.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());
        }

        public CustomValidationException(string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }
    }
}
