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

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a new created WorkoutExercise
        /// </summary>
        /// <param name="workoutExerciseDto">WorkoutExercise Model for creation</param>
        /// <returns></returns>
        public async Task<ApiResponse<WorkoutExerciseDTO>> CreateWorkoutExerciseAsync(WorkoutExerciseCreateDTO workoutExerciseDto)
        {
            return await SendAsync<WorkoutExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = workoutExerciseDto
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and deletes a WorkoutExercise
        /// </summary>
        /// <param name="id">WorkoutExercise Id for deletion</param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> DeleteWorkoutExerciseAsync(int id)
        {
            return await SendAsync<bool>(new ApiRequest()
            {
                ApiType = Enum.ApiType.DELETE,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a WorkoutExercise
        /// </summary>
        /// <param name="id">WorkoutExercise Id</param>
        /// <returns></returns>
        public async Task<ApiResponse<WorkoutExerciseDTO>> GetWorkoutExerciseByIdAsync(int id)
        {
            return await SendAsync<WorkoutExerciseDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/GetWorkoutExerciseById/{id}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a List of WorkoutExercises
        /// </summary>
        /// <param name="workoutId">Workout Id</param>
        /// <returns></returns>
        public async Task<ApiResponse<IList<WorkoutExerciseDTO>>> GetWorkoutExercisesAsync(int workoutId)
        {
            return await SendAsync<IList<WorkoutExerciseDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/{workoutId}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a updated WorkoutExercise
        /// </summary>
        /// <param name="id">WorkoutExercise Id for update</param>
        /// <param name="updateDTO">WorkoutExercise Model for Update</param>
        /// <returns></returns>
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
