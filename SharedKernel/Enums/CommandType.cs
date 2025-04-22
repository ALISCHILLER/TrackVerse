namespace SharedKernel.Enums
{
    /// <summary>
    /// تعریف انواع دستورات قابل ارسال به دستگاه‌ها.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// راه‌اندازی مجدد دستگاه.
        /// </summary>
        Reboot,

        /// <summary>
        /// دریافت موقعیت فعلی دستگاه.
        /// </summary>
        GetLocation,

        /// <summary>
        /// تنظیم پیکربندی دستگاه.
        /// </summary>
        SetConfig,

        /// <summary>
        /// ارسال هشدار به دستگاه.
        /// </summary>
        SendAlert,

        /// <summary>
        /// شروع ردیابی.
        /// </summary>
        StartTracking,

        /// <summary>
        /// توقف ردیابی.
        /// </summary>
        StopTracking,

        /// <summary>
        /// بروزرسانی نرم‌افزار دستگاه.
        /// </summary>
        UpdateFirmware,

        /// <summary>
        /// قطع اتصال دستگاه.
        /// </summary>
        Disconnect,

        /// <summary>
        /// تست اتصال دستگاه.
        /// </summary>
        Ping
    }
}