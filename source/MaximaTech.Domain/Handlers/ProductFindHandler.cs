using System.Threading;
using System.Threading.Tasks;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Domain.Commands.Requests;
using MaximaTech.Domain.Exceptions;
using MediatR;

namespace MaximaTech.Domain.Handlers
{
    public class ProductFindHandler : IRequestHandler<ProductFindRequest, Product>
    {
        protected readonly IUnitOfWork _uow;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="mapper"></param>
        public ProductFindHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handle the request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The product</returns>
        public async Task<Product> Handle(ProductFindRequest request, CancellationToken cancellationToken)
        {
            Product product = await _uow.ProductRepository.FindByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException();
            }

            return product;
        }
    }
}