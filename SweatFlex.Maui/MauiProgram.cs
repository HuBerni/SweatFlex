using AutoMapper;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using SweatFlex.Maui.Services;
using SweatFlex.Maui.ViewModels;
using SweatFlex.Maui.Views;
using SweatFlexAPIClient.APIModels;
using API = SweatFlexAPIClient.Services;

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

		//Adding AutoMapper
		var mappingConfig = new MapperConfiguration(mc =>
		{
            mc.AddProfile(new MappingProfile());
        });

		IMapper mapper = mappingConfig.CreateMapper();
		builder.Services.AddSingleton(mapper);

		//Registering Views with ViewModels and Routes
		builder.Services.AddTransientWithShellRoute<Login, LoginViewModel>(nameof(Login));
		builder.Services.AddTransientWithShellRoute<Register, RegisterViewModel>(nameof(Register));
		builder.Services.AddTransientWithShellRoute<Home, HomeViewModel>(nameof(Home));
		builder.Services.AddTransientWithShellRoute<Workouts, WorkoutsViewModel>(nameof(Workouts));
		builder.Services.AddTransientWithShellRoute<Exercises, ExercisesViewModel>(nameof(Exercises));
		builder.Services.AddTransientWithShellRoute<Progress, ProgressViewModel>(nameof(Progress));
		builder.Services.AddTransientWithShellRoute<Settings, SettingsViewModel>(nameof(Settings));
		builder.Services.AddTransientWithShellRoute<CurrentWorkout, CurrentWorkoutViewModel>(nameof(CurrentWorkout));
		builder.Services.AddTransientWithShellRoute<CreateExercise, CreateExerciseViewModel>(nameof(CreateExercise));
		builder.Services.AddTransientWithShellRoute<EditWorkout, EditWorkoutViewModel>(nameof(EditWorkout));

		//Registering Views

		//Registering ViewModels
		builder.Services.AddTransient<ConfirmationPopupViewModel>();
		builder.Services.AddTransient<ExerciseDetailsPopupViewModel>();

		//Registering Maui Services
		builder.Services.AddSingleton<AuthService>();
		builder.Services.AddSingleton<CurrentWorkoutService>();
		builder.Services.AddSingleton<ProgressService>();
		builder.Services.AddSingleton<EditWorkoutService>();
		
		//Registering API Services
		builder.Services.AddSingleton<API.AuthService>();
		builder.Services.AddSingleton<API.ExerciseService>();
		builder.Services.AddSingleton<API.WorkoutService>();
		builder.Services.AddSingleton<API.WorkoutExerciseService>();
		builder.Services.AddSingleton<API.TrainingExerciseService>();

		return builder.Build();
	}
}
