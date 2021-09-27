using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MaximaTech.Core.DTOs;
using MaximaTech.Core.Entities;
using MaximaTech.Domain.Commands.Requests;
using MaximaTech.Domain.Exceptions;
using MaximaTech.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MaximaTech.IntegrationTests.Application
{
    public class ProductTests : IntegrationTestBase
    {
        #region Index

        [Fact]
        public async Task Index_WhenCalled_ReturnSuccess()
        {
            List<Department> departments = Department
                .Faker()
                .Generate(5);

            await AddManyEntitiesAsync<Department>(departments);

            List<Product> products = Product
                .Faker()
                .Generate(10);

            Random random = new Random();
            foreach (Product product in products)
            {
                product.DepartmentId = departments[random.Next(departments.Count)].Id;
                await AddEntityAsync<Product>(product);
            }

            Pagination<Product> response = await SendAsync<Pagination<Product>>(new ProductListingRequest());
            response.Items.Should().HaveCount(c => c > 0);
        }

        #endregion

        #region Store

        [Fact]
        public async Task Store_WhenCalled_ReturnSuccess()
        {
            Department department = Department
                .Faker()
                .Generate();

            await AddEntityAsync<Department>(department);

            Product product = Product
                .Faker()
                .Generate();

            Product response = await SendAsync<Product>(new ProductAddRequest()
            {
                Code = product.Code,
                Description = product.Description,
                DepartmentId = product.DepartmentId,
                Price = product.Price,
            });

            IServiceScope scope = _scopeFactory.CreateScope();
            ApplicationDbContext db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            product = db.Products.Where(p => p.Status == true && p.Id == response.Id).FirstOrDefault();
            product.Should().NotBeNull();
        }

        #endregion

        #region Find

        [Fact]
        public async Task Find_WhenCalledWithWrongId_ReturnNotFound()
        {
            try
            {
                Product response = await SendAsync<Product>(new ProductFindRequest()
                {
                    Id = Guid.NewGuid(),
                });

                Assert.True(false);
            }
            catch (NotFoundException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task Find_WhenCalled_ReturnSuccess()
        {
            List<Department> departments = Department
                .Faker()
                .Generate(5);

            await AddManyEntitiesAsync<Department>(departments);

            Product product = Product
                .Faker()
                .Generate();

            Random random = new Random();
            product.DepartmentId = departments[random.Next(departments.Count)].Id;
            await AddEntityAsync<Product>(product);

            Product response = await SendAsync<Product>(new ProductFindRequest()
            {
                Id = product.Id,
            });
            response.Id.Should().Equals(product.Id);
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_WhenCalledWithWrongId_ReturnNotFound()
        {
            try
            {
                Product response = await SendAsync<Product>(new ProductUpdateRequest()
                {
                    Id = Guid.NewGuid(),
                });

                Assert.True(false);
            }
            catch (NotFoundException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task Update_WhenCalled_ReturnSuccess()
        {
            List<Department> departments = Department
                .Faker()
                .Generate(5);

            await AddManyEntitiesAsync<Department>(departments);

            Product product = Product
                .Faker()
                .Generate();

            Random random = new Random();
            product.DepartmentId = departments[random.Next(departments.Count)].Id;
            await AddEntityAsync<Product>(product);

            Product testProduct = Product
                .Faker()
                .Generate();

            Product response = await SendAsync<Product>(new ProductUpdateRequest()
            {
                Id = product.Id,
                Code = testProduct.Code,
                Description = testProduct.Description,
                DepartmentId = departments[random.Next(departments.Count)].Id,
                Price = testProduct.Price,
            });
            response.Code.Should().Equals(testProduct.Code);
            response.Description.Should().Equals(testProduct.Description);
            response.Price.Should().Equals(testProduct.Price);
        }

        #endregion

        #region Delete

        [Fact]
        public async Task Delete_WhenCalledWithWrongId_ReturnNotFound()
        {
            try
            {
                await SendAsync<object>(new ProductDeleteRequest()
                {
                    Id = Guid.NewGuid(),
                });

                Assert.True(false);
            }
            catch (NotFoundException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnSuccess()
        {
            List<Department> departments = Department
                .Faker()
                .Generate(5);

            await AddManyEntitiesAsync<Department>(departments);

            Product product = Product
                .Faker()
                .Generate();

            Random random = new Random();
            product.DepartmentId = departments[random.Next(departments.Count)].Id;
            await AddEntityAsync<Product>(product);

            await SendAsync<object>(new ProductDeleteRequest()
            {
                Id = product.Id
            });

            IServiceScope scope = _scopeFactory.CreateScope();
            ApplicationDbContext db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            product = db.Products.Where(p => p.Status == true && p.Id == product.Id).FirstOrDefault();
            product.Should().BeNull();
        }

        #endregion
    }
}
