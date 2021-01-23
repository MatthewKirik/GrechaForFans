﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(BuckwheatContext))]
    partial class BuckwheatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("DAL.Models.Lot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("TEXT");

                    b.Property<int>("ShopId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WeightInGrams")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("DAL.Models.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LotId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LotId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("DAL.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Shops");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Prom.ua"
                        });
                });

            modelBuilder.Entity("DAL.Models.Lot", b =>
                {
                    b.HasOne("DAL.Models.Shop", "Shop")
                        .WithMany("Lots")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("DAL.Models.Price", b =>
                {
                    b.HasOne("DAL.Models.Lot", "Lot")
                        .WithMany("Prices")
                        .HasForeignKey("LotId");

                    b.Navigation("Lot");
                });

            modelBuilder.Entity("DAL.Models.Lot", b =>
                {
                    b.Navigation("Prices");
                });

            modelBuilder.Entity("DAL.Models.Shop", b =>
                {
                    b.Navigation("Lots");
                });
#pragma warning restore 612, 618
        }
    }
}
