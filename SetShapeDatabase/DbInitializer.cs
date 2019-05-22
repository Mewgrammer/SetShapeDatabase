using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SetShapeDatabase.Entities;

namespace SetShapeDatabase
{
    public static class DbInitializer
    {
        public static void Initialize(SetShapeContext context)
        {
            context.Database.EnsureCreated();
            var workouts = new List<Workout>()
            {
                new Workout
                {
                    Name = "Bank Drücken"
                },
                new Workout
                {
                    Name = "Triceps Curls"
                },
                new Workout
                {
                    Name = "Dips"
                },
                new Workout
                {
                    Name = "Butterfly"
                }
            };

            // Look for any Workouts.
            if (!context.Workouts.Any())
            {
                context.Workouts.AddRange(workouts);
                context.SaveChanges();
            }
            workouts = context.Workouts.ToList(); // Load Workouts from DB
            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    Name = "Admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("StrengGeheim"),
                    Trainings = new List<TrainingPlan>
                    {
                        new TrainingPlan
                        {
                            Name = "AdminTraining",
                            Days = new List<TrainingDay>
                            {
                                new TrainingDay
                                {
                                    Name = "AdminTag1",
                                    Workouts = new List<Workout>
                                    {
                                        workouts[1],
                                        workouts[3],
                                    }
                                },
                                new TrainingDay
                                {
                                    Name = "AdminTag2",
                                    Workouts = new List<Workout>
                                    {
                                        workouts[2],
                                        workouts[1],
                                    },
                                    History =  new List<HistoryItem>()
                                    {
                                        new HistoryItem()
                                        {
                                            Repetitions = 5,
                                            Sets = 2,
                                            Weight = 999,
                                            Workout = workouts[1]
                                        }
                                    }

                                }
                            }
                        }
                    }
                };
                context.Users.Add(adminUser);
                context.SaveChanges();

            }
        }
    }
}
