﻿// <auto-generated />
using System;
using CanaRails.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(CanaRailsContext))]
    partial class CanaRailsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Hostnames")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Apps");
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

                    b.Property<int>("ContainerType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PublishOrderID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("PublishOrderID");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Entry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    b.Property<int?>("AppID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.ToTable("Entries");
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

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.PublishOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EntryID")
                        .HasColumnType("integer");

                    b.Property<int>("ImageID")
                        .HasColumnType("integer");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.Property<int>("Replica")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ID");

                    b.HasIndex("EntryID");

                    b.HasIndex("ImageID");

                    b.ToTable("PublishOrders");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Container", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.PublishOrder", "PublishOrder")
                        .WithMany()
                        .HasForeignKey("PublishOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PublishOrder");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Entry", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.App", null)
                        .WithMany("Entries")
                        .HasForeignKey("AppID");

                    b.HasOne("CanaRails.Database.Entities.App", "App")
                        .WithOne("DefaultEntry")
                        .HasForeignKey("CanaRails.Database.Entities.Entry", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("CanaRails.Database.Entities.EntryMatcher", "EntryMatchers", b1 =>
                        {
                            b1.Property<int>("EntryID")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("Key")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("EntryID", "Id");

                            b1.ToTable("Entries");

                            b1.ToJson("EntryMatchers");

                            b1.WithOwner()
                                .HasForeignKey("EntryID");
                        });

                    b.Navigation("App");

                    b.Navigation("EntryMatchers");
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

            modelBuilder.Entity("CanaRails.Database.Entities.PublishOrder", b =>
                {
                    b.HasOne("CanaRails.Database.Entities.Entry", null)
                        .WithMany("PublishOrders")
                        .HasForeignKey("EntryID");

                    b.HasOne("CanaRails.Database.Entities.Entry", "Entry")
                        .WithOne("CurrentPublishOrder")
                        .HasForeignKey("CanaRails.Database.Entities.PublishOrder", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CanaRails.Database.Entities.Image", "Image")
                        .WithMany("PublishOrders")
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.App", b =>
                {
                    b.Navigation("DefaultEntry");

                    b.Navigation("Entries");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Entry", b =>
                {
                    b.Navigation("CurrentPublishOrder");

                    b.Navigation("PublishOrders");
                });

            modelBuilder.Entity("CanaRails.Database.Entities.Image", b =>
                {
                    b.Navigation("PublishOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
