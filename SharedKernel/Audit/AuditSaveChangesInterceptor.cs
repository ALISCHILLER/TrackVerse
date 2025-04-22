using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Audit.Helpers;
using SharedKernel.Audit.Models;
using SharedKernel.Audit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SharedKernel.Audit
{
    /// <summary>
    /// Interceptor برای لاگ‌گیری خودکار تغییرات در SaveChanges.
    /// </summary>
    public class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IAuditRepository _auditRepository;

        /// <summary>
        /// سازنده کلاس.
        /// </summary>
        /// <param name="auditRepository">Instance از IAuditRepository برای ذخیره لاگ‌ها.</param>
        public AuditSaveChangesInterceptor(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
        }

        /// <summary>
        /// متد برای لاگ‌گیری خودکار تغییرات قبل از ذخیره‌سازی در دیتابیس.
        /// </summary>
        /// <param name="eventData">اطلاعات رویداد مرتبط با SaveChanges.</param>
        /// <param name="result">نتیجه میانی عملیات SaveChanges.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>InterceptionResult حاوی نتیجه عملیات.</returns>
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            // بررسی وجود Context
            if (eventData.Context == null)
                return await base.SavingChangesAsync(eventData, result, cancellationToken);

            // استخراج تغییرات از ChangeTracker
            var changes = new List<AuditChange>();

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                // دریافت نوع موجودیت و شناسه آن
                var entityType = entry.Entity.GetType();
                var entityId = GetEntityId(entry) ?? Guid.NewGuid().ToString();

                // تعیین نوع عملیات و استخراج تغییرات
                switch (entry.State)
                {
                    case EntityState.Added:
                        changes.AddRange(AuditTrailHelper.GetAuditChanges(
                            null,
                            entry.Entity,
                            "System",
                            entityId,
                            AuditOperationType.Create));
                        break;

                    case EntityState.Modified:
                        changes.AddRange(AuditTrailHelper.GetAuditChanges(
                            entry.OriginalValues.ToObject(),
                            entry.Entity,
                            "System",
                            entityId,
                            AuditOperationType.Update));
                        break;

                    case EntityState.Deleted:
                        changes.AddRange(AuditTrailHelper.GetAuditChanges(
                            entry.Entity,
                            null,
                            "System",
                            entityId,
                            AuditOperationType.Delete));
                        break;
                }
            }

            // ذخیره تغییرات در دیتابیس
            if (changes.Any())
            {
                try
                {
                    await _auditRepository.SaveAsync(changes);
                }
                catch (Exception ex)
                {
                    // ثبت خطای رخ‌داده در لاگ‌ها
                    Console.Error.WriteLine($"خطا در ذخیره لاگ‌ها: {ex.Message}");
                    throw new InvalidOperationException("خطا در ذخیره لاگ‌ها در دیتابیس.", ex);
                }
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// دریافت شناسه موجودیت (Entity ID).
        /// </summary>
        /// <param name="entry">ورودی ChangeTracker.</param>
        /// <returns>شناسه موجودیت به صورت رشته.</returns>
        private string GetEntityId(EntityEntry entry)
        {
            try
            {
                // بررسی وجود پراپرتی "Id" یا "ID"
                var idProperty = entry.Properties.FirstOrDefault(p => p.Metadata.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
                return idProperty?.CurrentValue?.ToString();
            }
            catch
            {
                // در صورت بروز خطا، شناسه پیش‌فرض برگردانده می‌شود
                return null;
            }
        }
    }
}