using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using SmartManage.Components.Data;
using SmartManage.Components.Models;
using System.Text;

namespace SmartManage.Components.Service
{
    public class AuthService
    {
        private readonly SmartManageContext _context;
        private readonly CurrentUserService _currentUser;

        public AuthService(SmartManageContext context, CurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<(bool Success, string? Error)> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            email = email.Trim().ToLowerInvariant();
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return (false, "Email already registered.");

            var salt = RandomNumberGenerator.GetBytes(16);
            var hash = HashPassword(password, salt);

            var user = new User
            {
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                Email = email,
                PasswordSalt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(hash),
                DateCreated = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // set current user (store minimal info)
            _currentUser.SetUser(user);
            return (true, null);
        }

        public async Task<(bool Success, string? Error)> LoginAsync(string email, string password)
        {
            email = email.Trim().ToLowerInvariant();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return (false, "Invalid email or password.");

            var salt = Convert.FromBase64String(user.PasswordSalt);
            var expectedHash = Convert.FromBase64String(user.PasswordHash);

            if (!VerifyPassword(password, salt, expectedHash))
                return (false, "Invalid email or password.");

            _currentUser.SetUser(user);
            return (true, null);
        }

        public void Logout()
        {
            _currentUser.Clear();
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: 100_000,
                HashAlgorithmName.SHA256,
                32);
        }

        private static bool VerifyPassword(string password, byte[] salt, byte[] expectedHash)
        {
            var hash = HashPassword(password, salt);
            return CryptographicOperations.FixedTimeEquals(hash, expectedHash);
        }
    }
}
