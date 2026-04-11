namespace HomeInsurance_MVC.Repositories
{
    using HomeInsurance_MVC.Models;

    /// <summary>
    /// Provides database access operations for users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user by identifier.
        /// </summary>
        Task<User?> GetByIdAsync(Guid userId);

        /// <summary>
        /// Gets a user by email address.
        /// </summary>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Gets all active users.
        /// </summary>
        Task<List<User>> GetAllAsync();

        /// <summary>
        /// Adds a new user record.
        /// </summary>
        Task AddAsync(User user);

        /// <summary>
        /// Updates an existing user record.
        /// </summary>
        Task UpdateAsync(User user);

        /// <summary>
        /// Soft deletes a user record.
        /// </summary>
        Task SoftDeleteAsync(Guid userId);
    }
}
