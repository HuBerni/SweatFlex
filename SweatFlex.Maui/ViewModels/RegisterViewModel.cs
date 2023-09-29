using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Views;

namespace SweatFlex.Maui.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {


        [RelayCommand]
        private async Task Register()
        {
            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }

        [RelayCommand]
        private async Task NavigateToLogin()
        {
            await Shell.Current.GoToAsync(nameof(Login));
        }
    }
}
