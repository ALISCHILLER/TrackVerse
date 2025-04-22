namespace SharedKernel.Enums
{
    /// <summary>
    /// تعریف انواع اعلان‌ها (Notifications).
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// اطلاعیه عمومی.
        /// </summary>
        Info,

        /// <summary>
        /// هشدار.
        /// </summary>
        Warning,

        /// <summary>
        /// اعلام خطر.
        /// </summary>
        Alert,

        /// <summary>
        /// نتیجه اجرای دستور.
        /// </summary>
        CommandResult,

        /// <summary>
        /// اعلان موفقیت.
        /// </summary>
        Success,

        /// <summary>
        /// اعلان خطا.
        /// </summary>
        Error,

        /// <summary>
        /// اعلان سیستمی.
        /// </summary>
        SystemMessage
    }
}