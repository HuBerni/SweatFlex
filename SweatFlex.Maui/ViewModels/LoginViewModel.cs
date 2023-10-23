using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using SweatFlex.Maui.Services;
using SweatFlex.Maui.Views;
using SweatFlexData.DTOs;

namespace SweatFlex.Maui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        
        [ObservableProperty]
        private LoginDTO _loginDto;

        public LoginViewModel(AuthService authService)
        {
            LoginDto = new LoginDTO();
            _authService = authService;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (LoginDto.Email.IsNullOrEmpty() || LoginDto.Password.IsNullOrEmpty())
            {
                var toast = Toast.Make("Bitte fülle alle Felder aus", ToastDuration.Short);
                await toast.Show();

                return;
            }

            var result = await _authService.LoginAsync(LoginDto);

            if (!result.IsSuccess)
            {
                var toast = Toast.Make($"Login Fehlgeschlagen!", ToastDuration.Short);
                await toast.Show();
                return;
            }

            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }

        [RelayCommand]
        private async Task NavigateToRegister()
        {             
            await Shell.Current.GoToAsync($"//{nameof(Register)}");
        }
    }
}
