﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreManagementSystemX.Database;

#nullable disable

namespace StoreManagementSystemX.Infrastructure.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20231222160354_InitialMigrate")]
    partial class InitialMigrate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("StoreManagementSystemX.Infrastructure.DTO.ProductDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("TEXT");

                    b.Property<int>("InStock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("StoreManagementSystemX.Infrastructure.DTO.UserDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c1a51ff5-e8c0-45a2-8061-d4badb8aa42f"),
                            Password = "password",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("StoreManagementSystemX.Infrastructure.DTO.ProductDTO", b =>
                {
                    b.HasOne("StoreManagementSystemX.Infrastructure.DTO.UserDTO", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("StoreManagementSystemX.Infrastructure.DTO.UserDTO", b =>
                {
                    b.HasOne("StoreManagementSystemX.Infrastructure.DTO.UserDTO", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });
#pragma warning restore 612, 618
        }
    }
}
