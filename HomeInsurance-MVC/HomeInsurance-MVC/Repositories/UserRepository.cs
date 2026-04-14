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
        /// Gets a user by identifier.
        /// </summary>
        public async Task<User?> GetByIdAsync(Guid userId)
        {
            User? user = await _context.Users
                .Include(existingUser => existingUser.Items)
                .FirstOrDefaultAsync(existingUser => existingUser.UserID == userId);
            return user;
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
        /// Gets all active users.
        /// </summary>
        public async Task<List<User>> GetAllAsync()
        {
            List<User> users = await _context.Users
                .OrderBy(existingUser => existingUser.FullName)
                .ToListAsync();
            return users;
        }

        /// <summary>
        /// Adds a new user record.
        /// </summary>
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing user record.
        /// </summary>
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Soft deletes a user record.
        /// </summary>
        public async Task SoftDeleteAsync(Guid userId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(existingUser => existingUser.UserID == userId);
            if (user is not null)
            {
                user.IsDeleted = true;
                user.IsActive = false;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
