using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TicketServiceAPI.BLL.ShemaValid;
using TicketServiceAPI.BLL.ValidShema;

namespace TicketService
{
    [ModelBinder(typeof(JsonShemaBinder))]
    public class Refund
    {
        [Required]
        [RegularExpression("(refund){1}", ErrorMessage = "Ожидался тип операции refund")]
        public string OperationType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string OperationTime { get; set; }

        [Required(AllowEmptyStrings = false)]
        [OnlyString]
        public string OperationPlace { get; set; }

        [Required]
        [OnlyNumbers]
        [StringLength(13)]
        public string TicketNumber { get; set; }
    }
}
