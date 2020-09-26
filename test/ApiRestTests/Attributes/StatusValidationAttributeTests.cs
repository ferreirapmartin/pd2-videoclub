using ApiRest.Attributes;
using ApiRest.Support;
using Moq;
using Xunit;

namespace ApiRestTests.Attributes
{
    public class StatusValidationAttributeTests
    {
        [Fact]
        public void ShouldUseStatusHelperToValidate()
        {
            // Assert
            var statusHelperMock = new Mock<StatusHelper>(MockBehavior.Strict);
            var attr = new StatusValidationAttribute(statusHelperMock.Object);
            var status = "Asd";
            var expected = false;
            statusHelperMock.Setup(i => i.IsValid(status)).Returns(expected);

            // Act 
            var actual = attr.IsValid(status);

            // Assert
            Assert.Equal(expected, actual);
            statusHelperMock.Verify(i => i.IsValid(status), Times.Once());
        }

        [Fact]
        public void ShouldBeValidWhenStatusIsNull()
        {
            // Assert
            var attr = new StatusValidationAttribute();
            object invalidStatus = null;

            // Act 
            var isValid = attr.IsValid(invalidStatus);

            // Assert
            Assert.True(isValid);
        }
    }
}
