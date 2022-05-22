using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using TicketService.model;
using TicketServiceAPI.BLL.ValidationModel;
using TicketServiceAPITests.Source;

namespace TicketServiceAPITests
{
    [TestFixture]
    public class ValidatorsTests
    {
        private static IFormFile _fileValidSale;
        private static IFormFile _fileValidRefund;

        private SaleValidator _saleValidator;
        private RefundValidator _refundValidator;

        private string _errorOperationTypeRefund = "sale";
  


        [SetUp]
        public void SetUp()
        {
            var streamValidSale = File.OpenRead(PathFileSource.pathValidSchemaSale);
            _fileValidSale = new FormFile(streamValidSale, 0, streamValidSale.Length, "", Path.GetFileName(PathFileSource.pathValidSchemaSale));

            var streamValidRefund = File.OpenRead(PathFileSource.PathValidSchemaRefund);
            _fileValidRefund = new FormFile(streamValidRefund, 0, streamValidRefund.Length, "", Path.GetFileName(PathFileSource.PathValidSchemaRefund));

            _saleValidator = new SaleValidator();
            _refundValidator = new RefundValidator();
        }

        [Test]
        public void ShouldReturnTrueIfValidObjectSale()
        {
            try
            {
                DataFile data = new DataFile(_fileValidSale);

                var sale = data.GetDataSale();

                var result = _saleValidator.Validate(sale);

                Assert.IsTrue(result.IsValid);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void ShouldReturnIfValidObjectRefund()
        {
            try
            {
                DataFile data = new DataFile(_fileValidRefund);

                var refund = data.GetDataRefund();

                var result = _refundValidator.Validate(refund);

                Assert.IsTrue(result.IsValid);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void ShouldHaveErrorWhenOperationTypeNotRefund()
        {
            DataFile data = new DataFile(_fileValidRefund);

            var refund = data.GetDataRefund();

            refund.OperationType = _errorOperationTypeRefund;

            var result = _refundValidator.Validate(refund);

            Assert.IsFalse(result.IsValid);
        }
    }
}
 


