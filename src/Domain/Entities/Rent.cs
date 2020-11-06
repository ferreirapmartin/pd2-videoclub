using System;

namespace Domain.Entities
{
    /// <summary>
    /// Representa una Renta
    /// </summary>
    public class Rent
    {
        /// <summary>
        /// Constructor necesario para Entity Framework funcione
        /// </summary>
        private Rent() { }

        /// <summary>
        /// Crea una renta
        /// </summary>
        /// <param name="productId">Id de Producto</param>
        /// <param name="clientId">Id del Cliente</param>
        /// <param name="status">Status</param>
        /// <param name="until">Fecha hasta</param>
        public Rent(Guid productId, Guid clientId, Status status, DateTime? until) : this()
        {
            ProductId = productId;
            ClientId = clientId;
            Status = status;
            Until = until;
        }

        /// <summary>
        /// Id del Producto alquilado
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Id del Cliente
        /// </summary>
        public Guid ClientId { get; private set; }

        /// <summary>
        /// Estado 
        /// </summary>
        public Status Status { get; private set; }

        /// <summary>
        /// Fecha hasta
        /// </summary>
        public DateTime? Until { get; private set; }

        public void ChangeStatus(Status status, DateTime? until)
        {
            Status = status;
            Until = until;
        }
    }
}
