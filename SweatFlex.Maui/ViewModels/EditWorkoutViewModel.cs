using AutoMapper;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Services;
using SweatFlex.Maui.Views;
using SweatFlexAPIClient.Services;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    [QueryProperty(nameof(WorkoutId), "WorkoutId")]
    public partial class EditWorkoutViewModel : ObservableObject
    {
        private readonly IMapper _mapper;
        private readonly WorkoutService _workoutService;
        private readonly EditWorkoutService _editWorkoutService;
        private readonly ExerciseService _exerciseService;

        public ObservableCollection<ExerciseSet> ExerciseSets { get; set; } = new();

        public ObservableCollection<Exercise> Exercises { get; set; } = new();

        [ObservableProperty]
        private int _workoutId;

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private Workout _workout;

        [ObservableProperty]
        private List<WorkoutExercise> _workoutExercises;

        public EditWorkoutViewModel(IMapper mapper, WorkoutService workoutService, EditWorkoutService editWorkoutService, ExerciseService exerciseService)
        {
            _mapper = mapper;
            _workoutService = workoutService;
            _editWorkoutService = editWorkoutService;
            _exerciseService = exerciseService;
        }

        public async Task InitializeAsnyc()
        {
            IsBusy = true;
            var workoutResponse = await _workoutService.GetWorkoutByIdAsync(WorkoutId);

            if (workoutResponse.IsSuccess)
            {
                Workout = _mapper.Map<Workout>(workoutResponse.Result);
            }

            var exerciseResponse = await _exerciseService.GetExercisesAsync();

            if (exerciseResponse.IsSuccess)
            {
                Exercises = exerciseResponse.Result.Select(_mapper.Map<Exercise>).ToObservableCollection();
            }
            IsBusy = false;
        }

        public async Task AddExerciseToWorkout(int exerciseId, int sets)
        {
            await _editWorkoutService.AddWorkoutExercise(WorkoutId, exerciseId, sets);
            await UpdateExerciseSetsCollection();
        }

        
        public async Task RemoveExerciseFromWorkout(int id)
        {
            await _editWorkoutService.RemoveWorkoutExercise(id);
            await UpdateExerciseSetsCollection();
        }

        [RelayCommand]
        private async Task SaveWorkout()
        {
            await _editWorkoutService.SaveWorkoutExercises();
            await Shell.Current.Navigation.PopAsync();
        }

        private async Task UpdateExerciseSetsCollection()
        {
            ExerciseSets.Clear();
            _editWorkoutService.ExerciseSets.ForEach(ExerciseSets.Add);
        }

        [RelayCommand]
        public async Task CancelEdit()
        {
            await _workoutService.DeleteWorkoutAsync(WorkoutId);
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
