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
        TrainingExerciseHandler _trainingExerciseHandler;
        WorkoutHandler _workoutHandler;
        public DataHandler(SweatFlexContext context)
        {
            _context = context;
            _exerciseHandler = new ExerciseHandler(_context);
            _userHandler = new UserHandler(_context);
            _trainingExerciseHandler = new TrainingExerciseHandler(_context);
            _workoutHandler = new WorkoutHandler(_context);
        }

        public async Task<ExerciseDTO> CreateExerciseAsync(ExerciseCreateDTO createDTO)
        {
            return await _exerciseHandler.CreateExerciseAsync(createDTO);
        }

        public async Task<TrainingExerciseDTO> CreateTrainingExerciseAsync(TrainingExerciseCreateDTO createDTO)
        {
            return await _trainingExerciseHandler.CreateTrainingExerciseAsync(createDTO);
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

        public async Task<bool> DeleteTrainingExerciseAsync(int id)
        {
            return await _trainingExerciseHandler.DeleteTrainingExerciseAsync(id);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            return await _userHandler.DeleteUserAsync(id);
        }

        public async Task<bool> DeleteWorkoutAsync(int id)
        {
            return await _workoutHandler.DeleteWorkoutAsync(id);
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

        public async Task<IList<TrainingExerciseDTO>> GetTrainingExerciesAsync(string? userId, int? workoutId = null)
        {
            return await _trainingExerciseHandler.GetTrainingExerciesAsync(userId, workoutId);
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

        public async Task<WorkoutDTO> GetWorkoutByIdAsync(int id)
        {
            return await _workoutHandler.GetWorkoutByIdAsync(id);
        }

        public Task<IList<WorkoutExerciseDTO>> GetWorkoutExerciseAsync(int workoutId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutExerciseDTO> GetWorkoutExerciseByIdAsnyc(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<WorkoutDTO>> GetWorkoutsAsync(string? UserId = null)
        {
            return await _workoutHandler.GetWorkoutsAsync(UserId);
        }

        public async Task<WorkoutDTO> UpdateWorkoutsAsynct(int id, WorkoutUpdateDTO updateDTO)
        {
            return await _workoutHandler.UpdateWorkoutsAsynct(id, updateDTO);
        }

        public async Task<WorkoutDTO> CreateWorkoutsAsynct(WorkoutCreateDTO createDTO)
        {
            return await _workoutHandler.CreateWorkoutsAsynct(createDTO);
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
