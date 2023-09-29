namespace SweatFlex.Maui.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Shell.SetTabBarIsVisible(Application.Current.MainPage, true);
    }
}