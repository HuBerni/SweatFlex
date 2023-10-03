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

        public async Task<UserDTO> RegisterAsync(UserCreateDTO createDTO)
        {
            //TODO: Create Stored Procedure for register in DB
            var result = await SendAsync<UserLoggedInDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Data = createDTO,
                Url = $"{SweatFlexURL}{_suffix}/register"
            });

            throw new NotImplementedException();
        }

        public async Task<UserDTO> LoginAsync(LoginDTO dto)
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

            return new UserDTO()
            {
                Coach = result.Result.Coach,
                Email = result.Result.Email,
                FirstName = result.Result.FirstName,
                LastName = result.Result.LastName,
                Id = result.Result.Id,
                Role = result.Result.Role
            };
        }
    }
}
