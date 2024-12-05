using System;

namespace DND1.Models
{
    public class ProfilePicture
    {
        public int ProfilePictureID { get; set; } // Primary Key
        public int UserID { get; set; } // Foreign Key
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty; // URL to access the image
        public string ThumbnailUrl { get; set; } = string.Empty; // URL to access the thumbnail
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User? User { get; set; }
    }
}