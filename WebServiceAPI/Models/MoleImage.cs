using System;

namespace DND1.Models
{
    public class MoleImage
    {
        public int MoleImageID { get; set; } // Primary Key
        public int UserID { get; set; } // Foreign Key
        public string FileName { get; set; } = string.Empty;
        public byte[] ImageData { get; set; } = Array.Empty<byte>(); // Image stored as binary data
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string? AnalysisResults { get; set; }

        // Navigation Properties
        public User? User { get; set; }
    }
}
