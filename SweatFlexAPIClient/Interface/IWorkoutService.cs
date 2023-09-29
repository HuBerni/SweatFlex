using SweatFlexAPI.Models;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexAPIClient.Interface
{
    public interface IWorkoutService
    {
        Task<ApiResponse> GetWorkoutsAsync(string? userId);
        Task<ApiResponse> GetWorkoutByIdAsync(int id);
        Task<ApiResponse> CreateWorkoutAsync(WorkoutCreateDTO createDTO);
        Task<ApiResponse> UpdateWorkoutAsync(int id, WorkoutUpdateDTO updateDTO);
        Task<ApiResponse> DeleteWorkoutAsync(int id);        
    }
}
