using DataAccess.Contexts;
using Domain.Entities;
using System;
using System.Linq;

namespace DataAccess.Configuration
{
    public class DbInitializer
    {
        public static void Initialize(IVideoclubDbContext context)
        {
            if (context.Rents.Any())
                return;

            context.Rents.Add(new Rent(Guid.Parse("b172a2ab-5900-4532-bd68-68a041752017"), Guid.Parse("5d65ac9e-d431-4138-a8e4-c4719205cb1b"), Status.Rentend, new DateTime(2020, 10, 1)));

            context.SaveChanges();
        }
    }
}
