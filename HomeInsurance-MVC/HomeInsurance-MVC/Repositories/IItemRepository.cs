/*
 * File: IItemRepository.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This interface defines the contract for item-related database operations.
 * It specifies the methods required to retrieve, create, update, and delete
 * inventory items associated with a specific user.
 */

namespace HomeInsurance_MVC.Repositories
{
    using HomeInsurance_MVC.Models;

    /// <summary>
    /// Provides database access operations for items.
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        /// Gets all items for one user.
        /// </summary>
        Task<List<Item>> GetAllByUserIdAsync(Guid userId);

        /// <summary>
        /// Gets one item by item and user id.
        /// </summary>
        Task<Item?> GetByIdAsync(Guid itemId, Guid userId);

        /// <summary>
        /// Adds a new item record.
        /// </summary>
        Task AddAsync(Item item);

        /// <summary>
        /// Updates an existing item record.
        /// </summary>
        Task UpdateAsync(Item item);

        /// <summary>
        /// Soft deletes an item record.
        /// </summary>
        Task SoftDeleteAsync(Guid itemId, Guid userId);
    }
}
