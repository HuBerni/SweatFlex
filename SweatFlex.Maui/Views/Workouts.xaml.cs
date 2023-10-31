using AutoMapper;
using SweatFlex.Maui.ViewModels;
using SweatFlexAPIClient.Services;

namespace SweatFlex.Maui.Views;

public partial class Workouts : ContentPage
{
	private readonly WorkoutsViewModel _viewModel;

    public Workouts(IMapper mapper, WorkoutService workoutService)
	{
		BindingContext = _viewModel = new WorkoutsViewModel(workoutService, mapper);
		InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs parameters)
    {
        base.OnNavigatedTo(parameters);
        await _viewModel.InitializeAsnyc();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}