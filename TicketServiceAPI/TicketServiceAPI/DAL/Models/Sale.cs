using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TicketServiceAPI.BLL.ShemaValid;
using TicketServiceAPI.BLL.ValidShema;

namespace TicketService.model
{
    
    [ModelBinder(typeof(JsonShemaBinder))]
    public class Sale
    {
        [Required]
        [RegularExpression("(sale){1}", ErrorMessage = "Ожидался тип операции sale")]
        public string OperationType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string OperationTime { get; set; }


        [Required(AllowEmptyStrings = false)]
        [OnlyString]
        public string OperationPlace { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Passenger Passenger { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        public Routes[] Routes { get; set; }

    }
}
