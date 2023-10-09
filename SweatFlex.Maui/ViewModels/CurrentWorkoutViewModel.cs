using CommunityToolkit.Mvvm.ComponentModel;
using SweatFlex.Maui.Models;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class CurrentWorkoutViewModel : ObservableObject
    {
        [ObservableProperty]
        private Workout _workout;

        public ObservableCollection<WorkoutExercise>? WorkoutExercises { get; set; }


        public CurrentWorkoutViewModel()
        {

        }
    }
}
