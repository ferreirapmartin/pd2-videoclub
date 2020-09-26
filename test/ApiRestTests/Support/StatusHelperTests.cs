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
            var status = Constants.StatusName.DeliveryToRent;
            var expected = Status.DeliveryToRent;
            
            // Act
            var actual = StatusHelper.Parse(status);

            // Assert
            Assert.Equal(expected, actual);
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
            var status = Status.DeliveryToRent;
            var expected = Constants.StatusName.DeliveryToRent;

            // Act
            var actual = StatusHelper.Parse(status);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
