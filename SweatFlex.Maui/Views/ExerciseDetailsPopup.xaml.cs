using CommunityToolkit.Maui.Views;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.ViewModels;

namespace SweatFlex.Maui.Views;

public partial class ExerciseDetailsPopup : Popup
{
	public ExerciseDetailsPopup(Exercise exercise)
	{
		BindingContext = new ExerciseDetailsPopupViewModel(exercise);
		InitializeComponent();
	}

	private void Close_Clicked(object sender, EventArgs e) => CloseAsync();
}