using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace DataAccess.Contexts
{
    public class VideoclubDbContext : DbContext, IVideoclubDbContext
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<VideoclubDbContext> logger;

        public VideoclubDbContext(IConfiguration configuration, ILogger<VideoclubDbContext> logger) : base()
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        public DbSet<Rent> Rents { get; set; }

        public void EnsureCreated() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = Environment.GetEnvironmentVariable("ENV_HOST") ?? "localhost";
            var conn = string.Format(configuration.GetConnectionString("VideoclubConnection"), host);
            
            optionsBuilder
                 .EnableSensitiveDataLogging(true)
                 .UseSqlServer(conn);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(Configuration.RentETC)));
            base.OnModelCreating(modelBuilder);
        }
    }
}
