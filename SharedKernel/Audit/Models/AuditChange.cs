using System;
using System.Text.Json;

namespace SharedKernel.Audit.Models
{
    /// <summary>
    /// مدل داده‌ای برای ثبت تغییرات در دیتابیس.
    /// </summary>
    public class AuditChange
    {
        /// <summary>
        /// شناسه یکتا لاگ تغییرات.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// نوع عملیات (Create, Update, Delete).
        /// </summary>
        public AuditOperationType OperationType { get; set; }

        /// <summary>
        /// نام موجودیت (Entity) که تغییر در آن انجام شده است.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// شناسه موجودیت (Entity ID) که تغییر در آن انجام شده است.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// نام فیلد (با dot notation برای Nested Objects).
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// مقدار قبلی فیلد (به صورت JSON ذخیره می‌شود).
        /// </summary>
        public JsonElement OldValue { get; set; }

        /// <summary>
        /// مقدار جدید فیلد (به صورت JSON ذخیره می‌شود).
        /// </summary>
        public JsonElement NewValue { get; set; }

        /// <summary>
        /// شناسه کاربر عامل.
        /// </summary>
        public string ChangedBy { get; set; }

        /// <summary>
        /// IP Address کاربر.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// User-Agent کاربر.
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// تاریخ و زمان تغییر.
        /// </summary>
        public DateTime ChangedAt { get; set; }

        /// <summary>
        /// دلیل تغییر (اختیاری).
        /// </summary>
        public string ChangeReason { get; set; }
    }

    
}