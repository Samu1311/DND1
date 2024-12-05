﻿// <auto-generated />
using System;
using DND1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebServiceAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241205082922_MigrationIssues")]
    partial class MigrationIssues
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("DND1.Models.Alert", b =>
                {
                    b.Property<int>("AlertID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRead")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MRIImageID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MoleImageID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RelatedItemType")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("XrayImageID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlertID");

                    b.HasIndex("MRIImageID");

                    b.HasIndex("MoleImageID");

                    b.HasIndex("UserID");

                    b.HasIndex("XrayImageID");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("DND1.Models.MRIImage", b =>
                {
                    b.Property<int>("MRIImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnalysisResults")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("MRIImageID");

                    b.HasIndex("UserID");

                    b.ToTable("MRIImages");
                });

            modelBuilder.Entity("DND1.Models.MedicalData", b =>
                {
                    b.Property<int>("MedicalDataID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Alcohol")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("DairyAllergy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("GlutenAllergy")
                        .HasColumnType("INTEGER");

                    b.Property<float?>("Height")
                        .HasColumnType("REAL");

                    b.Property<string>("OtherAllergies")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("PeanutsAllergy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PollenAllergy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShellfishAllergy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Smoking")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<float?>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("MedicalDataID");

                    b.HasIndex("UserId");

                    b.ToTable("MedicalData");
                });

            modelBuilder.Entity("DND1.Models.MoleImage", b =>
                {
                    b.Property<int>("MoleImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnalysisResults")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("MoleImageID");

                    b.HasIndex("UserID");

                    b.ToTable("MoleImages");
                });

            modelBuilder.Entity("DND1.Models.ProfilePicture", b =>
                {
                    b.Property<int>("ProfilePictureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProfilePictureID");

                    b.HasIndex("UserID");

                    b.ToTable("ProfilePictures");
                });

            modelBuilder.Entity("DND1.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmergencyContact")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Basic");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DND1.Models.XrayImage", b =>
                {
                    b.Property<int>("XrayImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnalysisResults")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("XrayImageID");

                    b.HasIndex("UserID");

                    b.ToTable("XrayImages");
                });

            modelBuilder.Entity("DND1.Models.Alert", b =>
                {
                    b.HasOne("DND1.Models.MRIImage", "MRIImage")
                        .WithMany()
                        .HasForeignKey("MRIImageID");

                    b.HasOne("DND1.Models.MoleImage", "MoleImage")
                        .WithMany()
                        .HasForeignKey("MoleImageID");

                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("Alerts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DND1.Models.XrayImage", "XrayImage")
                        .WithMany()
                        .HasForeignKey("XrayImageID");

                    b.Navigation("MRIImage");

                    b.Navigation("MoleImage");

                    b.Navigation("User");

                    b.Navigation("XrayImage");
                });

            modelBuilder.Entity("DND1.Models.MRIImage", b =>
                {
                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("MRIImages")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DND1.Models.MedicalData", b =>
                {
                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("MedicalData")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DND1.Models.MoleImage", b =>
                {
                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("MoleImages")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DND1.Models.ProfilePicture", b =>
                {
                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("ProfilePictures")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DND1.Models.XrayImage", b =>
                {
                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("XrayImages")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DND1.Models.User", b =>
                {
                    b.Navigation("Alerts");

                    b.Navigation("MRIImages");

                    b.Navigation("MedicalData");

                    b.Navigation("MoleImages");

                    b.Navigation("ProfilePictures");

                    b.Navigation("XrayImages");
                });
#pragma warning restore 612, 618
        }
    }
}
