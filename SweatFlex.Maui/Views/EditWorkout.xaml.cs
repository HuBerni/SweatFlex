using SweatFlex.Maui.Models;
using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class EditWorkout : ContentPage
{
    private readonly EditWorkoutViewModel _viewModel;

    public EditWorkout(EditWorkoutViewModel viewModel)
	{
		BindingContext = _viewModel = viewModel;
		InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs parameters)
    {
        base.OnNavigatedTo(parameters);
        await _viewModel.InitializeAsnyc();
        ExercisePicker.ItemsSource = _viewModel.Exercises;
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        var selectedExercise = ExercisePicker.SelectedItem as Exercise;

        await _viewModel.AddExerciseToWorkout(selectedExercise.Id, int.Parse(SetsEntry.Text));
    }
}