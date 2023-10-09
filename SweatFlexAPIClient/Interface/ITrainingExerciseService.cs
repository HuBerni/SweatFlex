using SweatFlexAPI.Models;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;

namespace SweatFlexAPIClient.Interface
{
    public interface ITrainingExerciseService
    {
        Task<ApiResponse<IList<TrainingExerciseDTO>>> GetTrainingExercisesAsync(string userId, int? workoutId = null);
        Task<ApiResponse<TrainingExerciseDTO>> GetTrainingExerciseByIdAsync(int id);
        Task<ApiResponse<TrainingExerciseDTO>> CreateTrainingExerciseAsync(TrainingExerciseCreateDTO createDTO);
        Task<ApiResponse<TrainingExerciseDTO>> UpdateTrainingExerciseAsync(int id, TrainingExerciseUpdateDTO updateDTO);
        Task<ApiResponse<bool>> DeleteTrainingExerciseAsync(int id);
    }
}
