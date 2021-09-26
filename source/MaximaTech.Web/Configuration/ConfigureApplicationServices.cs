using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MaximaTech.Web.Configuration
{
    public static class ConfigureApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}