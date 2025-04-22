using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Audit;
using SharedKernel.Audit.Repositories;

namespace SharedKernel.Audit.Extensions
{
    /// <summary>
    /// Extension Methods برای تنظیمات لاگ‌گیری.
    /// </summary>
    /// <remarks>
    /// این کلاس شامل متدهای Extension برای ثبت سرویس‌های مربوط به لاگ‌گیری در DI Container است.
    /// از این کلاس برای جداسازی منطق ثبت وابستگی‌ها از فایل‌های اصلی برنامه (مانند `Program.cs`) استفاده می‌شود.
    /// </remarks>
    public static class AuditExtensions
    {
        /// <summary>
        /// افزودن سرویس‌های لاگ‌گیری به DI Container.
        /// </summary>
        /// <param name="services">کانتینر Dependency Injection.</param>
        /// <returns>IServiceCollection پس از ثبت سرویس‌ها.</returns>
        public static IServiceCollection AddAuditServices(this IServiceCollection services)
        {
            // ثبت مخازن (Repositories) مرتبط با لاگ‌گیری
            services.AddScoped<IAuditRepository, AuditRepository>();

            // ثبت سرویس‌های لاگ‌گیری
            services.AddScoped<AuditLogService>();

            // ثبت سرویس‌های جانبی (اگر نیاز باشد)
            // services.AddSingleton<ISomeHelperService, SomeHelperService>();

            return services;
        }

        /// <summary>
        /// افزودن تنظیمات اختیاری برای لاگ‌گیری.
        /// </summary>
        /// <param name="services">کانتینر Dependency Injection.</param>
        /// <param name="configureOptions">تابعی برای تنظیم گزینه‌های لاگ‌گیری.</param>
        /// <returns>IServiceCollection پس از ثبت سرویس‌ها و تنظیمات.</returns>
        public static IServiceCollection AddAuditServices(
            this IServiceCollection services,
            Action<AuditOptions> configureOptions)
        {
            // افزودن سرویس‌های پایه لاگ‌گیری
            services.AddAuditServices();

            // ثبت تنظیمات اختیاری
            services.Configure(configureOptions);

            return services;
        }
    }

    /// <summary>
    /// گزینه‌های تنظیمات لاگ‌گیری.
    /// </summary>
    public class AuditOptions
    {
        /// <summary>
        /// آیا باید لاگ‌گیری برای مقادیر حساس فعال باشد؟
        /// </summary>
        public bool EnableSensitiveDataLogging { get; set; } = false;

        /// <summary>
        /// حداکثر تعداد لاگ‌های ذخیره‌شده در حافظه (اختیاری).
        /// </summary>
        public int MaxLogEntriesInMemory { get; set; } = 1000;

        /// <summary>
        /// لیست پراپرتی‌هایی که همیشه باید نادیده گرفته شوند (اختیاری).
        /// </summary>
        public List<string> IgnoredProperties { get; set; } = new List<string>();
    }
}