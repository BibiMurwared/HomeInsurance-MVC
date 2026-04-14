/*
 * File: UserService.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This class implements the IUserService interface and contains the business logic
 * for user account management. It handles operations such as user registration,
 * authentication, password hashing using BCrypt, profile updates, and account
 * deactivation, while delegating data access to the repository layer.
 */
namespace HomeInsurance_MVC.Services
{
    using HomeInsurance_MVC.Models;
    using HomeInsurance_MVC.Repositories;
    using BCryptNet = BCrypt.Net.BCrypt;

    /// <summary>
    /// Implements account logic on top of repository access.
    /// Uses BCrypt to hash and verify user passwords.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Creates a service with repository dependencies.
        /// </summary>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registers a user with hashed password.
        /// </summary>
        public async Task<User> RegisterAsync(string fullName, string email, string password, string? phoneNumber)
        {
            User? existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser is not null)
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            User user = new User
            {
                UserID = Guid.NewGuid(),
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow
            };

            user.PasswordHash = BCryptNet.HashPassword(password);
            await _userRepository.AddAsync(user);
            return user;
        }

        /// <summary>
        /// Validates login credentials for one user using BCrypt verification.
        /// </summary>
        public async Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            User? authenticatedUser = null;
            User? user = await _userRepository.GetByEmailAsync(email);
            if (user is not null && BCryptNet.Verify(password, user.PasswordHash))
            {
                authenticatedUser = user;
            }

            return authenticatedUser;
        }
    }
}
