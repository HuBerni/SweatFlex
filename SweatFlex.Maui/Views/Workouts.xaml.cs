using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class Workouts : ContentPage
{
	private readonly WorkoutsViewModel _viewModel;

	public Workouts()
	{
		BindingContext = _viewModel = new WorkoutsViewModel();
		InitializeComponent();
	}
}