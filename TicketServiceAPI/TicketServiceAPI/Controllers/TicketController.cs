
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketService.model;
using TicketServiceAPI.BLL.DTO;
using TicketServiceAPI.BLL.ShemaValid;
using TicketServiceAPI.DB;


namespace TicketService.Controllers
{
    /// <summary>
    /// Обработка билетов
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    [RequestSizeLimit(2048)]
    [ValidateModel]
    public class TicketController : ControllerBase
    {

    
      
        private readonly SegmentRepository _segmentRepository;
        private readonly IMapper _mapper;

     
        public TicketController(IMapper mapper,SegmentRepository segmentRepository)
        {
            _mapper = mapper;
            _segmentRepository = segmentRepository; 
            

        
        }

        /// <summary>
        /// Покупка билета
        /// </summary>
        /// <param name="sale"></param>
        /// <returns>Результат работы в виде http status code</returns>
        /// <response code="200">Билет добавлен в базу данных</response>
        /// <response code="400">Файл не прошел валидацию на соответсвие JSON schema</response> 
        /// <response code="413">Размер файла больше 2 КБ</response>
        ///  <response code="409">Покупаемый билет уже есть в базе данных</response>
        ///<response code = "500" > Ошибка в работе сервера</response>

        [HttpPost]
        public async Task<IActionResult> Sale([FromBody] Sale sale)
        {
            var saleDTO = _mapper.Map<Sale,SaleDTO>(sale);

            await _segmentRepository.CreateAsync(saleDTO);

            return Ok();
           
        }


        /// <summary>
        /// Возврат билета
        /// </summary>
        /// <returns>Результат работы в виде http status code</returns>
        /// <response code="200">Совершен возврат билета (пометка в базе данных)</response>
        /// <response code="400">Файл не прошел валидацию на соответсвие JSON schema</response> 
        /// <response code="413">Размер файла больше 2 КБ</response>
        ///  <response code="409">Повторный возврат билета</response>
        ///  <response code="409">В передаваемом файле отсутсвует номер билета</response>
        ///  <response code="500">Ошибка в работе сервера</response>

        [HttpPost]
        public async Task<IActionResult> Refund([FromBody] Refund refund)
        {

               var refundDTO = _mapper.Map<Refund,RefundDTO>(refund);
               await _segmentRepository.UpdateAsync(refundDTO);

                return Ok();
            }
        }
    }



