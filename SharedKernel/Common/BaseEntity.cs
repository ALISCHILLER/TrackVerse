using System;
using System.ComponentModel.DataAnnotations;

namespace SharedKernel.Common
{
    /// <summary>
    /// اینترفیس عمومی برای تمام موجودیت‌ها.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// شناسه یکتا برای هر موجودیت.
        /// </summary>
        Guid Id { get; set; }
    }

    /// <summary>
    /// کلاس پایه برای تمام موجودیت‌های دیتابیس.
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// شناسه یکتا برای هر موجودیت.
        /// </summary>
        public virtual Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// تاریخ و زمان ایجاد موجودیت.
        /// </summary>
        public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// تاریخ و زمان آخرین به‌روزرسانی موجودیت.
        /// </summary>
        public virtual DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// وضعیت حذف منطقی (Soft Delete).
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// شناسه کاربری که این موجودیت را ایجاد کرده.
        /// </summary>
        public virtual Guid? CreatedBy { get; set; }

        /// <summary>
        /// شناسه کاربری که آخرین بار این موجودیت را ویرایش کرده.
        /// </summary>
        public virtual Guid? UpdatedBy { get; set; }

        /// <summary>
        /// نسخه‌بندی برای کنترل همزمانی (Concurrency Control).
        /// </summary>
        [Timestamp]
        public virtual byte[] RowVersion { get; set; }

        /// <summary>
        /// متد برای به‌روزرسانی تاریخ آخرین تغییر.
        /// </summary>
        /// <param name="userId">شناسه کاربری که تغییر را انجام داده.</param>
        public void UpdateTimestamp(Guid? userId = null)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }

        /// <summary>
        /// متد برای نشانه‌گذاری موجودیت به عنوان حذف‌شده.
        /// </summary>
        /// <param name="userId">شناسه کاربری که حذف را انجام داده.</param>
        public void MarkAsDeleted(Guid? userId = null)
        {
            IsDeleted = true;
            UpdateTimestamp(userId);
        }

        /// <summary>
        /// متد برای نشانه‌گذاری موجودیت به عنوان بازیابی‌شده.
        /// </summary>
        /// <param name="userId">شناسه کاربری که بازیابی را انجام داده.</param>
        public void MarkAsRestored(Guid? userId = null)
        {
            IsDeleted = false;
            UpdateTimestamp(userId);
        }
    }
}