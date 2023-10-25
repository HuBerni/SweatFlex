using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class CreateExercise : ContentPage
{
    private readonly CreateExerciseViewModel _viewModel;

    public CreateExercise(CreateExerciseViewModel viewModel)
	{
        BindingContext = _viewModel = viewModel;
		InitializeComponent();
    }
}