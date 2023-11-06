using Microcharts;
using SkiaSharp;
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

        if (_viewModel._chartEntrysMonthly is not null && _viewModel._chartEntrysMonthly?.Count > 0)
        {
            MonthlyChart.Chart = new BarChart
            {
                Entries = _viewModel._chartEntrysMonthly,
                IsAnimated = true,
                AnimationProgress = 4,
                AnimationDuration = new TimeSpan(0, 0, 4),
                BackgroundColor = SKColor.Parse("#d3d3d3"),
                LabelTextSize = 40,
                SerieLabelTextSize = 40,
                ValueLabelTextSize = 40,
                LabelColor = SKColor.Parse("#000000")
            };
        }

        if(_viewModel._chartEntrys12Months is not null && _viewModel._chartEntrys12Months?.Length > 0)
        {
            TwelveMonthsHistory.Chart = new LineChart
            {
                Entries = _viewModel._chartEntrys12Months,
                IsAnimated = true,
                AnimationProgress = 4,
                AnimationDuration = new TimeSpan(0, 0, 4),
                BackgroundColor = SKColor.Parse("#d3d3d3"),
                LabelTextSize = 40,
                SerieLabelTextSize = 40,
                ValueLabelTextSize = 40,
                LabelColor = SKColor.Parse("#000000")
            };
        }
    }
}