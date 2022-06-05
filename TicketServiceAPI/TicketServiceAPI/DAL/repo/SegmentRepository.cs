using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using TicketService;
using TicketService.model;
using TicketServiceAPI.BLL.DTO;
using TicketServiceAPI.BLL.Exeption;
using TicketServiceAPI.repo;

namespace TicketServiceAPI.DB
{
    /// <summary>
    /// Операции с БД
    /// </summary>
    public class SegmentRepository : IRepository
    {
        
        private SegmentsContext _context;
        /// <summary>
        /// Добавление билета в БД
        /// </summary>
        /// <param name="saleDTO"></param>
        public async Task CreateAsync(SaleDTO saleDTO)
        {
            _context = new SegmentsContext();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET LOCAL lock_timeout = '120s'");

                    var segment = saleDTO.Routes.Select((r, s) => new Segment
                    {
                        Name = saleDTO.Passenger.Name,
                        Surname = saleDTO.Passenger.Surname,
                        Patronymic = saleDTO.Passenger.Patronymic,
                        DocType = saleDTO.Passenger.DocType,
                        DocNumber = saleDTO.Passenger.DocNumber,
                        Birthdate = saleDTO.Passenger.Birthdate,
                        TicketNumber = saleDTO.Passenger.TicketNumber,
                        PassengerType = saleDTO.Passenger.PassengerType,
                        Gender = saleDTO.Passenger.Gender,
                        TicketType = saleDTO.Passenger.TicketType,
                        OperationType = saleDTO.OperationType,
                        OperationTime = saleDTO.OperationTime,
                        OperationPlace = saleDTO.OperationPlace,
                        AirlineCode = r.AirlineCode,
                        ArriveDatetime = r.ArriveDatetime,
                        ArrivePlace = r.ArrivePlace,
                        DepartDatetime = r.DepartDatetime,
                        DepartPlace = r.DepartPlace,
                        FlightNum = r.FlightNum,
                        PnrId = r.PnrId,
                        SerialId = s
                    });

                    await _context.Segments.AddRangeAsync(segment);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        /// <summary>
        /// Обновление статуса билета
        /// </summary>
        /// <param name="refundDTO"></param>
        public async Task UpdateAsync(RefundDTO refundDTO)
        {
          
            if(refundDTO == null)
            {
                throw new ArgumentNullException(nameof(refundDTO)); 
            }
               
            using (_context = new SegmentsContext())
            {
                var checkTicketNumber = _context.Segments.Where(r => r.TicketNumber == refundDTO.TicketNumber);
                if (!checkTicketNumber.Any())
                {
                    throw new ThisTicketDoesNotExist("Возвращаемого билета не существует");
                }

               var segments =  _context.Segments.Where(r => r.OperationType != "refund" && r.TicketNumber == refundDTO.TicketNumber);
               if (segments.Count() == 0)
               {
                   throw  new RepeatedReturnExeption("Повторный возврат билета");
               }

               foreach(var segment in segments)
                {
                    segment.OperationType = refundDTO.OperationType;
                    segment.OperationPlace = refundDTO.OperationPlace;
                    segment.OperationTime = refundDTO.OperationTime;
                }

                await _context.SaveChangesAsync();
            }
        }
 
        }
    }



      

