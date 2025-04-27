using System;

namespace Infrastructure.Common.Caching.Keys
{
    /// <summary>
    /// این کلاس شامل کلیدهای کش مخصوص به داده‌های کاربران می‌باشد. 
    /// برای مدیریت کش اطلاعات کاربران مانند اطلاعات فردی، لیست کاربران و غیره استفاده می‌شود.
    /// </summary>
    public static class UserCacheKeys
    {
        // ------------------ User Management ------------------

        /// <summary>
        /// کلید کش برای دریافت اطلاعات یک کاربر بر اساس شناسه کاربری.
        /// </summary>
        private const string UserByIdKey = "Cache:User:ById:{0}";

        /// <summary>
        /// کلید کش برای دریافت اطلاعات یک کاربر بر اساس ایمیل.
        /// </summary>
        private const string UserByEmailKey = "Cache:User:ByEmail:{0}";

        /// <summary>
        /// کلید کش برای لیست تمام کاربران.
        /// </summary>
        public const string UserList = "Cache:User:List";

        /// <summary>
        /// کلید کش برای اطلاعات کاربر در حال حاضر وارد شده (جهت کش کردن داده‌های کاربر فعال).
        /// </summary>
        public const string CurrentUser = "Cache:User:Current";

        /// <summary>
        /// کلید کش برای دریافت وضعیت فعال یا غیرفعال بودن یک کاربر.
        /// </summary>
        private const string UserStatusKey = "Cache:User:Status:{0}";

        /// <summary>
        /// کلید کش برای تاریخ آخرین ورود یک کاربر.
        /// </summary>
        private const string UserLastLoginKey = "Cache:User:LastLogin:{0}";

        // ------------------ Dynamic Cache Keys ------------------

        /// <summary>
        /// تولید کلید کش برای دریافت اطلاعات یک کاربر بر اساس شناسه کاربری.
        /// </summary>
        /// <param name="userId">شناسه کاربر</param>
        public static string GetUserByIdKey(Guid userId)
        {
            return string.Format(UserByIdKey, userId);
        }

        /// <summary>
        /// تولید کلید کش برای دریافت اطلاعات یک کاربر بر اساس ایمیل.
        /// </summary>
        /// <param name="email">ایمیل کاربر</param>
        public static string GetUserByEmailKey(string email)
        {
            return string.Format(UserByEmailKey, email.ToLower());
        }

        /// <summary>
        /// تولید کلید کش برای وضعیت فعال یا غیرفعال بودن یک کاربر بر اساس شناسه کاربری.
        /// </summary>
        /// <param name="userId">شناسه کاربر</param>
        public static string GetUserStatusKey(Guid userId)
        {
            return string.Format(UserStatusKey, userId);
        }

        /// <summary>
        /// تولید کلید کش برای تاریخ آخرین ورود یک کاربر.
        /// </summary>
        /// <param name="userId">شناسه کاربر</param>
        public static string GetUserLastLoginKey(Guid userId)
        {
            return string.Format(UserLastLoginKey, userId);
        }

        // ------------------ Common Areas ------------------

        /// <summary>
        /// تولید کلید کش برای لیست تمام کاربران.
        /// </summary>
        public static string GetUserListKey()
        {
            return UserList;
        }

        /// <summary>
        /// تولید کلید کش برای اطلاعات کاربر وارد شده.
        /// </summary>
        public static string GetCurrentUserKey()
        {
            return CurrentUser;
        }
    }
}
