using ApiRest.Attributes;
using ApiRest.Support;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ApiRest.Messages
{
    /// <summary>
    /// Request de Rent
    /// </summary>
    [XmlRoot("rent")]
    public class RentRequest
    {
        /// <summary>
        /// Id del Producto
        /// </summary>
        [XmlElement("object_id")]
        [JsonPropertyName("objectId")]
        [GuidFormatValidation]
        [Required(ErrorMessage = Constants.ValidationMessages.Required, AllowEmptyStrings = false)]
        public string ProductId { get; set; }

        /// <summary>
        /// Id del cliente
        /// </summary>
        [XmlElement("client_id")]
        [GuidFormatValidation]
        [Required(ErrorMessage = Constants.ValidationMessages.Required, AllowEmptyStrings = false)]
        public string ClientId { get; set; }

        /// <summary>
        /// Detalle
        /// </summary>
        [XmlElement("details")]
        public RentDetails Details { get; set; }

        /// <summary>
        /// Representa el detalle
        /// </summary>
        public class RentDetails
        {
            /// <summary>
            /// Estado
            /// </summary>
            [XmlElement("status")]
            [StatusValidation]
            [Required(ErrorMessage = Constants.ValidationMessages.Required, AllowEmptyStrings = false)]
            public string Status { get; set; }
            
            /// <summary>
            /// Fecha hasta
            /// </summary>
            [XmlElement("until")]
            [DateFormatValidation]
            public string Until { get; set; }
        }
    }
}
