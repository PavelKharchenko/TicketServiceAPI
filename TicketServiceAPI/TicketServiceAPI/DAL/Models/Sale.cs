namespace TicketService.model
{
    public class Sale
    {
        
        public string OperationType { get; set; }
        public string OperationTime { get; set; }
        public string OperationPlace { get; set; }
        public Passenger Passenger { get; set; }
        public Routes[] Routes { get; set; }

    }
}
