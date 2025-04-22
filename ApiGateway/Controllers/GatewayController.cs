using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    /// <summary>
    /// کنترلر اصلی API Gateway
    /// </summary>
    [ApiController]
    [Route("api/gateway")]
    public class GatewayController : ControllerBase
    {
        /// <summary>
        /// بررسی وضعیت سلامت سیستم
        /// </summary>
        /// <returns>پاسخ موفق با پیام "Healthy"</returns>
        [HttpGet("health")]
        public IActionResult Health()
        {
            // این متد می‌تواند برای بررسی وضعیت سلامت سیستم توسط کلاینت‌ها و ابزارهای مانیتورینگ استفاده شود.
            return Ok("Healthy");
        }

        /// <summary>
        /// بررسی وضعیت گیت‌وی
        /// </summary>
        /// <returns>پاسخ موفق با پیام "API Gateway is running."</returns>
        [HttpGet("status")]
        public IActionResult Status()
        {
            // این متد می‌تواند برای بررسی وضعیت زنده بودن گیت‌وی استفاده شود.
            return Ok(new { Message = "API Gateway is running." });
        }
    }
}