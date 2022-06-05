using System.ComponentModel.DataAnnotations;

namespace TicketServiceAPI.BLL.ShemaValid
{
    public class OnlyStringAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string s)
            {
                return s.All(char.IsLetter);
            }
            else
            {
                ErrorMessage = $"{nameof(OnlyNumbersAttribute)}| Должны быть только буквы!";
                return false;
            }
        }
    }
}
