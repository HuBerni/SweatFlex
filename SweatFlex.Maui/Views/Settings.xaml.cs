using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class Settings : ContentPage
{
    private SettingsViewModel _viewModel;

    public Settings(SettingsViewModel viewModel)
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