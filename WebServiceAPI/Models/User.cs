using System;
using System.Collections.Generic;

namespace DND1.Models
{
    public class User
    {
        public int UserID { get; set; } // Primary Key
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string UserType { get; set; } = "Basic"; // Default to "Basic"

        // Navigation Properties
        public ICollection<MedicalData>? MedicalData { get; set; }
        public ICollection<ProfilePicture>? ProfilePictures { get; set; }
        public ICollection<MoleImage>? MoleImages { get; set; }
        public ICollection<MRIImage>? MRIImages { get; set; }
        public ICollection<XrayImage>? XrayImages { get; set; }
        public ICollection<Alert>? Alerts { get; set; }
    }
}
