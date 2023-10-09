using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class CurrentWorkout : ContentPage
{
	private readonly CurrentWorkoutViewModel _viewModel;

	public CurrentWorkout(CurrentWorkoutViewModel viewModel)
	{
		BindingContext = _viewModel = viewModel;
		InitializeComponent();
	}
}