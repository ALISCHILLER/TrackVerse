using Microsoft.EntityFrameworkCore;
using SharedKernel.Audit.Models;

namespace SharedKernel.Audit.Repositories
{
    /// <summary>
    /// DbContext برای مدیریت داده‌های لاگ‌گیری.
    /// </summary>
    public class AuditDbContext : DbContext
    {
        /// <summary>
        /// DbSet برای لاگ‌های تغییرات.
        /// </summary>
        public DbSet<AuditChange> AuditChanges { get; set; }

        /// <summary>
        /// سازنده کلاس.
        /// </summary>
        /// <param name="options">تنظیمات DbContext.</param>
        public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// تنظیمات مدل.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تنظیمات مربوط به جدول لاگ‌ها
            modelBuilder.Entity<AuditChange>(entity =>
            {
                entity.HasKey(ac => ac.Id); // تنظیم کلید اصلی
                entity.Property(ac => ac.EntityName).IsRequired().HasMaxLength(255); // نام موجودیت
                entity.Property(ac => ac.EntityId).IsRequired().HasMaxLength(255); // شناسه موجودیت
                entity.Property(ac => ac.PropertyName).HasMaxLength(255); // نام فیلد
                entity.Property(ac => ac.ChangedBy).HasMaxLength(255); // کاربر عامل
                entity.Property(ac => ac.IpAddress).HasMaxLength(50); // آدرس IP
                entity.Property(ac => ac.UserAgent).HasMaxLength(500); // User-Agent
                entity.Property(ac => ac.ChangedAt).HasDefaultValueSql("GETUTCDATE()"); // تاریخ تغییر
                entity.Property(ac => ac.ChangeReason).HasMaxLength(1000); // دلیل تغییر
            });
        }
    }
}