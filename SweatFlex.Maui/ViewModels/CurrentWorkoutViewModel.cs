using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Services;
using SweatFlex.Maui.SQLLite;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    [QueryProperty(nameof(Workout), "Workout")]
    [QueryProperty(nameof(SessionId), "SessionId")]
    public partial class CurrentWorkoutViewModel : ObservableObject
    {
        [ObservableProperty]
        private Workout? _workout;

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private bool _isReadOnly;

        private CurrentWorkoutService _currentWorkoutService;

        [ObservableProperty]
        private int? _sessionId;

        private TodoItemDatabase _lokalDB;
        private IMapper _mapper;

        public ObservableCollection<TrainingExercise> TrainingExercises { get; set; }

        public CurrentWorkoutViewModel(CurrentWorkoutService currentWorkoutService, TodoItemDatabase lokalDB, IMapper mapper)
        {
            _currentWorkoutService = currentWorkoutService;
            TrainingExercises = new();
            _lokalDB = lokalDB;
            _mapper = mapper;
        }

        public async Task InitializeAsnyc()
        {
            IsBusy = true;
            var trainingExercisesList = await _currentWorkoutService.CreateTrainingExercisesForWorkout(Workout.Id, Preferences.Get("UserId", ""));
            trainingExercisesList.ForEach(TrainingExercises.Add);

            if (SessionId is not null)
            {
                await SetupReadOnly();
                IsBusy = false;
                return;
            }

            if (trainingExercisesList is not null && trainingExercisesList.Any())
            {
                SessionId = trainingExercisesList.First().SessionId;
            }
            IsBusy = false;
        }

        [RelayCommand]
        private async Task SaveTrainingExercises()
        {

            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            foreach (var item in TrainingExercises)
            {
                if (accessType != NetworkAccess.Internet)
                {
                    await _lokalDB.SaveItemAsync(_mapper.Map<TrainingExerciseLocal>(item));
                }
                else
                {
                    await _currentWorkoutService.UpdateTrainingExerciseAsync(item);
                }
            }

            
            

            await Shell.Current.Navigation.PopAsync();
        }

        private async Task SetupReadOnly()
        {
            IsReadOnly = true;
        }
    }
}
