using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedKernel.Results
{
    /// <summary>
    /// کلاس عمومی برای مدیریت نتیجه عملیات بدون داده بازگشتی.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// آیا عملیات موفق بوده است؟
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// پیام برای بازخورد یا نمایش.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// کد وضعیت HTTP.
        /// </summary>
        public int HttpStatusCode { get; }

        /// <summary>
        /// استثناء احتمالی در صورت بروز خطا.
        /// </summary>
        [JsonIgnore]
        public Exception? Exception { get; }

        /// <summary>
        /// مجموعه‌ای از خطاها یا پیام‌های اعتبارسنجی.
        /// </summary>
        public List<string>? Errors { get; }

        // 🔒 سازنده خصوصی
        private OperationResult(bool isSuccess, string message, int httpStatusCode, Exception? exception = null, List<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            HttpStatusCode = httpStatusCode;
            Exception = exception;
            Errors = errors;
        }

        /// <summary>
        /// نتیجه موفق بدون داده بازگشتی.
        /// </summary>
        public static OperationResult Success(string message = "✅ عملیات با موفقیت انجام شد.", int httpStatusCode = 200)
        {
            return new OperationResult(true, message, httpStatusCode);
        }

        /// <summary>
        /// نتیجه ناموفق با پیام، کد وضعیت و جزئیات اختیاری.
        /// </summary>
        public static OperationResult Failure(string message = "❌ عملیات ناموفق بود.", int httpStatusCode = 500, Exception? exception = null, List<string>? errors = null)
        {
            return new OperationResult(false, message, httpStatusCode, exception, errors);
        }

        /// <summary>
        /// ساخت نتیجه از روی استثناء.
        /// </summary>
        public static OperationResult FromException(Exception exception, string? message = null)
        {
            return Failure(message ?? "❌ خطای غیرمنتظره رخ داد.", 500, exception);
        }

        /// <summary>
        /// Fluent API برای افزودن پیام.
        /// </summary>
        public OperationResult WithMessage(string message)
        {
            return new OperationResult(IsSuccess, message, HttpStatusCode, Exception, Errors);
        }

        /// <summary>
        /// تبدیل به متن برای لاگ یا نمایش.
        /// </summary>
        public override string ToString()
        {
            return IsSuccess
                ? $"✅ موفق ({HttpStatusCode}): {Message}"
                : $"❌ ناموفق ({HttpStatusCode}): {Message} | خطا: {Exception?.Message} | جزئیات: {string.Join(" | ", Errors ?? new List<string>())}";
        }
    }
}
