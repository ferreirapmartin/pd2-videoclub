using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ApiRest.Messages
{
    /// <summary>
    /// Response de Rent
    /// </summary>
    [XmlRoot("rent")]
    public class RentResponse
    {

        /// <summary>
        /// Id del Producto
        /// </summary>
        [XmlElement("object_id")]
        [JsonPropertyName("objectId")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Id del cliente
        /// </summary>
        [XmlElement("client_id")]
        public Guid ClientId { get; set; }

        /// <summary>
        /// Detalle
        /// </summary>
        [XmlElement("details")]
        public RentDetails Details { get; set; } = new RentDetails();

        /// <summary>
        /// Representa el detalle
        /// </summary>
        public class RentDetails
        {
            /// <summary>
            /// Estado
            /// </summary>
            [XmlElement("status")]
            public string Status { get; set; }

            /// <summary>
            /// Fecha hasta
            /// </summary>
            [XmlElement("until")]
            public string Until { get; set; }
        }
    }
}