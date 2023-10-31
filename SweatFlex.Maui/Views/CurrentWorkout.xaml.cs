using SweatFlex.Maui.Models;
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

	protected override async void OnNavigatedTo(NavigatedToEventArgs parameters)
	{
        base.OnNavigatedTo(parameters);
		await _viewModel.InitializeAsnyc();
    }
}