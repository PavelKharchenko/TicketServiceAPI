using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using System.Net;
using System.Text.Json;
using TicketServiceAPI.BLL.Error;
using TicketServiceAPI.BLL.Exeption;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TicketServiceAPI.BLL.Middleware
{
    public class CustomExeptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExeptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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
                case RequestFileToLargeExeption:
                    code = HttpStatusCode.RequestEntityTooLarge;
                    message = ErrorType.RequestFileToLarge;
                    break;
                case ValidateJsonSchemaExeption:
                    code = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
                case ThisTicketDoesNotExist e:
                    code = HttpStatusCode.BadRequest;
                    message = e.Message;
                    break;
                case JsonReaderException: 
                     code = HttpStatusCode.BadRequest;
                    message = "Передана не верная модель";
                    break;
                case Exception:
                    code = HttpStatusCode.InternalServerError;
                    message = ErrorType.ServerError;
                    break;
            }

            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            message = JsonConvert.SerializeObject(new ErrorMessage() { Message = message });
       

            return context.Response.WriteAsync(message);

        }

    }
}
