using System;

namespace DND1.Models
{
    public class MoleImage
    {
        public int MoleImageID { get; set; } // Primary Key
        public int UserID { get; set; } // Foreign Key
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string? AnalysisResults { get; set; }

        // Navigation Properties
        public User? User { get; set; }
    }
}
