using AutoMapper;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using SweatFlexAPIClient.Services;
using SweatFlexData.DTOs.Create;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class CreateExerciseViewModel : ObservableObject
    {
        private readonly IMapper _mapper;
        private readonly ExerciseService _exerciseService;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private int _musclegroup;

        [ObservableProperty]
        private int _equipment;

        [ObservableProperty]
        private int _type;

        public ObservableCollection<ExerciseType> ExerciseTypes { get; set; } = new();
        public ObservableCollection<Musclegroup> Musclegroups { get; set; } = new();
        public ObservableCollection<Equipment> Equipments { get; set; } = new();

        public CreateExerciseViewModel(IMapper mapper, ExerciseService exerciseService)
        {
            _mapper = mapper;
            _exerciseService = exerciseService;
        }

        public async Task InitializeAsync()
        {
            await SetExerciseAssets();
        }

        private async Task SetExerciseAssets()
        {
            var response = await _exerciseService.GetExerciseAssets();
            ExerciseTypes.Clear();
            Musclegroups.Clear();
            Equipments.Clear();

            if (response.IsSuccess)
            {
                response.Result.Types.ForEach(x => ExerciseTypes.Add(_mapper.Map<ExerciseType>(x)));
                response.Result.MuscleGroups.ForEach(x => Musclegroups.Add(_mapper.Map<Musclegroup>(x)));
                response.Result.Equipments.ForEach(x => Equipments.Add(_mapper.Map<Equipment>(x)));
            }
        }

        public async Task AddExercise(string name, string description, int musclegroupId, int typeId, int? equipmentId = null)
        {
            var exerciseCreateDto = new ExerciseCreateDTO()
            {
                Creator = Preferences.Get("UserId", ""),
                Description = description,
                Musclegroup = musclegroupId,
                Equipment = equipmentId,
                Type = typeId,
                Name = name
            };

            var response = await _exerciseService.CreateExerciseAsync(exerciseCreateDto);

            if (response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Exercise created successfully", "Ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
            }
        }
    }
}
