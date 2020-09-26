using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public interface IVideoclubDbContext : IDisposable
    {
        DbSet<Rent> Rents { get; }
        void EnsureCreated();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
