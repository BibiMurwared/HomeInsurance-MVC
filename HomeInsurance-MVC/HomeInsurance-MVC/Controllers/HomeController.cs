/*
 * File: HomeController.cs
 * Project: HomeInsurance-MVC
 * Author(s): Bibi
 * Date: [Enter Date]
 * Description:
 * This controller handles general site navigation such as the Home page,
 * Privacy page, and Error page. It is responsible for returning shared,
 * non-business-specific views to the user.
 */

/*
 Controllers

This is where request-handling classes live.

Think of controllers as the traffic managers.

Examples you will have:

HomeController.cs
ItemsController.cs
later AccountController.cs

What they do:

receive browser requests
decide what to do
load data
return a page

Example:
User goes to /Items
→ ItemsController runs
→ it loads items
→ returns the Items page
Purpose: Handles general pages like Home and Privacy.

What it does

This controller is usually the simplest one. It returns the general pages that are not part of item management or authentication.

Typical actions:

Index() → home page
Privacy() → privacy page
Error() → error page
Why it exists

It gives the site a landing page and basic shared pages.
 */
using HomeInsurance_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeInsurance_MVC.Controllers
{
    /// <summary>
    /// The HomeController class manages requests for general pages in the application.
    /// It returns the Home, Privacy, and Error views.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Displays the landing page.
        /// </summary>
        /// <returns>The Home Index view.</returns>
        public IActionResult Index()
        {
            IActionResult result = View();
            return result;
        }

        /// <summary>
        /// Displays the privacy page.
        /// </summary>
        /// <returns>The Privacy view.</returns>
        public IActionResult Privacy()
        {
            IActionResult result = View();
            return result;
        }

        /// <summary>
        /// Displays the error page with the current request identifier.
        /// </summary>
        /// <returns>The Error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ErrorViewModel model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            IActionResult result = View(model);
            return result;
        }
    }
}
