using SweatFlexAPIClient.APIModels;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;

namespace SweatFlexAPIClient.Interface
{
    public interface IExerciseService
    {
        Task<ApiResponse<IList<ExerciseDTO>>> GetExercisesAsync();
        Task<ApiResponse<IList<ExerciseDTO>>> GetExercisesAsync(string id);
        Task<ApiResponse<ExerciseDTO>> GetExerciseAsync(int id);
        Task<ApiResponse<ExerciseDTO>> CreateExerciseAsync(ExerciseCreateDTO createDTO);
        Task<ApiResponse<ExerciseDTO>> UpdateExerciseAsync(int id, ExerciseUpdateDTO updateDTO);
        Task<ApiResponse<bool>> DeleteExerciseAsync(int id);
    }
}
