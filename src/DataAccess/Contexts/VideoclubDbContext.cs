using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataAccess.Contexts
{
    public class VideoclubDbContext : DbContext, IVideoclubDbContext
    {
        private readonly IConfiguration configuration;

        public VideoclubDbContext(IConfiguration configuration) : base()
        {
            this.configuration = configuration;
        }
        public DbSet<Rent> Rents { get; set; }

        public void EnsureCreated() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                 .EnableSensitiveDataLogging()
                 .UseSqlServer(configuration.GetConnectionString("VideoclubConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(Configuration.RentETC)));
            base.OnModelCreating(modelBuilder);
        }
    }
}
