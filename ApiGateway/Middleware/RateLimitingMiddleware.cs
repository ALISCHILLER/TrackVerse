using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Redis;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Middleware
{
   

    /// <summary>
    /// میان‌افزار محدودیت تعداد درخواست‌ها (Rate Limiting)
    /// </summary>
    public class RateLimitingMiddleware
    {
        /// <summary>
        /// نگهدارنده درخواست بعدی در خط لول Middleware
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// اتصال به Redis برای ذخیره‌سازی تعداد درخواست‌ها
        /// </summary>
        private readonly IDatabase _redisCache;

        /// <summary>
        /// تنظیمات مربوط به محدودیت نرخ
        /// </summary>
        private readonly RateLimiterOptions _options;

        /// <summary>
        /// لاگ‌گیری برای ثبت اطلاعات درخواست‌ها
        /// </summary>
        private readonly ILogger<RateLimitingMiddleware> _logger;

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public RateLimitingMiddleware(
            RequestDelegate next,
            IConnectionMultiplexer redisConnection,
            IOptions<RateLimiterOptions> options,
            ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _redisCache = redisConnection.GetDatabase();
            _options = options.Value;
            _logger = logger;
        }

        /// <summary>
        /// اجرای میان‌افزار
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // بررسی مسیرهای استثناء (Whitelist)
                if (_options.WhitelistedPaths.Any(path => context.Request.Path.StartsWithSegments(path, StringComparison.OrdinalIgnoreCase)))
                {
                    await _next(context);
                    return;
                }

                // گرفتن آدرس IP و شناسه کاربری
                var ipAddress = GetClientIpAddress(context);
                var userId = context.User?.FindFirst("UserId")?.Value;

                // سفارشی‌سازی کلید Redis
                var cacheKey = GenerateCacheKey(context, ipAddress, userId);

                // بررسی تعداد درخواست‌ها در Redis
                var requestCount = await _redisCache.StringGetAsync(cacheKey);
                var currentCount = requestCount.HasValue ? int.Parse(requestCount.ToString()) : 0;

                // افزودن هدرهای استاندارد Rate Limit
                AddRateLimitHeaders(context, currentCount);

                if (currentCount >= GetMaxRequestsForRequest(context))
                {
                    // اگر تعداد درخواست‌ها بیشتر از حد مجاز باشد، خطای 429 Too Many Requests ارسال شود
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.Response.WriteAsync("Too many requests. Please try again later.");
                    return;
                }

                // افزایش تعداد درخواست‌ها و ذخیره در Redis با TTL
                await _redisCache.StringSetAsync(cacheKey, currentCount + 1, TimeSpan.FromMinutes(_options.TimeWindowInMinutes));

                // لاگ کردن درخواست
                LogRequest(ipAddress, userId);
            }
            catch (Exception ex)
            {
                // مدیریت خطا در صورت بروز مشکل
                _logger.LogError(ex, "خطا در اجرای میان‌افزار RateLimitingMiddleware");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal Server Error");
                return;
            }

            // ادامه دادن به خط لول Middleware
            await _next(context);
        }

        /// <summary>
        /// گرفتن آدرس IP کاربر
        /// </summary>
        private string GetClientIpAddress(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress) && context.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = context.Request.Headers["X-Forwarded-For"].ToString().Split(',')[0].Trim();
            }
            return ipAddress;
        }

        /// <summary>
        /// سفارشی‌سازی کلید Redis برای ذخیره‌سازی تعداد درخواست‌ها
        /// </summary>
        private string GenerateCacheKey(HttpContext context, string ipAddress, string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return $"rate_limit:user:{userId}";
            }
            else if (!string.IsNullOrEmpty(ipAddress))
            {
                return $"rate_limit:ip:{ipAddress}";
            }
            return $"rate_limit:unknown";
        }

        /// <summary>
        /// افزودن هدرهای استاندارد Rate Limit
        /// </summary>
        private void AddRateLimitHeaders(HttpContext context, int currentCount)
        {
            var maxRequests = GetMaxRequestsForRequest(context);
            var remainingRequests = Math.Max(0, maxRequests - currentCount);
            var resetTime = DateTime.UtcNow.AddMinutes(_options.TimeWindowInMinutes).ToString("R");

            context.Response.Headers.Add("RateLimit-Limit", maxRequests.ToString());
            context.Response.Headers.Add("RateLimit-Remaining", remainingRequests.ToString());
            context.Response.Headers.Add("RateLimit-Reset", resetTime);
        }

        /// <summary>
        /// تعیین حداکثر تعداد درخواست‌ها بر اساس نقش کاربر یا مسیر درخواست
        /// </summary>
        private int GetMaxRequestsForRequest(HttpContext context)
        {
            var userRole = context.User?.Claims?.FirstOrDefault(c => c.Type == "Role")?.Value;

            if (_options.RoleBasedLimits.ContainsKey(userRole))
            {
                return _options.RoleBasedLimits[userRole];
            }

            return _options.DefaultMaxRequestsPerMinute;
        }

        /// <summary>
        /// لاگ کردن درخواست‌ها
        /// </summary>
        private void LogRequest(string ipAddress, string userId)
        {
            _logger.LogInformation($"درخواست جدید - IP: {ipAddress}, UserId: {userId ?? "Guest"}, زمان: {DateTime.UtcNow}");
        }
    }
}
