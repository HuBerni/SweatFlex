using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using SweatFlexAPIClient.Services;
using System.Collections.ObjectModel;

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
        [ObservableProperty]
        private ChartEntry[] _chartEntrys;


        public ObservableCollection<DateTime?> TrainingExerciseDates { get; set; }
        

        public HomeViewModel(IMapper mapper, TrainingExerciseService trainingExerciseService)
        {
            TrainingExerciseDates = new();
            _mapper = mapper;
            _trainingExerciseService = trainingExerciseService;
        }

        public async Task InitializeAsnyc()
        {
            IsBusy = true;
            await SetTrainingHistory();
            IsBusy = false;
        }

        private async Task SetTrainingHistory()
        {
            var response = await _trainingExerciseService.GetTrainingExercisesAsync(Preferences.Get("UserId", ""));

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

            foreach(var TrainingExerciseDate in TrainingExerciseDates)
            {
                i++;
                ChartEntrys = ChartEntrys.Concat(new ChartEntry[] {
                    new ChartEntry(i)
                    {
                        ValueLabel = TrainingExerciseDate.ToString()
                    }
                }).ToArray();
            }
        }

    }
}
