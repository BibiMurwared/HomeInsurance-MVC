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
    /// <summary>
    /// The RegisterViewModel class stores user input for the registration form.
    /// </summary>
    public class RegisterViewModel
    {
    }
}
