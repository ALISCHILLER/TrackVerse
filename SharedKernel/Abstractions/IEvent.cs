using System;
using System.Collections.Generic;

namespace SharedKernel.Abstractions
{
    /// <summary>
    /// این Interface به‌عنوان قرارداد برای رویدادها (Events) استفاده می‌شود.
    /// رویدادها نشان‌دهنده تغییرات یا اتفاقات مهم در دامنه هستند و معمولاً برای انتشار به سایر بخش‌های سیستم استفاده می‌شوند.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// زمان وقوع رویداد.
        /// این فیلد نشان‌دهنده زمان دقیقی است که رویداد رخ داده است.
        /// </summary>
        DateTime OccurredOn { get; }

        /// <summary>
        /// شناسه منحصر به فرد رویداد.
        /// این شناسه برای ردیابی و شناسایی دقیق هر رویداد استفاده می‌شود.
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// نوع رویداد.
        /// این فیلد نشان‌دهنده نوع رویداد است (مثلاً "OrderPlaced", "UserRegistered").
        /// </summary>
        string EventType { get; }

        /// <summary>
        /// متادیتا (اطلاعات اضافی) مرتبط با رویداد.
        /// این فیلد می‌تواند شامل اطلاعات جانبی مانند منبع رویداد، اولویت، یا هر اطلاعات دیگری باشد.
        /// </summary>
        IDictionary<string, object> Metadata { get; }
    }
}