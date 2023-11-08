using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class Settings : ContentPage
{
    private SettingsViewModel _viewModel;

    public Settings(SettingsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs parameters)
    {
        base.OnNavigatedTo(parameters);
        await _viewModel.InitializeAsnyc();
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.ChangeTheme(((Picker)sender).SelectedItem.ToString());
    }
}