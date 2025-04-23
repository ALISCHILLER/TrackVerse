using System;
using SharedKernel.Common;

namespace SharedKernel.Interfaces
{
    /// <summary>
    /// اینترفیس پایه برای سرویس‌های حوزه کسب‌وکار.
    /// </summary>
    /// <typeparam name="TDto">نوع Dto (موجودیت انتقال داده).</typeparam>
    /// <typeparam name="TEntity">نوع موجودیت دیتابیس.</typeparam>
    public interface IService<TDto, TEntity> where TEntity : class
    {
        /// <summary>
        /// دریافت یک آیتم بر اساس شناسه.
        /// </summary>
        /// <param name="id">شناسه آیتم.</param>
        /// <returns>نتیجه عملیات همراه با Dto.</returns>
        Task<Result<TDto>> GetByIdAsync(Guid id);

        /// <summary>
        /// دریافت تمام آیتم‌ها با پیاده‌سازی.
        /// </summary>
        /// <param name="page">شماره صفحه.</param>
        /// <param name="pageSize">تعداد آیتم در هر صفحه.</param>
        /// <returns>نتیجه عملیات همراه با لیست Dtoهای پیاده‌سازی‌شده.</returns>
        Task<PaginatedResult<TDto>> GetAllAsync(int page = 1, int pageSize = 10);

        /// <summary>
        /// ایجاد آیتم جدید.
        /// </summary>
        /// <param name="dto">Dto حاوی اطلاعات آیتم جدید.</param>
        /// <returns>نتیجه عملیات همراه با Dtoی ایجادشده.</returns>
        Task<Result<TDto>> CreateAsync(TDto dto);

        /// <summary>
        /// به‌روزرسانی آیتم موجود.
        /// </summary>
        /// <param name="id">شناسه آیتم.</param>
        /// <param name="dto">Dto حاوی اطلاعات جدید.</param>
        /// <returns>نتیجه عملیات همراه با Dtoی به‌روزرسانی‌شده.</returns>
        Task<Result<TDto>> UpdateAsync(Guid id, TDto dto);

        /// <summary>
        /// حذف آیتم بر اساس شناسه.
        /// </summary>
        /// <param name="id">شناسه آیتم.</param>
        /// <returns>نتیجه عملیات حذف.</returns>
        Task<Result> DeleteAsync(Guid id);
    }
}