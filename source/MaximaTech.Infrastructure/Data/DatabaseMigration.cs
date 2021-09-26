using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MaximaTech.Infrastructure.Data
{
    public static class DatabaseMigration
    {
        public static void Run(IServiceProvider services)
        {
            ApplicationDbContext dbContext = services.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }
    }
}