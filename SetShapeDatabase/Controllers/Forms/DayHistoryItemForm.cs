using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SetShapeDatabase.Entities;

namespace SetShapeDatabase.Controller.Forms
{
    public class DayHistoryItemForm
    {
        [Required]
        public int DayId { get; set; }

        [Required]
        public HistoryItem Item { get; set; }
    }
}
