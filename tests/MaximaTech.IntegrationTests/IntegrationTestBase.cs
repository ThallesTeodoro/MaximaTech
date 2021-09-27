using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Infrastructure.Data;
using MaximaTech.Infrastructure.Seeds;
using MaximaTech.Web;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MaximaTech.IntegrationTests
{
    public class IntegrationTestBase
    {
        protected readonly WebApplicationFactory<Startup> _appFactory;
        protected readonly IServiceScopeFactory _scopeFactory;
        protected readonly IUnitOfWork _uow;
        protected readonly HttpClient TestClient;

        protected IntegrationTestBase()
        {
            _appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<ApplicationDbContext>));

                        services.Remove(descriptor);

                        services.AddDbContextPool<ApplicationDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb" + this.GetType().Name);
                        });

                        var sp = services.BuildServiceProvider();

                        using (var scope = sp.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                            db.Database.EnsureDeleted();
                            DatabaseSeed.Run(sp);
                        }
                    });
                });

            TestClient = _appFactory.CreateClient();
            _scopeFactory = _appFactory.Services.GetService<IServiceScopeFactory>();

            IServiceScope scope = _scopeFactory.CreateScope();

            _uow = scope.ServiceProvider.GetService<IUnitOfWork>();
        }

        /// <summary>
        /// Add entity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="IRepository"></typeparam>
        /// <returns>Task</returns>
        protected async Task<TEntity> AddEntityAsync<TEntity>(TEntity entity)
        {
            IServiceScope scope = _scopeFactory.CreateScope();

            ApplicationDbContext db = scope.ServiceProvider.GetService<ApplicationDbContext>();

            db.Add(entity);

            await db.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Add many entities to database
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="IRepository"></typeparam>
        /// <returns>Task</returns>
        protected async Task<List<TEntity>> AddManyEntitiesAsync<TEntity>(List<TEntity> entities)
        {
            IServiceScope scope = _scopeFactory.CreateScope();

            ApplicationDbContext db = scope.ServiceProvider.GetService<ApplicationDbContext>();

            foreach (TEntity item in entities)
            {
                db.Add(item);
            }

            await db.SaveChangesAsync();

            return entities;
        }

        /// <summary>
        /// Send Mediator request
        /// </summary>
        /// <param name="request"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            IServiceScope scope = _scopeFactory.CreateScope();

            IMediator mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        /// <summary>
        /// Check request validation
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of ValidationResult</returns>
        protected IList<ValidationResult> ValidateModel<TResponse>(IRequest<TResponse> model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
