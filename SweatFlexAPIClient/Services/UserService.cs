using Microsoft.Extensions.Configuration;
using SweatFlexAPIClient.APIModels;
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
        public UserService() : base()
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config["URL:SweatFlexRestAPI"];
            _suffix = "UserAPI";
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns all Users
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<IList<UserDTO>>> GetUsersAsync()
        {
            return await SendAsync<IList<UserDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        public async Task<ApiResponse<UserDTO>> GetUserAsync(string id)
        {
            return await SendAsync<UserDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a List of User
        /// </summary>
        /// <param name="coachId">Coach Id for Users</param>
        /// <returns></returns>
        public async Task<ApiResponse<IList<UserDTO>>> GetUserByCoachAsync(string coachId)
        {
            return await SendAsync<IList<UserDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/coach/{coachId}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a new created User
        /// </summary>
        /// <param name="createDTO">User Model for creation</param>
        /// <returns></returns>
        public async Task<ApiResponse<UserDTO>> CreateUserAsync(UserCreateDTO createDTO)
        {
            return await SendAsync<UserDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = createDTO
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a updated User
        /// </summary>
        /// <param name="id">User iD for update</param>
        /// <param name="updateDTO">User Model for update</param>
        /// <returns></returns>
        public async Task<ApiResponse<UserDTO>> UpdateUserAsync(string id, UserUpdateDTO updateDTO)
        {
            return await SendAsync<UserDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.PUT,
                Url = $"{SweatFlexURL}{_suffix}/{id}",
                Data = updateDTO
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and deletes a User
        /// </summary>
        /// <param name="id">User Id for deletion</param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> DeleteUserAsync(string id)
        {
            return await SendAsync<bool>(new ApiRequest()
            {
                ApiType = Enum.ApiType.DELETE,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and sets a User inaktive
        /// </summary>
        /// <param name="id">User Id for setting inactive</param>
        /// <returns></returns>
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
