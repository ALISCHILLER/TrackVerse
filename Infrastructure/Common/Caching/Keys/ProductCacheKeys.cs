using System;

namespace Infrastructure.Common.Caching.Keys
{
    /// <summary>
    /// این کلاس شامل کلیدهای کش مخصوص به داده‌های محصولات می‌باشد.
    /// برای مدیریت کش اطلاعات محصولات مانند جزئیات محصول، لیست محصولات و غیره استفاده می‌شود.
    /// </summary>
    public static class ProductCacheKeys
    {
        // ------------------ Product Management ------------------

        /// <summary>
        /// کلید کش برای دریافت جزئیات یک محصول بر اساس شناسه محصول.
        /// </summary>
        private const string ProductByIdKey = "Cache:Product:ById:{0}";

        /// <summary>
        /// کلید کش برای دریافت جزئیات یک محصول بر اساس نام محصول.
        /// </summary>
        private const string ProductByNameKey = "Cache:Product:ByName:{0}";

        /// <summary>
        /// کلید کش برای لیست تمام محصولات.
        /// </summary>
        public const string ProductList = "Cache:Product:List";

        /// <summary>
        /// کلید کش برای لیست محصولات بر اساس دسته‌بندی.
        /// </summary>
        private const string ProductByCategoryKey = "Cache:Product:ByCategory:{0}";

        /// <summary>
        /// کلید کش برای محصولات پر فروش.
        /// </summary>
        public const string TopSellingProducts = "Cache:Product:TopSelling";

        /// <summary>
        /// کلید کش برای محصولات جدید.
        /// </summary>
        public const string NewArrivals = "Cache:Product:NewArrivals";

        /// <summary>
        /// کلید کش برای لیست محصولات در جستجو.
        /// </summary>
        public const string ProductSearchResults = "Cache:Product:SearchResults:{0}";

        // ------------------ Dynamic Cache Keys ------------------

        /// <summary>
        /// تولید کلید کش برای دریافت جزئیات یک محصول بر اساس شناسه محصول.
        /// </summary>
        /// <param name="productId">شناسه محصول</param>
        public static string GetProductByIdKey(Guid productId)
        {
            return string.Format(ProductByIdKey, productId);
        }

        /// <summary>
        /// تولید کلید کش برای دریافت جزئیات یک محصول بر اساس نام محصول.
        /// </summary>
        /// <param name="productName">نام محصول</param>
        public static string GetProductByNameKey(string productName)
        {
            return string.Format(ProductByNameKey, productName.ToLower());
        }

        /// <summary>
        /// تولید کلید کش برای لیست محصولات بر اساس دسته‌بندی.
        /// </summary>
        /// <param name="category">نام دسته‌بندی</param>
        public static string GetProductByCategoryKey(string category)
        {
            return string.Format(ProductByCategoryKey, category.ToLower());
        }

        /// <summary>
        /// تولید کلید کش برای نتایج جستجوی محصولات.
        /// </summary>
        /// <param name="searchQuery">عبارت جستجو</param>
        public static string GetProductSearchResultsKey(string searchQuery)
        {
            return string.Format(ProductSearchResults, searchQuery.ToLower());
        }

        // ------------------ Common Areas ------------------

        /// <summary>
        /// تولید کلید کش برای لیست تمام محصولات.
        /// </summary>
        public static string GetProductListKey()
        {
            return ProductList;
        }

        /// <summary>
        /// تولید کلید کش برای محصولات پر فروش.
        /// </summary>
        public static string GetTopSellingProductsKey()
        {
            return TopSellingProducts;
        }

        /// <summary>
        /// تولید کلید کش برای محصولات جدید.
        /// </summary>
        public static string GetNewArrivalsKey()
        {
            return NewArrivals;
        }
    }
}
