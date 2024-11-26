using System;

namespace DND1.Models
{
    public class ProfilePicture
    {
        public int ProfilePictureID { get; set; } // Primary Key
        public int UserID { get; set; } // Foreign Key
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User? User { get; set; }
    }
}
