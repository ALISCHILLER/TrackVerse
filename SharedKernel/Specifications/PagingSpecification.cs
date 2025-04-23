using System;
using System.Linq;
using System.Linq.Expressions;

namespace SharedKernel.Specifications
{
    /// <summary>
    /// Specification برای صفحه‌بندی داده‌ها.
    /// </summary>
    public class PagingSpecification<T> : Specification<T>
    {
        private readonly int _pageNumber;
        private readonly int _pageSize;

        /// <summary>
        /// سازنده برای ایجاد Specification صفحه‌بندی.
        /// </summary>
        /// <param name="pageNumber">شماره صفحه (شروع از 1)</param>
        /// <param name="pageSize">تعداد آیتم‌ها در هر صفحه</param>
        public PagingSpecification(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "شماره صفحه باید بزرگ‌تر از 0 باشد.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "تعداد آیتم‌ها در هر صفحه باید بزرگ‌تر از 0 باشد.");

            _pageNumber = pageNumber;
            _pageSize = pageSize;
        }

        /// <summary>
        /// تبدیل Specification به یک عبارت LINQ.
        /// </summary>
        public override Expression<Func<T, bool>> ToExpression()
        {
            throw new NotSupportedException("Specification صفحه‌بندی نمی‌تواند به عبارت LINQ تبدیل شود.");
        }

        /// <summary>
        /// اعمال صفحه‌بندی بر روی یک Queryable.
        /// </summary>
        public IQueryable<T> ApplyPaging(IQueryable<T> query)
        {
            return query.Skip((_pageNumber - 1) * _pageSize).Take(_pageSize);
        }
    }
}