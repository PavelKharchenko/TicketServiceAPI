using FluentValidation;
using TicketService;
using TicketServiceAPI.BLL.ValidationModel.EnumSizeField;

namespace TicketServiceAPI.BLL.ValidationModel
{
    /// <summary>
    /// Проверка занчений операции по возвращению билета
    /// </summary>
    public class RefundValidator : AbstractValidator<Refund>
    {
        private string _operationType = "refund";

        /// <summary>
        /// Проверка полей файла операции по возвращению билета на требования к валидации
        /// </summary>
        public RefundValidator()
        {
            RuleFor(r => r.OperationType.Equals(_operationType)).NotNull().NotEmpty();

            RuleFor(r => r.TicketNumber.Length == (int)TicketFieldsSize.TICKETNUMBER);

            RuleForEach(r => r.TicketNumber.ToString().ToCharArray()).Must(Char.IsDigit).NotNull().NotEmpty().OverridePropertyName("ticket_number");
        }
    }
}
