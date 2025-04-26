using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using SharedKernel.Resources;
using SharedKernel.Common;

namespace SharedKernel.Behaviors
{
    // کلاس LoggingBehavior برای مدیریت لاگ‌گذاری درخواست‌ها و پاسخ‌ها استفاده می‌شود
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger; // Logger برای نوشتن لاگ‌ها
        private readonly IHttpContextAccessor _httpContextAccessor; // دسترسی به اطلاعات HTTP Context
        private readonly IStringLocalizer<SharedResource> _localizer; // برای ترجمه و محلی‌سازی پیام‌ها

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<SharedResource> localizer)
        {
            // اطمینان از تزریق وابستگی‌ها
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        // متد اصلی برای پردازش درخواست‌ها و لاگ‌گذاری آن‌ها
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name; // نام درخواست
            var stopwatch = Stopwatch.StartNew(); // شروع زمان‌سنج برای محاسبه مدت زمان درخواست
            var httpContext = _httpContextAccessor.HttpContext; // دسترسی به HTTP Context

            // گرفتن اطلاعات کاربر و IP از context
            var userId = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var ip = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
            var traceId = httpContext?.TraceIdentifier ?? Guid.NewGuid().ToString(); // شناسه پیگیری درخواست
            var correlationId = httpContext?.Request?.Headers["X-Correlation-ID"].ToString() ?? Guid.NewGuid().ToString(); // شناسه همبستگی برای پیگیری درخواست‌ها

            try
            {
                // نوشتن لاگ برای شروع درخواست
                _logger.LogInformation(
                    "[TraceID: {TraceId} | CorrelationID: {CorrelationId}] {Message} | Request: {RequestName}, User: {UserId}, IP: {IP}",
                    traceId,
                    correlationId,
                    _localizer["LOG_REQUEST_STARTED"], // پیام محلی‌سازی شده
                    requestName,
                    userId,
                    ip);

                // اگر لاگ با سطح Debug فعال باشد، درخواست را بدون فیلدهای حساس لاگ می‌کنیم
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    var sanitized = SanitizeRequest(request); // حذف داده‌های حساس از درخواست
                    _logger.LogDebug(
                        "[TraceID: {TraceId}] Sanitized Request Body for {RequestName}: {RequestBody}",
                        traceId,
                        requestName,
                        sanitized); // لاگ کردن درخواست تمیز شده
                }

                var response = await next(); // اجرای درخواست و گرفتن پاسخ
                stopwatch.Stop(); // توقف زمان‌سنج

                // نوشتن لاگ برای پایان درخواست و مدت زمان اجرا
                _logger.LogInformation(
                    "[TraceID: {TraceId}] {Message} | Request: {RequestName}, Duration: {Duration} ms",
                    traceId,
                    _localizer["LOG_REQUEST_COMPLETED"], // پیام محلی‌سازی شده
                    requestName,
                    stopwatch.ElapsedMilliseconds);

                return response; // بازگشت پاسخ
            }
            catch (Exception ex)
            {
                stopwatch.Stop(); // توقف زمان‌سنج در صورت بروز خطا

                // نوشتن لاگ برای خطا با جزئیات
                _logger.LogError(
                    ex,
                    "[TraceID: {TraceId}] {Message} | Request: {RequestName}, Error: {Error}, Duration: {Duration} ms",
                    traceId,
                    _localizer["LOG_REQUEST_FAILED"], // پیام محلی‌سازی شده
                    requestName,
                    ex.Message,
                    stopwatch.ElapsedMilliseconds);

                // در صورت بروز خطا، یک نتیجه خطا بر می‌گردانیم
                if (typeof(TResponse).IsGenericType &&
                    typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
                {
                    var genericType = typeof(TResponse).GenericTypeArguments[0];
                    var errorResult = Activator.CreateInstance(
                        typeof(Result<>).MakeGenericType(genericType),
                        false,
                        _localizer["GENERAL_ERROR_MESSAGE"].Value,
                        StatusCodes.Status500InternalServerError,
                        ex);

                    return (TResponse)errorResult!; // برگرداندن نتیجه خطا
                }

                throw; // اگر نوع پاسخ جوری نیست که خطا را هندل کند، دوباره پرتاب می‌کنیم
            }
        }

        /// <summary>
        /// حذف داده‌های حساس از درخواست برای لاگ‌کردن امن
        /// </summary>
        private string SanitizeRequest(object request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request); // سریال‌سازی درخواست به فرمت JSON

                // حذف فیلدهای حساس مانند password، token، secret و ...
                json = Regex.Replace(
                    json,
                    @"(""(?:password|token|secret|creditcard|apikey|authorization)"")\s*:\s*""[^""]*""",
                    "$1: \"***\"",
                    RegexOptions.IgnoreCase);

                // محدود کردن طول لاگ به 2000 کاراکتر
                const int maxLength = 2000;
                return json.Length > maxLength ? json.Substring(0, maxLength) + "..." : json;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to sanitize request body for logging."); // لاگ خطا در صورت عدم توانایی در تمیز کردن درخواست
                return "Could not sanitize request body"; // در صورتی که تمیز کردن درخواست ممکن نباشد، پیام خطا برمی‌گردانیم
            }
        }
    }
}
