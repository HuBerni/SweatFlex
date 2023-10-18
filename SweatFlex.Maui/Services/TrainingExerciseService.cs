using SweatFlex.Maui.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;

namespace SweatFlex.Maui.Services
{
    public class TrainingExerciseService
    {
        private readonly ITrainingExerciseService _trainingExerciseService;
        private readonly IWorkoutExerciseService _workoutExerciseService;

        public TrainingExerciseService(ITrainingExerciseService trainingExerciseService, IWorkoutExerciseService workoutExerciseService)
        {
            _trainingExerciseService = trainingExerciseService;
            _workoutExerciseService = workoutExerciseService;
        }

        public async Task<List<TrainingExercise>>? CreateTrainingExercises(int workoutId, string userId)
        {
            var workoutExercises = await _workoutExerciseService.GetWorkoutExercisesAsync(workoutId);

            if (!workoutExercises.IsSuccess)
            {
                return null;
            }

            var trainingExercises = await CreateTrainingExercises(workoutExercises.Result.ToList(), userId);

            return null;
        }

        private async Task<List<TrainingExerciseDTO>>? CreateTrainingExercises(List<WorkoutExerciseDTO> workoutExercises, string userId)
        {
            var trainingExercises = new List<TrainingExerciseDTO>();

            foreach (var workoutExercise in workoutExercises)
            {
                var trainingExerciseCreateDTO = new TrainingExerciseCreateDTO()
                {
                    ExerciseId = workoutExercise.Exercise.Id,
                    WorkoutExerciseId = workoutExercise.Id,
                    UserId = userId,
                    //TODO add SessionId
                };

                var result = await _trainingExerciseService.CreateTrainingExerciseAsync(trainingExerciseCreateDTO);
                
                if (result.IsSuccess)
                {
                    trainingExercises.Add(result.Result);
                }
            }


            return trainingExercises;
        }
    }
}
