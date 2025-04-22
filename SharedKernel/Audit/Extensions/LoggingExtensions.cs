using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using SharedKernel.Audit.Models;

namespace SharedKernel.Audit.Extensions
{
    /// <summary>
    /// کلاس کمکی برای افزودن قابلیت‌های لاگ‌گیری به برنامه.
    /// </summary>
    /// <remarks>
    /// این کلاس شامل متدهای Extension برای تسهیل لاگ‌گیری و مدیریت خطاهای سیستمی است.
    /// از این کلاس می‌توانید برای ثبت لاگ‌ها، مدیریت استثناها و ایجاد لاگ‌های ساختاریافته استفاده کنید.
    /// </remarks>
    public static class LoggingExtensions
    {
        /// <summary>
        /// ثبت یک لاگ اطلاعاتی (Information) با جزئیات تغییرات.
        /// </summary>
        /// <param name="logger">Instance از ILogger.</param>
        /// <param name="auditChanges">لیست تغییراتی که باید لاگ شوند.</param>
        /// <param name="message">پیام اختیاری برای لاگ.</param>
        public static void LogAuditChanges(this ILogger logger, List<AuditChange> auditChanges, string message = null)
        {
            if (auditChanges == null || !auditChanges.Any())
            {
                logger.LogInformation("No audit changes to log.");
                return;
            }

            foreach (var change in auditChanges)
            {
                logger.LogInformation(
                    "Audit Log - Entity: {EntityName}, Property: {PropertyName}, OldValue: {OldValue}, NewValue: {NewValue}, ChangedBy: {ChangedBy}, ChangedAt: {ChangedAt}",
                    change.EntityName,
                    change.PropertyName,
                    change.OldValue.ToString(),
                    change.NewValue.ToString(),
                    change.ChangedBy,
                    change.ChangedAt);

                if (!string.IsNullOrEmpty(message))
                {
                    logger.LogInformation("Additional Message: {Message}", message);
                }
            }
        }

        /// <summary>
        /// ثبت یک لاگ خطا (Error) با جزئیات استثنای رخ‌داده.
        /// </summary>
        /// <param name="logger">Instance از ILogger.</param>
        /// <param name="exception">استثنای رخ‌داده.</param>
        /// <param name="message">پیام اختیاری برای لاگ.</param>
        public static void LogErrorWithDetails(this ILogger logger, Exception exception, string message = null)
        {
            if (exception == null)
            {
                logger.LogWarning("No exception provided for error logging.");
                return;
            }

            logger.LogError(
                exception,
                "An error occurred: {ErrorMessage}. StackTrace: {StackTrace}",
                exception.Message,
                exception.StackTrace);

            if (!string.IsNullOrEmpty(message))
            {
                logger.LogError("Additional Message: {Message}", message);
            }
        }

        /// <summary>
        /// ثبت یک لاگ هشدار (Warning) با جزئیات.
        /// </summary>
        /// <param name="logger">Instance از ILogger.</param>
        /// <param name="warning">پیام هشدار.</param>
        public static void LogWarningWithDetails(this ILogger logger, string warning)
        {
            if (string.IsNullOrEmpty(warning))
            {
                logger.LogWarning("No warning message provided for logging.");
                return;
            }

            logger.LogWarning("Warning: {WarningMessage}", warning);
        }

        /// <summary>
        /// ثبت یک لاگ موفقیت‌آمیز (Success) با جزئیات.
        /// </summary>
        /// <param name="logger">Instance از ILogger.</param>
        /// <param name="successMessage">پیام موفقیت.</param>
        public static void LogSuccess(this ILogger logger, string successMessage)
        {
            if (string.IsNullOrEmpty(successMessage))
            {
                logger.LogInformation("No success message provided for logging.");
                return;
            }

            logger.LogInformation("Success: {SuccessMessage}", successMessage);
        }

        /// <summary>
        /// ثبت یک لاگ سفارشی با سطح دلخواه.
        /// </summary>
        /// <param name="logger">Instance از ILogger.</param>
        /// <param name="logLevel">سطح لاگ (Information, Warning, Error, etc.).</param>
        /// <param name="message">پیام لاگ.</param>
        /// <param name="args">آرگومان‌های اختیاری برای فرمت‌بندی پیام.</param>
        public static void LogCustom(this ILogger logger, LogLevel logLevel, string message, params object[] args)
        {
            if (string.IsNullOrEmpty(message))
            {
                logger.LogWarning("No message provided for custom logging.");
                return;
            }

            logger.Log(logLevel, message, args);
        }
    }
}