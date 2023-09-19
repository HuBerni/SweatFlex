using Microsoft.EntityFrameworkCore;
using NLog;
using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Interface;
using SweatFlexEF.DBClasses;
using SweatFlexEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexEF
{
    public class DataHandler : IDataHandler
    {
        Logger _logger;
        SweatFlexContext _context;
        DBExercise _dbExercise;
        public DataHandler()
        {
            _dbExercise = new DBExercise();
        }

        public ExerciseDTO CreateExercise(ExerciseCreateDTO createDTO)
        {
            throw new NotImplementedException();
        }

        public TrainingExerciseDTO CreateTrainingExercise(TrainingExerciseCreateDTO createDTO)
        {
            throw new NotImplementedException();
        }

        public UserDTO CreateUser(UserCreateDTO createDTO)
        {
            throw new NotImplementedException();
        }

        public WorkoutDTO CreateWorkout(WorkoutCreateDTO creatDTO)
        {
            throw new NotImplementedException();
        }

        public WorkoutExerciseDTO CreateWorkoutExcerise(WorkoutExerciseCreateDTO createDTO)
        {
            throw new NotImplementedException();
        }

        public bool DeleteExercise(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTrainingExercise(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWorkout(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWorkoutExercise(int id)
        {
            throw new NotImplementedException();
        }

        public IList<ExerciseDTO> GetExercise(int? UserId = null)
        {
            throw new NotImplementedException();
        }

        public ExerciseDTO GetExerciseById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<TrainingExerciseDTO> GetTrainingExercies(int? userId, int? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public TrainingExerciseDTO GetTrainingExercies(int id)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<UserDTO> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IList<UserDTO> GetUsersByCoach(int coachId)
        {
            throw new NotImplementedException();
        }

        public WorkoutDTO GetWorkoutById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<WorkoutExerciseDTO> GetWorkoutExercise(int workoutId)
        {
            throw new NotImplementedException();
        }

        public WorkoutExerciseDTO GetWorkoutExerciseById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<WorkoutDTO> GetWorkouts(int? UserId = null)
        {
            throw new NotImplementedException();
        }

        public ExerciseDTO UpdateExercise(int id, ExerciseUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }

        public ExerciseDTO UpdateTrainingExercise(int id, TrainingExerciseUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }

        public UserDTO UpdateUser(int id, UserUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }

        public WorkoutDTO UpdateWorkout(int id, WorkoutUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }

        public WorkoutExerciseDTO UpdateWorkoutExercise(int id, WorkoutExerciseUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
