using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.Interface.IDTOs;

namespace SweatFlexAPIClient.Services
{
    public class AuthService : BaseService<IAuthDTO>, IAuthService
    {
        string _suffix;
        public AuthService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
            _suffix = "AuthAPI";
        }

        public async Task<ApiResponse<UserDTO>> RegisterAsync(UserCreateDTO createDTO)
        {
            //TODO: Create Stored Procedure for register in DB
            var result = await SendAsync<UserLoggedInDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Data = createDTO,
                Url = $"{SweatFlexURL}{_suffix}/register"
            });

            if (result.StatusCode == System.Net.HttpStatusCode.OK && result.Result != null)
            {
                TokenStorage.Token = result.Result.Token;
            }

            return MapReturn(result);
        }

        public async Task<ApiResponse<UserDTO>> LoginAsync(LoginDTO dto)
        {
            var result = await SendAsync<UserLoggedInDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Data = dto,
                Url = $"{SweatFlexURL}{_suffix}/login"
            });

            if (result.StatusCode == System.Net.HttpStatusCode.OK && result.Result != null)
            {
                TokenStorage.Token = result.Result.Token;
            }

            return MapReturn(result);
        }

        private ApiResponse<UserDTO> MapReturn(ApiResponse<UserLoggedInDTO> apiResponse)
        {
            return new ApiResponse<UserDTO>()
            {
                ErrorMessages = apiResponse.ErrorMessages,
                IsSuccess = apiResponse.IsSuccess,
                Result = new UserDTO()
                {
                    Coach = apiResponse.Result.Coach,
                    Email = apiResponse.Result.Email,
                    FirstName = apiResponse.Result.FirstName,
                    LastName = apiResponse.Result.LastName,
                    Id = apiResponse.Result.Id,
                    Role = apiResponse.Result.Role
                },
                StatusCode = apiResponse.StatusCode
            };
        }
    }
}
