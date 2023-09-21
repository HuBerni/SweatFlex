using NLog;
using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Interface;
using SweatFlexEF.DBClasses;
using SweatFlexEF.Models;

namespace SweatFlexEF
{
    public class DataHandler : IDataHandler
    {
        SweatFlexContext _context;
        ExerciseHandler _exerciseHandler;
        UserHandler _userHandler;
        public DataHandler(SweatFlexContext context)
        {
            _context = context;
            _exerciseHandler = new ExerciseHandler(_context);
            _userHandler = new UserHandler(_context);
        }

        public async Task<ExerciseDTO> CreateExerciseAsync(ExerciseCreateDTO createDTO)
        {
            return await _exerciseHandler.CreateExerciseAsync(createDTO);
        }

        public Task<TrainingExerciseDTO> CreateTrainingExerciseAsync(TrainingExerciseCreateDTO createDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> CreateUserAsync(UserCreateDTO createDTO)
        {
            return await _userHandler.CreateUserAsync(createDTO);
        }

        public Task<WorkoutExerciseDTO> CreateWorkoutExceriseAsync(WorkoutExerciseCreateDTO createDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteExerciseAsync(int id)
        {
            return _exerciseHandler.DeleteExerciseAsync(id);
        }

        public Task<bool> DeleteTrainingExerciseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            return await _userHandler.DeleteUserAsync(id);
        }

        public Task<bool> DeleteWorkoutAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteWorkoutExerciseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ExerciseDTO>> GetExercisesAsync(string? userId = null)
        {
            return await _exerciseHandler.GetExercisesAsync(userId);
        }

        public Task<TrainingExerciseDTO> GetTrainingExerciseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ExerciseDTO> GetExerciseByIdAsync(int id)
        {
            return await _exerciseHandler.GetExerciseByIdAsync(id);
        }

        public Task<IList<TrainingExerciseDTO>> GetTrainingExerciesAsync(string? userId, int? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            return await _userHandler.GetUserByIdAsync(id);
        }

        public async Task<IList<UserDTO>> GetUsersAsync()
        {
            return await _userHandler.GetUsersAsync();
        }

        public async Task<IList<UserDTO>> GetUsersByCoachIdAsync(string coachId)
        {
            return await _userHandler.GetUsersByCoachIdAsync(coachId);
        }

        public Task<WorkoutDTO> GetWorkoutByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<WorkoutExerciseDTO>> GetWorkoutExerciseAsync(int workoutId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutExerciseDTO> GetWorkoutExerciseByIdAsnyc(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<WorkoutDTO>> GetWorkoutsAsync(string? UserId = null)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutDTO> GetWorkoutsAsynct(int id, WorkoutUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutDTO> GetWorkoutsAsynct(WorkoutCreateDTO creatDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ExerciseDTO> UpdateExerciseAsync(int id, ExerciseUpdateDTO updateDTO)
        {
            return await _exerciseHandler.UpdateExerciseAsync(id, updateDTO);
        }

        public Task<ExerciseDTO> UpdateTrainingExerciseAsync(int id, TrainingExerciseUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> UpdateUserAsync(string id, UserUpdateDTO updateDTO)
        {
            return await _userHandler.UpdateUserAsync(id, updateDTO);
        }

        public Task<WorkoutExerciseDTO> UpdateWorkoutExerciseAsync(int id, WorkoutExerciseUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
