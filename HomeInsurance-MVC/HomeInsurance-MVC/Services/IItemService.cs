namespace HomeInsurance_MVC.Services
{
    using HomeInsurance_MVC.Models;

    /// <summary>
    /// Provides business operations for item management.
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Gets all items for one user.
        /// </summary>
        Task<List<Item>> GetAllByUserIdAsync(Guid userId);

        /// <summary>
        /// Gets one item for one user.
        /// </summary>
        Task<Item?> GetByIdAsync(Guid itemId, Guid userId);

        /// <summary>
        /// Creates a new item for one user.
        /// </summary>
        Task<Item> CreateAsync(Item item);

        /// <summary>
        /// Updates an item for one user.
        /// </summary>
        Task<bool> UpdateAsync(Item item, Guid userId);

        /// <summary>
        /// Soft deletes an item for one user.
        /// </summary>
        Task SoftDeleteAsync(Guid itemId, Guid userId);
    }
}
