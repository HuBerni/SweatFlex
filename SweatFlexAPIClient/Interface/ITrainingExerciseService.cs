using SweatFlexAPIClient.APIModels;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;

namespace SweatFlexAPIClient.Interface
{
    public interface ITrainingExerciseService
    {
        Task<ApiResponse<IList<TrainingExerciseDTO>>> GetTrainingExercisesAsync(string userId, int? workoutId = null);
        Task<ApiResponse<TrainingExerciseDTO>> GetTrainingExerciseByIdAsync(int id);
        Task<ApiResponse<List<TrainingExerciseDTO>>> CreateTrainingExerciseAsync(List<TrainingExerciseCreateDTO> createDTO);
        Task<ApiResponse<TrainingExerciseDTO>> UpdateTrainingExerciseAsync(int id, TrainingExerciseUpdateDTO updateDTO);
        Task<ApiResponse<bool>> DeleteTrainingExerciseAsync(int id);
    }
}
