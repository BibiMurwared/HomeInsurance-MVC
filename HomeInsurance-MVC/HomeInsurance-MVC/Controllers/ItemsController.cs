
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
using Microsoft.AspNetCore.Mvc;

namespace HomeInsurance_MVC.Controllers
{
    /// <summary>
    /// The ItemsController class processes requests related to inventory items.
    /// It provides CRUD functionality and connects item data to the user interface.
    /// </summary>
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
