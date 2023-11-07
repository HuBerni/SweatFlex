using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using SkiaSharp;
using SweatFlex.Maui.Services;
using SweatFlexAPIClient.Services;
using SweatFlexUtility;

namespace SweatFlex.Maui.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IMapper _mapper;

        private readonly TrainingExerciseService _trainingExerciseService;
        private readonly ProgressService _progressService;
        [ObservableProperty]
        private bool _isBusy;
        [ObservableProperty]
        private string _userName = Preferences.Get("UserId", "");
        [ObservableProperty]
        private decimal? _totalWeight;

        public List<ChartEntry> _chartEntrysMonthly;

        public ChartEntry[] _chartEntrys12Months;
               
        

        public HomeViewModel(IMapper mapper, TrainingExerciseService trainingExerciseService, ProgressService progressService)
        {            
            _mapper = mapper;
            _trainingExerciseService = trainingExerciseService;
            _progressService = progressService;
        }

        public async Task InitializeAsnyc()
        {
            IsBusy = true;
            await SetTrainingHistoryEntrys();
            IsBusy = false;
        }

        private async Task SetTrainingHistoryEntrys()
        {
            List<DateTime?> workoutDates = new();
            var progresses = await _progressService.GetProgresses(Preferences.Get("UserId", ""));
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
                        Color = SKColor.Parse(months[z][1])
                    });

                z++;
            }

            var response = await _trainingExerciseService.GetTrainingExercisesAsync(Preferences.Get("UserId", ""));

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
                    Color = SKColor.Parse(months[currentMonth - 1][1])
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
        }

    }
}
