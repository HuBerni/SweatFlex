using AutoMapper;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
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

        /// <summary>
        /// Shows the add workout popup and creates a new workout if the user enters a name
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Starts the selected workout and navigates to the current workout page with the workout as parameter
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task WorkoutSelected()
        {
            if (Preferences.Get("RoleId", "") == "2")
            {
                await Application.Current.MainPage.ShowPopupAsync(new ConfirmationPopup("Sie können keine Workouts starten, da sie ein Coach sind."));
                SelectedWorkout = null;
                return;
            }
            
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

        /// <summary>
        /// Navigate to the edit workout page with the workout id
        /// </summary>
        /// <param name="workoutId">The Id of the workout</param>
        /// <returns></returns>
        [RelayCommand]
        public async Task GoToEditWorkout(object workoutId)
        {
            if (int.TryParse(workoutId.ToString(), out int workoutIdInt))
            {
                var navigationParams = new Dictionary<string, object>
                        {
                            { "WorkoutId", workoutIdInt }
                        };

                await Shell.Current.GoToAsync(nameof(EditWorkout), navigationParams);
            }
        }

        /// <summary>
        /// Sets the workouts created by the user and displays them in the list
        /// </summary>
        /// <returns></returns>
        private async Task SetMyWorkouts()
        {
            var response = await _workoutService.GetWorkoutsAsync(Preferences.Get("UserId", ""));

            if (!response.IsSuccess)
            {
                //TODO show error
                return;
            }

            var myWorkouts = response.Result?.Select(_mapper.Map<Workout>);
            myWorkouts?.ToList().ForEach(MyWorkouts.Add);
        }

        /// <summary>
        /// Sets the suggested workouts for the user and displays them in the list
        /// </summary>
        /// <returns></returns>
        private async Task SetSuggestedWorkouts()
        {
            var coachWorkouts = new List<Workout?>();


            if (!Preferences.Get("CoachId", "").IsNullOrEmpty())
            {

                var coachResponse = await _workoutService.GetWorkoutsAsync(Preferences.Get("CoachId", ""));
                if (!coachResponse.IsSuccess)
                {
                    //TODO show error
                    return;
                }

                coachWorkouts = coachResponse.Result?.ToList().Select(_mapper.Map<Workout>).ToList();
            }

            var response = await _workoutService.GetWorkoutsAsync();

            if (!response.IsSuccess)
            {
                //TODO show error
                return;
            }

            var recommendedWorkouts = response.Result?.ToList().Select(_mapper.Map<Workout>);

            coachWorkouts?.ForEach(PreBuiltWorkouts.Add);
            recommendedWorkouts?.ToList().ForEach(PreBuiltWorkouts.Add);
        }

        /// <summary>
        /// Creates a new workout and navigates to the edit page
        /// </summary>
        /// <param name="workoutName"></param>
        /// <returns></returns>
        private async Task CreateWorkout(string workoutName)
        {
            var workoutCreateDto = new WorkoutCreateDTO
            {
                Name = workoutName,
                CreatorId = Preferences.Get("UserId", "")
            };

            var workout = await _workoutService.CreateWorkoutAsync(workoutCreateDto);

            if (!workout.IsSuccess)
            {
                //TODO show error
                return;
            }

            MyWorkouts.Add(_mapper.Map<Workout>(workout.Result));

            await GoToEditWorkout(workout.Result.Id);
        }
    }
}
