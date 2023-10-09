using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Interface.IDTOs;

namespace SweatFlexAPIClient.Services
{
    public class TrainingExerciseService : BaseService<ITrainingExerciseDTO>, ITrainingExerciseService
    {
        string _suffix;
        public TrainingExerciseService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
            _suffix = "TrainingExerciseAPI";
        }

        public async Task<ApiResponse<TrainingExerciseDTO>> CreateTrainingExerciseAsync(TrainingExerciseCreateDTO createDTO)
        {
            return await SendAsync<TrainingExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = createDTO
            });
        }

        public async Task<ApiResponse<bool>> DeleteTrainingExerciseAsync(int id)
        {
            return await SendAsync<bool>(new ApiRequest()
            {
                ApiType = Enum.ApiType.DELETE,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        public async Task<ApiResponse<TrainingExerciseDTO>> GetTrainingExerciseByIdAsync(int id)
        {
            return await SendAsync<TrainingExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/GetTrainingExercise/{id}",
            });
        }

        public async Task<ApiResponse<IList<TrainingExerciseDTO>>> GetTrainingExercisesAsync(string userId, int? workoutId = null)
        {
            return await SendAsync<IList<TrainingExerciseDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/getExercises/{userId}/{workoutId}",
            });
        }

        public async Task<ApiResponse<TrainingExerciseDTO>> UpdateTrainingExerciseAsync(int id, TrainingExerciseUpdateDTO updateDTO)
        {
            return await SendAsync<TrainingExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.PUT,
                Url = $"{SweatFlexURL}{_suffix}/{id}",
                Data = updateDTO
            });
        }
    }
}
