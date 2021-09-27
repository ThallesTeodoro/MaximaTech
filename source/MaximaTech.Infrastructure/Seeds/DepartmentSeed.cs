using System;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MaximaTech.Infrastructure.Seeds
{
    public class DepartmentSeed
    {
        public static void Run(IServiceProvider services)
        {
            IDepartmentRepository departmentRepository = services.GetRequiredService<IDepartmentRepository>();

            if (departmentRepository.Count() == 0)
            {
                departmentRepository.AddMany(Department
                    .Faker()
                    .Generate(10));
            }
        }
    }
}