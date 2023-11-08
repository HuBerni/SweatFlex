using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Services;
using SweatFlex.Maui.Views;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private AuthService _authService;

        [ObservableProperty]
        private ObservableCollection<string> _themeOptions = new();

        public SettingsViewModel(AuthService authService)
        {
            _authService = authService;
        }

        public async Task InitializeAsnyc()
        {
            FillThemeList();
        }

        private void FillThemeList()
        {
            if (ThemeOptions.Count > 0)
                return;

            ThemeOptions.Add("System");
            ThemeOptions.Add("Light");
            ThemeOptions.Add("Dark");
        }

        [RelayCommand]
        public async Task ChangeTheme(string theme)
        {
            Preferences.Set("Theme", theme);

            //change theme acording to theme string
            if (theme == "System")
            {
                Application.Current.UserAppTheme = AppTheme.Unspecified;
            }
            else if (theme == "Light")
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
            else if (theme == "Dark")
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
        }

        [RelayCommand]
        private async Task Logout()
        {
            _authService.Logout();

            await Shell.Current.GoToAsync($"///MainPage");
        }
    }
}
