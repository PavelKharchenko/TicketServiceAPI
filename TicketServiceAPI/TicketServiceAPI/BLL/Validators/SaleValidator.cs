using FluentValidation;
using TicketService.model;
using TicketServiceAPI.BLL.ValidationModel.EnumSizeField;
using System.Linq;
namespace TicketServiceAPI.BLL.ValidationModel
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        private string _operationType = "sale";
        private string _genderMan = "M";
        private string _genderGerl = "F";
        private string _formatDate = "yyyy-MM-dd";

        /// <summary>
        /// Проверка билета на соответсвие требованиям валидации
        /// </summary>
        public SaleValidator()
        {
            RuleFor(s => s.OperationType).NotNull().NotEmpty();
            RuleFor(s => s.OperationType.Equals(_operationType));

            RuleFor(s => s.OperationTime).NotNull().NotEmpty();
 

            RuleFor(s => s.Passenger.Name).NotNull().NotEmpty();
            RuleForEach(s => s.Passenger.Name).Must(Char.IsLetter);

            RuleFor(s => s.Passenger.Surname).NotNull().NotEmpty();
            RuleForEach(s => s.Passenger.Surname).Must(Char.IsLetter);

            RuleFor(s => s.Passenger.Patronymic).NotNull().NotEmpty();
            RuleForEach(s => s.Passenger.Patronymic).Must(Char.IsLetter);

            RuleFor(s => DateTime.Parse(s.Passenger.Birthdate).ToString(_formatDate)).NotNull().NotEmpty();

            RuleFor(s => s.Passenger.Gender.Equals(_genderMan) || s.Passenger.Gender.Equals(_genderGerl)).NotNull().NotEmpty();

            RuleFor(s => s.Passenger.PassengerType).NotNull().NotEmpty();

      
            RuleFor(s => s.Passenger.TicketNumber.Length == (int)TicketFieldsSize.TICKETNUMBER);

            RuleFor(s => s.Passenger.DocType.Length == (int)TicketFieldsSize.DOCTYPE).NotNull().NotEmpty();
      

            RuleFor(s => s.Passenger.DocNumber.Length == (int)TicketFieldsSize.DOCNUMBER).NotNull().NotEmpty();
    

            RuleFor(s => s.Passenger.TicketType.ToString().Length == (int)TicketFieldsSize.TICKETTYPE).NotNull().NotEmpty();

            RuleFor(s => s.Routes.ToList()
            .TrueForAll(r => r.ArrivePlace.Length == (int)TicketFieldsSize.DEPARTORARRIVEPLACE && r.DepartPlace.Length == (int)TicketFieldsSize.DEPARTORARRIVEPLACE))
            .NotNull();

            RuleFor(s => s.Routes.ToList()
           .TrueForAll(r => r.ArrivePlace.Length == (int)TicketFieldsSize.DEPARTORARRIVEPLACE && r.DepartPlace.Length == (int)TicketFieldsSize.DEPARTORARRIVEPLACE))
           .NotNull();


            RuleFor(s => s.Routes.ToList()
            .TrueForAll(r => r.AirlineCode.Length == (int)TicketFieldsSize.AIRLINECODE))
            .NotNull();

            RuleForEach(s => s.Routes.ToList().SelectMany(r => r.AirlineCode.ToCharArray())).Must(Char.IsLetter).OverridePropertyName("AirlineCode");

            RuleFor(s => s.Routes.ToList()
            .TrueForAll(r => r.FlightNum.ToString().Length == (int)TicketFieldsSize.FLIGHTNUM))
            .NotNull();

            RuleForEach(s => s.Routes.ToList().SelectMany(r => r.FlightNum.ToString().ToCharArray())).Must(Char.IsDigit).OverridePropertyName("FlightNum");

        }
    }
}
