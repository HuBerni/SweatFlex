using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexAPIClient.Services
{
    public class ExerciseService : BaseService, IExerciseService
    {
        string _suffix;
        public ExerciseService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
            _suffix = "ExerciseAPI";
        }

        public async Task<ApiResponse> GetExercisesAsync()
        {
            return await SendAsync(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}"
            });
        }

        public async Task<ApiResponse> GetExercisesAsync(string id)
        {
            return await SendAsync(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/user/{id}"
            });
        }

        public async Task<ApiResponse> GetExerciseAsync(int id)
        {
            return await SendAsync(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        public async Task<ApiResponse> CreateExerciseAsync(ExerciseCreateDTO createDTO)
        {
            return await SendAsync(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = createDTO
            });
        }

        public async Task<ApiResponse> UpdateExerciseAsync(int id, ExerciseUpdateDTO updateDTO)
        {
            return await SendAsync(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = updateDTO
            });
        }

        public async Task<ApiResponse> DeleteExerciseAsync(int id)
        {
            return await SendAsync(new ApiRequest()
            {
                ApiType = Enum.ApiType.DELETE,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }
    }
}
