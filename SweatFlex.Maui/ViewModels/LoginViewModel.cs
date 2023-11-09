using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Services;
using SweatFlex.Maui.SQLLite;
using SweatFlex.Maui.Views;
using SweatFlexData.DTOs;

namespace SweatFlex.Maui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly CurrentWorkoutService _currentWorkoutService;
        
        [ObservableProperty]
        private LoginDTO _loginDto;

        [ObservableProperty]
        private bool _isBusy;

        private TodoItemDatabase _lokalDB;
        private IMapper _mapper;

        public LoginViewModel(AuthService authService, TodoItemDatabase lokalDB, CurrentWorkoutService currentWorkoutService, IMapper mapper)
        {
            _authService = authService;
            _lokalDB = lokalDB;
            _currentWorkoutService = currentWorkoutService;
            _mapper = mapper;
        }

        public async Task InitializeAsync()
        {
            var theme = Preferences.Get("Theme", "");

            if (!theme.IsNullOrEmpty())
            {
                if (theme == "Dark")
                {
                    Application.Current.UserAppTheme = AppTheme.Dark;
                }
                else if (theme == "Light")
                {
                    Application.Current.UserAppTheme = AppTheme.Light;
                }
                else
                {
                    Application.Current.UserAppTheme = AppTheme.Unspecified;
                }
            }

            LoginDto = new LoginDTO();
            if (await _authService.AutoLogin())
            {
                await Shell.Current.GoToAsync($"//{nameof(Home)}");
            }

            var lokalTEs = await _lokalDB.GetItemsNotDoneAsync();
            if(lokalTEs.Count > 0)
            {
                foreach(var item in lokalTEs)
                {
                    await _currentWorkoutService.UpdateTrainingExerciseAsync(item);
                }
            }
        }

        /// <summary>
        /// Gets the login data from the view and tries to login the user via the auth service
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Navigates to the register page
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task NavigateToRegister()
        {             
            await Shell.Current.GoToAsync($"//{nameof(Register)}");
        }
    }
}
