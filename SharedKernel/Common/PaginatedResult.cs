using System.Collections.Generic;

namespace SharedKernel.Common
{
    /// <summary>
    /// کلاس عمومی برای مدیریت نتایج پیاده‌سازی‌شده.
    /// </summary>
    /// <typeparam name="T">نوع داده‌ای که نتیجه برمی‌گرداند.</typeparam>
    public class PaginatedResult<T>
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
        /// تعداد کل آیتم‌ها.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// تعداد صفحات.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// صفحه فعلی.
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        /// تعداد آیتم‌های هر صفحه.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// داده‌های برگشتی.
        /// </summary>
        public IEnumerable<T> Data { get; }

        /// <summary>
        /// خطا (در صورت وجود).
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// سازنده برای نتیجه موفق.
        /// </summary>
        /// <param name="data">داده‌های برگشتی.</param>
        /// <param name="totalCount">تعداد کل آیتم‌ها.</param>
        /// <param name="currentPage">صفحه فعلی.</param>
        /// <param name="pageSize">تعداد آیتم‌های هر صفحه.</param>
        /// <param name="message">پیام موفقیت.</param>
        private PaginatedResult(IEnumerable<T> data, int totalCount, int currentPage, int pageSize, string message)
        {
            IsSuccess = true;
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Message = message;
        }

        /// <summary>
        /// سازنده برای نتیجه ناموفق.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        /// <param name="exception">خطای رخ‌داده (اختیاری).</param>
        private PaginatedResult(string message, Exception exception = null)
        {
            IsSuccess = false;
            Message = message;
            Exception = exception;
        }

        /// <summary>
        /// ایجاد نتیجه موفق برای داده‌های پیاده‌سازی‌شده.
        /// </summary>
        /// <param name="data">داده‌های برگشتی.</param>
        /// <param name="totalCount">تعداد کل آیتم‌ها.</param>
        /// <param name="currentPage">صفحه فعلی.</param>
        /// <param name="pageSize">تعداد آیتم‌های هر صفحه.</param>
        /// <param name="message">پیام موفقیت.</param>
        public static PaginatedResult<T> Success(IEnumerable<T> data, int totalCount, int currentPage, int pageSize, string message = "Operation completed successfully.")
        {
            return new PaginatedResult<T>(data, totalCount, currentPage, pageSize, message);
        }

        /// <summary>
        /// ایجاد نتیجه ناموفق.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        /// <param name="exception">خطای رخ‌داده (اختیاری).</param>
        public static PaginatedResult<T> Failure(string message, Exception exception = null)
        {
            return new PaginatedResult<T>(message, exception);
        }

        /// <summary>
        /// نمایش رشته‌ای نتیجه.
        /// </summary>
        public override string ToString()
        {
            return IsSuccess
                ? $"Success: {Message} - Page {CurrentPage}/{TotalPages}, Total Items: {TotalCount}"
                : $"Failure: {Message}{(Exception != null ? $" - Exception: {Exception.Message}" : "")}";
        }
    }
}