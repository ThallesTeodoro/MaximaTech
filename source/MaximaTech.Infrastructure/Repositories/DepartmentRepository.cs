using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Infrastructure.Data;

namespace MaximaTech.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}