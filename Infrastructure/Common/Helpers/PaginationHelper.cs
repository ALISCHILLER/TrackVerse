using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Common.Helpers
{
    /// <summary>
    /// کلاس PaginationHelper برای مدیریت صفحه‌بندی داده‌ها.
    /// </summary>
    public static class PaginationHelper
    {
        /// <summary>
        /// صفحه‌بندی داده‌ها بر اساس صفحه و تعداد آیتم‌ها در هر صفحه.
        /// </summary>
        /// <typeparam name="T">نوع داده مورد نظر (برای مثال: مدل داده).</typeparam>
        /// <param name="source">لیست کامل داده‌ها که باید صفحه‌بندی شوند.</param>
        /// <param name="pageNumber">شماره صفحه جاری (1-based index).</param>
        /// <param name="pageSize">تعداد آیتم‌ها در هر صفحه.</param>
        /// <returns>صفحه‌ای از داده‌ها.</returns>
        public static PaginatedResult<T> GetPagedResult<T>(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException("شماره صفحه باید بزرگتر از یا برابر ۱ باشد.", nameof(pageNumber));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("تعداد آیتم‌ها در هر صفحه باید بزرگتر از یا برابر ۱ باشد.", nameof(pageSize));
            }

            // تعداد کل داده‌ها
            int totalItemCount = source.Count();

            // محاسبه صفحه‌بندی
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // محاسبه تعداد کل صفحات
            int totalPages = (int)Math.Ceiling((double)totalItemCount / pageSize);

            // ساخت و بازگشت نتیجه صفحه‌بندی
            return new PaginatedResult<T>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItemCount = totalItemCount,
                TotalPages = totalPages
            };
        }

        /// <summary>
        /// کلاس نتیجه صفحه‌بندی.
        /// </summary>
        public class PaginatedResult<T>
        {
            /// <summary>
            /// داده‌های صفحه جاری.
            /// </summary>
            public List<T> Items { get; set; }

            /// <summary>
            /// شماره صفحه جاری.
            /// </summary>
            public int PageNumber { get; set; }

            /// <summary>
            /// تعداد آیتم‌ها در هر صفحه.
            /// </summary>
            public int PageSize { get; set; }

            /// <summary>
            /// تعداد کل داده‌ها.
            /// </summary>
            public int TotalItemCount { get; set; }

            /// <summary>
            /// تعداد کل صفحات.
            /// </summary>
            public int TotalPages { get; set; }
        }
    }
}
