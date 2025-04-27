namespace Infrastructure.Common.Constants
{
    /// <summary>
    /// کلاس برای نگهداری ثابت‌های عمومی اپلیکیشن.
    /// </summary>
    public static class ApplicationConstants
    {
        /// <summary>
        /// نسخه فعلی اپلیکیشن.
        /// </summary>
        public const string ApplicationVersion = "1.0.0";

        /// <summary>
        /// نام اپلیکیشن.
        /// </summary>
        public const string ApplicationName = "MyApplication";

        /// <summary>
        /// شناسه کاربری پیش‌فرض برای اپلیکیشن.
        /// </summary>
        public const string DefaultUserId = "default_user";

        /// <summary>
        /// پیام پیش‌فرض برای خطاهای عمومی.
        /// </summary>
        public const string GeneralErrorMessage = "An error occurred. Please try again later.";

        /// <summary>
        /// پیام موفقیت‌آمیز بودن درخواست.
        /// </summary>
        public const string SuccessMessage = "Operation completed successfully.";

        /// <summary>
        /// حداکثر تعداد درخواست‌های مجاز در دقیقه (Rate Limit).
        /// </summary>
        public const int MaxRequestsPerMinute = 1000;

        /// <summary>
        /// مدت زمان انقضای نشست (Session Timeout) در ثانیه.
        /// </summary>
        public const int SessionTimeoutInSeconds = 3600; // 1 hour

        /// <summary>
        /// فرمت تاریخ و زمان (برای نمایش به کاربران).
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// فرمت تاریخ (برای نمایش به کاربران).
        /// </summary>
        public const string DateOnlyFormat = "yyyy-MM-dd";

        /// <summary>
        /// فرمت زمان (برای نمایش به کاربران).
        /// </summary>
        public const string TimeOnlyFormat = "HH:mm:ss";

        /// <summary>
        /// پیام خطای معتبر نبودن ورودی.
        /// </summary>
        public const string InvalidInputErrorMessage = "The input provided is invalid.";

        /// <summary>
        /// پیام خطای نبود داده در سیستم.
        /// </summary>
        public const string DataNotFoundMessage = "Data not found.";

        /// <summary>
        /// پیام خطای نداشتن دسترسی.
        /// </summary>
        public const string UnauthorizedAccessMessage = "You do not have permission to access this resource.";

        /// <summary>
        /// شناسه پیش‌فرض برای توکن‌های دسترسی.
        /// </summary>
        public const string DefaultAccessTokenId = "default_access_token";

        /// <summary>
        /// پیام موفقیت‌آمیز بودن احراز هویت.
        /// </summary>
        public const string AuthenticationSuccessMessage = "Authentication successful.";

        /// <summary>
        /// پیام خطای عدم موفقیت در احراز هویت.
        /// </summary>
        public const string AuthenticationFailureMessage = "Authentication failed. Please check your credentials.";

        /// <summary>
        /// حداکثر تعداد کاراکترهای مجاز برای نام کاربری.
        /// </summary>
        public const int MaxUsernameLength = 30;

        /// <summary>
        /// حداقل تعداد کاراکترهای مجاز برای رمز عبور.
        /// </summary>
        public const int MinPasswordLength = 8;

        /// <summary>
        /// مدت زمان انقضای توکن‌های دسترسی در ثانیه.
        /// </summary>
        public const int AccessTokenExpirationTimeInSeconds = 3600; // 1 hour

        /// <summary>
        /// مدت زمان انقضای توکن‌های بازنشستگی در ثانیه.
        /// </summary>
        public const int RefreshTokenExpirationTimeInSeconds = 86400; // 24 hours

        /// <summary>
        /// دامنه (domain) برای کوکی‌ها و سشن‌ها.
        /// </summary>
        public const string CookieDomain = ".myapplication.com";

        /// <summary>
        /// تعداد حداکثر مجاز برای سعی در وارد کردن رمز عبور اشتباه.
        /// </summary>
        public const int MaxLoginAttempts = 5;

        /// <summary>
        /// پیام موفقیت‌آمیز بودن ثبت‌نام کاربر جدید.
        /// </summary>
        public const string UserRegistrationSuccessMessage = "User registration successful.";

        /// <summary>
        /// پیام خطای عدم موفقیت در ثبت‌نام کاربر.
        /// </summary>
        public const string UserRegistrationFailureMessage = "User registration failed. Please try again later.";

        /// <summary>
        /// پیام خطای وجود حساب کاربری تکراری.
        /// </summary>
        public const string DuplicateAccountMessage = "An account with this email or username already exists.";

        /// <summary>
        /// پیام خطای سیستم برای زمانی که قابلیت‌ها غیرفعال شده‌اند.
        /// </summary>
        public const string FeatureDisabledMessage = "This feature is currently disabled.";

        /// <summary>
        /// مشخصات سرور API.
        /// </summary>
        public const string ApiServerUrl = "https://api.myapplication.com";

        /// <summary>
        /// پسوند (suffix) فایل‌های بارگذاری شده.
        /// </summary>
        public const string FileUploadSuffix = "_uploaded";

        /// <summary>
        /// حداکثر اندازه فایل بارگذاری شده (به بایت).
        /// </summary>
        public const int MaxFileUploadSizeInBytes = 10485760; // 10 MB

        /// <summary>
        /// پیام خطای هنگام بارگذاری فایل.
        /// </summary>
        public const string FileUploadErrorMessage = "File upload failed. Please try again later.";

        /// <summary>
        /// پیام خطای هنگام پردازش درخواست.
        /// </summary>
        public const string RequestProcessingErrorMessage = "An error occurred while processing your request.";

        /// <summary>
        /// کلید پیش‌فرض برای ذخیره اطلاعات کاربر در کش.
        /// </summary>
        public const string DefaultUserCacheKey = "user_cache_key";

        /// <summary>
        /// زمان انقضای کش در ثانیه.
        /// </summary>
        public const int CacheExpirationTimeInSeconds = 600; // 10 minutes
    }
}
