using SharedKernel.Audit;

public class UserService
{
    private readonly AuditLogService _auditLogService;

    public UserService(AuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    public async Task CreateUserAsync(User newUser, string userId, string ipAddress, string userAgent)
    {
        // ثبت لاگ برای عملیات Create
        await _auditLogService.LogChangesAsync(
            null,
            newUser,
            "Admin",
            userId.ToString(),
            "Create",
            ipAddress,
            userAgent,
            "ایجاد کاربر جدید");
    }

    public async Task UpdateUserAsync(User oldUser, User updatedUser, string userId, string ipAddress, string userAgent)
    {
        // ثبت لاگ برای عملیات Update
        await _auditLogService.LogChangesAsync(
            oldUser,
            updatedUser,
            "Admin",
            userId.ToString(),
            "Update",
            ipAddress,
            userAgent,
            "به‌روزرسانی اطلاعات کاربر");
    }
}

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}