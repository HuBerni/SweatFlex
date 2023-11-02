using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using SweatFlexAPIClient.Services;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class AddExerciseToWorkoutViewModel : ObservableObject
    {
        private readonly IMapper _mapper;
        private ExerciseService _exerciseService;

        ObservableCollection<Exercise> Exercises { get; set; }

        public AddExerciseToWorkoutViewModel(IMapper mapper, ExerciseService exerciseService)
        {
            _mapper = mapper;
            _exerciseService = exerciseService;
        }

        public async Task InitializeAsnyc()
        {
            var response = await _exerciseService.GetExercisesAsync(Preferences.Get("UserId", ""));
            response.Result?.ToList().Select(_mapper.Map<Exercise>).ToList().ForEach(x => Exercises.Add(x));
        }

        [RelayCommand]
        private async Task AddExerciseToWorkout()
        {
            
        }
    }
}
