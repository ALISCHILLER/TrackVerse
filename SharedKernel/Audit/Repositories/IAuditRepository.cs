using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedKernel.Audit.Repositories
{
    /// <summary>
    /// Interface برای ذخیره لاگ‌ها در دیتابیس.
    /// </summary>
    public interface IAuditRepository
    {
        /// <summary>
        /// ذخیره تغییرات در دیتابیس.
        /// </summary>
        /// <param name="changes">لیست تغییراتی که باید ذخیره شوند.</param>
        /// <returns>Task برای عملیات ذخیره‌سازی.</returns>
        Task SaveAsync(IEnumerable<Models.AuditChange> changes);

        /// <summary>
        /// دریافت تغییرات مرتبط با یک موجودیت خاص.
        /// </summary>
        /// <param name="entityName">نام موجودیت (Entity).</param>
        /// <param name="entityId">شناسه موجودیت (Entity ID).</param>
        /// <returns>لیستی از تغییرات مرتبط با موجودیت مشخص‌شده.</returns>
        Task<IEnumerable<Models.AuditChange>> GetChangesByEntityAsync(string entityName, string entityId);

        /// <summary>
        /// دریافت تمام تغییرات در یک بازه زمانی مشخص.
        /// </summary>
        /// <param name="startDate">تاریخ شروع بازه.</param>
        /// <param name="endDate">تاریخ پایان بازه.</param>
        /// <returns>لیستی از تغییرات در بازه زمانی مشخص‌شده.</returns>
        Task<IEnumerable<Models.AuditChange>> GetChangesByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}