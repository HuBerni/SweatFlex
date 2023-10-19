using SweatFlexData.DTOs;
using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs.Create
{
    public class TrainingExerciseCreateDTO : ITrainingExerciseDTO
    {
        public decimal? Weight { get; set; }

        public int? Reputations { get; set; }

        public int? TimeInSec { get; set; }

        public string UserId { get; set; }

        public int WorkoutExerciseId { get; set; }

        public int ExerciseId { get; set; }
        public int? SessionId { get; set; }
    }
}
