using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class Exercises : ContentPage
{
	private readonly ExercisesViewModel _viewModel;
    public Exercises(ExercisesViewModel viewModel)
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