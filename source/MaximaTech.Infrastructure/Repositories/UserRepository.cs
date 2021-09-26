using System;
using System.Linq;
using System.Threading.Tasks;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MaximaTech.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User instance</returns>
        public User FindByEmail(string email)
        {
            return _dbContext
                .Users
                .Where(u => u.Email == email)
                .AsSplitQuery()
                .SingleOrDefault();
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User instance</returns>
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _dbContext
                .Users
                .Where(u => u.Email == email)
                .AsSplitQuery()
                .SingleOrDefaultAsync();
        }
    }
}