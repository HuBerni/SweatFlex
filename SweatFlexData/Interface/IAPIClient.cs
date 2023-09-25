using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;

namespace SweatFlexData.Interface
{
    public interface IAPIClient
    {
        // TODO ->> public UserDTO LogInAsync(UserDTO userDTO, Passwor);
        public UserDTO RegisterAsync(UserCreateDTO createDTO);
        public UserDTO UpdateUserAsync(UserUpdateDTO updateDTO);
    }
}
