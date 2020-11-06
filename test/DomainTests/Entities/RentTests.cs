using Domain.Entities;
using System;
using Xunit;

namespace DomainTests.Entities
{
    public class RentTests
    {
        [Fact]
        public void ShouldCreateRent()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var status = Status.DeliveryToReturn;
            var until = new DateTime();

            // Act
            var rent = new Rent(productId, clientId, status, until);

            // Assert
            Assert.Equal(productId, rent.ProductId);
            Assert.Equal(clientId, rent.ClientId);
            Assert.Equal(status, rent.Status);
            Assert.Equal(until, rent.Until);
        }

        [Fact]
        public void ShouldChangeStatus()
        {
            // Arrange
            var status = Status.DeliveryToReturn;
            var until = new DateTime(2020, 01, 01);
            var rent = new Rent(Guid.NewGuid(), Guid.NewGuid(), status, until);
            var statusExpected = Status.Return;
            var untilExpected = new DateTime();

            // Act
            rent.ChangeStatus(statusExpected, untilExpected);

            // Assert
            Assert.Equal(statusExpected, rent.Status);
            Assert.Equal(untilExpected, rent.Until);
        }
    }
}
