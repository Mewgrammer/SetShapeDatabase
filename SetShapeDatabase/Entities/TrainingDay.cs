using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SetShapeDatabase.Entities
{
    public class TrainingDay
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Workout> Workouts { get; set; }

        public List<HistoryItem> History { get; set; }
    }
}
