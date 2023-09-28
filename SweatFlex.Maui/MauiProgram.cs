using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using SweatFlex.Maui.ViewModels;
using SweatFlex.Maui.Views;

namespace SweatFlex.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkitCore()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		//Registering Views
		builder.Services.AddTransient<Login>();
		builder.Services.AddTransient<Register>();
		builder.Services.AddTransient<Home>();
		builder.Services.AddTransient<Workouts>();

		builder.Services.AddTransient<LoginViewModel>();

		return builder.Build();
	}
}
