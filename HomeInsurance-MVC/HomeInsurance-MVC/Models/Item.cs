/*
 * File: Item.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This model represents an inventory item stored in a user's residence.
 * It contains the data required to describe the item for cataloging and
 * insurance purposes, including value, category, description, and location.
 */
using System.ComponentModel.DataAnnotations;
using HomeInsurance_MVC.Constants;

namespace HomeInsurance_MVC.Models
{
    /// <summary>
    /// The Item class stores information about a residence item that can be
    /// cataloged in the application for insurance tracking purposes.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        [Key]
        public Guid ItemID { get; set; }

        [Required]
        [StringLength(ValidationConstants.ItemNameMaxLength)]
        [Display(Name = "Item Name *")]
        public string ItemName { get; set; } = string.Empty;

        [Required]
        [StringLength(ValidationConstants.ItemCategoryMaxLength)]
        [Display(Name = "Category *")]
        public string Category { get; set; } = string.Empty;

        [StringLength(ValidationConstants.ItemDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Range(typeof(decimal), ValidationConstants.MinEstimatedValue, ValidationConstants.MaxEstimatedValue)]
        [Display(Name = "Estimated Value *")]
        public decimal EstimatedValue { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime? PurchaseDate { get; set; }

        [StringLength(ValidationConstants.RoomLocationMaxLength)]
        [Display(Name = "Room Location")]
        public string? RoomLocation { get; set; }

        [StringLength(ValidationConstants.SerialNumberMaxLength)]
        [Display(Name = "Serial Number")]
        public string? SerialNumber { get; set; }

        [Required]
        public Guid UserID { get; set; }

        public User? User { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
