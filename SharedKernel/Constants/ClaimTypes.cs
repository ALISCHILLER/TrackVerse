using System.Security.Claims;

namespace SharedKernel.Constants
{
    /// <summary>
    /// تعریف نوع‌های ادعایی (Claims) مورد استفاده در سیستم.
    /// </summary>
    public static class ClaimTypes
    {
        /// <summary>
        /// ادعای شناسه کاربر.
        /// </summary>
        public const string UserId = "UserId";

        /// <summary>
        /// ادعای نام کاربری.
        /// </summary>
        public const string UserName = "UserName";

        /// <summary>
        /// ادعای ایمیل کاربر.
        /// </summary>
        public const string Email = "Email";

        /// <summary>
        /// ادعای نقش کاربر.
        /// </summary>
        public const string Role = "Role";

        /// <summary>
        /// ادعای شماره تلفن کاربر.
        /// </summary>
        public const string PhoneNumber = "PhoneNumber";

        /// <summary>
        /// ادعای وضعیت کاربر (فعال/غیرفعال).
        /// </summary>
        public const string UserStatus = "UserStatus";

        /// <summary>
        /// ادعای دسترسی‌های خاص کاربر.
        /// </summary>
        public const string Permissions = "Permissions";

        /// <summary>
        /// ادعای زمان آخرین ورود کاربر.
        /// </summary>
        public const string LastLoginDate = "LastLoginDate";

        /// <summary>
        /// ادعای شناسه جلسه کاربر.
        /// </summary>
        public const string SessionId = "SessionId";

        /// <summary>
        /// ادعای IP کاربر.
        /// </summary>
        public const string IpAddress = "IpAddress";

        /// <summary>
        /// ادعای شناسه سازمانی کاربر.
        /// </summary>
        public const string OrganizationId = "OrganizationId";

        /// <summary>
        /// ادعای شناسه دستگاه.
        /// </summary>
        public const string DeviceId = "DeviceId";
    }
}