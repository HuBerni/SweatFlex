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

        [ObservableProperty]
        private bool _isBusy;

        public LoginViewModel(AuthService authService)
        {
            LoginDto = new LoginDTO();
            _authService = authService;
        }

        public async Task InitializeAsync()
        {
            //remove when going live
            //await Shell.Current.GoToAsync($"//{nameof(Home)}");
            //return;

            //if (await _authService.AutoLogin())
            //{
            //    await Shell.Current.GoToAsync($"//{nameof(Home)}");
            //}
        }


        [RelayCommand]
        private async Task Login()
        {
            IsBusy = true;
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
                IsBusy = false;
                return;
            }

            IsBusy = false;

            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }

        [RelayCommand]
        private async Task NavigateToRegister()
        {             
            await Shell.Current.GoToAsync($"//{nameof(Register)}");
        }
    }
}
