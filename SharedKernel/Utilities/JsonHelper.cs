using System.Text.Json;

namespace SharedKernel.Utilities
{
    /// <summary>
    /// ابزارهای کمکی برای کار با JSON.
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// تبدیل یک شیء به رشته JSON.
        /// </summary>
        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        /// <summary>
        /// تبدیل یک رشته JSON به شیء.
        /// </summary>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}