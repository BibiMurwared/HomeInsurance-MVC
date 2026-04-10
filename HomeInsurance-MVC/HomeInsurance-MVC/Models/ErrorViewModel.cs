/*
 * File: ErrorViewModel.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: [Enter Date]
 * Description:
 * This model stores error-related information used by the shared error page.
 * It helps display request identifiers and supports basic debugging.
 */

/*Models

This is where your data classes go.

Examples:

Item.cs
User.cs
maybe LoginViewModel.cs
maybe RegisterViewModel.cs

What models do:

define your data shape
help EF Core create database tables
help validate form input

Example:
Item.cs defines fields like:

ItemName
Category
EstimatedValue*/
namespace HomeInsurance_MVC.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
