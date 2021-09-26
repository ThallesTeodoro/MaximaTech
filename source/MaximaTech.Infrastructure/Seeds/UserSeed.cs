using System;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MaximaTech.Infrastructure.Seeds
{
    public class UserSeed
    {
        public static void Run(IServiceProvider services)
        {
            IUserRepository userRepository = services.GetRequiredService<IUserRepository>();

            if (userRepository.FindByEmail("admin@maximatech.com") == null)
            {
                userRepository.Add(new User
                {
                    Name = "Admin",
                    Email = "admin@maximatech.com",
                    Password = "mjV2OfkRLFHzqQzFZ3WujCpVPeODCowFhfMQ2NLHV5c=", // secret
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                });
            }
        }
    }
}