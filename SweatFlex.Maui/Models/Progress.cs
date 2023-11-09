using System.Collections.ObjectModel;

namespace SweatFlex.Maui.Models
{
    /// <summary>
    /// Progress is a model class that represents a user's progress by trainingexercises
    /// </summary>
    public class Progress
    {
        public Workout Workout { get; set; }
        public int SessionId { get; set; }
        public decimal? TotalWeight { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public DateOnly Date { get; set; }
        public ObservableCollection<TrainingExercise> TrainingExercises { get; set; } = new();
    }
}
