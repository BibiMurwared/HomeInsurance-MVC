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

using HomeInsurance_MVC.Models;
using HomeInsurance_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeInsurance_MVC.Controllers
{
    /// <summary>
    /// The AccountController class handles user account actions such as
    /// registration, authentication, and logout.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the AccountController class.
        /// </summary>
        /// <param name="userService">The user service used for account operations.</param>
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Displays the user registration form.
        /// </summary>
        /// <returns>The Register view.</returns>
        public IActionResult Register()
        {
            IActionResult result = View();
            return result;
        }

        /// <summary>
        /// Receives the submitted registration form and creates a new user account if valid.
        /// </summary>
        /// <param name="model">The registration data entered by the user.</param>
        /// <returns>A redirect to Login if successful; otherwise the Register view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            IActionResult result;

            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.RegisterAsync(
                        model.FullName,
                        model.Email,
                        model.Password,
                        model.PhoneNumber);

                    result = RedirectToAction(nameof(Login));
                }
                catch (InvalidOperationException exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                    result = View(model);
                }
            }
            else
            {
                result = View(model);
            }

            return result;
        }

        /// <summary>
        /// Displays the login form.
        /// </summary>
        /// <returns>The Login view.</returns>
        public IActionResult Login()
        {
            IActionResult result = View();
            return result;
        }

        /// <summary>
        /// Receives the login form and validates the user credentials.
        /// </summary>
        /// <param name="model">The login data entered by the user.</param>
        /// <returns>A redirect to Items if successful; otherwise the Login view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            IActionResult result;

            if (ModelState.IsValid)
            {
                User? authenticatedUser = await _userService.ValidateCredentialsAsync(model.Email, model.Password);

                if (authenticatedUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    result = View(model);
                }
                else
                {
                    result = RedirectToAction("Index", "Items");
                }
            }
            else
            {
                result = View(model);
            }

            return result;
        }

        /// <summary>
        /// Logs the user out of the application.
        /// </summary>
        /// <returns>A redirect to the Home page.</returns>
        public IActionResult Logout()
        {
            IActionResult result = RedirectToAction("Index", "Home");
            return result;
        }
    }
}