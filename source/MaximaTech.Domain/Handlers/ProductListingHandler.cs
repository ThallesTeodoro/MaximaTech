using System.Threading;
using System.Threading.Tasks;
using MaximaTech.Core.DTOs;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Domain.Commands.Requests;
using MediatR;

namespace MaximaTech.Domain.Handlers
{
    public class ProductListingHandler : IRequestHandler<ProductListingRequest, Pagination<Product>>
    {
        protected readonly IUnitOfWork _uow;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="mapper"></param>
        public ProductListingHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handle the request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of products paginated</returns>
        public async Task<Pagination<Product>> Handle(ProductListingRequest request, CancellationToken cancellationToken)
        {
            int currentPage = 1;
            int perPage = 20;

            if (request.Page != null)
            {
                currentPage = (int) request.Page;
            }

            Pagination<Product> products = await _uow.ProductRepository.GetPaginated(currentPage, perPage);

            return products;
        }
    }
}