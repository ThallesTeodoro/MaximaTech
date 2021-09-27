using System;
using MaximaTech.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MaximaTech.Infrastructure.Seeds
{
    public class DatabaseSeed
    {
        public static void Run(IServiceProvider services)
        {
            IUnitOfWork uow = services.GetRequiredService<IUnitOfWork>();

            UserSeed.Run(services);
            DepartmentSeed.Run(services);
            uow.Commit();

            ProductSeed.Run(services);
            uow.Commit();
        }
    }
}