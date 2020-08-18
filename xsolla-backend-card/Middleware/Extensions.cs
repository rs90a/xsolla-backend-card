using Microsoft.AspNetCore.Builder;

namespace xsolla_backend_card.Middleware
{
    public static class Extensions
    {
        /// <summary>
        /// Метод расширения для встраивания компонента TokenContextMiddleware
        /// </summary>
        public static IApplicationBuilder UseTokenContext(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenContextMiddleware>();
        }
    }
}