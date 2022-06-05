namespace TicketServiceAPI.BLL.Middleware
{
    public static  class JsonFileSizeMiddlewareExtensions
    {
        public static IApplicationBuilder UseJsonFileMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<JsonFileSizeMiddleware>();
    }
}
