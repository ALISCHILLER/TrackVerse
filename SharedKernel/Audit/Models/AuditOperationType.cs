using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Audit.Models
{
    /// <summary>
    /// انواع عملیات لاگ‌گیری.
    /// </summary>
    public enum AuditOperationType
    {
        /// <summary>
        /// ایجاد رکورد جدید.
        /// </summary>
        Create,

        /// <summary>
        /// به‌روزرسانی رکورد موجود.
        /// </summary>
        Update,

        /// <summary>
        /// حذف رکورد.
        /// </summary>
        Delete
    }
}
