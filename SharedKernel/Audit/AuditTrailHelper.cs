using SharedKernel.Audit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace SharedKernel.Audit.Helpers
{
    /// <summary>
    /// کلاس کمکی برای تشخیص تغییرات بین نسخه قدیمی و جدید موجودیت‌ها.
    /// </summary>
    public static class AuditTrailHelper
    {
        /// <summary>
        /// مقایسه دو موجودیت و استخراج تغییرات.
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
        /// <returns>لیستی از تغییرات استخراج‌شده.</returns>
        public static List<AuditChange> GetAuditChanges<T>(
            T oldEntity,
            T newEntity,
            string changedBy,
            string entityId,
            AuditOperationType operationType,
            string ipAddress = null,
            string userAgent = null,
            string reason = null)
        {
            var changes = new List<AuditChange>();

            // ذخیره نام موجودیت برای استفاده در لاگ‌ها
            string entityName = typeof(T).Name;

            // لیست سیاه برای نادیده گرفتن پراپرتی‌های معروف
            var excludedProps = new[] { "UpdatedAt", "LastModified", "Timestamp" };

            // دریافت تمامی پراپرتی‌های موجودیت
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                // اگر پراپرتی باید از لاگ‌گیری حذف شود، رد می‌شود
                if (!prop.CanRead ||
                    prop.GetCustomAttributes(typeof(IgnoreAuditAttribute), false).Any() ||
                    excludedProps.Contains(prop.Name))
                    continue;

                // دریافت مقادیر قدیمی و جدید پراپرتی
                var oldValue = SafeSerialize(oldEntity != null ? prop.GetValue(oldEntity) : null);
                var newValue = SafeSerialize(newEntity != null ? prop.GetValue(newEntity) : null);

                // بررسی نوع عملیات و ثبت تغییرات متناظر
                if (operationType == AuditOperationType.Create && !string.IsNullOrEmpty(newValue))
                {
                    changes.Add(CreateAuditRecord(prop.Name, oldValue, newValue, changedBy, entityId, operationType, ipAddress, userAgent, reason, entityName));
                }
                else if (operationType == AuditOperationType.Delete && !string.IsNullOrEmpty(oldValue))
                {
                    changes.Add(CreateAuditRecord(prop.Name, oldValue, newValue, changedBy, entityId, operationType, ipAddress, userAgent, reason, entityName));
                }
                else if (oldValue != newValue)
                {
                    changes.Add(CreateAuditRecord(prop.Name, oldValue, newValue, changedBy, entityId, operationType, ipAddress, userAgent, reason, entityName));
                }
            }

            return changes;
        }

        /// <summary>
        /// ایجاد یک رکورد لاگ تغییرات.
        /// </summary>
        private static AuditChange CreateAuditRecord(
            string propertyName,
            string oldValue,
            string newValue,
            string changedBy,
            string entityId,
            AuditOperationType operationType,
            string ipAddress,
            string userAgent,
            string reason,
            string entityName)
        {
            return new AuditChange
            {
                Id = Guid.NewGuid(), // شناسه یکتا لاگ تغییرات
                OperationType = operationType, // نوع عملیات
                EntityName = entityName, // نام موجودیت
                EntityId = entityId, // شناسه موجودیت
                PropertyName = propertyName, // نام پراپرتی
                OldValue = ParseToJsonElement(oldValue), // مقدار قبلی (JSON)
                NewValue = ParseToJsonElement(newValue), // مقدار جدید (JSON)
                ChangedBy = changedBy, // شناسه کاربر عامل
                IpAddress = ipAddress, // آدرس IP کاربر
                UserAgent = userAgent, // User-Agent کاربر
                ChangedAt = DateTime.UtcNow, // تاریخ و زمان تغییر
                ChangeReason = reason // دلیل تغییر
            };
        }

        /// <summary>
        /// سریالایز امن برای مقادیر پراپرتی‌ها.
        /// </summary>
        private static string SafeSerialize(object value)
        {
            try
            {
                // اگر مقدار Null باشد، به صورت خالی ذخیره شود
                if (value == null) return "null";

                // اگر پراپرتی دارای Attribute [MaskAudit] باشد، مقدار ماسک شود
                var type = value.GetType();
                if (type.GetCustomAttributes(typeof(MaskAuditAttribute), false).Any())
                    return "\"*****\"";

                // سریالایز مقادیر به JSON
                return JsonSerializer.Serialize(value);
            }
            catch
            {
                // در صورت بروز خطای سریالایز، مقدار "[Unserializable]" ذخیره شود
                return "\"[Unserializable]\"";
            }
        }

        /// <summary>
        /// تبدیل رشته JSON به JsonElement.
        /// </summary>
        private static JsonElement ParseToJsonElement(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default;

            using var document = JsonDocument.Parse(value);
            return document.RootElement.Clone();
        }
    }

    /// <summary>
    /// Attribute برای ماسک کردن مقادیر حساس.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaskAuditAttribute : Attribute
    {
    }

    /// <summary>
    /// Attribute برای نادیده گرفتن پراپرتی‌های خاص.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAuditAttribute : Attribute
    {
    }
}