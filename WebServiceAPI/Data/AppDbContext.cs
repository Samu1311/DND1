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

            // Composite Primary Keys
            modelBuilder.Entity<MoleImage>().HasKey(mi => new { mi.MoleImageID, mi.UserID });
            modelBuilder.Entity<MRIImage>().HasKey(mri => new { mri.MRIImageID, mri.UserID });
            modelBuilder.Entity<XrayImage>().HasKey(xri => new { xri.XrayImageID, xri.UserID });
            modelBuilder.Entity<Alert>().HasKey(a => a.AlertID);

            // Relationships
            modelBuilder.Entity<MoleImage>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(mi => mi.UserID);

            modelBuilder.Entity<MRIImage>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(mri => mri.UserID);

            modelBuilder.Entity<XrayImage>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(xri => xri.UserID);

            modelBuilder.Entity<Alert>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserID);
        }
    }
}
