using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Services;
using SweatFlex.Maui.Views;

namespace SweatFlex.Maui.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private string _id;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _passwordConfirmation;

        [ObservableProperty]
        private string _firstName;

        [ObservableProperty]
        private string _lastName;

        [ObservableProperty]
        private int _roleId;

        [ObservableProperty]
        private bool _isBusy;

        public RegisterViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task Register()
        {
            IsBusy = true;
            if (Password != PasswordConfirmation)
            {
                //TODO show error
                return;
            }

            var result = await _authService.RegisterAsync(Id, Email, Password, FirstName, LastName, 1);

            if (!result.IsSuccess)
            {
                //TODO show error
                return;
            }

            IsBusy = false;

            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }

        [RelayCommand]
        private async Task NavigateToLogin()
        {
            await Shell.Current.GoToAsync(nameof(Login));
        }
    }
}
