using System;

namespace SmartManage.Components.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Unique identifier for login
        public string Email { get; set; } = string.Empty;

        // Stored as base64 strings
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
