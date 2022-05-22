using System.Net;
using TicketService;
using TicketService.model;
using TicketServiceAPI.BLL.DTO;

namespace TicketServiceAPI.repo
{
    /// <summary>
    /// Прослойка для работы с БД
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="refundDTO"></param>
        /// <returns>HTTP status</returns>

        public Task<HttpStatusCode> UpdateAsync(RefundDTO refundDTO);
        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="saleDTO"></param>
        /// <returns>HTTP status</returns>
        public Task<HttpStatusCode> CreateAsync(SaleDTO saleDTO);
    }
   
}
