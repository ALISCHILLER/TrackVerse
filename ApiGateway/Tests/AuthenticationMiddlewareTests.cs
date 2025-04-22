//using Xunit;
//using Microsoft.AspNetCore.Http;
//using System.Threading.Tasks;
//using Microsoft.IdentityModel.Tokens;

//namespace ApiGateway.Tests
//{
   

//    public class AuthenticationMiddlewareTests
//    {
//        [Fact]
//        public async Task InvokeAsync_InvalidToken_Returns401()
//        {
//            // Arrange
//            var middleware = new AuthenticationMiddleware(async (innerHttpContext) =>
//            {
//                await Task.CompletedTask; // شبیه‌سازی خط لول بعدی
//            });

//            var httpContext = new DefaultHttpContext();
//            httpContext.Request.Headers["Authorization"] = "Bearer InvalidToken";

//            // Act & Assert
//            await Assert.ThrowsAsync<SecurityTokenException>(() => middleware.InvokeAsync(httpContext));
//        }
//    }
//}
