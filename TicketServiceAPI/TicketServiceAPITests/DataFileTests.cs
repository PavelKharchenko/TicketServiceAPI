using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using TicketService;
using TicketService.model;
using TicketServiceAPITests.Source;

namespace TicketServiceAPITests
{
    [TestFixture]
    public class DataFileTests
    {
        private IFormFile _sale;
        private IFormFile _refund;
        [SetUp]
        public void SetUp()
        {
            var streamSale = File.OpenRead(PathFileSource.pathValidSchemaSale);
            var streamRefund = File.OpenRead(PathFileSource.PathValidSchemaRefund);

            _sale = new FormFile(streamSale, 0, streamSale.Length, "", Path.GetFileName(PathFileSource.pathValidSchemaSale));
            _refund = new FormFile(streamRefund,0,streamRefund.Length,"",Path.GetFileName(PathFileSource.PathValidSchemaRefund));
        }


        [Test]
        public void ShouldGetDataSaleReturnTypeModelSale()
        {
            try
            {
                DataFile data = new DataFile(_sale);

                var result = data.GetDataSale();

               Assert.That(result.GetType(), Is.EqualTo(typeof(Sale)));

            }catch (Exception ex)
            {
                Assert.Fail(ex.Message);    
            }
        }

        [Test]
        public void ShouldGetDataRefundReturnTypeModelRefund()
        {
            try
            {
                DataFile data = new DataFile(_refund);

                var result = data.GetDataRefund();

                Assert.That(result.GetType(), Is.EqualTo(typeof(Refund)));

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}