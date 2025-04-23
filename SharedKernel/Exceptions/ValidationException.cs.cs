using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedKernel.Exceptions
{
    /// <summary>
    /// استثنا زمانی که خطاهای اعتبارسنجی وجود داشته باشد.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// لیست خطاهای اعتبارسنجی.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; }

        /// <summary>
        /// سازنده پیش‌فرض.
        /// </summary>
        public ValidationException()
            : base("One or more validation errors occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// سازنده با پیام خطا.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        public ValidationException(string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// سازنده با لیست خطاهای اعتبارسنجی.
        /// </summary>
        /// <param name="errors">لیست خطاهای اعتبارسنجی.</param>
        public ValidationException(IDictionary<string, string[]> errors)
            : base("One or more validation errors occurred.")
        {
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        /// <summary>
        /// سازنده با پیام خطا و خطای داخلی.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        /// <param name="innerException">خطای داخلی.</param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// سازنده برای Serialization.
        /// </summary>
        /// <param name="info">اطلاعات Serialization.</param>
        /// <param name="context">Context Serialization.</param>
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Errors = (IDictionary<string, string[]>)info.GetValue("Errors", typeof(IDictionary<string, string[]>))!;
        }

        /// <summary>
        /// افزودن اطلاعات Serialization.
        /// </summary>
        /// <param name="info">اطلاعات Serialization.</param>
        /// <param name="context">Context Serialization.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Errors", Errors, typeof(IDictionary<string, string[]>));
        }
    }
}