namespace Infrastructure.Common.Constants
{
    /// <summary>
    /// کلاس برای نگهداری نام هدرهای HTTP سفارشی.
    /// </summary>
    public static class HeaderNames
    {
        /// <summary>
        /// نام هدر برای تایید هویت (مثلاً JWT Token).
        /// </summary>
        public const string Authorization = "Authorization";

        /// <summary>
        /// نام هدر برای مشخص کردن زبان درخواست (مانند en-US یا fa-IR).
        /// </summary>
        public const string AcceptLanguage = "Accept-Language";

        /// <summary>
        /// نام هدر برای تعیین نوع درخواست (مانند JSON یا XML).
        /// </summary>
        public const string ContentType = "Content-Type";

        /// <summary>
        /// هدر برای مشخص کردن توکن احراز هویت خاص برای دسترسی به منابع.
        /// </summary>
        public const string XAuthToken = "X-Auth-Token";

        /// <summary>
        /// هدر برای ارسال اطلاعات مربوط به نسخه اپلیکیشن.
        /// </summary>
        public const string XAppVersion = "X-App-Version";

        /// <summary>
        /// هدر برای ارسال اطلاعات IP کلاینت درخواست‌دهنده.
        /// </summary>
        public const string XClientIp = "X-Client-IP";

        /// <summary>
        /// هدر برای ارسال شناسه جلسه (Session ID).
        /// </summary>
        public const string XSessionId = "X-Session-ID";

        /// <summary>
        /// هدر برای ارسال اطلاعات مربوط به مرجع درخواست (Referer).
        /// </summary>
        public const string Referer = "Referer";

        /// <summary>
        /// هدر برای ارسال اطلاعات مربوط به User-Agent (جزئیات مرورگر).
        /// </summary>
        public const string UserAgent = "User-Agent";

        /// <summary>
        /// هدر برای ارسال اطلاعات مربوط به ردیابی درخواست‌ها.
        /// </summary>
        public const string XTraceId = "X-Trace-ID";

        /// <summary>
        /// هدر برای ارسال اطلاعات مربوط به سیاست‌های CORS.
        /// </summary>
        public const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";

        /// <summary>
        /// هدر برای ارسال زمان درخواست (Timestamp).
        /// </summary>
        public const string XRequestTimestamp = "X-Request-Timestamp";

        /// <summary>
        /// هدر برای ارسال اطلاعات مربوط به نوع اپلیکیشن (مانند mobile یا web).
        /// </summary>
        public const string XAppType = "X-App-Type";
    }
}
