using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedKernel.Common
{
    /// <summary>
    /// نتیجه عملیات همراه با داده خروجی.
    /// </summary>
    /// <typeparam name="T">نوع داده برگشتی.</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// داده برگشتی در صورت موفقیت.
        /// </summary>
        public T? Data { get; }

        protected Result(
            T? data,
            bool isSuccess,
            string message,
            int httpStatusCode,
            string? errorCode = null,
            Exception? exception = null,
            Dictionary<string, string[]>? validationErrors = null)
            : base(isSuccess, message, httpStatusCode, errorCode, exception, validationErrors)
        {
            Data = data;
        }

        public static Result<T> Success(T data, string message = "عملیات با موفقیت انجام شد.", int httpStatusCode = 200)
        {
            return new Result<T>(data, true, message, httpStatusCode);
        }

        public static Result<T> Failure(string message, int httpStatusCode = 500, string? errorCode = null, Exception? exception = null)
        {
            return new Result<T>(default, false, message, httpStatusCode, errorCode, exception);
        }

        public static Result<T> Failure(string message, Dictionary<string, string[]> validationErrors)
        {
            return new Result<T>(default, false, message, 400, errorCode: "VALIDATION_ERROR", validationErrors: validationErrors);
        }

        public override string ToString()
        {
            return IsSuccess
                ? $"موفق ({HttpStatusCode}): {Message} | داده: {Data}"
                : $"ناموفق ({HttpStatusCode}): {Message} | خطا: {ErrorCode} - {Exception?.Message}";
        }

        /// <summary>
        /// اجرای ایمن تابع async و گرفتن نتیجه استاندارد.
        /// </summary>
        public static async Task<Result<T>> TryAsync(
            Func<Task<T>> operation,
            string successMessage = "عملیات با موفقیت انجام شد.",
            string failMessage = "خطا در اجرای عملیات")
        {
            try
            {
                var data = await operation();
                return Success(data, successMessage);
            }
            catch (Exception ex)
            {
                return Failure(failMessage, 500, exception: ex);
            }
        }
    }
}
