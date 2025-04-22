using System;

namespace SharedKernel.Common.Exceptions
{
    /// <summary>
    /// استثناء سفارشی برای مدیریت خطاها.
    /// </summary>
    public class CustomException : Exception
    {
        /// <summary>
        /// کد خطا.
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// سازنده با پیام و کد خطا.
        /// </summary>
        public CustomException(string message, int errorCode = 400)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}