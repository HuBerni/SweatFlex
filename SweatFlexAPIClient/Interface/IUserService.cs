using SweatFlexData.DTOs.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexAPIClient.Interface
{
    public interface IUserService
    {
        Task<T> Register<T>(UserCreateDTO createDTO);
    }
}
