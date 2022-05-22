using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using TicketServiceAPI.DataValid;
using System.Net;
using TicketServiceAPITests.Source;

namespace TicketServiceAPITests
{
    [TestFixture]
    public class ValidShemaTests
    {
        
        private static IFormFile _fileValidSale;
        private static IFormFile _fileInvalidSale;
        private static IFormFile _fileValidRefund;
        private static IFormFile _fileInvalidRefund;

        [SetUp]
        public void SetUp()
        {
            try
            {
                var streamValidSchemaSale = File.OpenRead(PathFileSource.pathValidSchemaSale);
                _fileValidSale = new FormFile(streamValidSchemaSale, 0, streamValidSchemaSale.Length, "", Path.GetFileName(PathFileSource.pathValidSchemaSale));

                var streamValidSchemaRefund = File.OpenRead(PathFileSource.PathValidSchemaRefund);
                _fileValidRefund = new FormFile(streamValidSchemaRefund, 0, streamValidSchemaRefund.Length, "", Path.GetFileName(PathFileSource.PathValidSchemaRefund));

                var streamInvalidShemaSale = File.OpenRead(PathFileSource.PathInvalidSchemaSale);
                _fileInvalidSale = new FormFile(streamInvalidShemaSale, 0, streamInvalidShemaSale.Length, "", Path.GetFileName(PathFileSource.PathInvalidSchemaRefund));

                var streamInvalidSchemaRefund = File.OpenRead(PathFileSource.PathInvalidSchemaRefund);
                _fileInvalidRefund = new FormFile(streamInvalidSchemaRefund, 0, streamInvalidSchemaRefund.Length, "", Path.GetFileName(PathFileSource.PathInvalidSchemaRefund));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task ShouldReturnHttpStatusOKIfValidSaleSchema()
        {
            try
            {
                ValidSchema schema = new ValidSchema(_fileValidSale);

                HttpStatusCode statusCode = await schema.ValidJsonSchemaSaleAsync();

                Assert.AreEqual(HttpStatusCode.OK, statusCode);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
               
        }

        [Test]
        public async Task ShouldReturnHttpStatusBadRequestIfInValidSaleSchema()
        {
            try
            {
                ValidSchema schema = new ValidSchema(_fileInvalidSale);

                HttpStatusCode statusCode = await schema.ValidJsonSchemaSaleAsync();

                Assert.AreEqual(HttpStatusCode.BadRequest, statusCode);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
              
           
        }

        [Test]
        public async Task ShouldReturnHttpStatusOKIfValidRefundSchema()
        {
            try
            {
                ValidSchema schema = new ValidSchema(_fileValidRefund);

                HttpStatusCode statusCode = await schema.ValidJsonSchemaRefundAsync();

                Assert.AreEqual(HttpStatusCode.OK, statusCode);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task ShouldReturnHttpStatusBadRequestIfInValidRefundSchema()
        {
            try
            {
                ValidSchema schema = new ValidSchema(_fileInvalidRefund);

                HttpStatusCode statusCode = await schema.ValidJsonSchemaRefundAsync();

                Assert.AreEqual(HttpStatusCode.BadRequest, statusCode);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
               
        }

    }
}
