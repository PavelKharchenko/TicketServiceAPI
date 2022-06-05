namespace TicketServiceAPI.BLL.Exeption
{
    public class ThisTicketDoesNotExist : Exception
    {
        public ThisTicketDoesNotExist(string message) : base(message) { }
    }
}
