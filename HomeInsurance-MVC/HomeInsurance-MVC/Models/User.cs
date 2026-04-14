/*
 * File: User.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This model represents a registered user of the application. It stores
 * account-related information required for authentication, authorization,
 * and ownership of inventory items.
 */

namespace HomeInsurance_MVC.Models
{
    using HomeInsurance_MVC.Constants;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The User class stores account information for a person using the application.
    /// It is used to identify the user and associate inventory items with that user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public Guid UserID { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        [Required]
        [StringLength(ValidationConstants.UserFullNameMaxLength)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique email address for login.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(ValidationConstants.UserEmailMaxLength)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hashed password value.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the optional phone number for the user.
        /// </summary>
        [Phone]
        [StringLength(ValidationConstants.PhoneMaxLength)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets whether the account is currently active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets whether the account is soft deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the creation date for the account.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the items owned by this user.
        /// </summary>
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
