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
                var historyItems = new List<HistoryItem>
                {
                    new HistoryItem
                    {
                        Date = DateTime.Now,
                        Repetitions = 5,
                        Sets = 3,
                        Weight = 100,
                        Workout = workouts[0]
                    },
                    new HistoryItem
                    {
                        Date = DateTime.Now,
                        Repetitions = 5,
                        Sets = 3,
                        Weight = 100,
                        Workout = workouts[1]
                    },
                };
                var trainingDays = new List<TrainingDay>
                {
                    new TrainingDay {Name = "Admin Tag 1", History = {historyItems[1]}},
                    new TrainingDay {Name = "Admin Tag 2", History = {historyItems[0]}}
                };
                var trainingDayWorkouts = new List<TrainingDayWorkout>
                {
                    new TrainingDayWorkout
                    {
                        TrainingDay = trainingDays[0],
                        Workout = workouts[0]
                    },
                    new TrainingDayWorkout
                    {
                        TrainingDay = trainingDays[0],
                        Workout = workouts[1]
                    },
                    new TrainingDayWorkout
                    {
                        TrainingDay = trainingDays[1],
                        Workout = workouts[3]
                    },
                    new TrainingDayWorkout
                    {
                        TrainingDay = trainingDays[1],
                        Workout = workouts[2]
                    },
                };

                var adminUser = new User
                {
                    Name = "Admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("1"),
                    Trainings = new List<TrainingPlan>
                    {
                        new TrainingPlan
                        {
                            Name = "AdminTraining",
                            Days = new List<TrainingDay>
                            {
                                trainingDays[0],
                                trainingDays[1]
                            }
                        }
                    }
                };
                context.TrainingDayWorkouts.AddRange(trainingDayWorkouts);
                context.Users.Add(adminUser);
                context.SaveChanges();

            }
        }
    }
}
