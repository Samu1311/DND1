using System;
using System.ComponentModel.DataAnnotations;

namespace DND1.Models
{
    public class MedicalData
    {
        // Primary Key
        public int MedicalDataID { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Personal Information
        [Range(0, 500, ErrorMessage = "Weight must be between 0 and 500 kg.")]
        public float? Weight { get; set; }

        [Range(0, 300, ErrorMessage = "Height must be between 0 and 300 cm.")]
        public float? Height { get; set; }

        [RegularExpression("^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid Blood Type.")]
        public string? BloodType { get; set; }
        public User? User { get; set; }

        // Habits
        public bool Smoking { get; set; }

        [Required]
        public AlcoholConsumption Alcohol { get; set; }

        // Allergies
        public bool PeanutsAllergy { get; set; }
        public bool ShellfishAllergy { get; set; }
        public bool DairyAllergy { get; set; }
        public bool GlutenAllergy { get; set; }
        public bool PollenAllergy { get; set; }
        public string? OtherAllergies { get; set; }
    }

    public enum AlcoholConsumption
    {
        None,
        Occasionally,
        Regularly
    }
}