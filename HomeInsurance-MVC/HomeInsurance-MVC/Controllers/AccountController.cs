/*
 * File: AccountController.cs
 * Project: HomeInsurance-MVC
 * Author(s): Bibi
 * Date: 2026-04-13
 * Description:
 * This controller manages account-related functionality including user
 * registration, login, and logout. It is responsible for validating user
 * credentials and controlling access to secured parts of the application.
 */


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
        private readonly ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the AccountController class.
        /// </summary>
        /// <param name="userService">The user service used for account operations.</param>
        public AccountController(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
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
                    User registeredUser = await _userService.RegisterAsync(
                        model.FullName,
                        model.Email,
                        model.Password,
                        model.PhoneNumber);

                    await _logService.LogUserEventAsync(
                        registeredUser.UserID,
                        "Register",
                        "User account registered.",
                        true);

                    result = RedirectToAction(nameof(Login));
                }
                catch (InvalidOperationException exception)
                {
                    await _logService.LogUserEventAsync(
                        null,
                        "Register",
                        exception.Message,
                        false);

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
                    await _logService.LogUserEventAsync(
                        null,
                        "Login",
                        "Invalid email or password.",
                        false);

                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    result = View(model);
                }
                else
                {
                    HttpContext.Session.SetString("CurrentUserId", authenticatedUser.UserID.ToString());
                    HttpContext.Session.SetString("CurrentUserName", authenticatedUser.FullName);

                    await _logService.LogUserEventAsync(
                        authenticatedUser.UserID,
                        "Login",
                        "User login successful.",
                        true);

                    result = RedirectToAction("Index", "Items");
                }
            }
            else
            {
                await _logService.LogUserEventAsync(
                    null,
                    "Login",
                    "Login model validation failed.",
                    false);

                result = View(model);
            }

            return result;
        }

        /// <summary>
        /// Logs the user out of the application by clearing the session.
        /// </summary>
        /// <returns>A redirect to the Home page.</returns>
        public async Task<IActionResult> Logout()
        {
            string? sessionUserId = HttpContext.Session.GetString("CurrentUserId");
            Guid? userId = null;
            if (!string.IsNullOrWhiteSpace(sessionUserId))
            {
                userId = Guid.Parse(sessionUserId);
            }

            await _logService.LogUserEventAsync(
                userId,
                "Logout",
                "User logged out.",
                true);

            HttpContext.Session.Clear();
            IActionResult result = RedirectToAction("Index", "Home");
            return result;
        }
    }
}