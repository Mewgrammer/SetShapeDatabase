using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SetShapeDatabase.Entities;

namespace SetShapeDatabase.Controller.Forms
{
    public class TrainingPlanDayForm
    {
        [Required]
        public int TrainingPlanId { get; set; }

        [Required]
        public TrainingDay Day { get; set; }
    }
}
