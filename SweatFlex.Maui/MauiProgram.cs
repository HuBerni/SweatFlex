using CommunityToolkit.Maui;
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
			.UseMauiApp<App>().UseMauiCommunityToolkit()
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
		builder.Services.AddTransient<Exercises>();
		builder.Services.AddTransient<Progress>();
		builder.Services.AddTransient<Settings>();


		//Registering ViewModels
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<RegisterViewModel>();
		builder.Services.AddTransient<WorkoutsViewModel>();
		builder.Services.AddTransient<ExercisesViewModel>();
		builder.Services.AddTransient<ConfirmationPopupViewModel>();

		return builder.Build();
	}
}
