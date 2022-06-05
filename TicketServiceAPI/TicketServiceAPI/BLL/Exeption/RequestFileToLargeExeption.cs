using System;

namespace TicketServiceAPI.BLL.Exeption
{
    
    /// <summary>
    /// Ошибка обозначающаяя что полученный json file больше 2кб
    /// </summary>
    public class RequestFileToLargeExeption : Exception
    {
       /// <summary>
       /// Переопределеяем конструктор
       /// </summary>
        public RequestFileToLargeExeption(string message) : base(message)
        {
            
        }
    }
}
