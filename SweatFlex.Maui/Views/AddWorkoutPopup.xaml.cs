using CommunityToolkit.Maui.Views;

namespace SweatFlex.Maui.Views;

public partial class AddWorkoutPopup : Popup
{
	public AddWorkoutPopup()
	{
		InitializeComponent();
	}

	private async void Close_Clicked(object sender, EventArgs e) => await CloseAsync();

    private async void Add_Clicked(object sender, EventArgs e) => await CloseAsync(WorkoutName.Text);
}