using System.Threading.Tasks;

namespace SharedKernel.Audit
{
    /// <summary>
    /// اینترفیس برای کمک به استخراج تغییرات لاگ.
    /// </summary>
    public interface IAuditTrailHelper
    {
        /// <summary>
        /// استخراج تغییرات بین نسخه قدیمی و جدید موجودیت.
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
        Task<AuditLog> GetAuditChangesAsync<T>(
            T oldEntity,
            T newEntity,
            string changedBy,
            string entityId,
            string operationType,
            string ipAddress = null,
            string userAgent = null,
            string reason = null);
    }
}
