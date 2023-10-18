using CommunityToolkit.Mvvm.ComponentModel;
using SweatFlex.Maui.Models;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    [QueryProperty(nameof(Workout), "Workout")]
    public partial class CurrentWorkoutViewModel : ObservableObject
    {
        [ObservableProperty]
        private Workout _workout;

        public ObservableCollection<List<TrainingExercise>> TrainingExercisesSets { get; set; }

        public CurrentWorkoutViewModel()
        {
           
        }

        public async Task InitializeAsnyc()
        {
            //TODO Getting Training Exercises from Workout via Service and adding them to TrainingExercisesSets
        }
    }
}
