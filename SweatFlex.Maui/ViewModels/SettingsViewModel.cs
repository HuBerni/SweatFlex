using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Services;
using SweatFlex.Maui.Views;

namespace SweatFlex.Maui.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly AuthService _authService;

        public SettingsViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task Logout()
        {
            _authService.LogoutAsync();

            await Shell.Current.GoToAsync(nameof(Login));
        }
    }
}
