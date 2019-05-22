﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SetShapeDatabase;

namespace SetShapeDatabase.Migrations
{
    [DbContext(typeof(SetShapeContext))]
    partial class SetShapeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("TrainingDayId");

                    b.HasKey("Id");

                    b.HasIndex("TrainingDayId");

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

            modelBuilder.Entity("SetShapeDatabase.Entities.TrainingPlan", b =>
                {
                    b.HasOne("SetShapeDatabase.Entities.User")
                        .WithMany("Trainings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SetShapeDatabase.Entities.Workout", b =>
                {
                    b.HasOne("SetShapeDatabase.Entities.TrainingDay")
                        .WithMany("Workouts")
                        .HasForeignKey("TrainingDayId");
                });
#pragma warning restore 612, 618
        }
    }
}
