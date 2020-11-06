using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ApiRest.Support
{
    /// <summary>
    /// Validador de XML utilizando XSD
    /// </summary>
    public class RentXmlSchemaValidator : IRentXmlSchemaValidator
    {
        private readonly XmlSchemaSet schemas;
        /// <summary>
        /// Crea una instancia de XmlSchemaValidator
        /// </summary>
        /// <param name="schemaFile">Path absoluta del archivo XSD</param>
        public RentXmlSchemaValidator(string schemaFile)
        {
            schemas = new XmlSchemaSet();
            schemas.Add(null, schemaFile);
        }

        /// <summary>
        /// Valida el XML utilizando un XSD
        /// </summary>
        /// <param name="xml">Archivo XML a validar</param>
        /// <returns>Lista con los errores encontrados</returns>
        public string[] Validate(string xml)
        {
            var errors = new List<string>();

            try
            {
                XDocument.Parse(xml).Validate(schemas, (o, e) => errors.Add(e.Message));
            }
            catch (XmlException)
            {
                errors.Add(Constants.ValidationMessages.InvalidXml);
            }

            return errors.ToArray();
        }
    }
}
