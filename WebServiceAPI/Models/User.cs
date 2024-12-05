using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DND1.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; } // Primary Key

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(20)]
        public string UserType { get; set; } = "Basic"; // Default to "Basic"

        [Phone]
        public string? PhoneNumber { get; set; } // Optional field for user's phone number

        [Phone]
        public string? EmergencyContact { get; set; } // New field for emergency contact

        [StringLength(255)]
        public string? Bio { get; set; } // Optional field for user bio

        public string? ProfilePictureUrl { get; set; } // Optional field for profile picture URL

        // Navigation Properties
        public ICollection<MedicalData>? MedicalData { get; set; }
        public ICollection<ProfilePicture>? ProfilePictures { get; set; }
        public ICollection<MoleImage>? MoleImages { get; set; }
        public ICollection<MRIImage>? MRIImages { get; set; }
        public ICollection<XrayImage>? XrayImages { get; set; }
        public ICollection<Alert>? Alerts { get; set; }
    }
}
