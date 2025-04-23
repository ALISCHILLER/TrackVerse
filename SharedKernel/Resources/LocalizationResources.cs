using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace SharedKernel.Resources
{
    /// <summary>
    /// کلاس مدیریت منابع Localization برای پشتیبانی از چندزبانه‌سازی.
    /// </summary>
    public class LocalizationResources
    {
        private readonly IStringLocalizer _localizer;

        /// <summary>
        /// سازنده کلاس LocalizationResources.
        /// </summary>
        /// <param name="localizerFactory">کارخانه‌ی Localizer برای ساخت منابع محلی</param>
        public LocalizationResources(IStringLocalizerFactory localizerFactory)
        {
            // ایجاد Localizer برای فایل‌های منابع (resx)
            _localizer = localizerFactory.Create("ValidationMessages", typeof(LocalizationResources).Assembly.GetName().Name);
        }

        /// <summary>
        /// دریافت رشته محلی‌سازی‌شده با کلید مشخص‌شده.
        /// اگر رشته پیدا نشود، خود کلید بازگردانده می‌شود.
        /// </summary>
        /// <param name="key">کلید رشته</param>
        /// <returns>رشته محلی‌سازی‌شده یا کلید</returns>
        public string GetLocalizedString(string key)
        {
            var localized = _localizer[key];
            return localized.ResourceNotFound ? key : localized.Value;
        }

        /// <summary>
        /// دریافت رشته محلی‌سازی‌شده با کلید و پارامترهای جایگزینی.
        /// </summary>
        /// <param name="key">کلید پیام</param>
        /// <param name="args">پارامترها</param>
        /// <returns>پیام قالب‌بندی‌شده</returns>
        public string GetLocalizedString(string key, params object[] args)
        {
            var localized = _localizer[key];
            var value = localized.ResourceNotFound ? key : localized.Value;
            return string.Format(CultureInfo.CurrentCulture, value, args);
        }

        /// <summary>
        /// دریافت لیست کامل رشته‌های محلی‌سازی‌شده برای زبان فعلی.
        /// </summary>
        /// <returns>دیکشنری از کلیدها و مقادیر</returns>
        public Dictionary<string, string> GetAllLocalizedStrings()
        {
            var all = new Dictionary<string, string>();
            foreach (var item in _localizer.GetAllStrings(includeParentCultures: true))
            {
                all[item.Name] = item.Value;
            }
            return all;
        }
    }
}