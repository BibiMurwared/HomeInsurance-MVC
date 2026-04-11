namespace HomeInsurance_MVC.Services
{
    using HomeInsurance_MVC.Models;
    using HomeInsurance_MVC.Repositories;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Implements account logic on top of repository access.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        /// <summary>
        /// Creates a service with repository dependencies.
        /// </summary>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        /// <summary>
        /// Gets all active users.
        /// </summary>
        public async Task<List<User>> GetAllAsync()
        {
            List<User> users = await _userRepository.GetAllAsync();
            return users;
        }

        /// <summary>
        /// Gets one user by identifier.
        /// </summary>
        public async Task<User?> GetByIdAsync(Guid userId)
        {
            User? user = await _userRepository.GetByIdAsync(userId);
            return user;
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

            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            await _userRepository.AddAsync(user);
            return user;
        }

        /// <summary>
        /// Validates login credentials for one user.
        /// </summary>
        public async Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            User? authenticatedUser = null;
            User? user = await _userRepository.GetByEmailAsync(email);
            if (user is not null)
            {
                PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                if (verificationResult == PasswordVerificationResult.Success)
                {
                    authenticatedUser = user;
                }
            }

            return authenticatedUser;
        }

        /// <summary>
        /// Updates account profile fields.
        /// </summary>
        public async Task UpdateProfileAsync(Guid userId, string fullName, string? phoneNumber, bool isActive)
        {
            User? user = await _userRepository.GetByIdAsync(userId);
            if (user is not null)
            {
                user.FullName = fullName;
                user.PhoneNumber = phoneNumber;
                user.IsActive = isActive;
                await _userRepository.UpdateAsync(user);
            }
        }

        /// <summary>
        /// Soft deletes the account.
        /// </summary>
        public async Task SoftDeleteAsync(Guid userId)
        {
            await _userRepository.SoftDeleteAsync(userId);
        }
    }
}
