using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs
{
    public class TrainingExerciseDTO : ITrainingExerciseDTO
    {
        public int Id { get; set; }

        public decimal? Weight { get; set; }

        public int? Reputations { get; set; }

        public int? TimeInSec { get; set; }

        public UserDTO User { get; set; }

        public DateTime? ExerciseExecuted { get; set; }

        public ExerciseDTO Exercise { get; set; }
    }
}
