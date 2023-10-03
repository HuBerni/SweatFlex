using AutoMapper.Internal;
using SweatFlexAPI.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService<T>
    {
        Task<ApiResponse<T>> SendAsync<T>(ApiRequest apiRequest);
    }
}