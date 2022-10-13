﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using vendio_backend.Models;

#nullable disable

namespace vendio_backend.Migrations
{
    [DbContext(typeof(vendionContext))]
    [Migration("20221013140453_2442333")]
    partial class _2442333
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("datingAppBackend.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isProductPicture")
                        .HasColumnType("boolean");

                    b.Property<int>("productId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("photos");
                });

            modelBuilder.Entity("datingAppBackend.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("bornDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("countryId")
                        .HasColumnType("integer");

                    b.Property<bool>("deletedAccount")
                        .HasColumnType("boolean");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("lastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("showNumber")
                        .HasColumnType("boolean");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("vendio_backend.Models.favoriteVehiclesMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.Property<int>("vehicleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("favoritesMapping");
                });

            modelBuilder.Entity("vendio_backend.Models.vehicle", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("condition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("contactPhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("createdBy")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("features")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<bool>("isEnabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("isOffer")
                        .HasColumnType("boolean");

                    b.Property<bool>("isPublished")
                        .HasColumnType("boolean");

                    b.Property<string>("model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("modificationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("price")
                        .HasColumnType("bigint");

                    b.Property<string>("registerDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("vim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("year")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}