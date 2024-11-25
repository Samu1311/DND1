using System;

namespace DND1.Models
{
    public class MoleImage
    {
        public int Id { get; set; } // Primary Key
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string? AnalysisResult { get; set; } // Optional result field
    }
}
