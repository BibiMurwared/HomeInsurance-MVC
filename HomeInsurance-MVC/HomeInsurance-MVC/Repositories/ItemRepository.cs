namespace HomeInsurance_MVC.Repositories
{
    using HomeInsurance_MVC.Data;
    using HomeInsurance_MVC.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Implements item data access using EF Core.
    /// </summary>
    public class ItemRepository : IItemRepository
    {
        private readonly HomeInventoryContext _context;

        /// <summary>
        /// Creates a repository with database context.
        /// </summary>
        public ItemRepository(HomeInventoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all items for one user.
        /// </summary>
        public async Task<List<Item>> GetAllByUserIdAsync(Guid userId)
        {
            List<Item> items = await _context.Items
                .Where(existingItem => existingItem.UserID == userId)
                .OrderByDescending(existingItem => existingItem.CreatedDate)
                .ToListAsync();
            return items;
        }

        /// <summary>
        /// Gets one item by item and user id.
        /// </summary>
        public async Task<Item?> GetByIdAsync(Guid itemId, Guid userId)
        {
            Item? item = await _context.Items
                .FirstOrDefaultAsync(existingItem => existingItem.ItemID == itemId && existingItem.UserID == userId);
            return item;
        }

        /// <summary>
        /// Adds a new item record.
        /// </summary>
        public async Task AddAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing item record.
        /// </summary>
        public async Task UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Soft deletes an item record.
        /// </summary>
        public async Task SoftDeleteAsync(Guid itemId, Guid userId)
        {
            Item? item = await _context.Items
                .FirstOrDefaultAsync(existingItem => existingItem.ItemID == itemId && existingItem.UserID == userId);
            if (item is not null)
            {
                item.IsDeleted = true;
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
