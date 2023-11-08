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
            _authService = authService;
        }

        public async Task InitializeAsync()
        {
            LoginDto = new LoginDTO();
            if (await _authService.AutoLogin())
            {
                await Shell.Current.GoToAsync($"//{nameof(Home)}");
            }
        }


        [RelayCommand]
        private async Task Login()
        {
            IsBusy = true;
            if (LoginDto.Email.IsNullOrEmpty() || LoginDto.Password.IsNullOrEmpty())
            {
                await ToastService.ShowToast("Bitte fülle alle Felder aus");
                IsBusy = false;
                return;
            }

            var result = await _authService.LoginAsync(LoginDto);

            if (!result.IsSuccess)
            {
                await ToastService.ShowToast("Login Fehlgeschlagen!");
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
