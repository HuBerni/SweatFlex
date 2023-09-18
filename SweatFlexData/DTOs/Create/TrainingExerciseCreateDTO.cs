using SweatFlexData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs.Create
{
    public class TrainingExerciseCreateDTO
    {
        public decimal? Weight { get; set; }

        public int? Reputations { get; set; }

        public int? TimeInSec { get; set; }

        public int UserId { get; set; }

        public int ExerciseId { get; set; }
    }
}
