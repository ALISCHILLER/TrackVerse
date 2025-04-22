using System;

namespace SharedKernel.Abstractions
{
    /// <summary>
    /// این Interface به‌عنوان قرارداد برای Aggregate Roots استفاده می‌شود.
    /// Aggregate Rootها موجودیت‌هایی هستند که به‌عنوان نقطه ورود برای دسترسی به سایر موجودیت‌ها و Value Objects عمل می‌کنند.
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// شناسه منحصر به فرد Aggregate Root.
        /// این شناسه باید در تمام موجودیت‌ها یکتا باشد.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// زمان ایجاد Aggregate Root.
        /// این فیلد نشان‌دهنده زمان ایجاد اولیه موجودیت است.
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// زمان آخرین به‌روزرسانی Aggregate Root.
        /// این فیلد نشان‌دهنده زمان آخرین تغییر در موجودیت است.
        /// </summary>
        DateTime? UpdatedAt { get; }

        /// <summary>
        /// لیست رویدادهای دامنه (Domain Events) که در اثر تغییرات در Aggregate Root ایجاد شده‌اند.
        /// این رویدادها معمولاً برای انتشار به سایر بخش‌های سیستم استفاده می‌شوند.
        /// </summary>
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// اضافه کردن یک رویداد دامنه به لیست رویدادهای Aggregate Root.
        /// </summary>
        /// <param name="domainEvent">رویداد دامنه که باید اضافه شود.</param>
        void AddDomainEvent(IDomainEvent domainEvent);

        /// <summary>
        /// حذف یک رویداد دامنه از لیست رویدادهای Aggregate Root.
        /// </summary>
        /// <param name="domainEvent">رویداد دامنه که باید حذف شود.</param>
        void RemoveDomainEvent(IDomainEvent domainEvent);

        /// <summary>
        /// پاک کردن تمام رویدادهای دامنه از لیست رویدادهای Aggregate Root.
        /// </summary>
        void ClearDomainEvents();
    }
}