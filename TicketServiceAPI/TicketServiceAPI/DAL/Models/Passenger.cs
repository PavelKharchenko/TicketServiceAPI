using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TicketServiceAPI.BLL.ShemaValid;
using TicketServiceAPI.BLL.ValidShema;

namespace TicketService.model
{
    [ModelBinder(typeof(JsonShemaBinder))]
    public class Passenger
    {
        [Required(AllowEmptyStrings = false)]
        [OnlyString]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [OnlyString]
        public string Surname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [OnlyString]
        public string Patronymic { get; set; }

        [Required]
        [OnlyNumbers]
        public string DocType { get; set; }

        [Required]
        [OnlyNumbers]
        public string DocNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string Birthdate { get; set; }

        [Required]
        [RegularExpression("[MF]{1}", ErrorMessage = "Не праввильно указан гендер")]
        public string Gender { get; set; }

        [Required]
        [RegularExpression("(youth|adult|senior){1}", ErrorMessage = "Не правильно указан тип пассажира")]
        public string PassengerType { get; set; }

        [Required]
        [OnlyNumbers]
        [StringLength(13)]
        public string TicketNumber { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Не верно указан тип билета")]
        public int TicketType { get; set; }

    }
}
