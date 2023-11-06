using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using SweatFlexAPIClient.Services;
using System.Collections.ObjectModel;
using SkiaSharp;
using SweatFlexUtility;

namespace SweatFlex.Maui.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IMapper _mapper;

        private readonly TrainingExerciseService _trainingExerciseService;

        [ObservableProperty]
        private bool _isBusy;
        [ObservableProperty]
        private string _userName = Preferences.Get("UserId", "");

        public List<ChartEntry> _chartEntrysMonthly;

        public ChartEntry[] _chartEntrys12Months;
               
        

        public HomeViewModel(IMapper mapper, TrainingExerciseService trainingExerciseService)
        {            
            _mapper = mapper;
            _trainingExerciseService = trainingExerciseService;            
        }

        public async Task InitializeAsnyc()
        {
            IsBusy = true;
            await SetTrainingHistoryEntrys();
            IsBusy = false;
        }

        private async Task SetTrainingHistoryEntrys()
        {
            List<DateTime?> TrainingExerciseDates = new();
            var response = await _trainingExerciseService.GetTrainingExercisesAsync(Preferences.Get("UserId", ""));
            List<decimal?> weights = new();
            var months = StaticResources.GetMonths();

            foreach (var trainingExercise in response.Result)
            {
                if (trainingExercise.ExerciseExecuted != null &&
                    !TrainingExerciseDates.Contains(trainingExercise.ExerciseExecuted) && 
                    trainingExercise.ExerciseExecuted.Value.Month == DateTime.Now.Month)
                {
                    TrainingExerciseDates.Add(trainingExercise.ExerciseExecuted);
                }
            }

            int i = 0;

            foreach(var date in TrainingExerciseDates)
            {
                foreach(var te in response.Result)
                {
                    if(te.ExerciseExecuted != null && te.ExerciseExecuted == date)
                    {

                        weights.Add(te.Weight);
                    }
                }
                i++;
            }

            i = 0;
            int z = 0;

            _chartEntrysMonthly = new();

            foreach (var TrainingExerciseDate in TrainingExerciseDates)
            {
                if (z > 11)
                {
                    z = 0;
                }
                i++;
                
                _chartEntrysMonthly.Add(
                    new ChartEntry(i)
                    {
                        Label = TrainingExerciseDate.ToString(),
                        ValueLabel = weights[i - 1].ToString(),
                        Color = SKColor.Parse(months[z][1])
                    });

                z++;
            }

            response = await _trainingExerciseService.GetTrainingExercisesAsync(Preferences.Get("UserId", ""));

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
