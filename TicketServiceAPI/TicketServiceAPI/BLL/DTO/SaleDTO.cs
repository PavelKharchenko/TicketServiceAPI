namespace TicketServiceAPI.BLL.DTO
{
    public class SaleDTO
    {
        public string OperationType { get; set; }
        public string OperationTime { get; set; }
        public string OperationPlace { get; set; }
        public PassengerDTO Passenger { get; set; }
        public RoutesDTO[] Routes { get; set; }
    }
}
