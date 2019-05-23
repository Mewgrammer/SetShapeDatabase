using Microsoft.EntityFrameworkCore;
using SetShapeDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetShapeDatabase
{
    public sealed class SetShapeContext : DbContext
    {
        private static readonly bool[] _migrated = { false };

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<TrainingDay> TrainingDays { get; set; }
        public DbSet<HistoryItem> HistoryItems { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<TrainingDayWorkout> TrainingDayWorkouts { get; set; }
        public DbSet<User> Users { get; set; }


        public SetShapeContext(DbContextOptions options)
            : base(options)
        {
            if (!_migrated[0])
                lock (_migrated)
                    if (!_migrated[0])
                    {
                        Database.Migrate();
                        _migrated[0] = true;
                    }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // PersonPlace
            builder.Entity<User>()
                .HasOne(t => t.CurrentTrainingPlan);
            builder.Entity<User>()
                .HasMany(u => u.Trainings);
            builder.Entity<TrainingPlan>()
                .HasMany(t => t.Days);
            builder.Entity<TrainingDay>()
                .HasMany(t => t.History);
            builder.Entity<TrainingDayWorkout>()
                .HasKey(x => new {x.TrainingDayId, x.WorkoutId});
            builder.Entity<TrainingDayWorkout>()
                .HasOne( tdw => tdw.TrainingDay)
                .WithMany(d => d.TrainingDayWorkouts)
                .HasForeignKey(tdw => tdw.TrainingDayId);
            builder.Entity<TrainingDayWorkout>()
                .HasOne(tdw => tdw.Workout)
                .WithMany(d => d.TrainingDayWorkouts)
                .HasForeignKey(tdw => tdw.WorkoutId);
            builder.Entity<HistoryItem>()
                .HasOne(o => o.Workout);
        }

    }

}
