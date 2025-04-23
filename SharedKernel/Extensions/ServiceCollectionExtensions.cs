//using System;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;
//using Microsoft.EntityFrameworkCore;
//using SharedKernel.Audit;
//using SharedKernel.Common;
//using SharedKernel.Config;
//using SharedKernel.Exceptions;
//using SharedKernel.Interfaces;
//using SharedKernel.Middleware;
//using SharedKernel.Repositories;
//using SharedKernel.Services;
//using SharedKernel.Specifications;
//using SharedKernel.Utilities;
//using SharedKernel.Validation;
//using Microsoft.Extensions.Logging;
//using SharedKernel.Abstractions;
//using SharedKernel.Constants;

//namespace SharedKernel.Extensions
//{
//    /// <summary>
//    /// کلاس کمکی برای تنظیمات و ثبت سرویس‌های SharedKernel.
//    /// </summary>
//    public static class ServiceCollectionExtensions
//    {
//        /// <summary>
//        /// افزودن تمام سرویس‌های مورد نیاز SharedKernel به DI Container.
//        /// </summary>
//        public static IServiceCollection AddSharedKernelServices(this IServiceCollection services, IConfiguration configuration)
//        {
//            // اعتبارسنجی تنظیمات
//            ValidateConfiguration(configuration);

//            // 1. ثبت تنظیمات برنامه
//            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

//            // 2. ثبت DbContext اصلی (اگر وجود داشته باشد)
//            var connectionString = configuration.GetConnectionString("DefaultConnection");
//            if (!string.IsNullOrEmpty(connectionString))
//            {
//                services.AddDbContext<ApplicationDbContext>(options =>
//                    options.UseSqlServer(connectionString)
//                           .EnableSensitiveDataLogging() // فقط در حالت توسعه فعال شود
//                           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
//            }

//            // 3. ثبت سرویس‌ها و مخازن عمومی
//            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
//            services.AddScoped<IAuditLogService, AuditLogService>();
//            services.AddScoped<INotificationService, NotificationService>();
//            services.AddScoped<ICommandService, CommandService>();

//            // 4. ثبت Validation Extensions (با FluentValidation)
//            services.AddFluentValidationExtensions();

//            // 5. ثبت Utilities
//            services.AddSingleton<IDateTimeUtility, DateTimeUtility>();

//            // 6. ثبت Specifications
//            services.AddScoped(typeof(ISpecification<>), typeof(Specification<>));

//            // 7. ثبت Middlewareها
//            services.AddTransient<ExceptionHandlingMiddleware>();

//            // 8. ثبت سرویس‌های پس‌زمینه (Background Services)
//            services.AddHostedService<MqttBackgroundService>();

//            // 9. ثبت سایر قابلیت‌ها
//            services.AddSingleton(provider =>
//                provider.GetRequiredService<IConfiguration>().GetSection(nameof(AppConstants)).Get<AppConstants>());

//            return services;
//        }

//        /// <summary>
//        /// افزودن تنظیمات لاگ‌گیری با Serilog.
//        /// </summary>
//        public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
//        {
//            var serilogSettings = configuration.GetSection(nameof(SerilogSettings)).Get<SerilogSettings>();
//            if (serilogSettings?.IsEnabled ?? false)
//            {
//                services.AddLogging(loggingBuilder =>
//                {
//                    loggingBuilder.ClearProviders();
//                    loggingBuilder.AddSerilog(new LoggerConfiguration()
//                        .ReadFrom.Configuration(configuration)
//                        .Enrich.WithProperty("Application", "SharedKernel")
//                        .CreateLogger(), dispose: true);
//                });
//            }

//            return services;
//        }

//        /// <summary>
//        /// افزودن تنظیمات Health Checks.
//        /// </summary>
//        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
//        {
//            services.AddHealthChecks()
//                .AddCheck<CustomHealthCheck>("SharedKernel_HealthCheck")
//                .AddDbContextCheck<ApplicationDbContext>("Database_HealthCheck");

//            return services;
//        }

//        /// <summary>
//        /// افزودن تنظیمات Rate Limiter.
//        /// </summary>
//        public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
//        {
//            var rateLimiterOptions = configuration.GetSection(nameof(RateLimiterOptions)).Get<RateLimiterOptions>();
//            if (rateLimiterOptions != null)
//            {
//                services.AddRateLimiter(options =>
//                {
//                    options.AddPolicy("GeneralPolicy", policy =>
//                    {
//                        policy.Bulkhead(
//                            maxConcurrentRequests: rateLimiterOptions.MaxConcurrentRequests,
//                            queueProcessingOrder: QueueProcessingOrder.NewestFirst,
//                            queueLimit: rateLimiterOptions.QueueLimit);

//                        policy.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
//                    });
//                });
//            }

//            return services;
//        }

//        /// <summary>
//        /// اعتبارسنجی تنظیمات اصلی.
//        /// </summary>
//        private static void ValidateConfiguration(IConfiguration configuration)
//        {
//            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
//            if (appSettings == null)
//                throw new ConfigurationException("AppSettings is not configured properly.");

//            if (string.IsNullOrEmpty(configuration.GetConnectionString("DefaultConnection")))
//                throw new ConfigurationException("DefaultConnection string is missing.");
//        }
//    }
//}