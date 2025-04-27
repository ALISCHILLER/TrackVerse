namespace Infrastructure.Common.Constants
{
    /// <summary>
    /// کلاس برای نگهداری ثابت‌های مربوط به ClaimTypes در احراز هویت.
    /// </summary>
    public static class ClaimTypesConstants
    {
        /// <summary>
        /// شناسه کاربری (User ID) در سیستم.
        /// </summary>
        public const string UserId = "user_id";

        /// <summary>
        /// نام کاربری (Username) در سیستم.
        /// </summary>
        public const string Username = "username";

        /// <summary>
        /// ایمیل کاربر (Email) در سیستم.
        /// </summary>
        public const string Email = "email";

        /// <summary>
        /// نقش کاربر (Role) در سیستم.
        /// </summary>
        public const string Role = "role";

        /// <summary>
        /// نام اول کاربر (First Name) در سیستم.
        /// </summary>
        public const string FirstName = "first_name";

        /// <summary>
        /// نام خانوادگی کاربر (Last Name) در سیستم.
        /// </summary>
        public const string LastName = "last_name";

        /// <summary>
        /// شماره تلفن همراه کاربر (Mobile Number) در سیستم.
        /// </summary>
        public const string MobileNumber = "mobile_number";

        /// <summary>
        /// تاریخ تولد کاربر (Date of Birth) در سیستم.
        /// </summary>
        public const string DateOfBirth = "date_of_birth";

        /// <summary>
        /// شناسه ملی کاربر (National ID) در سیستم.
        /// </summary>
        public const string NationalId = "national_id";

        /// <summary>
        /// وضعیت تأیید ایمیل کاربر (Email Verified Status).
        /// </summary>
        public const string EmailVerified = "email_verified";

        /// <summary>
        /// وضعیت فعال بودن حساب کاربر (Account Active Status).
        /// </summary>
        public const string IsActive = "is_active";

        /// <summary>
        /// نقش دسترسی به سیستم (Access Role) برای تعیین سطح دسترسی.
        /// </summary>
        public const string AccessRole = "access_role";

        /// <summary>
        /// آدرس IP آخرین ورود کاربر (Last Login IP).
        /// </summary>
        public const string LastLoginIp = "last_login_ip";

        /// <summary>
        /// زمان آخرین ورود کاربر (Last Login Time).
        /// </summary>
        public const string LastLoginTime = "last_login_time";

        /// <summary>
        /// شناسه سازمان یا تیم کاربر (Organization or Team ID).
        /// </summary>
        public const string OrganizationId = "organization_id";

        /// <summary>
        /// شناسه پروژه‌ای که کاربر در آن فعالیت می‌کند (Project ID).
        /// </summary>
        public const string ProjectId = "project_id";

        /// <summary>
        /// فیلد برای ذخیره کلید API (API Key) در احراز هویت.
        /// </summary>
        public const string ApiKey = "api_key";

        /// <summary>
        /// فیلد برای ذخیره توکن‌های دسترسی (Access Tokens).
        /// </summary>
        public const string AccessToken = "access_token";

        /// <summary>
        /// فیلد برای ذخیره توکن‌های بازنشستگی (Refresh Tokens).
        /// </summary>
        public const string RefreshToken = "refresh_token";

        /// <summary>
        /// سطح دسترسی کاربر به منابع خاص سیستم (Resource Access Level).
        /// </summary>
        public const string ResourceAccessLevel = "resource_access_level";

        /// <summary>
        /// نوع احراز هویت کاربر (Authentication Type) برای سیستم.
        /// </summary>
        public const string AuthenticationType = "authentication_type";

        /// <summary>
        /// زمان انقضای توکن دسترسی (Access Token Expiration Time).
        /// </summary>
        public const string AccessTokenExpiration = "access_token_expiration";

        /// <summary>
        /// وضعیت احراز هویت کاربر (Authentication Status).
        /// </summary>
        public const string AuthenticationStatus = "authentication_status";

        /// <summary>
        /// شناسه دستگاه کاربر (Device ID).
        /// </summary>
        public const string DeviceId = "device_id";

        /// <summary>
        /// اطلاعات مربوط به وضعیت تایید حساب کاربری (Account Verification Status).
        /// </summary>
        public const string AccountVerified = "account_verified";

        /// <summary>
        /// نوع دسترسی کاربر (User Access Type).
        /// </summary>
        public const string UserAccessType = "user_access_type";

        /// <summary>
        /// آخرین تاریخ و زمان تغییر اطلاعات کاربر (Last User Data Modification Time).
        /// </summary>
        public const string LastDataModificationTime = "last_data_modification_time";

        /// <summary>
        /// شناسه زبان انتخابی کاربر (Preferred Language ID).
        /// </summary>
        public const string LanguageId = "language_id";

        /// <summary>
        /// نام کاربری در سیستم‌های خارجی (External System Username).
        /// </summary>
        public const string ExternalSystemUsername = "external_system_username";

        /// <summary>
        /// مدت زمان انقضای توکن (Token Expiration Duration).
        /// </summary>
        public const string TokenExpirationDuration = "token_expiration_duration";
    }
}
