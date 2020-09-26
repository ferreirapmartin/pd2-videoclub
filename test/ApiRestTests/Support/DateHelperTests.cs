using ApiRest.Support;
using System;
using Xunit;

namespace ApiRestTests.Support
{
    public class DateHelperTests
    {
        [Fact]
        public void ShouldBeValidWithCorrectFormat()
        {
            // Arrange
            var dateHelper = new DateHelper();
            var date = new DateTime(2020, 1, 1);
            var dateStr = date.ToString(DateHelper.Format);
            var expected = true;

            // Act
            var actual = dateHelper.IsValid(dateStr);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeInvalidWithWrongFormat()
        {
            // Arrange
            var dateHelper = new DateHelper();
            var date = new DateTime(2020, 1, 1);
            var dateStr = date.ToLongDateString();
            var expected = false;

            // Act
            var actual = dateHelper.IsValid(dateStr);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeInvalidWithWrongDate()
        {
            // Arrange
            var dateHelper = new DateHelper();
            var dateStr = "NoEsUnaFecha132";
            var expected = false;

            // Act
            var actual = dateHelper.IsValid(dateStr);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeInvalidWithNull()
        {
            // Arrange
            var dateHelper = new DateHelper();
            var dateStr = (string)null;
            var expected = false;

            // Act
            var actual = dateHelper.IsValid(dateStr);

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ShouldParseSuccessfullyWhenDateIsCorrect()
        {
            // Arrange
            var expected = new DateTime(2020, 7, 11);
            var dateStr = expected.ToString(DateHelper.Format);

            // Act
            var actual = DateHelper.Parse(dateStr);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowAnInvalidCastExceptionWhenDateIsNotCorrect()
        {
            // Arrange
            var dateStr = "No es una fecha";

            // Act
            var exp = Record.Exception(() => DateHelper.Parse(dateStr));

            // Assert
            Assert.NotNull(exp);
            Assert.IsType<InvalidCastException>(exp);
            Assert.Equal(Constants.ExceptionsMessages.InvalidCastDate, exp.Message);
        }

        [Fact]
        public void ShouldThrowAnArgumentNullExceptionWhenDateIsNull()
        {
            // Arrange
            var dateStr = (string)null;

            // Act
            var exp = Record.Exception(() => DateHelper.Parse(dateStr));

            // Assert
            Assert.NotNull(exp);
            Assert.IsType<ArgumentNullException>(exp);
        }
    }
}
