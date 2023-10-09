using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexAPIClient.APIModels;
using SweatFlexData.DTOs;

namespace SweatFlexAPIClient.Interface
{
    public interface IWorkoutExerciseService
    {
        Task<ApiResponse<IList<WorkoutExerciseDTO>>> GetWorkoutExercisesAsync(int workoutId);
        Task<ApiResponse<WorkoutExerciseDTO>> GetWorkoutExerciseByIdAsync(int id);
        Task<ApiResponse<WorkoutExerciseDTO>> CreateWorkoutExerciseAsync(WorkoutExerciseCreateDTO workoutExerciseDto);
        Task<ApiResponse<WorkoutExerciseDTO>> UpdateWorkoutExerciseAsync(int id, WorkoutExerciseUpdateDTO updateDTO);
        Task<ApiResponse<bool>> DeleteWorkoutExerciseAsync(int id);
    }
}
