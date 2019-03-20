﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using _322Api.Models;

namespace _322Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190320000835_migration_4")]
    partial class migration_4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("_322Api.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ComponentName");

                    b.Property<int>("DeviceID");

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("_322Api.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastCrawl");

                    b.Property<string>("Name");

                    b.Property<string[]>("Providers");

                    b.Property<int>("Score");

                    b.HasKey("Id");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("_322Api.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PhoneId");

                    b.Property<string>("ReviewText");

                    b.Property<int>("SourceId");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("_322Api.Models.ReviewSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SourceName");

                    b.HasKey("Id");

                    b.ToTable("ReviewSources");
                });

            modelBuilder.Entity("_322Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string[]>("History");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}