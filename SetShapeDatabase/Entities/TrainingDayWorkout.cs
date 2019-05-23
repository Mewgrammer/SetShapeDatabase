using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SetShapeDatabase.Entities
{
    public class TrainingDayWorkout
    {
        public int WorkoutId { get; set; }
        public int TrainingDayId { get; set; }

        public Workout Workout { get; set; }
        public TrainingDay TrainingDay { get; set; }
    }
}
