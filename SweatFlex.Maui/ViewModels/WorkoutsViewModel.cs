using AutoMapper;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Views;
using SweatFlexAPIClient.Services;
using SweatFlexData.DTOs.Create;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class WorkoutsViewModel : ObservableObject
    {
        
        public ObservableCollection<Workout> MyWorkouts { get; set; }

        public ObservableCollection<Workout> PreBuiltWorkouts { get; set; }

        [ObservableProperty]
        private Workout? _selectedWorkout;

        private readonly WorkoutService _workoutService;
        private readonly IMapper _mapper;

        public WorkoutsViewModel(WorkoutService workoutService, IMapper mapper)
        {
            _workoutService = workoutService;
            _mapper = mapper;

            MyWorkouts = new ObservableCollection<Workout>();
            PreBuiltWorkouts = new ObservableCollection<Workout>();
        }

        public async Task InitializeAsnyc()
        {
            await SetMyWorkouts();
            await SetSuggestedWorkouts();
        }

        [RelayCommand]
        public async Task ShowAddWorkoutPopup()
        {
            var result = await Application.Current.MainPage.ShowPopupAsync(new AddWorkoutPopup());

            if(result is string workoutName)
            {
                if (string.IsNullOrWhiteSpace(workoutName))
                    return;

                await CreateWorkout(workoutName);
            }
        }

        [RelayCommand]
        public async Task WorkoutSelected()
        {
            var result = await Application.Current.MainPage.ShowPopupAsync(new ConfirmationPopup($"Wollen sie das Workout {SelectedWorkout?.Name} starten?"));

            if(result is bool confirmed)
            {
                if (confirmed)
                {
                    var navigationParams = new Dictionary<string, object> 
                    {
                        { nameof(Workout), SelectedWorkout }
                    };
                    await Shell.Current.GoToAsync(nameof(CurrentWorkout), navigationParams);
                    return;
                }
            }
                
            SelectedWorkout = null;
        }

        private async Task SetMyWorkouts()
        {
            MyWorkouts.Clear();
            var response = await _workoutService.GetWorkoutsAsync("TESTI");

            if (!response.IsSuccess)
            {
                //TODO show error
                return;
            }

            var myWorkouts = response.Result?.Select(_mapper.Map<Workout>);
            myWorkouts?.ToList().ForEach(MyWorkouts.Add);
        }

        private async Task SetSuggestedWorkouts()
        {
            PreBuiltWorkouts.Clear();
            var response = await _workoutService.GetWorkoutsAsync();

            if (!response.IsSuccess)
            {
                //TODO show error
                return;
            }

            var recommendedWorkouts = response.Result?.ToList().Select(_mapper.Map<Workout>);
            recommendedWorkouts?.ToList().ForEach(PreBuiltWorkouts.Add);
        }

        private async Task CreateWorkout(string workoutName)
        {
            var workoutCreateDto = new WorkoutCreateDTO
            {
                Name = workoutName,
                CreatorId = "TESTI"
            };

            var workout = await _workoutService.CreateWorkoutAsync(workoutCreateDto);

            if (!workout.IsSuccess)
            {
                //TODO show error
                return;
            }

            MyWorkouts.Add(_mapper.Map<Workout>(workout.Result));
        }
    }
}
