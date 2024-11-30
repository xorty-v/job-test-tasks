﻿// <auto-generated />
using System;
using KIPServiceTestTask.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KIPServiceTestTask.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KIPServiceTestTask.Entities.QueryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("QueryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RequestLocalTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserDataId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserDataId");

                    b.ToTable("Queries");
                });

            modelBuilder.Entity("KIPServiceTestTask.Entities.UserStatisticModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("TimeIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TimeOut")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("UserStatisticModel");
                });

            modelBuilder.Entity("KIPServiceTestTask.Entities.QueryModel", b =>
                {
                    b.HasOne("KIPServiceTestTask.Entities.UserStatisticModel", "UserData")
                        .WithMany()
                        .HasForeignKey("UserDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserData");
                });
#pragma warning restore 612, 618
        }
    }
}
