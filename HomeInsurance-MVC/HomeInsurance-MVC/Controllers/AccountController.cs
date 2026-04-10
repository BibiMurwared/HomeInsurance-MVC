/*
 * File: AccountController.cs
 * Project: HomeInsurance-MVC
 * Author(s): Bibi
 * Date: [Enter Date]
 * Description:
 * This controller manages account-related functionality including user
 * registration, login, and logout. It is responsible for validating user
 * credentials and controlling access to secured parts of the application.
 */

/*Purpose: Handles registration, login, and logout.

What it does

This controller manages account-related actions:

display register form
create user account
display login form
validate login
log user out
Why it exists

Your assignment requires authentication and authorization, so this controller handles the user access side.

How it connects
Uses User
Uses LoginViewModel
Uses RegisterViewModel
Returns Views/Account/Login.cshtml
Returns Views/Account/Register.cshtml*/

using Microsoft.AspNetCore.Mvc;

namespace HomeInsurance_MVC.Controllers
{
    /// <summary>
    /// The AccountController class handles user account actions such as
    /// registration, authentication, and logout.
    /// </summary>
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
