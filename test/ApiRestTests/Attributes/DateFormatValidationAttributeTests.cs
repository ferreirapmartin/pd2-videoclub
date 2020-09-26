using ApiRest.Attributes;
using ApiRest.Support;
using Moq;
using Xunit;

namespace ApiRestTests.Attributes
{
    public class DateFormatValidationAttributeTests
    {
        [Fact]
        public void ShouldUseDateHelperToValidate()
        {
            // Assert
            var dateHelperMock = new Mock<DateHelper>(MockBehavior.Strict);
            var attr = new DateFormatValidationAttribute(dateHelperMock.Object);
            var dateStr = "2020/10/10";
            var expected = false;
            dateHelperMock.Setup(i => i.IsValid(dateStr)).Returns(expected);

            // Act 
            var actual = attr.IsValid(dateStr);

            // Assert
            Assert.Equal(expected, actual);
            dateHelperMock.Verify(i => i.IsValid(dateStr), Times.Once());
        }

        [Fact]
        public void ShouldBeValidWhenDateIsNull()
        {
            // Assert
            var attr = new DateFormatValidationAttribute();
            var date = (object)null;

            // Act 
            var isValid = attr.IsValid(date);

            // Assert
            Assert.True(isValid);
        }
    }
}
