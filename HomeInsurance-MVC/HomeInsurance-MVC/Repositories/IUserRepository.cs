/*
 * File: IUserRepository.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This interface defines the contract for user-related database operations.
 * It includes methods for retrieving users, creating new accounts, updating
 * user information, and performing soft deletion of user records.
 */
namespace HomeInsurance_MVC.Repositories
{
    using HomeInsurance_MVC.Models;

    /// <summary>
    /// Provides database access operations for users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user by email address.
        /// </summary>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Adds a new user record.
        /// </summary>
        Task AddAsync(User user);
    }
}
