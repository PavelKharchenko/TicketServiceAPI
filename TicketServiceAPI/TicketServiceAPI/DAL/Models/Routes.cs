using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TicketServiceAPI.BLL.ShemaValid;
using TicketServiceAPI.BLL.ValidShema;

namespace TicketService.model
{
    [ModelBinder(typeof(JsonShemaBinder))]
    public class Routes
    {
        [Required(AllowEmptyStrings = false)]
        [OnlyString]
        public string AirlineCode { get; set; }

        [Required]
        public int FlightNum { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string DepartPlace { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string DepartDatetime { get; set; }

        [Required(AllowEmptyStrings = false)]
        [OnlyString]
        public string ArrivePlace { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string ArriveDatetime { get; set; }

        [Required(AllowEmptyStrings = false)]
        [OnlyString]
        public string PnrId { get; set; }

    }
}
