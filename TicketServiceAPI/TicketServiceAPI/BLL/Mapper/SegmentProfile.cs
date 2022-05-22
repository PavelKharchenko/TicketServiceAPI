using AutoMapper;
using TicketService;
using TicketService.model;
using TicketServiceAPI.BLL.DTO;

namespace TicketServiceAPI.BLL.Mapper
{

    public class SegmentProfile : Profile
    {
        public SegmentProfile()
        {
            CreateMap<Passenger, PassengerDTO>();
            CreateMap<Sale, SaleDTO>();
            CreateMap<Routes, RoutesDTO>();
            CreateMap<Refund, RefundDTO>();
        }
    }
}
