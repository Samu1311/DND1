using System;

namespace DND1.Models
{
    public class Alert
    {
        public int AlertID { get; set; } // Primary Key
        public int UserID { get; set; } // Foreign Key
        public bool IsRead { get; set; } = false;
        public string? RelatedItemType { get; set; } // Mole, MRI, Xray
        public int? MoleImageID { get; set; }
        public int? MRIImageID { get; set; }
        public int? XrayImageID { get; set; }

        // Navigation Properties
        public User? User { get; set; }
        public MoleImage? MoleImage { get; set; }
        public MRIImage? MRIImage { get; set; }
        public XrayImage? XrayImage { get; set; }
    }
}
