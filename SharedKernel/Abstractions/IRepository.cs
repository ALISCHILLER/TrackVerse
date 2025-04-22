using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedKernel.Abstractions
{
    /// <summary>
    /// این Interface پایه‌ای برای Repositoryها است که عملیات CRUD را تعریف می‌کند.
    /// این Interface مستقل از نوع دیتابیس یا ORM است و می‌تواند در هر لایه‌ای از برنامه استفاده شود.
    /// </summary>
    /// <typeparam name="T">نوع موجودیت (Entity) که باید مدیریت شود.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// این متد برای دریافت یک موجودیت بر اساس شناسه (ID) استفاده می‌شود.
        /// </summary>
        /// <param name="id">شناسه (ID) موجودیت مورد نظر.</param>
        /// <returns>موجودیت مورد نظر یا null اگر موجودیت پیدا نشود.</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// این متد برای دریافت تمام موجودیت‌های موجود استفاده می‌شود.
        /// </summary>
        /// <returns>لیستی از تمام موجودیت‌ها.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// این متد برای اضافه کردن یک موجودیت جدید به دیتابیس استفاده می‌شود.
        /// </summary>
        /// <param name="entity">موجودیتی که باید اضافه شود.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// این متد برای به‌روزرسانی یک موجودیت موجود استفاده می‌شود.
        /// </summary>
        /// <param name="entity">موجودیتی که باید به‌روزرسانی شود.</param>
        void Update(T entity);

        /// <summary>
        /// این متد برای حذف یک موجودیت از دیتابیس استفاده می‌شود.
        /// </summary>
        /// <param name="entity">موجودیتی که باید حذف شود.</param>
        void Delete(T entity);

        // متدهای اضافی برای عملیات پیچیده‌تر
        Task<IEnumerable<T>> GetByFilterAsync(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
        Task ExecuteTransactionAsync(Func<Task> operations);

        // متد برای پشتیبانی از عملیات سفارشی‌تر
        IQueryable<T> Query();

    }
}