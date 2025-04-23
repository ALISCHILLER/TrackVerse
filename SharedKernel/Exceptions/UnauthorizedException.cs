using System;
using System.Runtime.Serialization;

namespace SharedKernel.Exceptions
{
    /// <summary>
    /// استثنا مربوط به دسترسی غیرمجاز (401 Unauthorized).
    /// </summary>
    [Serializable]
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// کد خطا (اختیاری).
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// جزئیات بیشتر درباره خطای دسترسی (مثل نام Policy یا دسترسی مورد نیاز).
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// سازنده پیش‌فرض.
        /// </summary>
        public UnauthorizedException()
            : this("دسترسی غیرمجاز به منبع مورد نظر.")
        {
        }

        /// <summary>
        /// سازنده با پیام خطا.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        public UnauthorizedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// سازنده با پیام خطا و کد خطا.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        /// <param name="errorCode">کد خطای سیستمی (مثلاً "AUTH_001").</param>
        public UnauthorizedException(string message, string errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// سازنده با پیام خطا، کد خطا، و جزئیات.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        /// <param name="errorCode">کد خطای سیستمی.</param>
        /// <param name="details">جزئیات بیشتر (مثلاً نام Policy مورد نیاز).</param>
        public UnauthorizedException(string message, string errorCode, string details)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = details;
        }

        /// <summary>
        /// سازنده برای Serialization.
        /// </summary>
        /// <param name="info">اطلاعات Serialization.</param>
        /// <param name="context">Context Serialization.</param>
        protected UnauthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = info.GetString(nameof(ErrorCode));
            Details = info.GetString(nameof(Details));
        }

        /// <summary>
        /// افزودن اطلاعات Serialization.
        /// </summary>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(ErrorCode), ErrorCode);
            info.AddValue(nameof(Details), Details);
        }
    }
}