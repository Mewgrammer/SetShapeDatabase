﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SetShapeDatabase;

namespace SetShapeDatabase.Migrations
{
    [DbContext(typeof(SetShapeContext))]
    [Migration("20190523084609_serializationOptimization")]
    partial class serializationOptimization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SetShapeDatabase.Entities.HistoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("Repetitions");

                    b.Property<int>("Sets");

                    b.Property<int?>("TrainingDayId");

                    b.Property<int>("Weight");

                    b.Property<int?>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("TrainingDayId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("HistoryItems");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.TrainingDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("TrainingPlanId");

                    b.HasKey("Id");

                    b.HasIndex("TrainingPlanId");

                    b.ToTable("TrainingDays");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.TrainingDayWorkout", b =>
                {
                    b.Property<int>("TrainingDayId");

                    b.Property<int>("WorkoutId");

                    b.HasKey("TrainingDayId", "WorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("TrainingDayWorkouts");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.TrainingPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TrainingPlans");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurrentTrainingPlanId");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("CurrentTrainingPlanId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.HistoryItem", b =>
                {
                    b.HasOne("SetShapeDatabase.Entities.TrainingDay")
                        .WithMany("History")
                        .HasForeignKey("TrainingDayId");

                    b.HasOne("SetShapeDatabase.Entities.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.TrainingDay", b =>
                {
                    b.HasOne("SetShapeDatabase.Entities.TrainingPlan")
                        .WithMany("Days")
                        .HasForeignKey("TrainingPlanId");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.TrainingDayWorkout", b =>
                {
                    b.HasOne("SetShapeDatabase.Entities.TrainingDay", "TrainingDay")
                        .WithMany("TrainingDayWorkouts")
                        .HasForeignKey("TrainingDayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SetShapeDatabase.Entities.Workout", "Workout")
                        .WithMany("TrainingDayWorkouts")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.TrainingPlan", b =>
                {
                    b.HasOne("SetShapeDatabase.Entities.User")
                        .WithMany("Trainings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.User", b =>
                {
                    b.HasOne("SetShapeDatabase.Entities.TrainingPlan", "CurrentTrainingPlan")
                        .WithMany()
                        .HasForeignKey("CurrentTrainingPlanId");
                });
#pragma warning restore 612, 618
        }
    }
}
