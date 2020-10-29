using ApiRest.Support;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace ApiRestTests.Support
{
    public class RentXmlSchemaValidatorTests
    {
        private readonly RentXmlSchemaValidator xmlSchemaValidator;
        public RentXmlSchemaValidatorTests()
        {
            var xsdPath = GetFullPath(Path.Combine("Schemas", "rent_request.xsd"));
            xmlSchemaValidator = new RentXmlSchemaValidator(xsdPath);
        }
        private string GetFullPath(string relativePath)
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            return Path.Combine(dirPath, relativePath);
        }
        
        [Fact]
        public void ShouldValidationBeSuccessfulWithValidXml()
        {
            // Arrange
            var xml = @"<rent>
                           <object_id>b172a2ab-5900-4532-bd68-68a04115201a</object_id>
                           <client_id>5d65ac9e-d431-4138-a8e4-c4719203ab1b</client_id>
                           <details>
                               <status>RENTED</status>
                               <until>2020/01/30</until>
                           </details>
                        </rent>";

            // Act
            var errors = xmlSchemaValidator.Validate(xml);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldValidateObjectIdAsUUID()
        {
            // Arrange
            var fieldExpected = "object_id";
            var xml = @"<rent>
                           <object_id>b172a2ab-5900-4532-bd68-68a04115a</object_id>
                           <client_id>5d65ac9e-d431-4138-a8e4-c4719203ab1b</client_id>
                           <details>
                               <status>RENTED</status>
                               <until>2020/01/30</until>
                           </details>
                        </rent>";

            // Act
            var errors = xmlSchemaValidator.Validate(xml);

            // Assert
            Assert.Single(errors);
            Assert.Contains(fieldExpected, errors[0]);
        }

        [Fact]
        public void ShouldValidateClientIdAsUUID()
        {
            // Arrange
            var fieldExpected = "client_id";
            var xml = @"<rent>
                           <object_id>b172a2ab-5900-4532-bd68-68a04115201a</object_id>
                           <client_id>5d65ac9e-d431-4138-a8e4-c4719203ab1?</client_id>
                           <details>
                               <status>RENTED</status>
                               <until>2020/01/30</until>
                           </details>
                        </rent>";

            // Act
            var errors = xmlSchemaValidator.Validate(xml);

            // Assert
            Assert.Single(errors);
            Assert.Contains(fieldExpected, errors[0]);
        }

        [Fact]
        public void ShouldValidateStatusAsListValues()
        {
            // Arrange
            var fieldExpected = "status";
            var xml = @"<rent>
                           <object_id>b172a2ab-5900-4532-bd68-68a04115201a</object_id>
                           <client_id>5d65ac9e-d431-4138-a8e4-c4719203ab1b</client_id>
                           <details>
                               <status>RENTEDX</status>
                               <until>2020/01/30</until>
                           </details>
                        </rent>";

            // Act
            var errors = xmlSchemaValidator.Validate(xml);

            // Assert
            Assert.Single(errors);
            Assert.Contains(fieldExpected, errors[0]);
        }

        [Fact]
        public void ShouldValidateStatusAsDate()
        {
            // Arrange
            var fieldExpected = "until";
            var xml = @"<rent>
                           <object_id>b172a2ab-5900-4532-bd68-68a04115201a</object_id>
                           <client_id>5d65ac9e-d431-4138-a8e4-c4719203ab1b</client_id>
                           <details>
                               <status>RENTED</status>
                               <until>2020/0A/30</until>
                           </details>
                        </rent>";

            // Act
            var errors = xmlSchemaValidator.Validate(xml);

            // Assert
            Assert.Single(errors);
            Assert.Contains(fieldExpected, errors[0]);
        }

        [Fact]
        public void ShouldValidateThatDetailExists()
        {
            // Arrange
            var fieldExpected = "details";
            var xml = @"<rent>
                           <object_id>b172a2ab-5900-4532-bd68-68a04115201a</object_id>
                           <client_id>5d65ac9e-d431-4138-a8e4-c4719203ab1b</client_id>
                        </rent>";

            // Act
            var errors = xmlSchemaValidator.Validate(xml);

            // Assert
            Assert.Single(errors);
            Assert.Contains(fieldExpected, errors[0]);
        }
    }
}
