using System;
using System.Reflection;
using SharedKernel.Audit.Attributes;

namespace SharedKernel.Audit.Extensions
{
    /// <summary>
    /// کلاس کمکی برای افزودن متدهای Extension برای PropertyInfo.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// بررسی اینکه آیا یک پراپرتی باید در لاگ‌گیری نادیده گرفته شود یا خیر.
        /// </summary>
        /// <param name="propertyInfo">اطلاعات پراپرتی.</param>
        /// <returns>True اگر پراپرتی باید نادیده گرفته شود، در غیر این صورت False.</returns>
        public static bool IsIgnoredForAudit(this PropertyInfo propertyInfo)
        {
            // بررسی وجود Attribute [IgnoreAudit]
            return propertyInfo.GetCustomAttributes(typeof(IgnoreAuditAttribute), false).Any();
        }

        /// <summary>
        /// دریافت دلیل نادیده گرفتن پراپرتی (اگر وجود داشته باشد).
        /// </summary>
        /// <param name="propertyInfo">اطلاعات پراپرتی.</param>
        /// <returns>دلیل نادیده گرفتن پراپرتی، اگر وجود داشته باشد؛ در غیر این صورت Null.</returns>
        public static string GetIgnoreReason(this PropertyInfo propertyInfo)
        {
            // دریافت Attribute [IgnoreAudit]
            var attribute = propertyInfo.GetCustomAttributes(typeof(IgnoreAuditAttribute), false)
                                        .OfType<IgnoreAuditAttribute>()
                                        .FirstOrDefault();

            // بازگرداندن دلیل (اگر وجود داشته باشد)
            return attribute?.Reason;
        }
    }
}