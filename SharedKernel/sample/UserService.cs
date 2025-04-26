//using SharedKernel.Audit;

//public class UserService
//{
//    private readonly AuditLogService _auditLogService;

//    public UserService(AuditLogService auditLogService)
//    {
//        _auditLogService = auditLogService ?? throw new ArgumentNullException(nameof(auditLogService));
//    }

//    public async Task CreateUserAsync(User newUser, string userId, string ipAddress, string userAgent)
//    {
//        if (newUser == null)
//            throw new ArgumentNullException(nameof(newUser), "کاربر جدید نباید خالی باشد.");

//        if (string.IsNullOrWhiteSpace(userId))
//            throw new ArgumentException("شناسه کاربر نباید خالی باشد.", nameof(userId));

//        try
//        {
//            await _auditLogService.LogChangesAsync(
//                null,
//                newUser,
//                "Admin",
//                userId,
//                OperationType.Create,
//                ipAddress,
//                userAgent,
//                "ایجاد کاربر جدید");
//        }
//        catch (Exception ex)
//        {
//            Console.Error.WriteLine($"خطا در ثبت لاگ: {ex.Message}");
//            throw;
//        }
//    }

//    public async Task UpdateUserAsync(User oldUser, User updatedUser, string userId, string ipAddress, string userAgent)
//    {
//        if (oldUser == null || updatedUser == null)
//            throw new ArgumentNullException("کاربر قدیمی یا جدید نباید خالی باشد.");

//        if (string.IsNullOrWhiteSpace(userId))
//            throw new ArgumentException("شناسه کاربر نباید خالی باشد.", nameof(userId));

//        try
//        {
//            await _auditLogService.LogChangesAsync(
//                oldUser,
//                updatedUser,
//                "Admin",
//                userId,
//                OperationType.Update,
//                ipAddress,
//                userAgent,
//                "به‌روزرسانی اطلاعات کاربر");
//        }
//        catch (Exception ex)
//        {
//            Console.Error.WriteLine($"خطا در ثبت لاگ: {ex.Message}");
//            throw;
//        }
//    }
//}

//public class User
//{
//    public Guid Id { get; set; }
//    public string Name { get; set; }
//    public string Email { get; set; }
//}

//public enum OperationType
//{
//    Create,
//    Update,
//    Delete
//}