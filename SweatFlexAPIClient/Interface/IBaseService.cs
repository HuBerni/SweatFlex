using AutoMapper.Internal;
using SweatFlexAPI.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        ApiResponse responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}