using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SharedKernel.Audit.Behaviors
{
    /// <summary>
    /// Behavior برای لاگ‌گیری خودکار در Pipelines.
    /// </summary>
    /// <typeparam name="TRequest">نوع درخواست.</typeparam>
    /// <typeparam name="TResponse">نوع پاسخ.</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// سازنده کلاس.
        /// </summary>
        /// <param name="logger">Instance از ILogger برای لاگ‌گیری.</param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// متد اصلی برای اجرای لاگ‌گیری قبل و بعد از اجرای عملیات.
        /// </summary>
        /// <param name="request">درخواست ورودی.</param>
        /// <param name="next">تابع بعدی در Pipeline.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>نتیجه عملیات.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // دریافت نام درخواست
            var requestName = typeof(TRequest).Name;

            // فیلتر کردن پارامترهای حساس
            var sensitiveFields = new[] { "Password", "Token", "NationalId" };
            var filteredParams = request?.GetType().GetProperties()
                .Select(p =>
                {
                    var value = sensitiveFields.Contains(p.Name) ? "***" : p.GetValue(request)?.ToString();
                    return $"{p.Name}: {value}";
                }) ?? Array.Empty<string>();

            var requestParameters = string.Join(", ", filteredParams);

            // شروع StopWatch برای اندازه‌گیری زمان اجرای عملیات
            var stopwatch = Stopwatch.StartNew();

            // ایجاد LogScope برای مدیریت Context
            using (_logger.BeginScope("RequestId: {RequestId}, RequestType: {RequestType}", Guid.NewGuid(), requestName))
            {
                try
                {
                    // لاگ‌گیری شروع عملیات
                    _logger.LogInformation("Handling {RequestName} with parameters: {RequestParameters}", requestName, requestParameters);

                    // اجرای عملیات اصلی
                    var response = await next();

                    // توقف StopWatch و لاگ‌گیری پایان موفقیت‌آمیز عملیات
                    stopwatch.Stop();
                    _logger.LogInformation("Successfully handled {RequestName} in {ElapsedMilliseconds}ms. Response: {@Response}",
                        requestName, stopwatch.ElapsedMilliseconds, response);

                    return response;
                }
                catch (Exception ex)
                {
                    // توقف StopWatch و لاگ‌گیری خطاهای رخ‌داده
                    stopwatch.Stop();
                    _logger.LogError(ex, "Error occurred while handling {RequestName} in {ElapsedMilliseconds}ms. Error: {ErrorMessage}",
                        requestName, stopwatch.ElapsedMilliseconds, ex.Message);

                    throw;
                }
            }
        }
    }
}