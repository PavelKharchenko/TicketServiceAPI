namespace TicketServiceAPI.BLL.Middleware
{
    public static class CustomExeptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExeptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExeptionHandlerMiddleware>();
        }
    }
}
