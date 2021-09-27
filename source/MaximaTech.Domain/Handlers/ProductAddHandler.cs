using System;
using System.Threading;
using System.Threading.Tasks;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Domain.Commands.Requests;
using MediatR;

namespace MaximaTech.Domain.Handlers
{
    public class ProductAddHandler : IRequestHandler<ProductAddRequest, Product>
    {
        protected readonly IUnitOfWork _uow;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="mapper"></param>
        public ProductAddHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handle the request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The product</returns>
        public Task<Product> Handle(ProductAddRequest request, CancellationToken cancellationToken)
        {
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                DepartmentId = request.DepartmentId,
                Code = request.Code,
                Description = request.Description,
                Price = request.Price,
                Status = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _uow.ProductRepository.Add(product);
            _uow.Commit();

            return Task.FromResult(product);
        }
    }
}