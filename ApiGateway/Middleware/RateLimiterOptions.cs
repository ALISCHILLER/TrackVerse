namespace ApiGateway.Middleware
{
    public class RateLimiterOptions
    {
        /// <summary>
        /// حداکثر تعداد درخواست مجاز در دقیقه (پیش‌فرض)
        /// </summary>
        public int DefaultMaxRequestsPerMinute { get; set; } = 100;

        /// <summary>
        /// بازه زمانی برای محدودیت (به دقیقه)
        /// </summary>
        public int TimeWindowInMinutes { get; set; } = 1;

        /// <summary>
        /// لیست مسیرهای استثناء (Whitelist)
        /// </summary>
        public List<string> WhitelistedPaths { get; set; } = new();

        /// <summary>
        /// محدودیت‌های بر اساس نقش کاربر
        /// </summary>
        public Dictionary<string, int> RoleBasedLimits { get; set; } = new();
    }
}
