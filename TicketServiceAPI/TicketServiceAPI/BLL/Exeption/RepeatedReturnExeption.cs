namespace TicketServiceAPI.BLL.Exeption
{
    /// <summary>
    /// Получение данной ошибки означает что произошла попытка повторно сдать билет.
    /// </summary>
    public class RepeatedReturnExeption : Exception
    {
        /// <summary>
        /// Переопределение конструктора
        /// </summary>
        /// <param name="message"></param>
        public RepeatedReturnExeption(string message) : base(message) {}   
    }
}
