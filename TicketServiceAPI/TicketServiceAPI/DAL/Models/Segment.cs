using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketServiceAPI
{
    public partial class Segment
    {
      
        public int Id { get; set; }
        public int SerialId { get; set; }
        public string OperationType { get; set; } = null!;
        public string OperationTime { get; set; } = null!;
        public string OperationPlace { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string DocType { get; set; } = null!;
        public string DocNumber { get; set; } = null!;
        public string Birthdate { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string PassengerType { get; set; } = null!;
        public string TicketNumber { get; set; } = null!;
        public int TicketType { get; set; }
        public string AirlineCode { get; set; } = null!;
        public int FlightNum { get; set; }
        public string DepartPlace { get; set; } = null!;
        public string DepartDatetime { get; set; } = null!;
        public string ArrivePlace { get; set; } = null!;
        public string ArriveDatetime { get; set; } = null!;
        public string PnrId { get; set; } = null!;
    
    }
}
