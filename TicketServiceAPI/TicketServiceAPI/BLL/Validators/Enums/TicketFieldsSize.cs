namespace TicketServiceAPI.BLL.ValidationModel.EnumSizeField
{
    /// <summary>
    /// Размеры полей в билете
    /// </summary>
    public enum TicketFieldsSize
    {
       /// <summary>
       /// Допустимый размер номера билета
       /// </summary>
        TICKETNUMBER = 13,
        /// <summary>
        /// Допустимый размер типа документа
        /// </summary>
        DOCTYPE = 2,
        /// <summary>
        /// Допустимый размер номера документа
        /// </summary>
        DOCNUMBER = 10,
        /// <summary>
        /// Допустимый размер типа билета
        /// </summary>
        TICKETTYPE = 1,
        /// <summary>
        /// Допустимый размер кода авиакомпании
        /// </summary>
        AIRLINECODE = 2,
        /// <summary>
        /// Допустимый размер номера рейса 
        /// </summary>
        FLIGHTNUM = 4,
        /// <summary>
        /// Допустимый размер кода места отправления и пребытия
        /// </summary>
        DEPARTORARRIVEPLACE = 3,
        /// <summary>
        /// Допустимый размер поля кода проверки подлинности билета
        /// </summary>
        PNRID = 6

    }
}
