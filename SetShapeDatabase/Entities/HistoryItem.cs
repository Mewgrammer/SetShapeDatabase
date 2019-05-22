using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SetShapeDatabase.Entities
{
    public class HistoryItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Sets { get; set; }
        
        public int Repetitions { get; set; }

        public int Weight { get; set; }

        public Workout Workout { get; set; }
    }
}
