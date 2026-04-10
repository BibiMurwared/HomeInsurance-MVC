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
    /// <summary>
    /// The LoginViewModel class stores user input for the login form.
    /// </summary>
    public class LoginViewModel
    {
    }
}
