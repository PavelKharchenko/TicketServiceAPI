using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TicketService.model
{
    public class DataFile
    {
       private Sale _sale;

       private Refund _refund;

        private IFormFile file;

       private JsonSerializerSettings _options = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public DataFile(IFormFile file)
        {
            this.file = file;
            
        }

        public Sale GetDataSale()
        {
            try
            {
                Stream stream = file.OpenReadStream();

                _sale = JsonConvert.DeserializeObject<Sale>(new StreamReader(stream).ReadToEnd(), _options);
            }
            catch 
            {
                return new Sale();
            }
           
            return _sale;
        }

        public Refund GetDataRefund()
        {
            try
            {
                Stream stream = file.OpenReadStream();

                _refund = JsonConvert.DeserializeObject<Refund>(new StreamReader(stream).ReadToEnd(), _options);
            }
            catch
            {
                return new Refund();
            }
           
            return _refund;

        }
    }
}
