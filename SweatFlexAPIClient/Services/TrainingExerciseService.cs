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
    public class TrainingExerciseService : BaseService, ITrainingExerciseService
    {
        public TrainingExerciseService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
        }

        public Task<ApiResponse> CreateTrainingExerciseAsync(TrainingExerciseCreateDTO createDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> DeleteTrainingExerciseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetTrainingExerciseByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetTrainingExercisesAsync(string userId, int? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateTrainingExerciseAsync(int id, TrainingExerciseUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
