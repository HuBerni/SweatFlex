using SweatFlex.Maui.Models;
using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class Progress : ContentPage
{
    private readonly ProgressViewModel _viewModel;

    public Progress(ProgressViewModel viewModel)
	{
        BindingContext = _viewModel = viewModel;
		InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await _viewModel.InitializeAsync();
    }

    private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.UserId = ((User)((Picker)sender).SelectedItem).Id;

        await _viewModel.SetProgresses();
    }
}