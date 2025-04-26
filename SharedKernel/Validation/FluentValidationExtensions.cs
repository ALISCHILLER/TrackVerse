using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;

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
        /// <param name="services">سرویس‌هایی که FluentValidation باید به آن اضافه شود.</param>
        /// <param name="assemblies">اسمبلی‌هایی که شامل Validatorها هستند.</param>
        /// <returns>خدمات به‌روز شده با FluentValidation.</returns>
        public static IServiceCollection AddCustomFluentValidation(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (assemblies == null || assemblies.Length == 0)
                throw new ArgumentException("حداقل یک اسمبلی باید مشخص شود.", nameof(assemblies));

            services.AddValidatorsFromAssemblies(assemblies);

            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
            });

            services.AddFluentValidationClientsideAdapters();

            services.AddSingleton<IValidatorInterceptor, CustomValidatorInterceptor>();

            return services;
        }

        /// <summary>
        /// اعتبارسنجی دستی یک شیء با استفاده از FluentValidation.
        /// </summary>
        /// <typeparam name="T">نوع شیء برای اعتبارسنجی.</typeparam>
        /// <param name="validator">شیء اعتبارسنجی برای اجرای فرآیند.</param>
        /// <param name="instance">شیء مورد نظر برای اعتبارسنجی.</param>
        /// <param name="logger">لاگر جهت ثبت خطاها (اختیاری).</param>
        public static void Validate<T>(this IValidator<T> validator, T instance, ILogger logger = null)
        {
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            var result = validator.Validate(instance);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .Select(g => new
                    {
                        Property = g.Key,
                        Messages = g.Select(e => e.ErrorMessage).ToArray()
                    })
                    .Select(e => $"{e.Property}: {string.Join(", ", e.Messages)}")
                    .ToList();

                var fullErrorMessage = string.Join("; ", errorMessages);

                logger?.LogError($"Validation failed: {fullErrorMessage}");

                // حالا استثناء اختصاصی پرتاب کن
                throw new CustomValidationException(result.Errors);
            }
        }

    }
}



//نمونه‌ای از استفاده در کد:
//مثلا توی یک سرویس بخوای دستی ولیدیت کنی:
//csharp
//Copy
//Edit
//public async Task CreateUserAsync(UserDto userDto)
//{
//    var validator = new UserDtoValidator();
//    validator.Validate(userDto, _logger); // اگر ایرادی باشه، CustomValidationException پرتاب میشه

//    // ادامه عملیات ذخیره
//}