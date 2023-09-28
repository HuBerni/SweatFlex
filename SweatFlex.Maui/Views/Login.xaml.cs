using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class Login : ContentPage
{
	private LoginViewModel _viewModel;

	public Login(LoginViewModel loginViewModel)
	{
		BindingContext = _viewModel = loginViewModel;
		InitializeComponent();
	}
}