using Microsoft.Extensions.Configuration;
using SweatFlexAPIClient.APIModels;
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

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a new created TrainingExercise
        /// </summary>
        /// <param name="createDTO">Model for creation</param>
        /// <returns></returns>
        public async Task<ApiResponse<TrainingExerciseDTO>> CreateTrainingExerciseAsync(TrainingExerciseCreateDTO createDTO)
        {
            return await SendAsync<TrainingExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = createDTO
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and deletes a TrainingExercise
        /// </summary>
        /// <param name="id">TrainingExercise Id for deletion</param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> DeleteTrainingExerciseAsync(int id)
        {
            return await SendAsync<bool>(new ApiRequest()
            {
                ApiType = Enum.ApiType.DELETE,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a TrainingExercise 
        /// </summary>
        /// <param name="id">TrainingExercise Id</param>
        /// <returns></returns>
        public async Task<ApiResponse<TrainingExerciseDTO>> GetTrainingExerciseByIdAsync(int id)
        {
            return await SendAsync<TrainingExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/GetTrainingExercise/{id}",
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and return a List of TrainingExercises
        /// </summary>
        /// <param name="userId">User Id for TrainingExercises</param>
        /// <param name="workoutId">Workout Id for TrainingExercises (nullable)</param>
        /// <returns></returns>
        public async Task<ApiResponse<IList<TrainingExerciseDTO>>> GetTrainingExercisesAsync(string userId, int? workoutId = null)
        {
            return await SendAsync<IList<TrainingExerciseDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/getExercises/{userId}/{workoutId}",
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and updates a TrainingExercise
        /// </summary>
        /// <param name="id">TrainingExercise Id fro Update</param>
        /// <param name="updateDTO">Training Exercise Model for Update</param>
        /// <returns></returns>
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
