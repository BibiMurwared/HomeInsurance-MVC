/*
 * File: LoginViewModel.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This view model stores the input fields required for user login.
 * It is used to transfer login form data between the Login view
 * and the AccountController.
 */

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
