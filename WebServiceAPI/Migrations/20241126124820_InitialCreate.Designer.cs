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
    [Migration("20241126124820_InitialCreate")]
    partial class InitialCreate
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

                    b.Property<int?>("MRIImageID1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MRIImageUserID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MoleImageID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MoleImageID1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MoleImageUserID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RelatedItemType")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserID1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("XrayImageID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("XrayImageID1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("XrayImageUserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlertID");

                    b.HasIndex("UserID");

                    b.HasIndex("UserID1");

                    b.HasIndex("MRIImageID1", "MRIImageUserID");

                    b.HasIndex("MoleImageID1", "MoleImageUserID");

                    b.HasIndex("XrayImageID1", "XrayImageUserID");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("DND1.Models.MRIImage", b =>
                {
                    b.Property<int>("MRIImageID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
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

                    b.Property<int?>("UserID1")
                        .HasColumnType("INTEGER");

                    b.HasKey("MRIImageID", "UserID");

                    b.HasIndex("UserID");

                    b.HasIndex("UserID1");

                    b.ToTable("MRIImages");
                });

            modelBuilder.Entity("DND1.Models.MedicalData", b =>
                {
                    b.Property<int>("MedicalDataID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BloodType")
                        .HasColumnType("TEXT");

                    b.Property<float?>("Height")
                        .HasColumnType("REAL");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<float?>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("MedicalDataID");

                    b.HasIndex("UserID");

                    b.ToTable("MedicalData");
                });

            modelBuilder.Entity("DND1.Models.MoleImage", b =>
                {
                    b.Property<int>("MoleImageID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
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

                    b.Property<int?>("UserID1")
                        .HasColumnType("INTEGER");

                    b.HasKey("MoleImageID", "UserID");

                    b.HasIndex("UserID");

                    b.HasIndex("UserID1");

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

                    b.Property<DateTime>("UploadedAt")
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

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DND1.Models.XrayImage", b =>
                {
                    b.Property<int>("XrayImageID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
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

                    b.Property<int?>("UserID1")
                        .HasColumnType("INTEGER");

                    b.HasKey("XrayImageID", "UserID");

                    b.HasIndex("UserID");

                    b.HasIndex("UserID1");

                    b.ToTable("XrayImages");
                });

            modelBuilder.Entity("DND1.Models.Alert", b =>
                {
                    b.HasOne("DND1.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("Alerts")
                        .HasForeignKey("UserID1");

                    b.HasOne("DND1.Models.MRIImage", "MRIImage")
                        .WithMany()
                        .HasForeignKey("MRIImageID1", "MRIImageUserID");

                    b.HasOne("DND1.Models.MoleImage", "MoleImage")
                        .WithMany()
                        .HasForeignKey("MoleImageID1", "MoleImageUserID");

                    b.HasOne("DND1.Models.XrayImage", "XrayImage")
                        .WithMany()
                        .HasForeignKey("XrayImageID1", "XrayImageUserID");

                    b.Navigation("MRIImage");

                    b.Navigation("MoleImage");

                    b.Navigation("User");

                    b.Navigation("XrayImage");
                });

            modelBuilder.Entity("DND1.Models.MRIImage", b =>
                {
                    b.HasOne("DND1.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("MRIImages")
                        .HasForeignKey("UserID1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DND1.Models.MedicalData", b =>
                {
                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("MedicalData")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DND1.Models.MoleImage", b =>
                {
                    b.HasOne("DND1.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("MoleImages")
                        .HasForeignKey("UserID1");

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
                    b.HasOne("DND1.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DND1.Models.User", "User")
                        .WithMany("XrayImages")
                        .HasForeignKey("UserID1");

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