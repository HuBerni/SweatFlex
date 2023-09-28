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
		InitializeComponent();
	}
}
