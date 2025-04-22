using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
namespace ApiGateway.Middleware
{



    /// <summary>
    /// میان‌افزار احراز هویت
    /// </summary>
    public class AuthenticationMiddleware
    {
        /// <summary>
        /// نگهدارنده درخواست بعدی در خط لول Middleware
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// تنظیمات JWT
        /// </summary>
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// لاگ‌گیری برای ثبت اطلاعات و خطاها
        /// </summary>
        private readonly ILogger<AuthenticationMiddleware> _logger;

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public AuthenticationMiddleware(
            RequestDelegate next,
            IOptions<JwtSettings> jwtOptions,
            ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _jwtSettings = jwtOptions.Value;
            _logger = logger;
        }

        /// <summary>
        /// اجرای میان‌افزار
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // گرفتن توکن از هدر Authorization
                var token = ExtractTokenFromHeader(context);

                // اعتبارسنجی توکن
                var principal = ValidateToken(token);

                // ذخیره اطلاعات کاربر در HttpContext.User برای دسترسی در سایر بخش‌ها
                context.User = principal;
            }
            catch (SecurityTokenException ex)
            {
                // لاگ‌گیری برای درخواست‌های نامعتبر
                _logger.LogWarning("درخواست به {Path} با توکن نامعتبر: {Message}", context.Request.Path, ex.Message);

                // ارسال پاسخ 401 Unauthorized
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { Message = "دسترسی غیرمجاز" });
                return;
            }

            // ادامه دادن به خط لول Middleware
            await _next(context);
        }

        /// <summary>
        /// استخراج توکن از هدر Authorization
        /// </summary>
        private string ExtractTokenFromHeader(HttpContext context)
        {
            // گرفتن مقدار هدر Authorization
            var authHeader = context.Request.Headers["Authorization"].ToString();

            // بررسی وجود پیشوند "Bearer "
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                _logger.LogWarning("هدر Authorization نامعتبر یا وجود ندارد: {Header}", authHeader);
                throw new SecurityTokenException("هدر Authorization یافت نشد یا فرمت آن نادرست است.");
            }

            // جدا کردن توکن از پیشوند "Bearer "
            var token = authHeader.Replace("Bearer ", "").Trim();

            // بررسی مقدار توکن
            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("مقدار توکن خالی است.");
                throw new SecurityTokenException("توکن ارسال نشده است.");
            }

            return token;
        }

        /// <summary>
        /// اعتبارسنجی توکن JWT
        /// </summary>
        private ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                // تنظیمات اعتبارسنجی توکن
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // اعتبارسنجی کلید امضای ایجادکننده
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true, // اعتبارسنجی ایجادکننده
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true, // اعتبارسنجی مخاطب
                    ValidAudience = _jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero // عدم تحمل اختلاف زمانی
                };

                // اعتبارسنجی توکن و بازگرداندن اطلاعات کاربر
                return tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch (Exception ex)
            {
                // لاگ‌گیری با اطلاعات بیشتر (بدون لو رفتن توکن کامل)
                _logger.LogWarning("اعتبارسنجی ناموفق توکن: {ShortToken} | خطا: {Message}",
                    token?.Substring(0, Math.Min(10, token.Length)), ex.Message);

                throw new SecurityTokenException("توکن معتبر نیست.");
            }
        }
    }
}
