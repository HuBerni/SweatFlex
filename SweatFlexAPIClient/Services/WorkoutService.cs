using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Interface.IDTOs;
using SweatFlexEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexAPIClient.Services
{
    public class WorkoutService : BaseService<IWorkoutDTO>, IWorkoutService
    {
        string _suffix;

        public WorkoutService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
            _suffix = "WorkoutAPI";
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a new created Workout
        /// </summary>
        /// <param name="createDTO">Workout Model for creation</param>
        /// <returns></returns>
        public async Task<ApiResponse<WorkoutDTO>> CreateWorkoutAsync(WorkoutCreateDTO createDTO)
        {
            return await SendAsync<WorkoutDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = createDTO
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and deletes a Workout
        /// </summary>
        /// <param name="id">Workout Id for deletion</param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> DeleteWorkoutAsync(int id)
        {
            return await SendAsync<bool>(new ApiRequest()
            {
                ApiType = Enum.ApiType.DELETE,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a Workout
        /// </summary>
        /// <param name="id">Workout Id</param>
        /// <returns></returns>
        public async Task<ApiResponse<WorkoutDTO>> GetWorkoutByIdAsync(int id)
        {
            return await SendAsync<WorkoutDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/{id}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a List of Workouts
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public async Task<ApiResponse<IList<WorkoutDTO>>> GetWorkoutsAsync(string? userId)
        {
            return await SendAsync<IList<WorkoutDTO>>(new ApiRequest()
            {
                ApiType = Enum.ApiType.GET,
                Url = $"{SweatFlexURL}{_suffix}/workouts/{userId}"
            });
        }

        /// <summary>
        /// calls the coresponding API HTTPAction and returns a updated Workout
        /// </summary>
        /// <param name="id">Workout Id for update</param>
        /// <param name="updateDTO">Workout Model for update</param>
        /// <returns></returns>
        public async Task<ApiResponse<WorkoutDTO>> UpdateWorkoutAsync(int id, WorkoutUpdateDTO updateDTO)
        {
            return await SendAsync<WorkoutDTO>(new ApiRequest()
            {
                ApiType = Enum.ApiType.PUT,
                Url = $"{SweatFlexURL}{_suffix}",
                Data = updateDTO
            });
        }
    }
}
