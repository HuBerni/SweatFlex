using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Interface.IDTOs;

namespace SweatFlexAPIClient.Services
{
    public class UserService : BaseService<IUserDTO>, IUserService
    {
        string _suffix;
        public UserService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
            _suffix = "UserAPI";
        }

        public async Task<ApiResponse<IList<UserDTO>>> GetUsersAsync()
        {
            return await SendAsync<IList<UserDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}"
            });
        }

        public async Task<ApiResponse<UserDTO>> GetUserAsync(string id)
        {
            return await SendAsync<UserDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        public async Task<ApiResponse<IList<UserDTO>>> GetUserByCoachAsync(string coachId)
        {
            return await SendAsync<IList<UserDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/coach/{coachId}"
            });
        }

        public async Task<ApiResponse<UserDTO>> CreateUserAsync(UserCreateDTO createDTO)
        {
            return await SendAsync<UserDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = createDTO
            });
        }

        public async Task<ApiResponse<UserDTO>> UpdateUserAsync(string id, UserUpdateDTO updateDTO)
        {
            return await SendAsync<UserDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.PUT,
                Url = $"{SweatFlexURL}{_suffix}/{id}",
                Data = updateDTO
            });
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(string id)
        {
            return await SendAsync<bool>(new ApiRequest()
            {
                ApiType = Enum.ApiType.DELETE,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        public async Task<ApiResponse<bool>> SetUserInactiveAsync(string id)
        {
            return await SendAsync<bool>(new ApiRequest()
            {
                ApiType = Enum.ApiType.PUT,
                Url = $"{SweatFlexURL}{_suffix}/setInactive/{id}"
            });
        }

    }
}
