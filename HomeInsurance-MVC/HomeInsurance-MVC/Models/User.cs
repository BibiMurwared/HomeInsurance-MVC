/*
 * File: User.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: [Enter Date]
 * Description:
 * This model represents a registered user of the application. It stores
 * account-related information required for authentication, authorization,
 * and ownership of inventory items.
 */

/*Purpose: Represents an application user.

What it does

Defines the data for a registered user.

Typical fields:

UserID
FullName
Email
PasswordHash
PhoneNumber
IsActive
IsDeleted
CreatedDate
Why it exists

The system needs to know:

who is logged in
which items belong to which user
How it connects
Used by AccountController
Saved through HomeInventoryContext
Linked to items by UserID*/
namespace HomeInsurance_MVC.Models
{
    /// <summary>
    /// The User class stores account information for a person using the application.
    /// It is used to identify the user and associate inventory items with that user.
    /// </summary>
    public class User
    {
    }
}
