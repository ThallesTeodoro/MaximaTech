using System;
using System.Threading.Tasks;
using MaximaTech.Core.Entities;

namespace MaximaTech.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User instance</returns>
        User FindByEmail(string email);

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User instance</returns>
        Task<User> FindByEmailAsync(string email);
    }
}