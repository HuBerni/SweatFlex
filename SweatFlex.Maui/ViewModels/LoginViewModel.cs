using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Views;
using SweatFlexData.DTOs;

namespace SweatFlex.Maui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private LoginDTO _loginProp;

        public LoginViewModel()
        {
                
        }

        [RelayCommand]
        private async Task Login()
        {
            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }

        [RelayCommand]
        private async Task NavigateToRegister()
        {             
            await Shell.Current.GoToAsync($"//{nameof(Register)}");
        }
    }
}
