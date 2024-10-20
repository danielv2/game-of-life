﻿// <auto-generated />
using System;
using GOF.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GOF.Infra.Migrations
{
    [DbContext(typeof(SQLiteDbContext))]
    [Migration("20241017213157_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("GOF.Domain.Entities.GameEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("InitialState")
                        .HasColumnType("TEXT");

                    b.Property<int>("SquareSideSize")
                        .HasColumnType("INTEGER");

                    b.Property<int>("maxGenerations")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("GameEntity");
                });

            modelBuilder.Entity("GOF.Domain.Entities.GameStageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Generation")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Population")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("GameStages");
                });

            modelBuilder.Entity("GOF.Domain.Entities.GameStageEntity", b =>
                {
                    b.HasOne("GOF.Domain.Entities.GameEntity", "Game")
                        .WithMany("Stages")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("GOF.Domain.Entities.GameEntity", b =>
                {
                    b.Navigation("Stages");
                });
#pragma warning restore 612, 618
        }
    }
}
