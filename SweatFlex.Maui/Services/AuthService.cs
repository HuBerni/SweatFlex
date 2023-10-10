using Microsoft.IdentityModel.Tokens;
using SweatFlexAPIClient;
using SweatFlexAPIClient.APIModels;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using API = SweatFlexAPIClient.Services;

namespace SweatFlex.Maui.Services
{
    public class AuthService
    {
        private readonly API.AuthService _apiService;

        public AuthService(API.AuthService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponse<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var response = await _apiService.LoginAsync(loginDTO);

            if (!TokenStorage.Token.IsNullOrEmpty())
            {
                Preferences.Set(nameof(TokenStorage), TokenStorage.Token);
            }

            return response;
        }

        public void LogoutAsync()
        {
            Preferences.Set(nameof(TokenStorage), "");
            TokenStorage.Token = "";
            return;
        }

        public async Task<ApiResponse<UserDTO>> RegisterAsync
            (string id, string email, string password, string firstName, string lastName, int roleId)
        {
            var userCreateDto = new UserCreateDTO()
            {
                Id = id,
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Role = roleId
            };

            var result = await _apiService.RegisterAsync(userCreateDto);

            if (!TokenStorage.Token.IsNullOrEmpty())
            {
                Preferences.Set(nameof(TokenStorage), TokenStorage.Token);
            }

            return result;
        }
    }
}
