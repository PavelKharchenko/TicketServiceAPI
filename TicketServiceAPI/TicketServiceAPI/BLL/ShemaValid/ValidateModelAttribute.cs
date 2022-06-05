using Microsoft.AspNetCore.Mvc.Filters;
using TicketServiceAPI.BLL.Exeption;

namespace TicketServiceAPI.BLL.ShemaValid
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ValidateJsonSchemaExeption("Ошибка валидации");
            }

            await next.Invoke();
        }

    }
}
