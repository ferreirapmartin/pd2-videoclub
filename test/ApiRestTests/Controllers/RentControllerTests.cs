using ApiRest.Controllers;
using ApiRest.Messages;
using ApiRest.Support;
using AutoMapper;
using DataAccess.Contexts;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiRestTests.Controllers
{
    public class RentControllerTests
    {
        private Mock<IServiceProvider> serviceProviderMock;
        private Mock<IMapper> mapperMock;
        private Mock<IUrlHelper> urlHelperMock;
        private Mock<StatusHelper> statusHelperMock;
        private Mock<IRentXmlSchemaValidator> xmlSchemaValidatorMock;
        private Mock<HttpRequest> requestMock;
        private Mock<IVideoclubDbContext> videoclubDbContextMock;
        private HttpContext httpContextMock;
        private ControllerContext controllerContext;
        private RentController controller;

        public RentControllerTests()
        {
            serviceProviderMock = new Mock<IServiceProvider>();
            mapperMock = new Mock<IMapper>();
            urlHelperMock = new Mock<IUrlHelper>(MockBehavior.Strict);
            statusHelperMock = new Mock<StatusHelper>();
            xmlSchemaValidatorMock = new Mock<IRentXmlSchemaValidator>();
            requestMock = new Mock<HttpRequest>();
            httpContextMock = Mock.Of<HttpContext>(_ => _.Request == requestMock.Object);
            controllerContext = new ControllerContext() { HttpContext = httpContextMock };
            videoclubDbContextMock = new Mock<IVideoclubDbContext>();
            serviceProviderMock.Setup(i => i.GetService(typeof(IVideoclubDbContext))).Returns(videoclubDbContextMock.Object);
            controller = new RentController(serviceProviderMock.Object, mapperMock.Object, xmlSchemaValidatorMock.Object)
            {
                ControllerContext = controllerContext,
                Url = urlHelperMock.Object
            };
        }


        [Fact]
        public async Task ShouldReturnNotFoundIfRentNotExists()
        {
            // Arrange
            var rents = new List<Rent>();
            var rentsMock = rents.AsQueryable().BuildMockDbSet();
            videoclubDbContextMock.Setup(i => i.Rents).Returns(rentsMock.Object);

            //Act
            var response = await controller.Get(Guid.NewGuid(), Guid.NewGuid()).ConfigureAwait(false);

            //Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task ShouldReturnRentIfRentExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var rent1 = new Rent(productId, clientId, Status.DeliveryToRent, DateTime.Now);
            var rent2 = new Rent(Guid.NewGuid(), Guid.NewGuid(), Status.Rentend, null);
            var rents = new List<Rent>() { rent2, rent1 };
            var rentsMock = rents.AsQueryable().BuildMockDbSet();
            var responseExpected = new RentResponse()
            {
                ClientId = clientId,
                ProductId = productId
            };
            videoclubDbContextMock.Setup(i => i.Rents).Returns(rentsMock.Object);
            mapperMock.Setup(i => i.Map<RentResponse>(rent1)).Returns(responseExpected);

            //Act
            var response = await controller.Get(productId, clientId).ConfigureAwait(false);

            //Assert
            var responseActual = Assert.IsType<OkObjectResult>(response);
            Assert.Same(responseExpected, responseActual.Value);
        }
    }
}
