namespace SweatFlex.Maui.Models
{
    public class TrainingExerciseLocal
    {
        public int Id { get; set; }
        public bool Done { get; set; }
        public decimal? Weight { get; set; }
        public int? Reputations { get; set; }
        public int? TimeInSec { get; set; }
        public User User { get; set; }
        public DateTime? ExerciseExecuted { get; set; }
        public Exercise Exercise { get; set; }
        public int WorkoutExerciseId { get; set; }
        public int SessionId { get; set; }
    }
}
