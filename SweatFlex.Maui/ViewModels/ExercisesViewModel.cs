using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using SweatFlex.Maui.Models;
using SweatFlexAPIClient.Services;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ExercisesViewModel : ObservableObject
    {
        private readonly IMapper _mapper;

        private readonly ExerciseService _exerciseService;

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

            var exercises = response.Result?.ToList().Select(_mapper.Map<Exercise>);
            exercises?.ToList().ForEach(Exercises.Add);
        }
    }
}
