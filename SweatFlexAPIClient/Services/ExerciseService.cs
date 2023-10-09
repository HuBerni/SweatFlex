using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Interface.IDTOs;

namespace SweatFlexAPIClient.Services
{
    public class ExerciseService : BaseService<IExerciseDTO>, IExerciseService
    {
        string _suffix;
        public ExerciseService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
            _suffix = "ExerciseAPI";
        }

        public async Task<ApiResponse<IList<ExerciseDTO>>> GetExercisesAsync()
        {
            return await SendAsync<IList<ExerciseDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}"
            });
        }

        public async Task<ApiResponse<IList<ExerciseDTO>>> GetExercisesAsync(string id)
        {
            return await SendAsync<IList<ExerciseDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/user/{id}"
            });
        }

        public async Task<ApiResponse<ExerciseDTO>> GetExerciseAsync(int id)
        {
            return await SendAsync<ExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });            
        }

        public async Task<ApiResponse<ExerciseDTO>> CreateExerciseAsync(ExerciseCreateDTO createDTO)
        {
            return await SendAsync<ExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = createDTO
            });
        }

        public async Task<ApiResponse<ExerciseDTO>> UpdateExerciseAsync(int id, ExerciseUpdateDTO updateDTO)
        {
            return await SendAsync<ExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.PUT,
                Url = $"{SweatFlexURL}{_suffix}/{id}",
                Data = updateDTO
            });
        }

        public async Task<ApiResponse<bool>> DeleteExerciseAsync(int id)
        {
            return await SendAsync<bool>(new ApiRequest()
            {
                ApiType = Enum.ApiType.DELETE,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }
    }
}
