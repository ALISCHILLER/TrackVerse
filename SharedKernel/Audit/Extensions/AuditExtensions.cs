using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Audit;
using SharedKernel.Audit.Repositories;

namespace SharedKernel.Audit.Extensions
{
    /// <summary>
    /// کلاس Extension برای تنظیمات لاگ‌گیری.
    /// </summary>
    /// <remarks>
    /// این کلاس شامل متدهای Extension برای ثبت سرویس‌ها و تنظیمات مربوط به لاگ‌گیری در کانتینر Dependency Injection است.
    /// از این کلاس برای جداسازی منطق ثبت وابستگی‌ها از فایل‌های اصلی برنامه (مانند `Program.cs`) استفاده می‌شود.
    /// این رویکرد باعث می‌شود کد تمیز، مرتب و قابل نگهداری باشد.
    /// </remarks>
    public static class AuditExtensions
    {
        /// <summary>
        /// افزودن سرویس‌های پایه لاگ‌گیری به کانتینر Dependency Injection.
        /// </summary>
        /// <param name="services">کانتینر Dependency Injection.</param>
        /// <returns>IServiceCollection پس از ثبت سرویس‌ها.</returns>
        public static IServiceCollection AddAuditServices(this IServiceCollection services)
        {
            // ثبت Repository مرتبط با لاگ‌گیری
            // این Repository مسئول ذخیره و بازیابی لاگ‌ها در دیتابیس است.
            services.AddScoped<IAuditRepository, AuditRepository>();

            // ثبت سرویس لاگ‌گیری
            // این سرویس مسئول مدیریت و ثبت تغییرات در دیتابیس است.
            services.AddScoped<AuditService>();

            // ثبت سرویس‌های جانبی (در صورت نیاز)
            // مثال: services.AddSingleton<ISomeHelperService, SomeHelperService>();

            return services;
        }

        /// <summary>
        /// افزودن سرویس‌های لاگ‌گیری همراه با تنظیمات اختیاری به کانتینر Dependency Injection.
        /// </summary>
        /// <param name="services">کانتینر Dependency Injection.</param>
        /// <param name="configureOptions">
        /// یک Action برای تنظیم گزینه‌های لاگ‌گیری.
        /// این پارامتر امکان شخصی‌سازی تنظیمات لاگ‌گیری را فراهم می‌کند.
        /// </param>
        /// <returns>IServiceCollection پس از ثبت سرویس‌ها و تنظیمات.</returns>
        public static IServiceCollection AddAuditServices(
            this IServiceCollection services,
            Action<AuditOptions> configureOptions)
        {
            // افزودن سرویس‌های پایه لاگ‌گیری
            services.AddAuditServices();

            // ثبت تنظیمات اختیاری لاگ‌گیری
            // این تنظیمات شامل گزینه‌هایی مانند فعال‌سازی لاگ‌گیری مقادیر حساس و لیست پراپرتی‌های نادیده‌گرفته‌شده است.
            services.Configure(configureOptions);

            return services;
        }
    }

    /// <summary>
    /// کلاس تنظیمات لاگ‌گیری.
    /// </summary>
    /// <remarks>
    /// این کلاس شامل گزینه‌هایی است که می‌توانند برای شخصی‌سازی لاگ‌گیری استفاده شوند.
    /// </remarks>
    public class AuditOptions
    {
        /// <summary>
        /// آیا باید لاگ‌گیری برای مقادیر حساس فعال باشد؟
        /// </summary>
        /// <remarks>
        /// اگر این گزینه فعال باشد، مقادیر حساس (مانند رمز عبور، توکن‌ها و غیره) در لاگ‌ها ذخیره می‌شوند.
        /// پیش‌فرض: false (غیرفعال).
        /// </remarks>
        public bool EnableSensitiveDataLogging { get; set; } = false;

        /// <summary>
        /// حداکثر تعداد لاگ‌های ذخیره‌شده در حافظه (اختیاری).
        /// </summary>
        /// <remarks>
        /// این گزینه تعیین می‌کند که حداکثر چه تعداد لاگ در حافظه نگهداری شود.
        /// پیش‌فرض: 1000 لاگ.
        /// </remarks>
        public int MaxLogEntriesInMemory { get; set; } = 1000;

        /// <summary>
        /// لیست پراپرتی‌هایی که همیشه باید نادیده گرفته شوند (اختیاری).
        /// </summary>
        /// <remarks>
        /// این گزینه امکان نادیده‌گرفتن پراپرتی‌های مشخص‌شده در لاگ‌گیری را فراهم می‌کند.
        /// مثال: ["Password", "Token"].
        /// پیش‌فرض: لیست خالی.
        /// </remarks>
        public List<string> IgnoredProperties { get; set; } = new List<string>();
    }
}