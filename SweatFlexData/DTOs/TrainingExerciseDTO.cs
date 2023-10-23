using SweatFlexData.Interface.IDTOs;

namespace SweatFlexData.DTOs
{
    public class TrainingExerciseDTO : ITrainingExerciseDTO
    {
        public int Id { get; set; }

        public decimal? Weight { get; set; }

        public int? Reputations { get; set; }

        public int? TimeInSec { get; set; }
        public int? SessionId { get; set; }

        public UserDTO User { get; set; }

        public DateTime? ExerciseExecuted { get; set; }

        public ExerciseDTO Exercise { get; set; }
    }
}
