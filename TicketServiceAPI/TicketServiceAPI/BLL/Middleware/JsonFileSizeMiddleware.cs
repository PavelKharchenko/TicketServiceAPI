using TicketServiceAPI.BLL.Error;
using TicketServiceAPI.BLL.Exeption;

namespace TicketServiceAPI.BLL.Middleware
{
    public class JsonFileSizeMiddleware
    {
        private readonly RequestDelegate _next;
        private const int MaxFileSize = 2048;
        public JsonFileSizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var fileSize = context.Request.ContentLength;

            if (fileSize > MaxFileSize)
            {
                throw new RequestFileToLargeExeption(ErrorType.RequestFileToLarge);
            }
            else
            {
                await _next(context);
            }

        }
    }
}
