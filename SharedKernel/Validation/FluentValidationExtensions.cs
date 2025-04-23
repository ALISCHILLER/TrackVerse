using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Validation.Localizations;
using System.Reflection;

namespace SharedKernel.Validation
{
    /// <summary>
    /// افزونه‌های مرتبط با FluentValidation.
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// افزودن FluentValidation به سرویس‌های Dependency Injection.
        /// </summary>
        public static IServiceCollection AddCustomFluentValidation(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (assemblies == null || assemblies.Length == 0)
                throw new ArgumentException("حداقل یک اسمبلی باید مشخص شود.", nameof(assemblies));

            // افزودن Validatorها از اسمبلی‌های مشخص‌شده
            services.AddValidatorsFromAssemblies(assemblies);

            // ادغام FluentValidation با ASP.NET Core MVC
            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true; // غیرفعال کردن DataAnnotations
            });

            // پشتیبانی از اعتبارسنجی کلاینت‌ها
            services.AddFluentValidationClientsideAdapters();

            // اضافه کردن Interceptor برای پشتیبانی از Localization
            services.AddSingleton<IValidatorInterceptor, CustomValidatorInterceptor>();

            return services;
        }

        /// <summary>
        /// اعتبارسنجی دستی یک شیء با استفاده از FluentValidation.
        /// </summary>
        public static void Validate<T>(this IValidator<T> validator, T instance)
        {
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            var result = validator.Validate(instance);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                throw new ValidationException(errors);
            }
        }
    }
}