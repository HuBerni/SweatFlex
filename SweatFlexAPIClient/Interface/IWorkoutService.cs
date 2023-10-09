using SweatFlexAPI.Models;
using SweatFlexData.DTOs;
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
        Task<ApiResponse<IList<WorkoutDTO>>> GetWorkoutsAsync(string? userId);
        Task<ApiResponse<WorkoutDTO>> GetWorkoutByIdAsync(int id);
        Task<ApiResponse<WorkoutDTO>> CreateWorkoutAsync(WorkoutCreateDTO createDTO);
        Task<ApiResponse<WorkoutDTO>> UpdateWorkoutAsync(int id, WorkoutUpdateDTO updateDTO);
        Task<ApiResponse<bool>> DeleteWorkoutAsync(int id);
    }
}
