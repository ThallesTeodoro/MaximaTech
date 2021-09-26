using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Infrastructure.Data;

namespace MaximaTech.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepository;
        private IProductRepository _productRepository;
        private IDepartmentRepository _departmentRepository;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Commit database transaction
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Rollback database transection
        /// </summary>
        public void Rollback()
        {
            _context.Dispose();
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_context); }
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository = _productRepository ?? new ProductRepository(_context); }
        }

        public IDepartmentRepository DepartmentRepository
        {
            get { return _departmentRepository = _departmentRepository ?? new DepartmentRepository(_context); }
        }
    }
}