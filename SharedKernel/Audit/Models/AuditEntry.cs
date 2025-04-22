using System;
using System.Text.Json;

namespace SharedKernel.Audit.Models
{
    /// <summary>
    /// مدل داده‌ای برای ثبت تغییرات سیستمی (Audit Entry).
    /// </summary>
    public class AuditEntry
    {
        /// <summary>
        /// شناسه یکتا لاگ تغییرات.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

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
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// دلیل تغییر (اختیاری).
        /// </summary>
        public string ChangeReason { get; set; }

        /// <summary>
        /// وضعیت حذف منطقی (Soft Delete).
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// زمان آخرین به‌روزرسانی.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// متد برای سریالایز مقادیر به JsonElement.
        /// </summary>
        /// <param name="value">مقدار ورودی.</param>
        /// <returns>JsonElement حاصل از سریالایز.</returns>
        public static JsonElement SerializeToJsonElement(object value)
        {
            if (value == null)
                return default;

            try
            {
                var json = JsonSerializer.Serialize(value);
                using var document = JsonDocument.Parse(json);
                return document.RootElement.Clone();
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// متد برای دی‌سریالایز JsonElement به نوع دلخواه.
        /// </summary>
        /// <typeparam name="T">نوع خروجی.</typeparam>
        /// <param name="jsonElement">JsonElement ورودی.</param>
        /// <returns>مقدار دی‌سریالایز شده.</returns>
        public static T DeserializeFromJsonElement<T>(JsonElement jsonElement)
        {
            if (jsonElement.ValueKind == JsonValueKind.Undefined || jsonElement.ValueKind == JsonValueKind.Null)
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(jsonElement.GetRawText());
            }
            catch
            {
                return default;
            }
        }
    }

  
}