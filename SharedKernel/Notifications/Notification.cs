using System;

namespace SharedKernel.Notifications
{
    /// <summary>
    /// کلاس پیام اعلان (Notification) برای مدیریت اطلاعات اعلان‌ها.
    /// </summary>
    public record Notification
    {
        /// <summary>
        /// متن پیام اعلان.
        /// </summary>
        public string Message { get; init; }

        /// <summary>
        /// نوع اعلان (مانند Info, Warning, Error, Success).
        /// </summary>
        public string Type { get; init; }

        /// <summary>
        /// تاریخ و زمان ایجاد اعلان.
        /// </summary>
        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// شناسه کاربر مقصد (در صورتی که اعلان اختصاصی باشد).
        /// </summary>
        public string? UserId { get; init; }

        /// <summary>
        /// آیا اعلان خوانده شده است؟
        /// </summary>
        public bool IsRead { get; init; }

        /// <summary>
        /// تاریخ و زمان خواندن اعلان (در صورتی که خوانده شده باشد).
        /// </summary>
        public DateTime? ReadAt { get; init; }

        /// <summary>
        /// سازنده پیش‌فرض برای ایجاد یک اعلان جدید.
        /// </summary>
        public Notification(string message, string type = "Info", string? userId = null)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
            ReadAt = null;
        }

        /// <summary>
        /// متد برای علامت‌گذاری اعلان به عنوان خوانده‌شده.
        /// </summary>
        public Notification MarkAsRead()
        {
            return this with { IsRead = true, ReadAt = DateTime.UtcNow };
        }
    }
}