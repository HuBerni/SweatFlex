using SweatFlex.Maui.Models;
using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class CreateExercise : ContentPage
{
    private readonly CreateExerciseViewModel _viewModel;

    public CreateExercise(CreateExerciseViewModel viewModel)
	{
        BindingContext = _viewModel = viewModel;
		InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await _viewModel.InitializeAsync();
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        var musclegroup = MusclegroupPicker.SelectedItem as Musclegroup;
        var type = TypePicker.SelectedItem as ExerciseType;

        if (musclegroup == null || type == null)
        {
            //TODO add error message
            return;
        }

        var equipment = EquipmentPicker.SelectedItem as Equipment;
        await _viewModel.AddExercise(ExerciseName.Text, Description.Text, musclegroup.Id, type.Id, equipment?.Id);
    }
}