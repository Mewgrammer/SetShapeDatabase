﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SetShapeDatabase.Entities
{
    public class TrainingPlan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TrainingDay> Days { get; set; } = new List<TrainingDay>();

        public void PrepareSerialize(ICollection<Workout> workouts)
        {
            foreach (var day in Days)
            {
                day.PrepareSerialize(workouts);
            }
        }

    }
}
