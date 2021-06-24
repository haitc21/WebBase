using Microsoft.AspNetCore.Builder;

namespace WebBase.Helpers
{
    /// <summary>
    /// chay midle ware app.UseErrorWrapping
    /// </summary>
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorWrapping(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorWrappingMiddleware>();
        }
    }
}