using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetShapeDatabase.Entities
{
    public class TrainingDayWorkout
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }

        public int TrainingDayId { get; set; }
        public TrainingDay TrainingDay { get; set; }
    }
}
