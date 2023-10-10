using SweatFlex.Maui.Views;

namespace SweatFlex.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		Routing.RegisterRoute(nameof(Home), typeof(Home));
		Routing.RegisterRoute(nameof(Progress), typeof(Progress));
		Routing.RegisterRoute(nameof(Settings), typeof(Settings));
		InitializeComponent();
	}
}
