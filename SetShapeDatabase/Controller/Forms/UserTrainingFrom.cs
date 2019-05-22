using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SetShapeDatabase.Entities;

namespace SetShapeDatabase.Controller.Forms
{
    public class UserTrainingForm
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public TrainingPlan TrainingPlan { get; set; }
    }
}
