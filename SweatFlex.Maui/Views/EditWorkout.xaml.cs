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
}