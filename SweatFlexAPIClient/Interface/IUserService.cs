using SweatFlexAPI.Models;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;

namespace SweatFlexAPIClient.Interface
{
    public interface IUserService
    {
        Task<ApiResponse> GetUsersAsync();
        Task<ApiResponse> GetUserAsync(string id);
        Task<ApiResponse> GetUserByCoachAsync(string coachId);
        Task<ApiResponse> CreateUserAsync(UserCreateDTO createDTO);
        Task<ApiResponse> UpdateUserAsync(string id, UserUpdateDTO updateDTO);
        Task<ApiResponse> DeleteUserAsync(string id);
        Task<ApiResponse> SetUserInactiveAsync(string id);
    }
}
