using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.Interface
{
    public interface IDataHandler
    {
        IList<UserDTO> GetUsers();
        IList<UserDTO> GetUsersByCoach(int coachId);
        UserDTO GetUserById(int id);
        UserDTO UpdateUser(int id, UserUpdateDTO updateDTO);
        bool DeleteUser(int id);
        UserDTO CreateUser(UserCreateDTO createDTO);
       
        IList<WorkoutDTO> GetWorkouts(int? UserId = null);
        WorkoutDTO GetWorkoutById(int id);
        WorkoutDTO UpdateWorkout(int id, WorkoutUpdateDTO updateDTO);
        bool DeleteWorkout(int id);
        WorkoutDTO CreateWorkout(WorkoutCreateDTO creatDTO);

        //TODO: implement Method for getting Exercise with lambda function parameter for bodypart, type ...

        IList<ExerciseDTO> GetExercise(int? UserId = null);        
        ExerciseDTO GetExerciseById(int id);
        ExerciseDTO UpdateExercise(int id, ExerciseUpdateDTO updateDTO);
        bool DeleteExercise(int id);
        ExerciseDTO CreateExercise(ExerciseCreateDTO createDTO);

        IList<TrainingExerciseDTO> GetTrainingExercies(int? userId, int? workoutId = null);
        TrainingExerciseDTO GetTrainingExercies(int id);
        ExerciseDTO UpdateTrainingExercise(int id, TrainingExerciseUpdateDTO updateDTO);
        bool DeleteTrainingExercise(int id);
        TrainingExerciseDTO CreateTrainingExercise(TrainingExerciseCreateDTO createDTO);

        IList<WorkoutExerciseDTO> GetWorkoutExercise(int workoutId);
        WorkoutExerciseDTO GetWorkoutExerciseById(int id);
        WorkoutExerciseDTO UpdateWorkoutExercise(int id, WorkoutExerciseUpdateDTO updateDTO);
        bool DeleteWorkoutExercise(int id);
        WorkoutExerciseDTO CreateWorkoutExcerise(WorkoutExerciseCreateDTO createDTO);

    }
}
