using System;
using System.ComponentModel.DataAnnotations;

namespace DND1.Models
{
    public class MoleImage
    {
        public int MoleImageID { get; set; } // Primary Key

        [Required]
        public int UserID { get; set; } // Foreign Key, links the image to a specific user

        [Required]
        public string FileName { get; set; } = string.Empty; // Name of the uploaded file

        [Required]
        public string FilePath { get; set; } = string.Empty; // Path where the file is stored locally

        [Required]
        public string Url { get; set; } = string.Empty; // URL to access the image

        public string? ThumbnailUrl { get; set; } = string.Empty; // URL to access the thumbnail image

        [Required]
        public string AnalysisResults { get; set; } = string.Empty; // Analysis results of the image

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the image was uploaded

        // Navigation Properties
        public User? User { get; set; } // Links to the User model (optional, depending on use case)
    }
}