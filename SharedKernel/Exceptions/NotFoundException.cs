using System;
using System.Runtime.Serialization;

namespace SharedKernel.Exceptions
{
    /// <summary>
    /// استثنا زمانی که یک منبع یا آیتم پیدا نشود.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// شناسه منبع مورد نظر.
        /// </summary>
        public string ResourceId { get; }

        /// <summary>
        /// نوع منبع مورد نظر.
        /// </summary>
        public string ResourceType { get; }

        /// <summary>
        /// سازنده پیش‌فرض.
        /// </summary>
        public NotFoundException()
        {
        }

        /// <summary>
        /// سازنده با پیام خطا.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// سازنده با مشخصات منبع.
        /// </summary>
        /// <param name="resourceType">نوع منبع.</param>
        /// <param name="resourceId">شناسه منبع.</param>
        public NotFoundException(string resourceType, string resourceId)
            : this($"Resource of type '{resourceType}' with ID '{resourceId}' was not found.")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }

        /// <summary>
        /// سازنده با پیام خطا و خطای داخلی.
        /// </summary>
        /// <param name="message">پیام خطا.</param>
        /// <param name="innerException">خطای داخلی.</param>
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// سازنده برای Serialization.
        /// </summary>
        /// <param name="info">اطلاعات Serialization.</param>
        /// <param name="context">Context Serialization.</param>
        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}