using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SetShapeDatabase.Entities
{
    public class TrainingDay
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<HistoryItem> History { get; set; } = new List<HistoryItem>();

        [NotMapped]
        public List<Workout> Workouts { get; set; } = new List<Workout>();

        [JsonIgnore]
        public List<TrainingDayWorkout> TrainingDayWorkouts { get; set; } = new List<TrainingDayWorkout>();

        public void PrepareSerialize(ICollection<Workout> workouts)
        {
            Workouts = workouts.Where(w => TrainingDayWorkouts.Any(tdw => tdw.WorkoutId == w.Id || tdw.Workout?.Id == w.Id)).ToList();
        }
    }
}
