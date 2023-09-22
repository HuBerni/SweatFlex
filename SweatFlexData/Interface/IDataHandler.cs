﻿using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;

namespace SweatFlexData.Interface
{
    public interface IDataHandler
    {
        Task<IList<UserDTO>> GetUsersAsync();
        Task<IList<UserDTO>> GetUsersByCoachIdAsync(string coachId);
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<UserDTO> UpdateUserAsync(string id, UserUpdateDTO updateDTO);
        Task<bool> DeleteUserAsync(string id);
        Task<UserDTO> CreateUserAsync(UserCreateDTO createDTO);

        Task<IList<WorkoutDTO>> GetWorkoutsAsync(string? userId = null);
        Task<WorkoutDTO> GetWorkoutByIdAsync(int id);
        Task<WorkoutDTO> UpdateWorkoutAsync(int id, WorkoutUpdateDTO updateDTO);
        Task<bool> DeleteWorkoutAsync(int id);
        Task<WorkoutDTO> CreateWorkoutAsync(WorkoutCreateDTO creatDTO);

        //TODO: implement Method for getting Exercise with lambda function parameter for bodypart, type ...

        Task<IList<ExerciseDTO>> GetExercisesAsync(string? userId = null);
        Task<ExerciseDTO> GetExerciseByIdAsync(int id);
        Task<ExerciseDTO> UpdateExerciseAsync(int id, ExerciseUpdateDTO updateDTO);
        Task<bool> DeleteExerciseAsync(int id);
        Task<ExerciseDTO> CreateExerciseAsync(ExerciseCreateDTO createDTO);

        Task<IList<TrainingExerciseDTO>> GetTrainingExerciesAsync(string? userId, int? workoutId = null);
        Task<TrainingExerciseDTO> GetTrainingExerciseAsync(int id);
        Task<ExerciseDTO> UpdateTrainingExerciseAsync(int id, TrainingExerciseUpdateDTO updateDTO);
        Task<bool> DeleteTrainingExerciseAsync(int id);
        Task<TrainingExerciseDTO> CreateTrainingExerciseAsync(TrainingExerciseCreateDTO createDTO);

        Task<IList<WorkoutExerciseDTO>> GetWorkoutExerciseAsync(int workoutId);
        Task<WorkoutExerciseDTO> GetWorkoutExerciseByIdAsnyc(int id);
        Task<WorkoutExerciseDTO> UpdateWorkoutExerciseAsync(int id, WorkoutExerciseUpdateDTO updateDTO);
        Task<bool> DeleteWorkoutExerciseAsync(int id);
        Task<WorkoutExerciseDTO> CreateWorkoutExceriseAsync(WorkoutExerciseCreateDTO createDTO);

    }
}
