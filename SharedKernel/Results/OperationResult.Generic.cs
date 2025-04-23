using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedKernel.Results
{
    /// <summary>
    /// کلاس عمومی برای مدیریت نتیجه عملیات‌های جنریک.
    /// </summary>
    public class OperationResult<T>
    {
        /// <summary>
        /// آیا عملیات با موفقیت انجام شده است؟
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// پیام بازخوردی برای کاربر یا لاگ‌زنی.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// کد وضعیت HTTP برای بازگشت به کلاینت.
        /// </summary>
        public int HttpStatusCode { get; }

        /// <summary>
        /// در صورت خطا، شیء استثناء مربوطه.
        /// </summary>
        [JsonIgnore]
        public Exception? Exception { get; }

        /// <summary>
        /// خروجی یا نتیجه عملیات (در صورت موفق بودن).
        /// </summary>
        public T? Data { get; }

        /// <summary>
        /// لیستی از خطاهای اعتبارسنجی یا پیام‌های خطا.
        /// </summary>
        public List<string>? Errors { get; }

        // 🔒 سازنده خصوصی - فقط از طریق متدهای استاتیک ساخته می‌شود
        private OperationResult(bool isSuccess, string message, int httpStatusCode, T? data = default, Exception? exception = null, List<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            HttpStatusCode = httpStatusCode;
            Data = data;
            Exception = exception;
            Errors = errors;
        }

        /// <summary>
        /// ایجاد نتیجه موفق با داده.
        /// </summary>
        public static OperationResult<T> Success(T data, string message = "✅ عملیات با موفقیت انجام شد.", int httpStatusCode = 200)
        {
            return new OperationResult<T>(true, message, httpStatusCode, data);
        }

        /// <summary>
        /// ایجاد نتیجه ناموفق با پیام خطا و جزئیات.
        /// </summary>
        public static OperationResult<T> Failure(string message = "❌ عملیات با شکست مواجه شد.", int httpStatusCode = 500, Exception? exception = null, List<string>? errors = null)
        {
            return new OperationResult<T>(false, message, httpStatusCode, default, exception, errors);
        }

        /// <summary>
        /// ایجاد نتیجه ناموفق از روی استثناء.
        /// </summary>
        public static OperationResult<T> FromException(Exception exception, string? message = null)
        {
            return Failure(message ?? "❌ خطای غیرمنتظره رخ داد.", 500, exception);
        }

        /// <summary>
        /// افزودن یا تغییر پیام (Fluent).
        /// </summary>
        public OperationResult<T> WithMessage(string message)
        {
            return new OperationResult<T>(IsSuccess, message, HttpStatusCode, Data, Exception, Errors);
        }

        /// <summary>
        /// تبدیل به رشته برای لاگ یا نمایش.
        /// </summary>
        public override string ToString()
        {
            return IsSuccess
                ? $"✅ موفق ({HttpStatusCode}): {Message} | داده: {Data}"
                : $"❌ ناموفق ({HttpStatusCode}): {Message} | خطا: {Exception?.Message} | جزئیات: {string.Join(" | ", Errors ?? new List<string>())}";
        }
    }
}
