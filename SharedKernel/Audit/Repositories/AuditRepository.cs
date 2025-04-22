using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Audit.Models;

namespace SharedKernel.Audit.Repositories
{
    /// <summary>
    /// پیاده‌سازی Repository برای ذخیره لاگ‌ها در دیتابیس.
    /// </summary>
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditDbContext _context;

        /// <summary>
        /// سازنده کلاس.
        /// </summary>
        /// <param name="context">Instance از DbContext.</param>
        public AuditRepository(AuditDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// ذخیره تغییرات در دیتابیس.
        /// </summary>
        /// <param name="changes">لیست تغییراتی که باید ذخیره شوند.</param>
        /// <returns>Task برای عملیات ذخیره‌سازی.</returns>
        public async Task SaveAsync(IEnumerable<AuditChange> changes)
        {
            if (changes == null || !changes.Any())
                return;

            try
            {
                await _context.AuditChanges.AddRangeAsync(changes);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // ثبت خطای رخ‌داده در لاگ‌ها
                Console.Error.WriteLine($"خطا در ذخیره لاگ‌ها: {ex.Message}");
                throw new InvalidOperationException("خطا در ذخیره لاگ‌ها در دیتابیس.", ex);
            }
        }

        /// <summary>
        /// دریافت تغییرات مرتبط با یک موجودیت خاص.
        /// </summary>
        /// <param name="entityName">نام موجودیت (Entity).</param>
        /// <param name="entityId">شناسه موجودیت (Entity ID).</param>
        /// <returns>لیستی از تغییرات مرتبط با موجودیت مشخص‌شده.</returns>
        public async Task<IEnumerable<AuditChange>> GetChangesByEntityAsync(string entityName, string entityId)
        {
            if (string.IsNullOrEmpty(entityName) || string.IsNullOrEmpty(entityId))
                throw new ArgumentException("نام موجودیت و شناسه آن نباید خالی باشند.");

            return await _context.AuditChanges
                                 .Where(ac => ac.EntityName == entityName && ac.EntityId == entityId)
                                 .ToListAsync();
        }

        /// <summary>
        /// دریافت تمام تغییرات در یک بازه زمانی مشخص.
        /// </summary>
        /// <param name="startDate">تاریخ شروع بازه.</param>
        /// <param name="endDate">تاریخ پایان بازه.</param>
        /// <returns>لیستی از تغییرات در بازه زمانی مشخص‌شده.</returns>
        public async Task<IEnumerable<AuditChange>> GetChangesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("تاریخ شروع نباید بزرگ‌تر از تاریخ پایان باشد.");

            return await _context.AuditChanges
                                 .Where(ac => ac.ChangedAt >= startDate && ac.ChangedAt <= endDate)
                                 .ToListAsync();
        }
    }
}