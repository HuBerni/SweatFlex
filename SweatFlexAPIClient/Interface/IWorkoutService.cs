using SweatFlexAPIClient.APIModels;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;

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
