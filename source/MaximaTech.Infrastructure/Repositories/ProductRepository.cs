using System;
using System.Linq;
using System.Threading.Tasks;
using MaximaTech.Core.DTOs;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MaximaTech.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Find product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Product</returns>
        public override async Task<Product> FindByIdAsync(Guid id)
        {
            return await _dbContext
                .Products
                .Include(p => p.Department)
                .Where(p => p.Status == true && p.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Remove product
        /// </summary>
        /// <param name="entity"></param>
        public override void Remove(Product entity)
        {
            entity.Status = false;
            _dbContext.Products.Update(entity);
        }

        /// <summary>
        /// Get products paginated
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns>Products paginated</returns>
        public async Task<Pagination<Product>> GetPaginated(int page, int perPage)
        {
            var query = _dbContext
                .Products
                .Include(p => p.Department)
                .Where(p => p.Status == true);

            Pagination<Product> pagination = new Pagination<Product>();

            pagination.Total = await query.CountAsync();
            pagination.CurrentPage = page;
            pagination.PerPage = perPage;

            pagination.Items = await query
                .OrderBy(p => p.CreatedAt)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();

            return pagination;
        }
    }
}