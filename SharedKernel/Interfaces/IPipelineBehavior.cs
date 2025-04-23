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
using SharedKernel.Models;
using SharedKernel.Resources;
using SharedKernel.Common;

namespace SharedKernel.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<SharedResource> localizer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            var stopwatch = Stopwatch.StartNew();
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            try
            {
                // لاگ شروع درخواست
                _logger.LogInformation(
                    _localizer["LOG_REQUEST_STARTED"],
                    requestName,
                    userId,
                    ip);

                // لاگ بدنه درخواست (فقط در سطح Debug)
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    var sanitizedRequest = SanitizeRequest(request);
                    _logger.LogDebug("Request Body: {RequestBody}", sanitizedRequest);
                }

                // اجرای درخواست
                var response = await next();

                stopwatch.Stop();

                // لاگ پاسخ موفق
                _logger.LogInformation(
                    _localizer["LOG_REQUEST_COMPLETED"],
                    requestName,
                    stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                // لاگ خطا
                _logger.LogError(
                    ex,
                    _localizer["LOG_REQUEST_FAILED"],
                    requestName,
                    ex.Message,
                    stopwatch.ElapsedMilliseconds);

                // ایجاد پاسخ خطا با Result<T>
                if (typeof(TResponse).IsGenericType &&
                    typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
                {
                    var errorResult = (Result)Activator.CreateInstance(
                        typeof(Result<>).MakeGenericType(typeof(TResponse).GenericTypeArguments[0]),
                        false,
                        _localizer["GENERAL_ERROR_MESSAGE"],
                        StatusCodes.Status500InternalServerError,
                        ex)!;

                    return (TResponse)errorResult;
                }

                throw;
            }
        }

        /// <summary>
        /// پاکسازی اطلاعات حساس از درخواست
        /// </summary>
        private string SanitizeRequest(object request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                return Regex.Replace(
                    json,
                    @"(""(?:password|token|secret|creditcard)""):\s*"".*?""",
                    "$1: \"***\"",
                    RegexOptions.IgnoreCase);
            }
            catch
            {
                return "Could not sanitize request body";
            }
        }
    }
}