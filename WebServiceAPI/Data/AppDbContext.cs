using Microsoft.EntityFrameworkCore;
using DND1.Models;

namespace DND1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define DbSets for each table
        public DbSet<User>? Users { get; set; }
        public DbSet<MedicalData>? MedicalData { get; set; }
        public DbSet<ProfilePicture>? ProfilePictures { get; set; }
        public DbSet<MoleImage>? MoleImages { get; set; }
        public DbSet<MRIImage>? MRIImages { get; set; }
        public DbSet<XrayImage>? XrayImages { get; set; }
        public DbSet<Alert>? Alerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define Primary Keys
            modelBuilder.Entity<MoleImage>().HasKey(mi => mi.MoleImageID);
            modelBuilder.Entity<MRIImage>().HasKey(mri => mri.MRIImageID);
            modelBuilder.Entity<XrayImage>().HasKey(xri => xri.XrayImageID);
            modelBuilder.Entity<Alert>().HasKey(a => a.AlertID);

            // Define Relationships

            // MoleImage
            modelBuilder.Entity<MoleImage>()
                .HasOne(mi => mi.User)
                .WithMany(u => u.MoleImages) // Assuming User has a collection of MoleImages
                .HasForeignKey(mi => mi.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // MRIImage
            modelBuilder.Entity<MRIImage>()
                .HasOne(mri => mri.User)
                .WithMany(u => u.MRIImages) // Assuming User has a collection of MRIImages
                .HasForeignKey(mri => mri.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // XrayImage
            modelBuilder.Entity<XrayImage>()
                .HasOne(xri => xri.User)
                .WithMany(u => u.XrayImages) // Assuming User has a collection of XrayImages
                .HasForeignKey(xri => xri.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Alerts
            modelBuilder.Entity<Alert>()
                .HasOne(a => a.User)
                .WithMany(u => u.Alerts) // Assuming User has a collection of Alerts
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);

                // Add default constraints for UserType
            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasDefaultValue("Basic");
        }
    }
}
