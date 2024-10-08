﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutTrackerApi.DbContexts;

#nullable disable

namespace WorkoutTrackerApi.Migrations
{
    [DbContext(typeof(WorkoutContext))]
    [Migration("20240922235146_AddCascadeDelete")]
    partial class AddCascadeDelete
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("PlanExercise", b =>
                {
                    b.Property<int>("PlanId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlanId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("PlanExercises", (string)null);
                });

            modelBuilder.Entity("WorkoutTrackerApi.Data.Entities.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Categories")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameExercise")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("Reps")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sets")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("ExerciseId");

                    b.ToTable("Exercises");

                    b.HasData(
                        new
                        {
                            ExerciseId = 1,
                            Categories = 1,
                            Description = "",
                            NameExercise = "Bench Press",
                            Reps = 12,
                            Sets = 4,
                            Weight = 100.0
                        },
                        new
                        {
                            ExerciseId = 2,
                            Categories = 1,
                            Description = "",
                            NameExercise = "Incline Dumbbell Press",
                            Reps = 10,
                            Sets = 4,
                            Weight = 30.0
                        },
                        new
                        {
                            ExerciseId = 3,
                            Categories = 1,
                            Description = "",
                            NameExercise = "Openings",
                            Reps = 10,
                            Sets = 4,
                            Weight = 15.0
                        },
                        new
                        {
                            ExerciseId = 4,
                            Categories = 2,
                            Description = "",
                            NameExercise = "Pull-ups",
                            Reps = 8,
                            Sets = 4,
                            Weight = 0.0
                        },
                        new
                        {
                            ExerciseId = 5,
                            Categories = 2,
                            Description = "",
                            NameExercise = "Chest Pull",
                            Reps = 10,
                            Sets = 4,
                            Weight = 70.0
                        },
                        new
                        {
                            ExerciseId = 6,
                            Categories = 2,
                            Description = "",
                            NameExercise = "Dumbbell Row",
                            Reps = 12,
                            Sets = 4,
                            Weight = 30.0
                        });
                });

            modelBuilder.Entity("WorkoutTrackerApi.Data.Entities.Plan", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PlanDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlanDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("PlanState")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlanId");

                    b.HasIndex("UserId");

                    b.ToTable("Plans");

                    b.HasData(
                        new
                        {
                            PlanId = 1,
                            PlanDate = new DateTime(2024, 9, 22, 23, 51, 45, 646, DateTimeKind.Utc).AddTicks(3327),
                            PlanDescription = "Day of work on biggest muscles",
                            PlanName = "Chest and Back",
                            PlanState = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("WorkoutTrackerApi.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("TEXT");

                    b.Property<double>("BodyHeight")
                        .HasColumnType("REAL");

                    b.Property<double>("BodyWeight")
                        .HasColumnType("REAL");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("UserState")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Birthday = new DateTime(1999, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BodyHeight = 180.0,
                            BodyWeight = 93.0,
                            Email = "lorenzocarignani@outlook.com",
                            Name = "Lorenzo",
                            Password = "1234",
                            UserState = true
                        });
                });

            modelBuilder.Entity("PlanExercise", b =>
                {
                    b.HasOne("WorkoutTrackerApi.Data.Entities.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutTrackerApi.Data.Entities.Plan", null)
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutTrackerApi.Data.Entities.Plan", b =>
                {
                    b.HasOne("WorkoutTrackerApi.Data.Entities.User", "User")
                        .WithMany("Plans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkoutTrackerApi.Data.Entities.User", b =>
                {
                    b.Navigation("Plans");
                });
#pragma warning restore 612, 618
        }
    }
}
