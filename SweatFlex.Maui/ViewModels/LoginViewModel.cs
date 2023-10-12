using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            var result = await _authService.LoginAsync(LoginDto);

            if (!result.IsSuccess)
            {
                //error handling
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
