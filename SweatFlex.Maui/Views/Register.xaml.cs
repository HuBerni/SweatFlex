using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class Register : ContentPage
{
	private RegisterViewModel _viewModel;
	public Register(RegisterViewModel viewModel)
	{
		BindingContext = _viewModel = viewModel;
		InitializeComponent();
	}
}