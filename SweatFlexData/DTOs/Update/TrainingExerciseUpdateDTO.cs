using SweatFlexData.DTOs;
using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs.Update
{
    public class TrainingExerciseUpdateDTO : ITrainingExerciseDTO
    {
        public decimal? Weight { get; set; }

        public int? Reputations { get; set; }

        public int? TimeInSec { get; set; }

        public DateTime? ExerciseExecuted { get; set; }
    }
}
