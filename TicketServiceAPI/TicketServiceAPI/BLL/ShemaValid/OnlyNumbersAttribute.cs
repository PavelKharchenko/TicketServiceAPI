using System.ComponentModel.DataAnnotations;

namespace TicketServiceAPI.BLL.ShemaValid
{
    public class OnlyNumbersAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string s)
            {
                return s.All(char.IsNumber);
            }
            else
            {
                ErrorMessage = $"{nameof(OnlyNumbersAttribute)}| Должны быть только числа!";
                return false;
            }
        }
    }
}

