/*
 * File: IUserService.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This interface defines the business operations for user management.
 * It includes methods for retrieving users, registering new accounts,
 * validating login credentials, updating user profiles, and performing
 * soft deletion of user accounts.
 */
namespace HomeInsurance_MVC.Services
{
    using HomeInsurance_MVC.Models;

    /// <summary>
    /// Provides business operations for user management.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all active users.
        /// </summary>
        Task<List<User>> GetAllAsync();

        /// <summary>
        /// Gets one user by identifier.
        /// </summary>
        Task<User?> GetByIdAsync(Guid userId);

        /// <summary>
        /// Registers a user with hashed password.
        /// </summary>
        Task<User> RegisterAsync(string fullName, string email, string password, string? phoneNumber);

        /// <summary>
        /// Validates login credentials for one user.
        /// </summary>
        Task<User?> ValidateCredentialsAsync(string email, string password);

        /// <summary>
        /// Updates account profile fields.
        /// </summary>
        Task UpdateProfileAsync(Guid userId, string fullName, string? phoneNumber, bool isActive);

        /// <summary>
        /// Soft deletes the account.
        /// </summary>
        Task SoftDeleteAsync(Guid userId);
    }
}
