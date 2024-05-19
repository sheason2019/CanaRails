﻿// <auto-generated />
using System;
using CanaRails.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(CanaRailsContext))]
    [Migration("20240519134044_AddImageReadyStatus")]
    partial class AddImageReadyStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CanaRails.Database.Entities.App", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.AppMatcher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("AppID")
                        .HasColumnType("integer");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.ToTable("AppMatchers");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Container", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("ContainerID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EntryID")
                        .HasColumnType("integer");

                    b.Property<int>("ImageID")
                        .HasColumnType("integer");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("EntryID");

                    b.HasIndex("ImageID");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Entry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("AppID")
                        .HasColumnType("integer");

                    b.Property<bool>("Default")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.EntryMatcher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("EntryID")
                        .HasColumnType("integer");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("EntryID");

                    b.ToTable("EntryMatchers");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Image", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("AppID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Ready")
                        .HasColumnType("boolean");

                    b.Property<string>("Registry")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.AppMatcher", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.App", "App")
                        .WithMany("AppMatchers")
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

            modelBuilder.Entity("CanaRails.Database.Entities.EntryMatcher", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.Entry", "Entry")
                        .WithMany("EntryMatchers")
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
                    b.Navigation("AppMatchers");

                    b.Navigation("Entries");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Entry", b =>
                {
                    b.Navigation("Containers");

                    b.Navigation("EntryMatchers");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Image", b =>
                {
                    b.Navigation("Containers");
                });
#pragma warning restore 612, 618
        }
    }
}
