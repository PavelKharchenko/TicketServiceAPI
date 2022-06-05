namespace TicketServiceAPI.BLL.Error
{
    public class ErrorType
    {
        public const string RequestTimeOut = "Tайм-аут запроса";
        public const string DublicateTicket = "Повторная покупка билета";
        public const string ServerError = "Ошибка в работе сервера";
        public const string RequestFileToLarge = "Передача файла размером больше 2 КБ не допустима";
        
    }
}
