using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using System.Text.Json;
using TicketServiceAPI.BLL.Error;

namespace TicketServiceAPI.BLL.Middleware
{
    public class CustomExeptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExeptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }catch (Exception ex)
            {
                await HandleExeptionAsync(context, ex);
            }
        }

        private  Task HandleExeptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var message = string.Empty;

            switch (ex) 
            {
                case DbUpdateException up:
                    
                    if (up.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                    {
                        code = HttpStatusCode.Conflict;
                        message = ErrorType.DublicateTicket;
                        
                    }
                    code = HttpStatusCode.InternalServerError;
                    message = ErrorType.DublicateTicket;

                    break;

                case PostgresException pg:
                   
                    if (pg.SqlState == PostgresErrorCodes.QueryCanceled)
                    {

                        code = HttpStatusCode.RequestTimeout;
                        message = ErrorType.RequestTimeOut;
                    }
                    code= HttpStatusCode.InternalServerError;
                    message = ErrorType.ServerError;
                    break;

                case Exception:
                    
                    code = HttpStatusCode.InternalServerError;
                    message = ErrorType.ServerError;
                    break;
            }

            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            message = JsonSerializer.Serialize(new ErrorMessage() { Message = message });

            return context.Response.WriteAsync(message);

        }

    }
}
