
using NJsonSchema;
using NJsonSchema.Validation;
using System.Net;
using TicketService.model;

namespace TicketServiceAPI
{
    public class ValidSchema
    {
        public static int IsJsonValid(string data, Type type)
        {
            ICollection<ValidationError> _errors;

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            
            var schema = JsonSchema.FromType<Type>();

            _errors = schema.Validate(data);

            return _errors.Count;
        }
    }
}
