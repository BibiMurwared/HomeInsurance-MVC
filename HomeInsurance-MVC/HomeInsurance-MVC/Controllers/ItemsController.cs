
/*
 * File: ItemsController.cs
 * Project: HomeInsurance-MVC
 * Author(s): Bibi
 * Date: [Enter Date]
 * Description:
 * This controller manages all item-related operations in the application.
 * It handles Create, Read, Update, and Delete functionality for residence
 * inventory items and connects the Item model to the item views.
 */
/*This handles:

item list
create form
edit form
delete form
details page

This is the equivalent of MoviesController in MvcMovie
Purpose: Handles all item CRUD operations.

What it does

This is one of the most important files in the project.

It will:

display all items
show item details
show create form
save new item
show edit form
update item
show delete confirmation
delete item
Why it exists

This is the controller for the main assignment feature:
cataloging residence items for insurance purposes.

How it connects
Uses HomeInventoryContext
Reads/writes Item
Returns views in Views/Items
 */

using HomeInsurance_MVC.Models;
using HomeInsurance_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HomeInsurance_MVC.Controllers
{
    /// <summary>
    /// The ItemsController class processes requests related to inventory items.
    /// It provides CRUD functionality and connects item data to the user interface.
    /// All actions require an authenticated session.
    /// </summary>
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;

        /// <summary>
        /// Initializes a new instance of the ItemsController class.
        /// </summary>
        /// <param name="itemService">The item service used for item business logic.</param>
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Enforces authentication on every action in this controller by redirecting
        /// unauthenticated visitors to the Login page.
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? sessionUserId = HttpContext.Session.GetString("CurrentUserId");
            if (string.IsNullOrEmpty(sessionUserId))
            {
                context.Result = RedirectToAction("Login", "Account");
            }

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Gets the current user identifier from session storage.
        /// Assumes OnActionExecuting has already verified the session value exists.
        /// </summary>
        /// <returns>The current user's unique identifier.</returns>
        private Guid GetCurrentUserId()
        {
            string? sessionUserId = HttpContext.Session.GetString("CurrentUserId");
            Guid currentUserId = Guid.Parse(sessionUserId!);
            return currentUserId;
        }

        /// <summary>
        /// Retrieves and displays all items for the current user.
        /// When a search string is provided, results are filtered by item name,
        /// category, or room location (case-insensitive substring match).
        /// </summary>
        /// <param name="searchString">Optional keyword for filtering items.</param>
        /// <returns>The Items Index view with the matching items.</returns>
        public async Task<IActionResult> Index(string? searchString)
        {
            Guid currentUserId = GetCurrentUserId();
            List<Item> items = await _itemService.GetAllByUserIdAsync(currentUserId);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                string keyword = searchString.Trim().ToLowerInvariant();
                items = items
                    .Where(existingItem =>
                        existingItem.ItemName.ToLowerInvariant().Contains(keyword)
                        || existingItem.Category.ToLowerInvariant().Contains(keyword)
                        || (existingItem.RoomLocation != null && existingItem.RoomLocation.ToLowerInvariant().Contains(keyword)))
                    .ToList();
            }

            ViewData["SearchString"] = searchString;
            IActionResult result = View(items);
            return result;
        }

        /// <summary>
        /// Retrieves and displays the details for a specific item.
        /// </summary>
        /// <param name="id">The unique identifier of the item.</param>
        /// <returns>The Details view if found; otherwise NotFound.</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            IActionResult result;

            if (id == null)
            {
                result = NotFound();
            }
            else
            {
                Guid currentUserId = GetCurrentUserId();
                Item? item = await _itemService.GetByIdAsync(id.Value, currentUserId);

                if (item == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = View(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Displays the form used to create a new inventory item.
        /// </summary>
        /// <returns>The Create view.</returns>
        public IActionResult Create()
        {
            IActionResult result = View();
            return result;
        }

        /// <summary>
        /// Receives the item form submission and creates a new item if valid.
        /// </summary>
        /// <param name="item">The item submitted by the user.</param>
        /// <returns>A redirect to Index if successful; otherwise the Create view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {
            IActionResult result;

            if (ModelState.IsValid)
            {
                item.UserID = GetCurrentUserId();
                await _itemService.CreateAsync(item);
                result = RedirectToAction(nameof(Index));
            }
            else
            {
                result = View(item);
            }

            return result;
        }

        /// <summary>
        /// Retrieves the selected item and displays it in the edit form.
        /// </summary>
        /// <param name="id">The unique identifier of the item to edit.</param>
        /// <returns>The Edit view if found; otherwise NotFound.</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            IActionResult result;

            if (id == null)
            {
                result = NotFound();
            }
            else
            {
                Guid currentUserId = GetCurrentUserId();
                Item? item = await _itemService.GetByIdAsync(id.Value, currentUserId);

                if (item == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = View(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Receives the edited item data and updates the item if valid.
        /// </summary>
        /// <param name="id">The unique identifier of the item being edited.</param>
        /// <param name="item">The updated item data.</param>
        /// <returns>A redirect to Index if successful; otherwise the Edit view or NotFound.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Item item)
        {
            IActionResult result;

            if (id != item.ItemID)
            {
                result = NotFound();
            }
            else if (ModelState.IsValid)
            {
                Guid currentUserId = GetCurrentUserId();
                bool wasUpdated = await _itemService.UpdateAsync(item, currentUserId);

                if (!wasUpdated)
                {
                    result = NotFound();
                }
                else
                {
                    result = RedirectToAction(nameof(Index));
                }
            }
            else
            {
                result = View(item);
            }

            return result;
        }

        /// <summary>
        /// Retrieves the selected item and displays the delete confirmation page.
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete.</param>
        /// <returns>The Delete view if found; otherwise NotFound.</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            IActionResult result;

            if (id == null)
            {
                result = NotFound();
            }
            else
            {
                Guid currentUserId = GetCurrentUserId();
                Item? item = await _itemService.GetByIdAsync(id.Value, currentUserId);

                if (item == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = View(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Soft deletes the selected item after confirmation.
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete.</param>
        /// <returns>A redirect to the Index action.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Guid currentUserId = GetCurrentUserId();
            await _itemService.SoftDeleteAsync(id, currentUserId);
            IActionResult result = RedirectToAction(nameof(Index));
            return result;
        }
    }
}