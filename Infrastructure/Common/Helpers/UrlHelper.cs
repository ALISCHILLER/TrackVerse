using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Infrastructure.Common.Helpers
{
    /// <summary>
    /// کلاس UrlHelper برای ساخت و ویرایش URL ها.
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// اضافه کردن پارامترهای کوئری به یک URL موجود.
        /// </summary>
        /// <param name="url">URL اصلی.</param>
        /// <param name="queryParams">پارامترهای کوئری که باید اضافه شوند.</param>
        /// <returns>URL جدید با پارامترهای کوئری اضافه شده.</returns>
        public static string AddQueryParams(string url, NameValueCollection queryParams)
        {
            if (string.IsNullOrEmpty(url) || queryParams == null || queryParams.Count == 0)
            {
                return url;
            }

            UriBuilder uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (string key in queryParams.AllKeys)
            {
                query[key] = queryParams[key];
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        /// <summary>
        /// استخراج بخش مسیر (Path) از یک URL.
        /// </summary>
        /// <param name="url">URL که می‌خواهید بخش مسیر آن را استخراج کنید.</param>
        /// <returns>بخش مسیر URL.</returns>
        public static string GetPath(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            Uri uri = new Uri(url);
            return uri.AbsolutePath;
        }

        /// <summary>
        /// استخراج بخش دامنه (Host) از یک URL.
        /// </summary>
        /// <param name="url">URL که می‌خواهید بخش دامنه آن را استخراج کنید.</param>
        /// <returns>بخش دامنه URL.</returns>
        public static string GetHost(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            Uri uri = new Uri(url);
            return uri.Host;
        }

        /// <summary>
        /// استخراج بخش پروتکل (Scheme) از یک URL (مانند http، https).
        /// </summary>
        /// <param name="url">URL که می‌خواهید بخش پروتکل آن را استخراج کنید.</param>
        /// <returns>بخش پروتکل URL.</returns>
        public static string GetScheme(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            Uri uri = new Uri(url);
            return uri.Scheme;
        }

        /// <summary>
        /// استخراج پارامترهای کوئری از یک URL.
        /// </summary>
        /// <param name="url">URL که می‌خواهید پارامترهای کوئری آن را استخراج کنید.</param>
        /// <returns>پارامترهای کوئری URL به صورت یک NameValueCollection.</returns>
        public static NameValueCollection GetQueryParams(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            Uri uri = new Uri(url);
            return HttpUtility.ParseQueryString(uri.Query);
        }

        /// <summary>
        /// ترکیب بخش‌های مختلف URL به یک URL کامل.
        /// </summary>
        /// <param name="scheme">پروتکل (مانند http یا https).</param>
        /// <param name="host">دامنه یا آدرس سرور.</param>
        /// <param name="path">مسیر URL.</param>
        /// <param name="queryParams">پارامترهای کوئری.</param>
        /// <returns>URL کامل ساخته شده.</returns>
        public static string CombineUrl(string scheme, string host, string path, NameValueCollection queryParams)
        {
            if (string.IsNullOrEmpty(scheme) || string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("پروتکل (scheme) و دامنه (host) باید مشخص باشند.");
            }

            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = scheme,
                Host = host,
                Path = path ?? string.Empty
            };

            if (queryParams != null && queryParams.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (string key in queryParams.AllKeys)
                {
                    query[key] = queryParams[key];
                }

                uriBuilder.Query = query.ToString();
            }

            return uriBuilder.ToString();
        }

        /// <summary>
        /// بررسی اینکه آیا یک URL معتبر است یا خیر.
        /// </summary>
        /// <param name="url">URL که می‌خواهید اعتبار آن را بررسی کنید.</param>
        /// <returns>برگشت true اگر URL معتبر باشد، در غیر اینصورت false.</returns>
        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
        }

        /// <summary>
        /// حذف پارامتر کوئری از URL.
        /// </summary>
        /// <param name="url">URL که می‌خواهید پارامتر آن را حذف کنید.</param>
        /// <param name="paramName">نام پارامتر کوئری که باید حذف شود.</param>
        /// <returns>URL جدید با پارامتر حذف شده.</returns>
        public static string RemoveQueryParam(string url, string paramName)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(paramName))
            {
                return url;
            }

            UriBuilder uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query.Remove(paramName);

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
    }
}
