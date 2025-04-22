using System;

namespace SharedKernel.Common
{
    /// <summary>
    /// کلاس عمومی برای مدیریت نتایج عملیات‌ها.
    /// </summary>
    /// <typeparam name="T">نوع داده‌ای که نتیجه برمی‌گرداند.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// وضعیت موفقیت عملیات.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// پیام مرتبط با نتیجه.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// داده‌های برگشتی در صورت موفقیت.
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// خطا (در صورت وجود).
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// سازنده برای نتیجه موفق.
        /// </summary>
        /// <param name="data">داده‌های برگشتی.</param>
        /// <param name="message">پیام موفقیت.</param>
        private Result(T data, string message)
        {
            IsSuccess = true;
            Data = data;
            Message = message;
        }

        /// <summary>
        /// سازنده برای نتیجه ناموفق.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        /// <param name="exception">خطای رخ‌داده (اختیاری).</param>
        private Result(string message, Exception exception = null)
        {
            IsSuccess = false;
            Message = message;
            Exception = exception;
        }

        /// <summary>
        /// ایجاد نتیجه موفق.
        /// </summary>
        /// <param name="data">داده‌های برگشتی.</param>
        /// <param name="message">پیام موفقیت.</param>
        public static Result<T> Success(T data, string message = "Operation completed successfully.")
        {
            return new Result<T>(data, message);
        }

        /// <summary>
        /// ایجاد نتیجه ناموفق.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        /// <param name="exception">خطای رخ‌داده (اختیاری).</param>
        public static Result<T> Failure(string message, Exception exception = null)
        {
            return new Result<T>(message, exception);
        }

        /// <summary>
        /// نمایش رشته‌ای نتیجه.
        /// </summary>
        public override string ToString()
        {
            return IsSuccess
                ? $"Success: {Message}"
                : $"Failure: {Message}{(Exception != null ? $" - Exception: {Exception.Message}" : "")}";
        }
    }
}