using System;

namespace DND1.Models
{
    public class MedicalData
    {
        public int MedicalDataID { get; set; } // Primary Key
        public int UserID { get; set; } // Foreign Key
        public string? BloodType { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }

        // Navigation Properties
        public User? User { get; set; }
    }
}
