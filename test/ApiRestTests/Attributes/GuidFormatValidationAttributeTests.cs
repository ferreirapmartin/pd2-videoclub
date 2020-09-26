using ApiRest.Attributes;
using Xunit;

namespace ApiRestTests.Attributes
{
    public class GuidFormatValidationAttributeTests
    {
        [Fact]
        public void ShouldBeValidWhenGuidIsCorrect()
        {
            // Arrange
            var attr = new GuidFormatValidationAttribute();
            var guidStr = "b172a2ab-5900-4532-bd68-68a041752017";
            var expected = true;
            
            // Act
            var actual = attr.IsValid(guidStr);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeInvalidWhenGuidIsNotCorrect()
        {
            // Arrange
            var attr = new GuidFormatValidationAttribute();
            var invalidGuidStr = "b172a2ab-5900-4532-bd68_68a041752017";
            var expected = false;

            // Act
            var actual = attr.IsValid(invalidGuidStr);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeInvalidWhenGuidIsEmpty()
        {
            // Arrange
            var attr = new GuidFormatValidationAttribute();
            var emptyGuidStr = "00000000-0000-0000-0000-000000000000";
            var expected = false;

            // Act
            var actual = attr.IsValid(emptyGuidStr);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeValidWhenGuidIsNull()
        {
            // Arrange
            var attr = new GuidFormatValidationAttribute();
            var value = (object)null;
            var expected = true;

            // Act
            var actual = attr.IsValid(value);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
