/*
 * File: Item.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: [Enter Date]
 * Description:
 * This model represents an inventory item stored in a user's residence.
 * It contains the data required to describe the item for cataloging and
 * insurance purposes, including value, category, description, and location.
 */
/*
 * Purpose: Represents a residence inventory item.

What it does

Defines the structure of an item stored in the system.

Typical fields:

ItemID
ItemName
Category
Description
EstimatedValue
PurchaseDate
RoomLocation
SerialNumber
IsDeleted
CreatedDate
Why it exists

This is the core business model for the whole assignment.

How it connects
Saved through HomeInventoryContext
Used in ItemsController
Displayed in Views/Items

 * This is our main business object.

It represents an inventory item in a home.

Later:

EF Core makes a database table from it
forms bind to it
controller saves it

---------------------------------------

ItemID: unique ID
ItemName: what the item is
Category: electronics, jewelry, furniture
Description: useful for insurance details
EstimatedValue: central business field
PurchaseDate: useful record detail
RoomLocation: where it is in the residence
SerialNumber: useful for electronics
IsDeleted: lets you do soft delete later
CreatedDate: useful tracking field

Why use data annotations:
because they help with:

validation
form generation
database schema hints
 * 
 */
using System.ComponentModel.DataAnnotations;

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
        [StringLength(100)]
        public string ItemName { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, 999999999.99)]
        public decimal EstimatedValue { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }

        [StringLength(100)]
        public string RoomLocation { get; set; }

        [StringLength(100)]
        public string SerialNumber { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
