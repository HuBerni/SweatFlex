using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Services;
using SweatFlex.Maui.Views;

namespace SweatFlex.Maui.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private AuthService _authService;

        [ObservableProperty]
        private int _pauseTime;

        public SettingsViewModel(AuthService authService)
        {
            _authService = authService;
        }

        public async Task InitializeAsnyc()
        {
            PauseTime = Preferences.Get("PauseTime", 60);
        }

        [RelayCommand]
        private async Task Logout()
        {
            _authService.Logout();

            await Shell.Current.GoToAsync($"{nameof(Login)}");
        }
    }
}
