using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace TicketServiceAPI.BLL.ValidShema
{
    public class JsonShemaBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            
                bindingContext.HttpContext.Request.EnableBuffering();

                var memoryStream = new MemoryStream();

                await bindingContext.HttpContext.Request.Body.CopyToAsync(memoryStream);
                var requestBytes = memoryStream.ToArray();

                var requestString = Encoding.UTF8.GetString(requestBytes);

             
                var model = JsonConvert.DeserializeObject(requestString, bindingContext.ModelType,
                       new JsonSerializerSettings()
                       {
                            ContractResolver = new DefaultContractResolver()
                            {
                                NamingStrategy = new SnakeCaseNamingStrategy()
                            }
                       });

                   bindingContext.Result = ModelBindingResult.Success(model);
         
        }
    }
}
