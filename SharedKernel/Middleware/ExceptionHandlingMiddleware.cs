using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedKernel.Notifications;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedKernel.Middlewares
{
    /// <summary>
    /// Middleware برای مدیریت سراسری خطاها در برنامه.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // تعیین کد وضعیت HTTP بر اساس نوع خطا
            var statusCode = GetStatusCodeFromException(exception);

            // لاگ‌زنی خطا
            _logger.LogError(exception, "Unhandled Exception: {Message}", exception.Message);

            // ایجاد Notification برای خطا
            var notification = new Notification(
                message: GetErrorMessage(exception),
                type: "Error",
                userId: context.User?.Identity?.Name
            );

            // ساخت پاسخ JSON
            var result = JsonSerializer.Serialize(new
            {
                error = true,
                notification.Message,
                notification.Type,
                notification.CreatedAt,
                notification.UserId,
                statusCode
            }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            // تنظیم پاسخ HTTP
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result);
        }

        /// <summary>
        /// تعیین کد وضعیت HTTP بر اساس نوع خطا.
        /// </summary>
        private HttpStatusCode GetStatusCodeFromException(Exception exception)
        {
            return exception switch
            {
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                ArgumentException or InvalidOperationException => HttpStatusCode.BadRequest,
                NotImplementedException => HttpStatusCode.NotImplemented,
                _ => HttpStatusCode.InternalServerError
            };
        }

        /// <summary>
        /// تعیین پیام خطای مناسب بر اساس محیط (Development/Production).
        /// </summary>
        private string GetErrorMessage(Exception exception)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                return exception.ToString(); // پیام کامل خطا برای محیط توسعه
            }

            return exception switch
            {
                UnauthorizedAccessException => "دسترسی غیرمجاز.",
                ArgumentException or InvalidOperationException => "داده‌های ورودی نامعتبر.",
                NotImplementedException => "این عملیات پیاده‌سازی نشده است.",
                _ => "خطای داخلی سرور."
            };
        }
    }
}