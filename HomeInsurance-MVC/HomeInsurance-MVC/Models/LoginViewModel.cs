/*
 * File: LoginViewModel.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: [Enter Date]
 * Description:
 * This view model stores the input fields required for user login.
 * It is used to transfer login form data between the Login view
 * and the AccountController.
 */
/**Purpose: Stores login form input only.

What it does

This model is for the login page form.

Usually includes:

Email
Password
Why it exists

A login form does not need the full User model.
It only needs the fields entered during login.

This keeps the code cleaner and safer.*/
namespace HomeInsurance_MVC.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The LoginViewModel class stores user input for the login form.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the user email for sign in.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the plain password entered by the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
