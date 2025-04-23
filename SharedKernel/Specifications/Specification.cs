using System;
using System.Linq.Expressions;

namespace SharedKernel.Specifications
{
    /// <summary>
    /// کلاس پایه برای الگوی Specification.
    /// این کلاس برای تعریف قوانین و فیلترهای پیچیده استفاده می‌شود.
    /// </summary>
    public abstract class Specification<T>
    {
        /// <summary>
        /// تبدیل Specification به یک عبارت LINQ.
        /// </summary>
        public abstract Expression<Func<T, bool>> ToExpression();

        /// <summary>
        /// ترکیب دو Specification با عملگر AND.
        /// </summary>
        public Specification<T> And(Specification<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return new AndSpecification<T>(this, other);
        }

        /// <summary>
        /// ترکیب دو Specification با عملگر OR.
        /// </summary>
        public Specification<T> Or(Specification<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return new OrSpecification<T>(this, other);
        }
    }

    /// <summary>
    /// Specification برای ترکیب دو Specification با عملگر AND.
    /// </summary>
    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(leftExpression, parameter),
                Expression.Invoke(rightExpression, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

    /// <summary>
    /// Specification برای ترکیب دو Specification با عملگر OR.
    /// </summary>
    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.OrElse(
                Expression.Invoke(leftExpression, parameter),
                Expression.Invoke(rightExpression, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}