using System;

namespace SharedKernel.Common.ValueObjects
{
    /// <summary>
    /// Value Object برای نمایش مختصات جغرافیایی.
    /// </summary>
    public sealed class GeoCoordinate : ValueObject
    {
        /// <summary>
        /// عرض جغرافیایی.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// طول جغرافیایی.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// سازنده برای اعتبارسنجی و مقداردهی اولیه.
        /// </summary>
        /// <param name="latitude">عرض جغرافیایی.</param>
        /// <param name="longitude">طول جغرافیایی.</param>
        public GeoCoordinate(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
                throw new ArgumentException("Invalid geographic coordinates");

            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// پیاده‌سازی GetEqualityComponents برای مقایسه.
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }

        /// <summary>
        /// نمایش رشته‌ای مختصات جغرافیایی.
        /// </summary>
        public override string ToString() => $"Latitude: {Latitude}, Longitude: {Longitude}";
    }
}