using SweatFlexAPIClient.APIModels;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;

namespace SweatFlexAPIClient.Interface
{
    public interface IAuthService
    {
        Task<ApiResponse<UserDTO>> LoginAsync(LoginDTO loginDto);
        Task<ApiResponse<UserDTO>> RegisterAsync(UserCreateDTO createDto);
    }
}
