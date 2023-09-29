using SweatFlexAPI.Models;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;

namespace SweatFlexAPIClient.Interface
{
    public interface IAuthService
    {
        Task<UserDTO> LoginAsync(LoginDTO loginDto);
        Task<UserDTO> RegisterAsync(UserCreateDTO createDto);
    }
}
