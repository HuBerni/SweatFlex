using SweatFlexAPI.Models;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;

namespace SweatFlexAPIClient.Interface
{
    public interface IUserService
    {
        Task<ApiResponse<IList<UserDTO>>> GetUsersAsync();
        Task<ApiResponse<UserDTO>> GetUserAsync(string id);
        Task<ApiResponse<IList<UserDTO>>> GetUserByCoachAsync(string coachId);
        Task<ApiResponse<UserDTO>> CreateUserAsync(UserCreateDTO createDTO);
        Task<ApiResponse<UserDTO>> UpdateUserAsync(string id, UserUpdateDTO updateDTO);
        Task<ApiResponse<bool>> DeleteUserAsync(string id);
        Task<ApiResponse<bool>> SetUserInactiveAsync(string id);
    }
}
