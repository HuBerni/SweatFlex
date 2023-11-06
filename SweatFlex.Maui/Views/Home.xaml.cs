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

        //MonthlyChart.Chart = new BarChart
        //{
        //    Entries = _viewModel._chartEntrysMonthly,
        //    IsAnimated = true,
        //    AnimationProgress = 4,
        //    AnimationDuration = new TimeSpan(0, 0, 4),
        //    BackgroundColor = SKColor.Parse("#d3d3d3"),
        //    LabelTextSize = 40,
        //    SerieLabelTextSize = 40,
        //    ValueLabelTextSize = 40,
        //    LabelColor = SKColor.Parse("#000000")
        //};

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