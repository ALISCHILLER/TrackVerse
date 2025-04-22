using ApiGateway.Middleware;

namespace ApiGateway.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiGatewayServices(this IServiceCollection services, IConfiguration config)
        {
            // ثبت تنظیمات مربوط به محدودیت نرخ
            services.Configure<RateLimiterOptions>(config.GetSection("RateLimiter"));

            // ثبت میان‌افزار محدودیت نرخ
            services.AddSingleton<RateLimitingMiddleware>();

            return services;
        }
    }
}
