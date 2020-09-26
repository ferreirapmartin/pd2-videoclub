using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration
{
    public class RentETC : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.HasKey(i => new { i.ProductId, i.ClientId});

            builder.Property(i => i.Status).IsRequired();
        }
    }
}
