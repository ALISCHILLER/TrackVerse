using System;

namespace SharedKernel.Audit.Attributes
{
    /// <summary>
    /// Attribute برای مشخص کردن فیلدهایی که نباید در لاگ‌گیری ثبت شوند.
    /// </summary>
    /// <remarks>
    /// این Attribute روی پراپرتی‌هایی قرار می‌گیرد که نمی‌خواهید در فرآیند لاگ‌گیری تغییرات، مقادیر آن‌ها ذخیره شود.
    /// به عنوان مثال، پراپرتی‌های حساس مثل "Password" یا پراپرتی‌های غیرضروری مثل "UpdatedAt" می‌توانند با این Attribute نادیده گرفته شوند.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreAuditAttribute : Attribute
    {
        /// <summary>
        /// دلیل نادیده گرفتن پراپرتی (اختیاری).
        /// </summary>
        public string Reason { get; }

        /// <summary>
        /// سازنده پیش‌فرض کلاس.
        /// </summary>
        /// <param name="reason">دلیل نادیده گرفتن پراپرتی (اختیاری).</param>
        public IgnoreAuditAttribute(string reason = "")
        {
            Reason = reason;
        }
    }
}