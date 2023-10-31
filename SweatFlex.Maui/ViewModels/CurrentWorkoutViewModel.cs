using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Services;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    [QueryProperty(nameof(Workout), "Workout")]
    public partial class CurrentWorkoutViewModel : ObservableObject
    {
        [ObservableProperty]
        private Workout? _workout;

        [ObservableProperty]
        private bool _isBusy;

        private CurrentWorkoutService _currentWorkoutService;


        private int _sessionId;

        public ObservableCollection<TrainingExercise> TrainingExercises { get; set; }

        public CurrentWorkoutViewModel(CurrentWorkoutService currentWorkoutService)
        {
            _currentWorkoutService = currentWorkoutService;
            TrainingExercises = new();
        }

        public async Task InitializeAsnyc()
        {
            IsBusy = true;
            var trainingExercisesList = await _currentWorkoutService.CreateTrainingExercisesForWorkout(Workout.Id, Preferences.Get("UserId", ""));
            trainingExercisesList.ForEach(TrainingExercises.Add);

            if (trainingExercisesList is not null && trainingExercisesList.Any())
            {
                _sessionId = trainingExercisesList.First().SessionId;
            }
            IsBusy = false;
        }

        [RelayCommand]
        private async Task SaveTrainingExercises()
        {
            foreach (var item in TrainingExercises)
            {
                await _currentWorkoutService.UpdateTrainingExerciseAsync(item);
            }
        }
    }
}
