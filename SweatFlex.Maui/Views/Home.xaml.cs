using Microcharts;
using SkiaSharp;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class Home : ContentPage
{
	private readonly HomeViewModel _viewModel;

	public Home(HomeViewModel viewModel)
	{
		BindingContext = _viewModel = viewModel;
		InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs parameters)
    {
        base.OnNavigatedTo(parameters);
        await _viewModel.InitializeAsnyc();

        SetGraphs();
    }

    private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.UserId = ((User)((Picker)sender).SelectedItem).Id;

        await _viewModel.SetTrainingHistoryEntrys();
        SetGraphs();
    }

    private void SetGraphs()
    {
        if (_viewModel._chartEntrysMonthly is not null && _viewModel._chartEntrysMonthly?.Count > 0)
        {
            MonthlyChart.Chart = new BarChart
            {
                Entries = _viewModel._chartEntrysMonthly,
                IsAnimated = true,
                AnimationProgress = 4,
                AnimationDuration = new TimeSpan(0, 0, 4),
                BackgroundColor = SKColor.Parse("#2C2E43"),
                LabelTextSize = 40,
                SerieLabelTextSize = 40,
                ValueLabelTextSize = 40,
                LabelColor = SKColor.Parse("#FFFFFF")
            };
        }

        if (_viewModel._chartEntrys12Months is not null && _viewModel._chartEntrys12Months?.Length > 0)
        {
            TwelveMonthsHistory.Chart = new LineChart
            {
                Entries = _viewModel._chartEntrys12Months,
                IsAnimated = true,
                AnimationProgress = 4,
                AnimationDuration = new TimeSpan(0, 0, 4),
                BackgroundColor = SKColor.Parse("#2C2E43"),
                LabelTextSize = 40,
                SerieLabelTextSize = 40,
                ValueLabelTextSize = 40,
                LabelColor = SKColor.Parse("#FFFFFF")
            };
        }
    }
}