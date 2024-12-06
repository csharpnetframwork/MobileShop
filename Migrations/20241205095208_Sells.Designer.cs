﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Data;

#nullable disable

namespace Shop.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241205095208_Sells")]
    partial class Sells
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shop.Models.RegisterModel", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isadmin")
                        .HasColumnType("bit");

                    b.HasKey("Email");

                    b.ToTable("RegisterModel");
                });

            modelBuilder.Entity("Shop.Models.Sell", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifyBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifyDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SellDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Specification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("iteminstock")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Sells");
                });

            modelBuilder.Entity("Shop.Models.StockItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifyBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifyDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Rate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("iteminstock")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
