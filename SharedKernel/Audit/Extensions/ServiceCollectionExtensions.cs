using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Audit.Repositories;

namespace SharedKernel.Audit.Extensions
{
    /// <summary>
    /// Extension Methods برای ثبت سرویس‌های لاگ‌گیری.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// افزودن سرویس‌های لاگ‌گیری به DI Container.
        /// </summary>
        /// <param name="services">کانتینر Dependency Injection.</param>
        /// <param name="connectionString">رشته اتصال به دیتابیس.</param>
        /// <returns>IServiceCollection پس از ثبت سرویس‌ها.</returns>
        public static IServiceCollection AddAuditServices(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("رشته اتصال به دیتابیس نباید خالی باشد.");

            // ثبت DbContext
            services.AddDbContext<AuditDbContext>(options =>
                options.UseSqlServer(connectionString));

            // ثبت Repository
            services.AddScoped<IAuditRepository, AuditRepository>();

            return services;
        }
    }
}