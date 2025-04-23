using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedKernel.Common
{
    /// <summary>
    /// نتیجه عملیات بدون داده خروجی.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// موفقیت یا شکست عملیات.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// پیام نتیجه عملیات.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// کد وضعیت HTTP مرتبط.
        /// </summary>
        public int HttpStatusCode { get; }

        /// <summary>
        /// خطای داخلی در صورت شکست عملیات.
        /// </summary>
        [JsonIgnore]
        public Exception? Exception { get; }

        /// <summary>
        /// کد خطای داخلی جهت تشخیص برنامه‌نویسی (مثلاً: USER_NOT_FOUND).
        /// </summary>
        public string? ErrorCode { get; }

        /// <summary>
        /// لیست خطاهای اعتبارسنجی.
        /// </summary>
        public Dictionary<string, string[]>? ValidationErrors { get; init; }

        protected Result(
            bool isSuccess,
            string message,
            int httpStatusCode,
            string? errorCode = null,
            Exception? exception = null,
            Dictionary<string, string[]>? validationErrors = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            HttpStatusCode = httpStatusCode;
            Exception = exception;
            ErrorCode = errorCode;
            ValidationErrors = validationErrors;
        }

        public static Result Success(string message = "عملیات با موفقیت انجام شد.", int httpStatusCode = 200)
        {
            return new Result(true, message, httpStatusCode);
        }

        public static Result Failure(string message, int httpStatusCode = 500, string? errorCode = null, Exception? exception = null)
        {
            return new Result(false, message, httpStatusCode, errorCode, exception);
        }

        public static Result Failure(string message, Dictionary<string, string[]> validationErrors)
        {
            return new Result(false, message, 400, errorCode: "VALIDATION_ERROR", validationErrors: validationErrors);
        }

        public override string ToString()
        {
            return IsSuccess
                ? $"موفق ({HttpStatusCode}): {Message}"
                : $"ناموفق ({HttpStatusCode}): {Message} | خطا: {ErrorCode} - {Exception?.Message}";
        }
    }
}
