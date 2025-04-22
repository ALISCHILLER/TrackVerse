using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedKernel.Common.ValueObjects
{
    /// <summary>
    /// کلاس پایه برای تمام Value Objects.
    /// </summary>
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        /// <summary>
        /// مقایسه دو Value Object بر اساس مقادیر آنها.
        /// </summary>
        public bool Equals(ValueObject other)
        {
            if (other is null)
                return false;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// مقایسه دو Value Object بر اساس مقادیر آنها.
        /// </summary>
        public override bool Equals(object obj)
            => obj is ValueObject other && Equals(other);

        /// <summary>
        /// محاسبه HashCode بر اساس مقادیر Value Object.
        /// </summary>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// عملگر تساوی برای مقایسه دو Value Object.
        /// </summary>
        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// عملگر نامساوی برای مقایسه دو Value Object.
        /// </summary>
        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        /// <summary>
        /// این متد باید در کلاس‌های فرزند پیاده‌سازی شود.
        /// </summary>
        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}