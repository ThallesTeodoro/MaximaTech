using System.Threading.Tasks;
using MaximaTech.Core.DTOs;
using MaximaTech.Core.Entities;

namespace MaximaTech.Core.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Get products paginated
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns>Products paginated</returns>
        Task<Pagination<Product>> GetPaginated(int page, int perPage);
    }
}