using Azure;
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

        /// <summary>
        /// Calls the API to login the user and stores the token in the preferences
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        public async Task<ApiResponse<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var response = await _apiService.LoginAsync(loginDTO);

            if (!TokenStorage.Token.IsNullOrEmpty())
            {
                Preferences.Set(nameof(TokenStorage), TokenStorage.Token);
                Preferences.Set("UserId", response.Result?.Id);
                Preferences.Set("RoleId", response.Result?.Role.ToString());
                Preferences.Set("CoachId", response.Result?.Coach?.Id ?? "");
            }

            return response;
        }

        /// <summary>
        /// Checks if the user has a token stored in the preferences and sets the token in the TokenStorage
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AutoLogin()
        {
            var token = Preferences.Get(nameof(TokenStorage), "");

            bool tokenExists = !token.IsNullOrEmpty();

            TokenStorage.Token = tokenExists ? Preferences.Get(nameof(TokenStorage), "") : "";

            if (tokenExists)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the token from the preferences and the TokenStorage
        /// </summary>
        public void Logout()
        {
            Preferences.Set(nameof(TokenStorage), "");
            Preferences.Set("UserId", "");
            Preferences.Set("RoleId", "");
            Preferences.Set("CoachId", "");
            TokenStorage.Token = "";
        }

        /// <summary>
        /// Creates a new user and stores the token in the preferences
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
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
                Role = roleId,
                CoachId = null
            };

            var response = await _apiService.RegisterAsync(userCreateDto);

            if (!TokenStorage.Token.IsNullOrEmpty())
            {
                Preferences.Set(nameof(TokenStorage), TokenStorage.Token);
                Preferences.Set("UserId", response.Result?.Id);
                Preferences.Set("RoleId", response.Result?.Role.ToString());
                Preferences.Set("CoachId", response.Result?.Coach?.Id ?? "");
            }

            return response;
        }
    }
}
