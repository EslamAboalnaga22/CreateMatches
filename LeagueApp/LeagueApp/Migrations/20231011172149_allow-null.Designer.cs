﻿// <auto-generated />
using System;
using LeagueApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LeagueApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231011172149_allow-null")]
    partial class allownull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LeagueApp.Models.Matches", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GoalsTeamOne")
                        .HasColumnType("int");

                    b.Property<int?>("GoalsTeamTwo")
                        .HasColumnType("int");

                    b.Property<int>("TeamOne")
                        .HasColumnType("int");

                    b.Property<int>("TeamTwo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamOne");

                    b.HasIndex("TeamTwo");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("LeagueApp.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Drawn")
                        .HasColumnType("int");

                    b.Property<int>("GA")
                        .HasColumnType("int");

                    b.Property<int>("GD")
                        .HasColumnType("int");

                    b.Property<int>("GF")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lost")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Played")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("Won")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("LeagueApp.Models.Matches", b =>
                {
                    b.HasOne("LeagueApp.Models.Team", "One")
                        .WithMany("MatchesOne")
                        .HasForeignKey("TeamOne")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LeagueApp.Models.Team", "Two")
                        .WithMany("MatchesTwo")
                        .HasForeignKey("TeamTwo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("One");

                    b.Navigation("Two");
                });

            modelBuilder.Entity("LeagueApp.Models.Team", b =>
                {
                    b.Navigation("MatchesOne");

                    b.Navigation("MatchesTwo");
                });
#pragma warning restore 612, 618
        }
    }
}
