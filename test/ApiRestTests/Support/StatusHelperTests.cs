using ApiRest.Support;
using Domain.Entities;
using System;
using Xunit;

namespace ApiRestTests.Support
{
    public class StatusHelperTests
    {
        [Fact]
        public void ShouldBeValidWithCorrectStatus()
        {
            // Arrange
            var statusHelper = new StatusHelper();
            var status = Constants.StatusName.DeliveryToRent;
            var expected = true;

            // Act
            var actual = statusHelper.IsValid(status);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeInvalidWithWrongStatus()
        {
            // Arrange
            var statusHelper = new StatusHelper();
            var status = "Estado erroneo 123";
            var expected = false;

            // Act
            var actual = statusHelper.IsValid(status);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeInvalidWithNull()
        {
            // Arrange
            var statusHelper = new StatusHelper();
            var dateStr = (string)null;
            var expected = false;

            // Act
            var actual = statusHelper.IsValid(dateStr);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldParseSuccessfullyWhenStatusIsCorrect()
        {
            // Arrange
            var statusDeliveryToRent = Constants.StatusName.DeliveryToRent;
            var statusDeliveryToReturn = Constants.StatusName.DeliveryToReturn;
            var statusRented = Constants.StatusName.Rented;
            var statusReturn = Constants.StatusName.Return;
            var expectedDeliveryToRent = Status.DeliveryToRent;
            var expectedDeliveryToReturn = Status.DeliveryToReturn;
            var expectedRentend = Status.Rentend;
            var expectedReturn = Status.Return;

            // Act
            var actualDeliveryToRent = StatusHelper.Parse(statusDeliveryToRent);
            var actualStatusDeliveryToReturn = StatusHelper.Parse(statusDeliveryToReturn);
            var actualStatusRented = StatusHelper.Parse(statusRented);
            var actualStatusReturn = StatusHelper.Parse(statusReturn);

            // Assert
            Assert.Equal(expectedDeliveryToRent, actualDeliveryToRent);
            Assert.Equal(expectedDeliveryToReturn, actualStatusDeliveryToReturn);
            Assert.Equal(expectedRentend, actualStatusRented);
            Assert.Equal(expectedReturn, actualStatusReturn);
        }

        [Fact]
        public void ShouldThrowAnInvalidCastExceptionWhenStatusIsNotCorrect()
        {
            // Arrange
            var dateStr = "No es un estado";

            // Act
            var exp = Record.Exception(() => StatusHelper.Parse(dateStr));

            // Assert
            Assert.NotNull(exp);
            Assert.IsType<InvalidCastException>(exp);
            Assert.Equal(Constants.ExceptionsMessages.InvalidCastStatus, exp.Message);
        }

        [Fact]
        public void ShouldThrowAnArgumentNullExceptionWhenStatusIsNull()
        {
            // Arrange
            var status = (string)null;

            // Act
            var exp = Record.Exception(() => StatusHelper.Parse(status));

            // Assert
            Assert.NotNull(exp);
            Assert.IsType<ArgumentNullException>(exp);
        }

        [Fact]
        public void ShouldParseToStringSuccessfullyWhenIsKnownStatus()
        {
            // Arrange
            var statusDeliveryToRent = Status.DeliveryToRent;
            var statusDeliveryToReturn = Status.DeliveryToReturn;
            var statusRentend = Status.Rentend;
            var statusReturn = Status.Return;
            var expectedDeliveryToRent = Constants.StatusName.DeliveryToRent;
            var expectedDeliveryToReturn = Constants.StatusName.DeliveryToReturn;
            var expectedRented = Constants.StatusName.Rented;
            var expectedReturn = Constants.StatusName.Return;

            // Act
            var actualStatusDeliveryToRent = StatusHelper.Parse(statusDeliveryToRent);
            var actualStatusDeliveryToReturn = StatusHelper.Parse(statusDeliveryToReturn);
            var actualStatusRentend = StatusHelper.Parse(statusRentend);
            var actualstatusReturn = StatusHelper.Parse(statusReturn);

            // Assert
            Assert.Equal(expectedDeliveryToRent, actualStatusDeliveryToRent);
            Assert.Equal(expectedDeliveryToReturn, actualStatusDeliveryToReturn);
            Assert.Equal(expectedRented, actualStatusRentend);
            Assert.Equal(expectedReturn, actualstatusReturn);
        }

        [Fact]
        public void ShouldThrowAnInvalidCastExceptionWhenStatusIsInvalid()
        {
            // Arrange
            var invalidStatus = (Status)int.MaxValue;
            var messageExpected = Constants.ExceptionsMessages.InvalidCastStatus;

            // Act
            var exp = Record.Exception(() => StatusHelper.Parse(invalidStatus));

            // Assert
            Assert.NotNull(exp);
            Assert.IsType<InvalidCastException>(exp);
            Assert.Equal(messageExpected, exp.Message);
        }
    }
}
