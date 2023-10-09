using SweatFlex.Maui.Views;

namespace SweatFlex.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		Routing.RegisterRoute(nameof(Workouts), typeof(Workouts));
		Routing.RegisterRoute(nameof(Register), typeof(Register));
		Routing.RegisterRoute(nameof(Login), typeof(Login));
		Routing.RegisterRoute(nameof(Home), typeof(Home));
		Routing.RegisterRoute(nameof(Progress), typeof(Progress));
		Routing.RegisterRoute(nameof(Settings), typeof(Settings));
		Routing.RegisterRoute(nameof(Exercises), typeof(Exercises));
		Routing.RegisterRoute(nameof(CurrentWorkout), typeof(CurrentWorkout));
		InitializeComponent();
	}
}
