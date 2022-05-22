using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AutoMapper;
using TicketServiceAPI.BLL.Mapper;
using TicketService.model;
using TicketServiceAPI.BLL.DTO;
using TicketServiceAPITests.Source;
using Microsoft.AspNetCore.Http;
using TicketService;

namespace TicketServiceAPITests
{
    [TestFixture]
    public class SegmentProfileTests
    {
        private IFormFile _fileSale;
        private IFormFile _fileRefund;
        private IMapper _mapper;
        [SetUp]
        public void SetUp()
        {
            var streamObjectSale = File.OpenRead(PathFileSource.pathValidSchemaSale);
            _fileSale = new FormFile(streamObjectSale,0,streamObjectSale.Length,"",Path.GetFileName(PathFileSource.pathValidSchemaSale));

            var streamObjectRefund = File.OpenRead(PathFileSource.PathValidSchemaRefund);
            _fileRefund = new FormFile(streamObjectRefund, 0, streamObjectRefund.Length, "", Path.GetFileName(PathFileSource.PathValidSchemaRefund));
        }

        [Test]
        public void ShouldConfigureMapperIsValid()
        {
            var conf = new MapperConfiguration(cfg => cfg.AddProfile<SegmentProfile>());

            conf.AssertConfigurationIsValid();

        }

        [Test]
        public void ShouldObjectValuesSaleDTOAreEqualToObjectValuesSale()
        {
            try
            {
                DataFile data = new DataFile(_fileSale);

                var sale = data.GetDataSale();

                var conf = new MapperConfiguration(cfg => cfg.AddProfile<SegmentProfile>());

                _mapper = conf.CreateMapper();

                var saleDTO = _mapper.Map<Sale, SaleDTO>(sale);
                Assert.NotNull(saleDTO);

                Assert.AreEqual(saleDTO.OperationType,sale.OperationType);
                Assert.AreEqual(saleDTO.Passenger.Name, sale.Passenger.Name);   
                Assert.AreEqual(saleDTO.Passenger.TicketNumber, sale.Passenger.TicketNumber);
                Assert.AreEqual(saleDTO.Routes[0].AirlineCode, sale.Routes[0].AirlineCode);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
           
        }

        [Test]
        public void ShouldObjectValuesRefundDTOAreEqualToObjectValuesRefudn()
        {
            try
            {
                DataFile data = new DataFile(_fileRefund);

                var refund = data.GetDataRefund();

                var conf = new MapperConfiguration(cfg => cfg.AddProfile<SegmentProfile>());

                _mapper = conf.CreateMapper();

                var refundDTO = _mapper.Map<Refund, RefundDTO>(refund);
                Assert.NotNull(refundDTO);

                Assert.AreEqual(refundDTO.TicketNumber,refund.TicketNumber);
                Assert.AreEqual(refundDTO.OperationPlace,refund.OperationPlace);
                Assert.AreEqual(refundDTO.OperationType,refundDTO.OperationType);
            
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}
