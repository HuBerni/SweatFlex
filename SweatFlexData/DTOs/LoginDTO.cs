using SweatFlexData.Interface.IDTOs;

namespace SweatFlexData.DTOs
{
    public class LoginDTO : IAuthDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
