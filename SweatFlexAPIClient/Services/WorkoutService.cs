//using Microsoft.Extensions.Configuration;
//using SweatFlexAPI.Models;
//using SweatFlexAPIClient.Interface;
//using SweatFlexData.DTOs.Create;
//using SweatFlexData.DTOs.Update;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SweatFlexAPIClient.Services
//{
//    public class WorkoutService : BaseService, IWorkoutService
//    {
//        public WorkoutService(IHttpClientFactory httpClient) : base(httpClient)
//        {
//            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
//            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
//        }
//        public Task<ApiResponse> CreateWorkoutAsync(WorkoutCreateDTO createDTO)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApiResponse> DeleteWorkoutAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApiResponse> GetWorkoutByIdAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApiResponse> GetWorkoutsAsync(string? userId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApiResponse> UpdateWorkoutAsync(int id, WorkoutUpdateDTO updateDTO)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
