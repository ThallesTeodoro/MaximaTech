using System.Threading;
using System.Threading.Tasks;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Domain.Commands.Requests;
using MaximaTech.Domain.Exceptions;
using MediatR;

namespace MaximaTech.Domain.Handlers
{
    public class ProductUpdateHandler : IRequestHandler<ProductUpdateRequest, Product>
    {
        protected readonly IUnitOfWork _uow;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="mapper"></param>
        public ProductUpdateHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handle the request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The product</returns>
        public async Task<Product> Handle(ProductUpdateRequest request, CancellationToken cancellationToken)
        {
            Product product = await _uow.ProductRepository.FindByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException();
            }

            product.Code = request.Code;
            product.Description = request.Description;
            product.DepartmentId = request.DepartmentId;
            product.Price = request.Price;
            _uow.ProductRepository.Update(product);
            _uow.Commit();

            return product;
        }
    }
}