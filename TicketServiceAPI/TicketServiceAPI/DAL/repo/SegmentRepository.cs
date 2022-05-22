using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using TicketService;
using TicketService.model;
using TicketServiceAPI.BLL.DTO;
using TicketServiceAPI.repo;

namespace TicketServiceAPI.DB
{
    /// <summary>
    /// Операции с БД
    /// </summary>
    public class SegmentRepository : IRepository
    {
        private const int _zeroRows = 0;
        private static IConfiguration? _conf;
        private static string? _updateOperationType;
        private static string? _lockTimeout;    
        public static void InitSegmentRepository(IConfiguration conf)
        {
            _conf = conf;
            _updateOperationType = conf["UpdateOperationType"];
            _lockTimeout = conf["LockTimeout"];
        }

        private SegmentsContext _context;
        /// <summary>
        /// Добавление билета в БД
        /// </summary>
        /// <param name="saleDTO"></param>
        /// <returns>статус операции</returns>
        public async Task<HttpStatusCode> CreateAsync(SaleDTO saleDTO)
        {

           try
           {
                _context = new SegmentsContext();
                
                    using (var transaction = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                     _context.Database.ExecuteSqlRaw(_lockTimeout);

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
                            catch(DbUpdateException  ex)
                           {
                                if (ex.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                                {
                                    transaction.Rollback();
                                    return HttpStatusCode.Conflict;
                                }
                                transaction.Rollback();
                                 return HttpStatusCode.InternalServerError;
                            }catch(PostgresException ex)
                            {
                                if(ex.SqlState == PostgresErrorCodes.QueryCanceled)
                                {
                                    transaction.Rollback();
                                    return HttpStatusCode.RequestTimeout;
                                }
                            }
                            catch (Exception)
                            {
                                return HttpStatusCode.InternalServerError;  
                            }
                        }
                
            }
            catch (Exception)
           {
                return HttpStatusCode.InternalServerError;
           }
            
               
            return HttpStatusCode.OK;
        }
          
               
              
            
        /// <summary>
        /// Обновление статуса билета
        /// </summary>
        /// <param name="refundDTO"></param>
        /// <returns>результат операции</returns>
        public async Task<HttpStatusCode> UpdateAsync(RefundDTO refundDTO)
        {
           try
         {
                
                using (_context = new SegmentsContext())
               {
                int countRows = _context.Database.ExecuteSqlRaw(_updateOperationType + refundDTO.TicketNumber);

                    if (countRows == _zeroRows)
                    {
                        return HttpStatusCode.Conflict;
                    }
                }
           }
          catch (Exception)
           {
              return HttpStatusCode.InternalServerError;
         }
            
            return HttpStatusCode.OK;
        }
    }
}



      

