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

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<TrainingDay> TrainingDays { get; set; }
        public DbSet<HistoryItem> HistoryItems { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
