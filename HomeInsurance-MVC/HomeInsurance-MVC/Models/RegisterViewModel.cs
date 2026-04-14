/*
 * File: RegisterViewModel.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad, Julia
 * Date: 2026-04-13
 * Description:
 * This view model stores the input fields required to register a new user.
 * It is used to transfer registration data between the Register view
 * and the AccountController.
 * Reference(s) :
 * https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-10.0
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
        [Display(Name = "Full Name *")]
        [StringLength(ValidationConstants.UserFullNameMaxLength)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique email entered by the user.
        /// </summary>
        [Required]
        [Display(Name = "Email *")]
        [EmailAddress]
        [StringLength(ValidationConstants.UserEmailMaxLength)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password entered by the user.
        /// </summary>
        [Required]
        [Display(Name = "Password *")]
        [DataType(DataType.Password)]
        [StringLength(ValidationConstants.PasswordMaxLength, MinimumLength = ValidationConstants.PasswordMinLength)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation password entered by the user.
        /// </summary>
        [Required]
        [Display(Name = "Confirm Password *")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number entered by the user.
        /// </summary>
        [Phone]
        [Display(Name = "Phone Number")]
        [StringLength(ValidationConstants.PhoneMaxLength)]
        public string? PhoneNumber { get; set; }
    }
}
