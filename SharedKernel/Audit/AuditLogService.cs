using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedKernel.Audit
{
    /// <summary>
    /// سرویس لاگ‌گیری تغییرات.
    /// </summary>
    public class AuditLogService
    {
        private readonly Repositories.IAuditRepository _auditRepository;

        /// <summary>
        /// سازنده سرویس لاگ‌گیری.
        /// </summary>
        /// <param name="auditRepository">Repository برای ذخیره لاگ‌ها.</param>
        public AuditLogService(Repositories.IAuditRepository auditRepository)
        {
            // تزریق وابستگی IAuditRepository برای ذخیره لاگ‌ها در دیتابیس
            _auditRepository = auditRepository;
        }

        /// <summary>
        /// ثبت تغییرات در دیتابیس.
        /// </summary>
        /// <typeparam name="T">نوع موجودیت (Entity).</typeparam>
        /// <param name="oldEntity">نسخه قدیمی موجودیت (برای عملیات Update و Delete).</param>
        /// <param name="newEntity">نسخه جدید موجودیت (برای عملیات Create و Update).</param>
        /// <param name="changedBy">شناسه کاربر عامل.</param>
        /// <param name="entityId">شناسه موجودیت.</param>
        /// <param name="operationType">نوع عملیات (Create, Update, Delete).</param>
        /// <param name="ipAddress">آدرس IP کاربر (اختیاری).</param>
        /// <param name="userAgent">User-Agent کاربر (اختیاری).</param>
        /// <param name="reason">دلیل تغییر (اختیاری).</param>
        public async Task LogChangesAsync<T>(
            T oldEntity,
            T newEntity,
            string changedBy,
            string entityId,
            string operationType,
            string ipAddress = null,
            string userAgent = null,
            string reason = null)
        {
            // استفاده از AuditTrailHelper برای استخراج تغییرات بین نسخه قدیمی و جدید
            var changes = Helpers.AuditTrailHelper.GetAuditChanges(
                oldEntity,
                newEntity,
                changedBy,
                entityId,
                operationType,
                ipAddress,
                userAgent,
                reason);

            // ذخیره تغییرات در دیتابیس
            await _auditRepository.SaveAsync(changes);
        }
    }
}