namespace SharedKernel.Enums
{
    /// <summary>
    /// تعریف نقش‌های سیستم.
    /// </summary>
    public static class Roles
    {
        /// <summary>
        /// نقش مدیر سیستم.
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// نقش کاربر عادی.
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// نقش مدیر بخش.
        /// </summary>
        public const string Manager = "Manager";

        /// <summary>
        /// نقش مشتری.
        /// </summary>
        public const string Customer = "Customer";

        /// <summary>
        /// نقش توسعه‌دهنده.
        /// </summary>
        public const string Developer = "Developer";

        /// <summary>
        /// نقش ناظر.
        /// </summary>
        public const string Auditor = "Auditor";

        /// <summary>
        /// لیست تمام نقش‌ها.
        /// </summary>
        public static readonly string[] AllRoles = { Admin, User, Manager, Customer, Developer, Auditor };
    }
}