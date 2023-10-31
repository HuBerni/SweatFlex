using AutoMapper;
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

        [ObservableProperty]
        private bool _isBusy;

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
            IsBusy = true;
            PreBuiltWorkouts.Clear();
            MyWorkouts.Clear();
            await SetMyWorkouts();
            await SetSuggestedWorkouts();
            IsBusy = false;
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

        [RelayCommand]
        private async Task GoToEditWorkout(object workoutId)
        {
            if (int.TryParse(workoutId.ToString(), out int workoutIdInt))
            {
                var navigationParams = new Dictionary<string, object>
                        {
                            { "WorkoutId", 1 }
                        };

                await Shell.Current.GoToAsync(nameof(EditWorkout), navigationParams);
            }
        }

        private async Task SetMyWorkouts()
        {
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
