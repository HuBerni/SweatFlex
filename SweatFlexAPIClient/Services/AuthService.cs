using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;

namespace SweatFlexAPIClient.Services
{
    internal class AuthService : BaseService, IAuthService
    {
        string _suffix;
        public AuthService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
            _suffix = "AuthAPI";
        }

        public async Task<UserDTO> RegisterAsync(UserCreateDTO createDTO)
        {
            //TODO: This API Action should return a bool
            var registerSuccess = await SendAsync(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Data = createDTO,
                Url = $"{SweatFlexURL}{_suffix}"
            });
           
            if ((bool)registerSuccess.Result)
            {
                var userDto = SendAsync(new ApiRequest()
                {
                    ApiType = Enum.ApiType.GET,
                    Url = $"{SweatFlexURL}{_suffix}/mail/{createDTO.Email}"
                }).Result;

                if(userDto is UserDTO)
                {
                    return userDto as UserDTO;
                }

            }
        }

        public async Task<UserDTO> LoginAsync(LoginDTO dto)
        {
            var result = await SendAsync(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Data = dto,
                Url = $"{SweatFlexURL}{_suffix}"
            });

            if (result.StatusCode == System.Net.HttpStatusCode.OK && result.Result != null)
            {
                TokenStorage.Token = result.Result.ToString();
            }

            //getUserWithEmail \(*.*)/

            return result;
        }
    }
}
