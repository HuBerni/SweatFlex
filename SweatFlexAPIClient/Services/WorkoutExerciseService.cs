using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Enum;
using SweatFlexAPIClient.Interface;
using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexAPIClient.Services
{
    public class WorkoutExerciseService : BaseService<IWorkoutExerciseDTO>, IWorkoutExerciseService
    {
        string _suffix;
        public WorkoutExerciseService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
            _suffix = "WorkoutExerciseAPI";
        }

        public async Task<ApiResponse<WorkoutExerciseDTO>> CreateWorkoutExerciseAsync(WorkoutExerciseCreateDTO workoutExerciseDto)
        {
            return await SendAsync<WorkoutExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = workoutExerciseDto
            });
        }

        public async Task<ApiResponse<bool>> DeleteWorkoutExerciseAsync(int id)
        {
            return await SendAsync<bool>(new ApiRequest()
            {
                ApiType = Enum.ApiType.DELETE,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        public async Task<ApiResponse<WorkoutExerciseDTO>> GetWorkoutExerciseByIdAsync(int id)
        {
            return await SendAsync<WorkoutExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/GetWorkoutExerciseById/{id}"
            });
        }

        public async Task<ApiResponse<IList<WorkoutExerciseDTO>>> GetWorkoutExercisesAsync(int workoutId)
        {
            return await SendAsync<IList<WorkoutExerciseDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/{workoutId}"
            });
        }

        public async Task<ApiResponse<WorkoutExerciseDTO>> UpdateWorkoutExerciseAsync(int id, WorkoutExerciseUpdateDTO updateDTO)
        {
            return await SendAsync<WorkoutExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.PUT,
                Url = $"{SweatFlexURL}{_suffix}/{id}",
                Data = updateDTO                
            });
        }
    }
}
