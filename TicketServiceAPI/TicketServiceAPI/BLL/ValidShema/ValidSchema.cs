
using NJsonSchema;
using NJsonSchema.Validation;
using System.Net;

namespace TicketServiceAPI.DataValid
{
    
    public class ValidSchema
    {
        private IFormFile _file;
        private static string _schemaSale;
        private static string _schemaRefund;
        private Stream _stream;
        private static IConfiguration _configuration;


        public static void InitValidSchema(IConfiguration conf)
        {
            _configuration = conf;

            _schemaSale = File.ReadAllText(conf["Sale"]);
            _schemaRefund = File.ReadAllText(conf["Refund"]);
        }


        public ValidSchema(IFormFile file)
        {
            this._file = file;   
        }

        public async Task<HttpStatusCode> ValidJsonSchemaSaleAsync()
        {
            try
            {
                _stream = _file.OpenReadStream();

                var schema = await JsonSchema.FromJsonAsync(_schemaSale);

                var validator = new JsonSchemaValidator();

                var result = validator.Validate(new StreamReader(_stream).ReadToEnd(), schema);

                if (result.Count == 0)
                {
                    return HttpStatusCode.OK;
                }

                return HttpStatusCode.BadRequest;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
           
        }

        public async Task<HttpStatusCode> ValidJsonSchemaRefundAsync()
        {
            try
            {
                _stream = _file.OpenReadStream();

                var schema = await JsonSchema.FromJsonAsync(_schemaRefund);

                var validator = new JsonSchemaValidator();

                var result = validator.Validate(new StreamReader(_stream).ReadToEnd(), schema);

                if (result.Count == 0)
                {
                    return HttpStatusCode.OK;
                }

                return HttpStatusCode.BadRequest;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
           
        }

    }
}
