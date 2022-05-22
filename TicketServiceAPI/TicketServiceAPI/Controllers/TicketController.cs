
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketService.model;
using TicketServiceAPI.BLL.DTO;
using TicketServiceAPI.BLL.ValidationModel;
using TicketServiceAPI.DataValid;
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
    public class TicketController : ControllerBase
    {

        private readonly IValidator<Sale> _validatorSale;
        private readonly IValidator<Refund> _refundValidation;
        private readonly SegmentRepository _segmentRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контролера Ticket
        /// </summary>
        /// <param name="mapper"></param>
        public TicketController(IMapper mapper,IValidator<Sale> validatorSale,IValidator<Refund> validatorRefund)
        {
            _mapper = mapper;
            _validatorSale = validatorSale;
            _refundValidation = validatorRefund;

        
        }

        /// <summary>
        /// Покупка билета
        /// </summary>
        /// <param name="uploadedFile"> Файл с описанием билета </param>
        /// <returns>Результат работы в виде http status code</returns>
        /// <response code="200">Билет добавлен в базу данных</response>
        /// <response code="400">Файл не прошел валидацию на соответсвие JSON schema</response> 
        /// <response code="413">Размер файла больше 2 КБ</response>
        ///  <response code="409">Покупаемый билет уже есть в базе данных</response>
        ///<response code = "500" > Ошибка в работе сервера</response>

        [HttpPost]
        public async Task<IActionResult> Sale(IFormFile uploadedFile)
        {

                ValidSchema schema = new ValidSchema(uploadedFile);

                HttpStatusCode statusCode = await schema.ValidJsonSchemaSaleAsync();

                if (statusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest();
                }

                DataFile dataFile = new DataFile(uploadedFile);
                Sale sale = dataFile.GetDataSale();

                var validationStatus = _validatorSale.Validate(sale);
                if (!validationStatus.IsValid)
                {
                     return BadRequest();
                }

               var saleDTO = _mapper.Map<Sale,SaleDTO>(sale);  

                SegmentRepository operation = new SegmentRepository();
                statusCode = await operation.CreateAsync(saleDTO);

                if (statusCode == HttpStatusCode.RequestTimeout)
                {
                    return StatusCode(StatusCodes.Status408RequestTimeout);
                }
                if (statusCode == HttpStatusCode.InternalServerError)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                if (statusCode == HttpStatusCode.Conflict)
                {
                    return Conflict();
                }
                return Ok();
           
        }


        /// <summary>
        /// Возврат билета
        /// </summary>
        /// <param name="uploadedFile"> Файл с информацией для возврата билета </param>
        /// <returns>Результат работы в виде http status code</returns>
        /// <response code="200">Совершен возврат билета (пометка в базе данных)</response>
        /// <response code="400">Файл не прошел валидацию на соответсвие JSON schema</response> 
        /// <response code="413">Размер файла больше 2 КБ</response>
        ///  <response code="409">Повторный возврат билета</response>
        ///  <response code="409">В передаваемом файле отсутсвует номер билета</response>
        ///  <response code="500">Ошибка в работе сервера</response>

        [HttpPost]
        public async Task<IActionResult> Refund(IFormFile uploadedFile)
        {
           
                ValidSchema schema = new ValidSchema(uploadedFile);
                HttpStatusCode statusCode = await schema.ValidJsonSchemaRefundAsync();

                if (statusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest();
                }

                DataFile dataFile = new DataFile(uploadedFile);

                Refund refund = dataFile.GetDataRefund();
                
                 var valideStatus = _refundValidation.Validate(refund);
                if (!valideStatus.IsValid)
                {
                    return BadRequest();
                }

                var refundDTO = _mapper.Map<Refund,RefundDTO>(refund);

                SegmentRepository operation = new SegmentRepository();

                statusCode = await operation.UpdateAsync(refundDTO);

                if (statusCode == HttpStatusCode.Conflict)
                {
                    return Conflict();
                }
                if (statusCode == HttpStatusCode.InternalServerError)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return Ok();
            }
        }
    }



