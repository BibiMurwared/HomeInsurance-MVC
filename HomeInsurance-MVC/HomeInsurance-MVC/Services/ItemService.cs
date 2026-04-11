namespace HomeInsurance_MVC.Services
{
    using HomeInsurance_MVC.Models;
    using HomeInsurance_MVC.Repositories;

    /// <summary>
    /// Implements item logic on top of repository access.
    /// </summary>
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        /// <summary>
        /// Creates a service with repository dependencies.
        /// </summary>
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// Gets all items for one user.
        /// </summary>
        public async Task<List<Item>> GetAllByUserIdAsync(Guid userId)
        {
            List<Item> items = await _itemRepository.GetAllByUserIdAsync(userId);
            return items;
        }

        /// <summary>
        /// Gets one item for one user.
        /// </summary>
        public async Task<Item?> GetByIdAsync(Guid itemId, Guid userId)
        {
            Item? item = await _itemRepository.GetByIdAsync(itemId, userId);
            return item;
        }

        /// <summary>
        /// Creates a new item for one user.
        /// </summary>
        public async Task<Item> CreateAsync(Item item)
        {
            item.ItemID = Guid.NewGuid();
            item.CreatedDate = DateTime.UtcNow;
            item.IsDeleted = false;
            await _itemRepository.AddAsync(item);
            return item;
        }

        /// <summary>
        /// Updates an item for one user.
        /// </summary>
        public async Task<bool> UpdateAsync(Item item, Guid userId)
        {
            bool wasUpdated = false;
            Item? existingItem = await _itemRepository.GetByIdAsync(item.ItemID, userId);
            if (existingItem is not null)
            {
                existingItem.ItemName = item.ItemName;
                existingItem.Category = item.Category;
                existingItem.Description = item.Description;
                existingItem.EstimatedValue = item.EstimatedValue;
                existingItem.PurchaseDate = item.PurchaseDate;
                existingItem.RoomLocation = item.RoomLocation;
                existingItem.SerialNumber = item.SerialNumber;
                existingItem.ImageURL = item.ImageURL;
                await _itemRepository.UpdateAsync(existingItem);
                wasUpdated = true;
            }

            return wasUpdated;
        }

        /// <summary>
        /// Soft deletes an item for one user.
        /// </summary>
        public async Task SoftDeleteAsync(Guid itemId, Guid userId)
        {
            await _itemRepository.SoftDeleteAsync(itemId, userId);
        }
    }
}
