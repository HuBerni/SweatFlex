using CommunityToolkit.Mvvm.ComponentModel;
using SweatFlexAPIClient.Services;

namespace SweatFlex.Maui.ViewModels
{
    public partial class CreateExerciseViewModel : ObservableObject
    {
        private readonly ExerciseService _exerciseService;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _musclegroup;

        [ObservableProperty]
        private string _equipment;

        [ObservableProperty]
        private string _type;

        public CreateExerciseViewModel(ExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }


    }
}
