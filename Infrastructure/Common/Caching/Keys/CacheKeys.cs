using System;

namespace Infrastructure.Common.Caching.Keys
{
    /// <summary>
    /// این کلاس شامل کلیدهای ثابت کش و توابع تولید کلید به صورت داینامیک برای حوزه‌های مختلف سیستم می‌باشد.
    /// </summary>
    public static class CacheKeys
    {
        // ------------------ User Management ------------------

        /// <summary>
        /// کلید کش کاربر بر اساس شناسه کاربری
        /// </summary>
        private const string UserByIdKey = "Cache:User:ById:{0}";

        /// <summary>
        /// کلید کش کاربر بر اساس ایمیل
        /// </summary>
        private const string UserByEmailKey = "Cache:User:ByEmail:{0}";

        /// <summary>
        /// کلید کش لیست تمام کاربران
        /// </summary>
        public const string UserList = "Cache:User:List";

        /// <summary>
        /// تولید کلید کش برای دریافت کاربر بر اساس شناسه
        /// </summary>
        /// <param name="userId">شناسه کاربر</param>
        public static string GetUserByIdKey(Guid userId)
        {
            return string.Format(UserByIdKey, userId);
        }

        /// <summary>
        /// تولید کلید کش برای دریافت کاربر بر اساس ایمیل
        /// </summary>
        /// <param name="email">ایمیل کاربر</param>
        public static string GetUserByEmailKey(string email)
        {
            return string.Format(UserByEmailKey, email.ToLower());
        }

        // ------------------ Product Management ------------------

        /// <summary>
        /// کلید کش محصول بر اساس شناسه محصول
        /// </summary>
        private const string ProductByIdKey = "Cache:Product:ById:{0}";

        /// <summary>
        /// کلید کش لیست محصولات
        /// </summary>
        public const string ProductList = "Cache:Product:List";

        /// <summary>
        /// کلید کش محصولات یک دسته‌بندی خاص
        /// </summary>
        private const string ProductCategoryKey = "Cache:Product:Category:{0}";

        /// <summary>
        /// تولید کلید کش برای دریافت محصول بر اساس شناسه
        /// </summary>
        /// <param name="productId">شناسه محصول</param>
        public static string GetProductByIdKey(Guid productId)
        {
            return string.Format(ProductByIdKey, productId);
        }

        /// <summary>
        /// تولید کلید کش برای محصولات یک دسته‌بندی خاص
        /// </summary>
        /// <param name="categoryId">شناسه دسته‌بندی</param>
        public static string GetProductCategoryKey(Guid categoryId)
        {
            return string.Format(ProductCategoryKey, categoryId);
        }

        // ------------------ Settings ------------------

        /// <summary>
        /// کلید کش تنظیمات سیستمی
        /// </summary>
        public const string SystemSettings = "Cache:Settings:System";

        /// <summary>
        /// کلید کش فعال یا غیرفعال بودن امکانات (Feature Toggles)
        /// </summary>
        public const string FeatureToggles = "Cache:Settings:FeatureToggles";

        // ------------------ Common Areas ------------------

        /// <summary>
        /// کلید کش مقادیر دیکشنری (Lookup Values) بر اساس نوع
        /// </summary>
        private const string LookupValuesKey = "Cache:Common:LookupValues:{0}";

        /// <summary>
        /// کلید کش زبان‌بندی و لوکالایزیشن بر اساس زبان
        /// </summary>
        private const string LocalizationKey = "Cache:Common:Localization:{0}";

        /// <summary>
        /// تولید کلید کش برای مقادیر دیکشنری خاص
        /// </summary>
        /// <param name="lookupType">نوع دیکشنری</param>
        public static string GetLookupValuesKey(string lookupType)
        {
            return string.Format(LookupValuesKey, lookupType.ToLower());
        }

        /// <summary>
        /// تولید کلید کش برای زبان‌بندی خاص
        /// </summary>
        /// <param name="languageCode">کد زبان (مثل en, fa, ar)</param>
        public static string GetLocalizationKey(string languageCode)
        {
            return string.Format(LocalizationKey, languageCode.ToLower());
        }
    }
}
