using AutoMapper;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Views;
using SweatFlexAPIClient.Services;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ExercisesViewModel : ObservableObject
    {
        private readonly IMapper _mapper;

        private readonly ExerciseService _exerciseService;

        [ObservableProperty]
        private Exercise _selectedExercise;

        [ObservableProperty]
        private bool _isBusy;

        public ObservableCollection<Exercise> Exercises { get; set; }

        public ExercisesViewModel(IMapper mapper, ExerciseService exerciseService)
        {
            Exercises = new();
            _mapper = mapper;
            _exerciseService = exerciseService;
        }

        public async Task InitializeAsnyc()
        {
            IsBusy = true;
            await SetExercises();
            IsBusy = false;
        }

        private async Task SetExercises()
        {
            Exercises.Clear();
            var response = await _exerciseService.GetExercisesAsync();
            var userResponse = await _exerciseService.GetExercisesAsync(Preferences.Get("UserId", ""));

            var exercises = response.Result?.ToList().Select(_mapper.Map<Exercise>).ToList();
            exercises?.AddRange(userResponse.Result?.ToList().Select(_mapper.Map<Exercise>).ToList());
            exercises?.ToList().ForEach(Exercises.Add);
        }

        [RelayCommand]
        private async Task ShowExerciseDetails()
        {
            await Application.Current.MainPage.ShowPopupAsync(new ExerciseDetailsPopup(SelectedExercise));
            SelectedExercise = null;
        }

        [RelayCommand]
        private async Task NavigateToCreateExercise()
        {
            await Shell.Current.GoToAsync(nameof(CreateExercise));
        }
    }
}
