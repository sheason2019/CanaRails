﻿// <auto-generated />
using System;
using CanaRails.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(CanaRailsContext))]
    [Migration("20240504095704_AddContainerEntity")]
    partial class AddContainerEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("CanaRails.Database.Entities.App", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.AppMatch", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.ToTable("AppMatch");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Container", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContainerID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntryID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ImageID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("EntryID");

                    b.HasIndex("ImageID");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Entry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.EntryMatch", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntryID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("EntryID");

                    b.ToTable("EntryMatch");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Image", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Registry")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.AppMatch", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.App", "App")
                        .WithMany("AppMatches")
                        .HasForeignKey("AppID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Container", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.Entry", "Entry")
                        .WithMany("Containers")
                        .HasForeignKey("EntryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CanaRails.Database.Entities.Image", "Image")
                        .WithMany("Containers")
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Entry", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.App", "App")
                        .WithMany("Entries")
                        .HasForeignKey("AppID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.EntryMatch", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.Entry", "Entry")
                        .WithMany("EntryMatches")
                        .HasForeignKey("EntryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Image", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.App", "App")
                        .WithMany("Images")
                        .HasForeignKey("AppID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.App", b =>
                {
                    b.Navigation("AppMatches");

                    b.Navigation("Entries");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Entry", b =>
                {
                    b.Navigation("Containers");

                    b.Navigation("EntryMatches");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Image", b =>
                {
                    b.Navigation("Containers");
                });
#pragma warning restore 612, 618
        }
    }
}
