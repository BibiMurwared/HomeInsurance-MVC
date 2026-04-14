/*
 * File: UserRepository.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This class implements the IUserRepository interface using Entity Framework Core.
 * It manages all database interactions related to users, including retrieval,
 * registration, updates, and soft deletion of user accounts.
 */
namespace HomeInsurance_MVC.Repositories
{
    using HomeInsurance_MVC.Data;
    using HomeInsurance_MVC.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Implements user data access using EF Core.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly HomeInventoryContext _context;

        /// <summary>
        /// Creates a repository with database context.
        /// </summary>
        public UserRepository(HomeInventoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a user by email address.
        /// </summary>
        public async Task<User?> GetByEmailAsync(string email)
        {
            User? user = await _context.Users
                .FirstOrDefaultAsync(existingUser => existingUser.Email == email);
            return user;
        }

        /// <summary>
        /// Adds a new user record.
        /// </summary>
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
