using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexAPIClient.Services
{
    public class WorkoutExerciseService : BaseService, IWorkoutExerciseService
    {
        public WorkoutExerciseService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
        }

        public Task<ApiResponse> CreateWorkoutExerciseAsync(WorkoutExerciseCreateDTO workoutExerciseDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> DeleteWorkoutExerciseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetWorkoutExerciseByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetWorkoutExercisesAsync(int workoutId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateWorkoutExerciseAsync(int id, WorkoutExerciseUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
