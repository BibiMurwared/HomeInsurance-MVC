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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            IActionResult result = View();
            return result;
        }

        /// <summary>
        /// Receives the login form and validates the user credentials.
        /// </summary>
        /// <param name="model">The login data entered by the user.</param>
        /// <returns>A redirect to Items if successful; otherwise the Login view.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            IActionResult result;
            string resolvedReturnUrl = returnUrl ?? Url.Content("~/")!;

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
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, authenticatedUser.UserID.ToString()),
                        new Claim(ClaimTypes.Name, authenticatedUser.FullName),
                        new Claim(ClaimTypes.Email, authenticatedUser.Email)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    AuthenticationProperties authenticationProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authenticationProperties);

                    await _logService.LogUserEventAsync(
                        authenticatedUser.UserID,
                        "Login",
                        "User login successful.",
                        true);

                    result = LocalRedirect(resolvedReturnUrl);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            string? sessionUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            IActionResult result = RedirectToAction("Index", "Home");
            return result;
        }
    }
}