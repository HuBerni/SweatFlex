using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using SkiaSharp;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Services;
using SweatFlexAPIClient.APIModels;
using SweatFlexAPIClient.Services;
using SweatFlexData.DTOs;
using SweatFlexUtility;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IMapper _mapper;

        private readonly TrainingExerciseService _trainingExerciseService;
        private readonly ProgressService _progressService;
        private readonly UserService _userService;
        [ObservableProperty]
        private bool _isBusy;
        [ObservableProperty]
        private string? _userId;
        [ObservableProperty]
        private decimal? _totalWeight;
        [ObservableProperty]
        private bool _isCoach;
        [ObservableProperty]
        private bool _chartVisible;

        public List<ChartEntry> _chartEntrysMonthly;

        public ChartEntry[] _chartEntrys12Months;

        public ObservableCollection<User> Users { get; set; } = new();

        public HomeViewModel(IMapper mapper, TrainingExerciseService trainingExerciseService, ProgressService progressService, UserService userService)
        {            
            _mapper = mapper;
            _trainingExerciseService = trainingExerciseService;
            _progressService = progressService;
            _userService = userService;
        }

        public async Task InitializeAsnyc()
        {
            IsBusy = true;

            if (Preferences.Get("RoleId", "") == "2" && Users.Count == 0)
            {
                IsCoach = true;
                var response = await _userService.GetUserByCoachAsync(Preferences.Get("UserId", ""));
                var user = response.Result?.Select(_mapper.Map<User>).ToList();
                user?.ForEach(Users.Add);
            }
            else if (Preferences.Get("RoleId","") == "2")
            {
                IsCoach = true;
            }
            else
            {
                UserId = "";
                IsCoach = false;
            }

            await SetTrainingHistoryEntrys();
            IsBusy = false;
        }

        public async Task SetTrainingHistoryEntrys()
        {
            IsBusy = true;
            ChartVisible = false;
            List<DateTime?> workoutDates = new();
            var progresses = new List<Progress?>();

            if (!string.IsNullOrEmpty(UserId) && IsCoach)
            {
                progresses = await _progressService.GetProgresses(UserId);
            }
            else
            {
                progresses = await _progressService.GetProgresses(Preferences.Get("UserId", ""));
            }

            if (progresses?.Count == 0)
            {
                IsBusy = false;
                return;
            }
            
            progresses = progresses?.OrderBy(x => x.Date).ToList();
            List<decimal?> weights = new();
            TotalWeight = progresses?.Sum(x => x.TotalWeight);
            var months = StaticResources.GetMonths();

            foreach (var progress in progresses)
            {
                workoutDates.Add(progress.Date.ToDateTime(new TimeOnly()));
            }

            int i = 0;

            foreach(var date in workoutDates)
            {
                foreach(var progress in progresses)
                {
                    weights.Add(progress.TotalWeight);
                }
                i++;
            }

            i = 0;
            int z = 0;

            _chartEntrysMonthly = new();

            foreach (var TrainingExerciseDate in workoutDates)
            {
                if (TrainingExerciseDate is null)
                {
                    continue;
                }

                if (z > 11)
                {
                    z = 0;
                }
                i++;
                
                _chartEntrysMonthly.Add(
                    new ChartEntry(i)
                    {
                        Label = TrainingExerciseDate.Value.ToString("dd.MM"),
                        ValueLabel = weights[i - 1].ToString(),
                        Color = SKColor.Parse(months[z][1]),
                        ValueLabelColor = SKColor.Parse("#FFFFFF")
                    });

                z++;
            }

            ApiResponse<IList<TrainingExerciseDTO>>? response = null;

            if (!string.IsNullOrEmpty(UserId) && IsCoach)
            {
                response = await _trainingExerciseService.GetTrainingExercisesAsync(UserId!);
            }
            else
            {
                response = await _trainingExerciseService.GetTrainingExercisesAsync(Preferences.Get("UserId", ""));
            }

            _chartEntrys12Months = new ChartEntry[12];

            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            

            for(int j = 0; j < 12; j++)
            {
                var count = response.Result.Where(r => r.ExerciseExecuted != null && r.ExerciseExecuted.Value.Year == currentYear && r.ExerciseExecuted.Value.Month == currentMonth).ToList();
                _chartEntrys12Months[(12 - j) - 1] = new ChartEntry(count.Count())
                {
                    Label = months[currentMonth - 1][0],
                    ValueLabel = count.Count().ToString(),
                    Color = SKColor.Parse(months[currentMonth - 1][1]),
                    ValueLabelColor = SKColor.Parse("#FFFFFF")
                };

                if(currentMonth != 1)
                {
                    currentMonth -= 1;
                }
                else
                {
                    currentYear -= 1;
                    currentMonth = 12;
                }
            }

            IsBusy = false;
            ChartVisible = true;
        }

    }
}
