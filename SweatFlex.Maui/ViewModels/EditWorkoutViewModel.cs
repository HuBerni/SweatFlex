using CommunityToolkit.Mvvm.ComponentModel;

namespace SweatFlex.Maui.ViewModels
{
    [QueryProperty("WorkoutId", "WorkoutId")]
    public partial class EditWorkoutViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _workoutId;

        public EditWorkoutViewModel()
        {
            var x = WorkoutId;
        }
    }
}
