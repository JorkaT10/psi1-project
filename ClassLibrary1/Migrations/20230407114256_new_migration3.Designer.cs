﻿// <auto-generated />
using System;
using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClassLibrary.Migrations
{
    [DbContext(typeof(ProjectDatabaseContext))]
    [Migration("20230407114256_new_migration3")]
    partial class newmigration3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ClassLibrary.Advertisement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BuyerId")
                        .HasColumnType("uuid");

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.Property<Guid>("DistributorId")
                        .HasColumnType("uuid");

                    b.Property<string>("MealName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PickupTimeSpan")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeOfMaking")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("DistributorId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("DistributorProfile", b =>
                {
                    b.Property<Guid>("SubscribersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SubscriptionsId")
                        .HasColumnType("uuid");

                    b.HasKey("SubscribersId", "SubscriptionsId");

                    b.HasIndex("SubscriptionsId");

                    b.ToTable("DistributorProfile");
                });

            modelBuilder.Entity("ProfileClasses.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ProfileClasses.Distributor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Distributors");
                });

            modelBuilder.Entity("ProfileClasses.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cuisines")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ProfileClasses.Ratings", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DistributorId")
                        .HasColumnType("uuid");

                    b.Property<int>("rating")
                        .HasColumnType("integer");

                    b.HasKey("Id", "DistributorId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("ClassLibrary.Advertisement", b =>
                {
                    b.HasOne("ProfileClasses.Profile", "Buyer")
                        .WithMany("Reservations")
                        .HasForeignKey("BuyerId");

                    b.HasOne("ProfileClasses.Distributor", "Distributor")
                        .WithMany("Advertisements")
                        .HasForeignKey("DistributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("DistributorProfile", b =>
                {
                    b.HasOne("ProfileClasses.Profile", null)
                        .WithMany()
                        .HasForeignKey("SubscribersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileClasses.Distributor", null)
                        .WithMany()
                        .HasForeignKey("SubscriptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileClasses.Distributor", b =>
                {
                    b.Navigation("Advertisements");
                });

            modelBuilder.Entity("ProfileClasses.Profile", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
