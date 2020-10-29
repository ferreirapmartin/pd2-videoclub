using ApiRest.Controllers;
using ApiRest.Support;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using System;
using Xunit;

namespace ApiRestTests.Support
{
    public class UrlUtlsTest
    {
        [Fact]
        public void ShouldRemoveControllerSuffix()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var mapperMock = new Mock<IMapper>();
            var urlHelperMock = new Mock<IUrlHelper>(MockBehavior.Strict);
            var statusHelperMock = new Mock<StatusHelper>();
            var xmlSchemaValidatorMock = new Mock<IRentXmlSchemaValidator>();
            var requestMock = new Mock<HttpRequest>();
            var httpContextMock = Mock.Of<HttpContext>(_ => _.Request == requestMock.Object);
            var controllerContext = new ControllerContext() { HttpContext = httpContextMock };
            var values = new { };
            var actionName = "GetAll";
            var controllerName = "Rent";
            var scheme = "http";

            requestMock.Setup(x => x.Scheme).Returns(scheme);
            urlHelperMock.Setup(url => url.Action(It.IsAny<UrlActionContext>())).Returns("mock/url");

            var controller = new RentController(serviceProviderMock.Object, mapperMock.Object, xmlSchemaValidatorMock.Object)
            {
                ControllerContext = controllerContext,
                Url = urlHelperMock.Object
            };

            //Act
            UrlUtls.URI(controller, "RentController", "GetAll", values);

            //Assert
            urlHelperMock.Verify(url => url.Action(It.Is<UrlActionContext>(i => i.Action == actionName && i.Controller == controllerName && i.Protocol == scheme)), Times.Once());
        }
    }
}
