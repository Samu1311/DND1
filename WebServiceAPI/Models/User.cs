namespace DND1.Models
{
    public class User
    {
        public string Id { get; set; } // Unique ID for the user
        public string Email { get; set; } // Email address (unique identifier)
        public string PasswordHash { get; set; } // Store hashed password
        public string FullName { get; set; }
    }
}
