using System;
using System.Collections.Generic;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MaximaTech.Infrastructure.Seeds
{
    public class ProductSeed
    {
        public static void Run(IServiceProvider services)
        {
            IProductRepository productRepository = services.GetRequiredService<IProductRepository>();
            IDepartmentRepository departmentRepository = services.GetRequiredService<IDepartmentRepository>();

            if (productRepository.Count() == 0)
            {
                List<Department> departments = departmentRepository.All();
                List<Product> products = Product
                    .Faker()
                    .Generate(50);

                Random random = new Random();

                foreach (Product product in products)
                {
                    product.DepartmentId = departments[random.Next(departments.Count)].Id;
                    productRepository.Add(product);
                }
            }
        }
    }
}