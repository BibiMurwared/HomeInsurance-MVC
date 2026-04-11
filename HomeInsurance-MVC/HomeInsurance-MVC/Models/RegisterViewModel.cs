/*
 * File: RegisterViewModel.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: [Enter Date]
 * Description:
 * This view model stores the input fields required to register a new user.
 * It is used to transfer registration data between the Register view
 * and the AccountController.
 */
/*
 Purpose: Stores registration form input only.

What it does

This model is for the register page form.

Usually includes:

FullName
Email
Password
ConfirmPassword
PhoneNumber
Why it exists

The registration form needs fields that are not exactly the same as the stored User model.

Example:

ConfirmPassword is needed in the form
but it should not be stored in the database
 */

namespace HomeInsurance_MVC.Models
{
    using HomeInsurance_MVC.Constants;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The RegisterViewModel class stores user input for the registration form.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the full name entered at registration.
        /// </summary>
        [Required]
        [StringLength(ValidationConstants.UserFullNameMaxLength)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique email entered by the user.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(ValidationConstants.UserEmailMaxLength)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password entered by the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(ValidationConstants.PasswordMaxLength, MinimumLength = ValidationConstants.PasswordMinLength)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation password entered by the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number entered by the user.
        /// </summary>
        [Phone]
        [StringLength(ValidationConstants.PhoneMaxLength)]
        public string? PhoneNumber { get; set; }
    }
}
