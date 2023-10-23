using AutoMapper;
using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using SweatFlex.Maui.Models;
using SweatFlexAPIClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ExercisesViewModel : ObservableObject
    {
        private readonly IMapper _mapper;

        private readonly ExerciseService _exerciseService;

        public ObservableCollection<Exercise> Exercises { get; set; }

        public ExercisesViewModel(IMapper mapper, ExerciseService exerciseService)
        {
            Exercises = new();
            _mapper = mapper;
            _exerciseService = exerciseService;
        }

        public async Task InitializeAsnyc()
        {
            await SetExercises();
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
