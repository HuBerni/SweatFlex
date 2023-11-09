using AutoMapper;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
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

        /// <summary>
        /// Sets the exercises for the current user and the coach if the user has a coach
        /// </summary>
        /// <returns></returns>
        private async Task SetExercises()
        {
            Exercises.Clear();
            var response = await _exerciseService.GetExercisesAsync();
            var userResponse = await _exerciseService.GetExercisesAsync(Preferences.Get("UserId", ""));
            
            if (!Preferences.Get("CoachId", "").IsNullOrEmpty())
            {
                var coachResponse = await _exerciseService.GetExercisesAsync(Preferences.Get("CoachId", ""));
                coachResponse.Result?.ToList().ForEach(x => Exercises.Add(_mapper.Map<Exercise>(x)));
            }

            var exercises = response.Result?.ToList().Select(_mapper.Map<Exercise>).ToList();
            exercises?.AddRange(userResponse.Result?.ToList().Select(_mapper.Map<Exercise>).ToList());
            exercises?.ToList().ForEach(Exercises.Add);
        }


        /// <summary>
        /// Opens a popup with the details of the selected exercise
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task ShowExerciseDetails()
        {
            await Application.Current.MainPage.ShowPopupAsync(new ExerciseDetailsPopup(SelectedExercise));
            SelectedExercise = null;
        }

        /// <summary>
        /// Navigates to the create exercise page
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task NavigateToCreateExercise()
        {
            await Shell.Current.GoToAsync(nameof(CreateExercise));
        }
    }
}
