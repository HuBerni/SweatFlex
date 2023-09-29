using AutoMapper.Internal;
using SweatFlexAPI.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        ApiResponse responseModel { get; set; }
        Task<ApiResponse> SendAsync(ApiRequest apiRequest);
    }
}