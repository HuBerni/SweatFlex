using CommunityToolkit.Maui.Views;
using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class ConfirmationPopup : Popup
{
	private ConfirmationPopupViewModel _viewModel;

	public ConfirmationPopup(string title)
	{
		BindingContext = _viewModel = new ConfirmationPopupViewModel(title);
		InitializeComponent();
	}

	private async void Close_Clicked(object sender, EventArgs e) => await CloseAsync(false);

    private async void Confirm_Clicked(object sender, EventArgs e) => await CloseAsync(true);
}